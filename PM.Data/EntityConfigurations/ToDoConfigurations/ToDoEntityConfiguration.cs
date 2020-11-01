using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PM.Data.Entities.ToDos;
using PM.Data.EntityConfigurations.BaseConfigurations;

namespace PM.Data.EntityConfigurations.ToDoConfigurations
{
    public class ToDoEntityConfiguration : IdBaseEntityConfiguration<ToDo>
    {
        public override void Configure(EntityTypeBuilder<ToDo> builder)
        {
            base.Configure(builder);
            builder
                .HasOne(x => x.User)
                .WithMany(x => x.ToDos)
                .HasForeignKey(x => x.UserId);
            builder.Property(x => x.Description).IsRequired();
            builder.Property(x => x.UserId).IsRequired();
        }
    }
}
