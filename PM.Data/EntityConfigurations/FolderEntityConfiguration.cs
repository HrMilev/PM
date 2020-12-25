using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PM.Data.Entities;
using PM.Data.EntityConfigurations.BaseConfigurations;

namespace PM.Data.EntityConfigurations
{
    public class FolderEntityConfiguration : IdBaseEntityConfiguration<Folder, int>
    {
        public override void Configure(EntityTypeBuilder<Folder> builder)
        {
            base.Configure(builder);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
            builder.HasOne(x => x.ParentFolder)
                .WithMany(x => x.ChildFolders)
                .HasForeignKey(x => x.ParentFolderId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.Creator)
                .WithMany(x => x.Folders)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
