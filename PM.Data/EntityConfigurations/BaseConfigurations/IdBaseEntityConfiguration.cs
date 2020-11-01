using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PM.Data.Entities.Bases;

namespace PM.Data.EntityConfigurations.BaseConfigurations
{
    public abstract class IdBaseEntityConfiguration<T> : IEntityTypeConfiguration<T> where T : IdBase
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
