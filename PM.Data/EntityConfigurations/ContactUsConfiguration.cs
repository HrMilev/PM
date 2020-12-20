using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PM.Data.Entities;
using PM.Data.EntityConfigurations.BaseConfigurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM.Data.EntityConfigurations
{
    public class ContactUsConfiguration : IdBaseEntityConfiguration<UserQuestion, int>
    {
        public override void Configure(EntityTypeBuilder<UserQuestion> builder)
        {
            base.Configure(builder);
            builder.HasOne(x => x.UserCreator)
                .WithMany(x => x.UserQuestions)
                .HasForeignKey(x => x.UserCreatorId);
            builder.HasOne(x => x.UserResponder)
                .WithMany(x => x.ContactUsResponses)
                .HasForeignKey(x => x.UserResponderId);
            builder.Property(x => x.UserCreatorId).IsRequired();
            builder.Property(x => x.Subject).IsRequired().HasMaxLength(500);
            builder.Property(x => x.CreatorMessage).IsRequired().HasMaxLength(5000);
            builder.Property(x => x.CreateDate).IsRequired();
        }
    }
}
