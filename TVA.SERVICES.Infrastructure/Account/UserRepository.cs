using DVG.BDS.DAL.SQLServer.Entity;
using TVA.COMMON.Security;
using TVA.DATA.DAL.Entity.DataType;
using TVA.DATA.DAL.Entity.Parameters;
using TVA.DATA.DAL.IData.IDatabase;
using TVA.MODEL.Account;
using TVA.SERVICES.Domain.Account;
using SPORTEA.SERVICES.Infrastructure;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;

namespace TVA.SERVICES.Infrastructure.Account
{
    public class UserRepository: IUserRepository
    {
        public UserRepository()
        {
        }

        public UserProfile UpdateUserInfoFromSocialNetwork(UserProfile user)
        {
            using (var context = new DatabaseContext())
            {
                // add user
                var newUser = new UserProfile
                {
                    FullName = user.FullName,
                    Mobile = user.Mobile,
                    SocialNetworkId = user.SocialNetworkId,
                    Email = user.Email,
                    Year = user.Year
                };
                context.UserProfiles.Add(newUser);
                context.SaveChanges();

                return newUser;
            }
        }
    }
}
