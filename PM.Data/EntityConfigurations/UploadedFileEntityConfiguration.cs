using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PM.Data.Entities;
using PM.Data.EntityConfigurations.BaseConfigurations;
using System;

namespace PM.Data.EntityConfigurations
{
    public class UploadedFileEntityConfiguration : IdBaseEntityConfiguration<UploadedFile, Guid>
    {
        public override void Configure(EntityTypeBuilder<UploadedFile> builder)
        {
            base.Configure(builder);
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Extension).IsRequired();
            builder.HasOne(x => x.Folder)
                .WithMany(x => x.Files)
                .HasForeignKey(x => x.FolderId).OnDelete(DeleteBehavior.SetNull);
        }
    }
}
