using Balto.Application.Email;
using Balto.Domain.Aggregates.Project;
using Balto.Infrastructure.SqlServer.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Balto.Application.Aggregates.Project
{
    public class ProjectBackgroundProcessing : IProjectBackgroundProcessing
    {
        private readonly IEmailService _emailService;
        private readonly BaltoDbContext _dbContext;

        public ProjectBackgroundProcessing(
            IEmailService emailService,
            BaltoDbContext dbContext)
        {
            _emailService = emailService ??
                throw new ArgumentNullException(nameof(emailService));

            _dbContext = dbContext ??
                throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task SendEmailNotificationsDayBefore()
        {
            var cards = await _dbContext.Projects
                .Include(e => e.Tables)
                .ThenInclude(e => e.Cards)
                .SelectMany(e => e.Tables)
                .SelectMany(e => e.Cards)
                .Where(e => e.Deadline.Notify && !e.Finished.Finished && e.Deadline.Date != null)
                .Where(e => (e.Deadline.Date.Value - DateTime.Now.Date).TotalDays == 1)
                .AsNoTracking()
                .ToListAsync();

            var users = await _dbContext.Users
                .Where(e => e.DeletedAt != null)
                .AsNoTracking()
                .ToDictionaryAsync(k => k.Id.Value);

            foreach(var card in cards)
            {
                var addresses = new List<string>();

                if (users.ContainsKey(card.CreatorId.Value)) addresses.Add(users[card.CreatorId.Value].Email.Value);
                
                if (card.Deadline.UserId.HasValue)
                {
                    if (users.ContainsKey(card.Deadline.UserId.Value)) addresses.Add(users[card.Deadline.UserId.Value].Email.Value);
                }

                await _emailService.SendEmail(addresses.ToArray(), $"Balto deadline notification - { card.Title.Value }", $"Tommorow is the deadline of: { card.Title.Value } ({card.Id.Value})");
            }
        }

        public async Task SendEmailNotificationsThreeDaysBefore()
        {
            var cards = await _dbContext.Projects
                .Include(e => e.Tables)
                .ThenInclude(e => e.Cards)
                .SelectMany(e => e.Tables)
                .SelectMany(e => e.Cards)
                .Where(e => e.Deadline.Notify && !e.Finished.Finished && e.Deadline.Date != null)
                .Where(e => (e.Deadline.Date.Value - DateTime.Now.Date).TotalDays == 3)
                .AsNoTracking()
                .ToListAsync();

            var users = await _dbContext.Users
                .Where(e => e.DeletedAt != null)
                .AsNoTracking()
                .ToDictionaryAsync(k => k.Id.Value);

            foreach (var card in cards)
            {
                var addresses = new List<string>();

                if (users.ContainsKey(card.CreatorId.Value)) addresses.Add(users[card.CreatorId.Value].Email.Value);

                if (card.Deadline.UserId.HasValue)
                {
                    if (users.ContainsKey(card.Deadline.UserId.Value)) addresses.Add(users[card.Deadline.UserId.Value].Email.Value);
                }

                await _emailService.SendEmail(addresses.ToArray(), $"Balto deadline notification - { card.Title.Value }", $"In three days is the deadline of: { card.Title.Value } ({card.Id.Value})");
            }
        }
    }
}
