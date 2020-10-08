using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using PM.Localizations;

namespace PM.WebAPI.Models
{
    public class MultilanguageIdentityErrorDescriber : IdentityErrorDescriber
    {
        private readonly IStringLocalizer<Localization> _localizer;

        public MultilanguageIdentityErrorDescriber(IStringLocalizer<Localization> localizer)
        {
            _localizer = localizer;
        }

        public override IdentityError DuplicateEmail(string email)
        {
            return new IdentityError()
            {
                Code = nameof(DuplicateEmail),
                Description = string.Format(_localizer["Email '{0}' is already taken"], email)
            };
        }

        public override IdentityError LoginAlreadyAssociated()
        {
            return new IdentityError
            {
                Code = nameof(LoginAlreadyAssociated),
                Description = _localizer["A user with this login already exists"]
            };
        }

        public override IdentityError DuplicateUserName(string userName)
        {
            return new IdentityError
            {
                Code = nameof(DuplicateUserName),
                Description = string.Format(_localizer["User name '{0}' is already taken"], userName)
            };
        }
    }
}
