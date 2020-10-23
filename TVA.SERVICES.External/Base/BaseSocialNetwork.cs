using System.Threading.Tasks;
using TVA.MODEL.Account;

namespace TVA.SERVICES.External.Base
{
    public abstract class BaseSocialNetwork
    {
        public abstract Task<User> GetUserSocialNetworkByTokenAsync(string token);
    }
}