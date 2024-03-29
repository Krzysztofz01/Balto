﻿using Balto.Application.Settings;
using Balto.Domain.Core.Extensions;
using Balto.Domain.Identities;
using Balto.Infrastructure.Core.Abstraction;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace Balto.Application.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthenticationDataAccessService _authenticationDataAccessService;
        private readonly IAuthenticationWrapperService _authenticationWrapperService;
        private readonly IScopeWrapperService _scopeWrapperService;
        private readonly JsonWebTokenSettings _jwtSettings;
        private readonly AuthorizationSettings _authorizationSettings;

        public AuthenticationService(
            IUnitOfWork unitOfWork,
            IAuthenticationDataAccessService authenticationDataAccessService,
            IAuthenticationWrapperService authenticationWrapperService,
            IScopeWrapperService scopeWrapperService,
            IOptions<JsonWebTokenSettings> jsonWebTokenSettings,
            IOptions<AuthorizationSettings> authorizationSettings)
        {
            _unitOfWork = unitOfWork ??
                throw new ArgumentNullException(nameof(unitOfWork));

            _authenticationDataAccessService = authenticationDataAccessService ??
                throw new ArgumentNullException(nameof(authenticationDataAccessService));

            _authenticationWrapperService = authenticationWrapperService ??
                throw new ArgumentNullException(nameof(authenticationWrapperService));

            _scopeWrapperService = scopeWrapperService ??
                throw new ArgumentNullException(nameof(scopeWrapperService));

            _jwtSettings = jsonWebTokenSettings.Value ??
                throw new ArgumentNullException(nameof(jsonWebTokenSettings));

            _authorizationSettings = authorizationSettings.Value ??
                throw new ArgumentNullException(nameof(authorizationSettings));
        }

        public async Task<Responses.V1.Login> Login(Requests.V1.Login request)
        {
            var identity = await _authenticationDataAccessService.GetUserByEmail(request.Email);

            if (!_authenticationWrapperService.VerifyPasswordHashes(request.Password, identity.PasswordHash))
                throw new InvalidOperationException("Invalid authentication credentials.");

            string bearerToken = _authenticationWrapperService.GenerateJsonWebToken(identity);

            string refreshToken = _authenticationWrapperService.GenerateRefreshToken();

            string refreshTokenHash = _authenticationWrapperService.HashString(refreshToken);

            identity.Apply(new Events.V1.IdentityAuthenticated
            {
                Id = identity.Id,
                TokenExpirationDays = _jwtSettings.RefreshTokenExpirationDays,
                IpAddress = _scopeWrapperService.GetIpAddress(),
                TokenHash = refreshTokenHash
            });

            await _unitOfWork.Commit();

            return new Responses.V1.Login
            {
                JsonWebToken = bearerToken,
                RefreshToken = refreshToken
            };
        }

        public async Task Logout(Requests.V1.Logout request)
        {
            var identity = await _unitOfWork.IdentityRepository.Get(_scopeWrapperService.GetUserId());

            string refreshTokenHash = _authenticationWrapperService.HashString(request.RefreshToken);

            identity.Apply(new Events.V1.IdentityTokenRevoked
            {
                Id = identity.Id,
                IpAddress = _scopeWrapperService.GetIpAddress(),
                TokenHash = refreshTokenHash
            });

            await _unitOfWork.Commit();
        }

        public async Task PasswordReset(Requests.V1.PasswordReset request)
        {
            var identity = await _unitOfWork.IdentityRepository.Get(_scopeWrapperService.GetUserId());

            if (!_authenticationWrapperService.ValidatePasswords(request.Password, request.PasswordRepeat))
                throw new InvalidOperationException("Invalid authentication credentials");

            var passwordHash = _authenticationWrapperService.HashPassword(request.Password);

            identity.Apply(new Events.V1.IdentityPasswordChanged
            {
                Id = identity.Id,
                PasswordHash = passwordHash
            });

            await _unitOfWork.Commit();
        }

        public async Task<Responses.V1.Refresh> Refresh(Requests.V1.Refresh request)
        {
            if (request.RefreshToken.IsEmpty())
                throw new InvalidOperationException("Invalid authentication credentials.");

            var refreshTokenHash = _authenticationWrapperService.HashString(request.RefreshToken);

            var identity = await _authenticationDataAccessService.GetUserByRefreshToken(refreshTokenHash);

            var bearerToken = _authenticationWrapperService.GenerateJsonWebToken(identity);

            var replacementToken = _authenticationWrapperService.GenerateRefreshToken();

            var replacementTokenHash = _authenticationWrapperService.HashString(replacementToken);

            identity.Apply(new Events.V1.IdentityTokenRefreshed
            {
                Id = identity.Id,
                IpAddress = _scopeWrapperService.GetIpAddress(),
                TokenExpirationDays = _jwtSettings.RefreshTokenExpirationDays,
                TokenHash = refreshTokenHash,
                ReplacementTokenHash = replacementTokenHash
            });

            await _unitOfWork.Commit();

            return new Responses.V1.Refresh
            {
                JsonWebToken = bearerToken,
                RefreshToken = replacementToken
            };
        }

        public async Task Register(Requests.V1.Register request)
        {
            if (await _authenticationDataAccessService.UserWithEmailExsits(request.Email))
                throw new InvalidOperationException("Invalid authentication credentials");

            if (!_authenticationWrapperService.ValidatePasswords(request.Password, request.PasswordRepeat))
                throw new InvalidOperationException("Invalid authentication credentials");

            string passwordHash = _authenticationWrapperService.HashPassword(request.Password);

            var identity = Identity.Factory.Create(new Events.V1.IdentityCreated
            {
                Email = request.Email,
                IpAddress = _scopeWrapperService.GetIpAddress(),
                Name = request.Name,
                PasswordHash = passwordHash
            });

            if (_authorizationSettings.PromoteFirstAccount && !await _authenticationDataAccessService.AnyUsersExists())
            {
                identity.Apply(new Events.V1.IdentityActivationChanged
                {
                    Id = identity.Id,
                    Activated = true
                });

                identity.Apply(new Events.V1.IdentityRoleChanged
                {
                    Id = identity.Id,
                    Role = UserRole.Admin
                });
            }

            await _unitOfWork.IdentityRepository.Add(identity);

            await _unitOfWork.Commit();
        }
    }
}
