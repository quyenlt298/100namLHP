using System.Threading.Tasks;
using TVA.MODEL.Account;
using TVA.SERVICES.External.Facebook;
using TVA.SERVICES.External.Google;
using TVA.SERVICES.External.Type;

namespace TVA.SERVICES.External.Base
{
    public class SocialNetworkProvider : ISocialNetworkProvider
    {
        public async Task<User> GetUserSocialNetworkByTokenAsync(SocialNetworkEnumType socialNetworkEnumType, string token)
        {
            switch (socialNetworkEnumType)
            {
                case SocialNetworkEnumType.Facebook:
                    return await new FacebookProvider().GetUserSocialNetworkByTokenAsync(token);
                case SocialNetworkEnumType.Google:
                    return await new GoogleProvider().GetUserSocialNetworkByTokenAsync(token);
            }

            return null;
        }
    }
}
