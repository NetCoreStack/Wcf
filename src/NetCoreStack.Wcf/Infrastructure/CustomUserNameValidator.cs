using System;
using System.IdentityModel.Selectors;

namespace NetCoreStack.Wcf
{
    public class CustomUserNameValidator : UserNamePasswordValidator
    {
        public CustomUserNameValidator()
        {
        }

        public override void Validate(string userName, string password)
        {
            if (null == userName || null == password)
            {
                throw new ArgumentNullException();
            }

            int detsisNo = 0;
            if (!int.TryParse(userName, out detsisNo))
            {
                throw new ArgumentOutOfRangeException(nameof(userName));
            }

            // throw new System.IdentityModel.Tokens.SecurityTokenException("Unknown message credentials");
            return;
        }
    }
}