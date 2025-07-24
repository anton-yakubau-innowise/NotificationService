using NotificationService.Application.Interfaces;
using NotificationService.Domain.Repositories;
using NotificationService.Infrastructure.Persistence.Repositories;

namespace NotificationService.Infrastructure.Persistence
{
public class UnitOfWork(NotificationDbContext dbContext) : IUnitOfWork
{
    public INotificationRepository Notifications { get; } = new NotificationRepository(dbContext);

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return dbContext.SaveChangesAsync(cancellationToken);
        }

    public void Dispose()
    {
        dbContext.Dispose();
        GC.SuppressFinalize(this);
    }
}
}