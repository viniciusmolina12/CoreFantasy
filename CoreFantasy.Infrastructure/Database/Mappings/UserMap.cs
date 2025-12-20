using CoreFantasy.Domain.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreFantasy.Infrastructure.Database.Mappings
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                   .HasConversion(
                       id => id.Value,
                       value => UserId.Create(value)
                   );

            builder.OwnsOne(x => x.Name, n =>
            {
                n.Property(p => p.Value)
                 .HasColumnName("name")
                 .HasMaxLength(150)
                 .IsRequired();
            });

            builder.OwnsOne(x => x.Email, e =>
            {
                e.Property(p => p.Value)
                 .HasColumnName("email")
                 .HasMaxLength(150)
                 .IsRequired();
            });

            builder.OwnsOne(x => x.Phone, p =>
            {
                p.Property(p => p.Value)
                 .HasColumnName("phone")
                 .HasMaxLength(20);
            });

            builder.OwnsOne(x => x.IdentityId, i =>
            {
                i.Property(p => p.Value)
                 .HasColumnName("identity_provider_id")
                 .IsRequired();
            });
        }
    }
}
