using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PM.Data.Entities;
using PM.Data.EntityConfigurations.BaseConfigurations;

namespace PM.Data.EntityConfigurations
{
    public class UserQuestionEntityConfiguration : IdBaseEntityConfiguration<UserQuestion, int>
    {
        public override void Configure(EntityTypeBuilder<UserQuestion> builder)
        {
            base.Configure(builder);
            builder.HasOne(x => x.UserCreator)
                .WithMany(x => x.UserQuestions)
                .HasForeignKey(x => x.UserCreatorId).IsRequired();
            builder.HasOne(x => x.UserResponder)
                .WithMany(x => x.UserQuestionResponses)
                .HasForeignKey(x => x.UserResponderId);
            builder.Property(x => x.Subject).IsRequired().HasMaxLength(500);
            builder.Property(x => x.CreatorMessage).IsRequired().HasMaxLength(5000);
            builder.Property(x => x.CreateDate).IsRequired();
        }
    }
}
