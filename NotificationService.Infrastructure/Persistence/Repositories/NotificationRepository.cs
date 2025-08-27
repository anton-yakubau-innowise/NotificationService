using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using NotificationService.Domain.Entities;
using NotificationService.Domain.Repositories;

namespace NotificationService.Infrastructure.Persistence.Repositories
{
    public class NotificationRepository(NotificationDbContext dbContext) : INotificationRepository
    {
        public async Task<Notification?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await GetBaseQuery()
                                   .FirstOrDefaultAsync(v => v.Id == id, cancellationToken);
        }

        public async Task<Notification?> GetByIdAsNoTrackingAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await GetBaseQuery()
                                   .AsNoTracking()
                                   .FirstOrDefaultAsync(v => v.Id == id, cancellationToken);
        }

        public async Task<IEnumerable<Notification>> ListAllAsync(CancellationToken cancellationToken = default)
        {
            return await GetBaseQuery()
                                   .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Notification>> ListAllAsNoTrackingAsync(CancellationToken cancellationToken = default)
        {
            return await GetBaseQuery()
                                   .AsNoTracking()
                                   .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Notification>> ListAsync(Expression<Func<Notification, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await GetBaseQuery()
                                   .Where(predicate)
                                   .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Notification>> ListAsNoTrackingAsync(Expression<Func<Notification, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await GetBaseQuery()
                                   .AsNoTracking()
                                   .Where(predicate)
                                   .ToListAsync(cancellationToken);
        }

        public async Task AddAsync(Notification notification, CancellationToken cancellationToken = default)
        {
            await dbContext.Notifications.AddAsync(notification, cancellationToken);
        }

        public void Delete(Notification notification)
        {
            dbContext.Notifications.Remove(notification);
        }

        private IQueryable<Notification> GetBaseQuery()
        {
            return dbContext.Notifications;
        }
    }
}
