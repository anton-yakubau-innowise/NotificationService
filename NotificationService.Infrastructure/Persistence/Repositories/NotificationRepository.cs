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

        public async Task<IEnumerable<Notification>> ListAllAsync(CancellationToken cancellationToken = default)
        {
            return await dbContext.Notifications
                                   .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Notification>> ListAsync(Expression<Func<Notification, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await dbContext.Notifications
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