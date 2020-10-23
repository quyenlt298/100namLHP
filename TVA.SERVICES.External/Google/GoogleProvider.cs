using System.Threading.Tasks;
using TVA.COMMON.Http.HttpRequest;
using TVA.COMMON.Http.Type;
using TVA.COMMON.Library.Convert;
using TVA.MODEL.Account;
using TVA.SERVICES.External.Base;

namespace TVA.SERVICES.External.Google
{
    public class GoogleProvider : BaseSocialNetwork
    {

        private const string QueryString =
            "https://www.googleapis.com/oauth2/v3/tokeninfo?id_token=";


        public override async Task<User> GetUserSocialNetworkByTokenAsync(string token)
        {
            RequestInfo requestInfo = new RequestInfo
            {
                UrlBase = $"{QueryString}{token}"
            };

            var responseData = await RequestAPI.ConnectRestAPI(requestInfo, MethodType.GET);

            var user = new User();
            if (responseData.Code == ApiStatusCode.Ok && !string.IsNullOrEmpty(responseData.Data))
            {
                var userGooogle = ConvertJson.Deserialize<UserGoogleInfo>(responseData.Data);

                user.FullName = ($"{userGooogle.GivenName} {userGooogle.FamilyName}").Trim();
                user.UserName = userGooogle.Name;
                user.SocialNetworkId = userGooogle.Sub;
                user.Email = userGooogle.Email;
                //user.Gender
                user.Avatar = userGooogle.Picture;
            }

            return user;
        }
    }
}