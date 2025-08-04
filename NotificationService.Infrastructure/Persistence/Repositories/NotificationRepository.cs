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
            return await dbContext.Notifications
                                   .FirstOrDefaultAsync(v => v.Id == id, cancellationToken);
        }

        public async Task<Notification?> GetByIdAsNoTrackingAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await dbContext.Notifications
                                   .AsNoTracking()
                                   .FirstOrDefaultAsync(v => v.Id == id, cancellationToken);
        }

        public async Task<IEnumerable<Notification>> ListAllAsync(CancellationToken cancellationToken = default)
        {
            return await dbContext.Notifications
                                   .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Notification>> ListAllAsNoTrackingAsync(CancellationToken cancellationToken = default)
        {
            return await dbContext.Notifications
                                   .AsNoTracking()
                                   .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Notification>> ListAsync(Expression<Func<Notification, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await dbContext.Notifications
                                   .Where(predicate)
                                   .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Notification>> ListAsNoTrackingAsync(Expression<Func<Notification, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await dbContext.Notifications
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
    }
}
