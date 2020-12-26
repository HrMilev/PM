using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PM.Data.Entities;

namespace PM.Data.EntityConfigurations
{
    public class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public virtual void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasOne(x => x.RootFolder)
                .WithOne()
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
