using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NotificationService.Domain.Entities;

namespace NotificationService.Infrastructure.Persistence.Configuration
{
    public class NotificationEntityTypeConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.HasKey(n => n.Id);

            builder.Property(n => n.Recipient)
                .IsRequired()
                .HasMaxLength(256);

            builder.Property(n => n.Subject)
                .HasMaxLength(512);

            builder.Property(n => n.Message)
                .IsRequired()
                .HasMaxLength(5000);

            builder.Property(n => n.Type)
                .IsRequired()
                .HasConversion<string>();

            builder.Property(n => n.Status)
                .IsRequired()
                .HasConversion<string>();

            builder.Property(n => n.CreatedAt)
                .IsRequired();

            builder.Property(n => n.FailureReason)
                .HasMaxLength(1000);
        }
    }
}