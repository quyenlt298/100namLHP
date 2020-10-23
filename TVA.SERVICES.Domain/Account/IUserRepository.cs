using TVA.MODEL.Account;

namespace TVA.SERVICES.Domain.Account
{
    public interface IUserRepository
    {
        UserProfile UpdateUserInfoFromSocialNetwork(UserProfile user);
    }
}
