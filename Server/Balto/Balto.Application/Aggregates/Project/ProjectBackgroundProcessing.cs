using Balto.Domain.Aggregates.Project;
using Balto.Domain.Common;
using Balto.Infrastructure.SqlServer.Context;
using System;
using System.Threading.Tasks;

namespace Balto.Application.Aggregates.Project
{
    public class ProjectBackgroundProcessing : IProjectBackgroundProcessing
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly BaltoDbContext _dbContext;

        public ProjectBackgroundProcessing(
            IUnitOfWork unitOfWork,
            BaltoDbContext dbContext)
        {
            _unitOfWork = unitOfWork ??
                throw new ArgumentNullException(nameof(unitOfWork));

            _dbContext = dbContext ??
                throw new ArgumentNullException(nameof(dbContext));
        }

        public Task SendEmailNotifications()
        {
            throw new NotImplementedException();
        }
    }
}
