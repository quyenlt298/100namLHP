using System.Threading.Tasks;
using TVA.MODEL.Account;
using TVA.SERVICES.External.Type;

namespace TVA.SERVICES.External.Base
{
    public interface ISocialNetworkProvider
    {
        Task<User> GetUserSocialNetworkByTokenAsync(SocialNetworkEnumType socialNetworkEnumType, string token);
    }
}