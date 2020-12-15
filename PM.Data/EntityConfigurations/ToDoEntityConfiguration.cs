using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PM.Data.Entities;
using PM.Data.EntityConfigurations.BaseConfigurations;
using System;

namespace PM.Data.EntityConfigurations
{
    public class ToDoEntityConfiguration : IdBaseEntityConfiguration<ToDo, Guid>
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
