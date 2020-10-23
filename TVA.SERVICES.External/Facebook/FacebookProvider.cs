using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using TVA.COMMON.Http.HttpRequest;
using TVA.COMMON.Http.Type;
using TVA.COMMON.Library.Convert;
using TVA.MODEL.Account;
using TVA.SERVICES.External.Base;

namespace TVA.SERVICES.External.Facebook
{
    public class FacebookProvider : BaseSocialNetwork
    {
        private const string QueryString =
            "https://graph.facebook.com/me?fields=first_name,last_name,name,id,email,gender,picture.type(large){url}&access_token=";

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
                var userFacebook = ConvertJson.Deserialize<UserFacebookInfo>(responseData.Data);

                user.FullName = userFacebook.Name;
                user.UserName = userFacebook.Email;
                user.SocialNetworkId = userFacebook.Id;
                user.Email = userFacebook.Email;
                //user.Gender = userFacebook.Gender;
                user.Avatar = userFacebook.Picture?.data?.url ?? string.Empty;
            }

            return user;
        }
    }
}
