using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PM.Data.Entities.Bases;

namespace PM.Data.EntityConfigurations.BaseConfigurations
{
    public abstract class IdBaseEntityConfiguration<T, Tid> : IEntityTypeConfiguration<T> where T : IdBase<Tid>
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
