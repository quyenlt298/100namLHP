using IdentityModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using TVA.COMMON.Http.Type;
using TVA.COMMON.Library.Convert;
using TVA.MODEL.Account;
using TVA.MODEL.Base;

namespace TVA.ENDPOINT.API.Auth
{
    public class Authorization : ActionFilterAttribute
    {
        private StringValues _authorizationToken;

        public override async Task OnActionExecutionAsync(ActionExecutingContext actionContext, ActionExecutionDelegate next)
        {
            var isAuthHeader = actionContext.HttpContext.Request.Headers.TryGetValue("Authorization", out _authorizationToken);

            if (!isAuthHeader)
            {
                actionContext.Result = new ContentResult
                {
                    Content = "UnAuthorized - Bạn không có quyền truy cập chức năng này",
                    StatusCode = ApiStatusCode.UnAuthorized
                };

                return;
            }

            string token = _authorizationToken.ToString().Substring("Bearer ".Length);

            JwtSecurityToken secToken = new JwtSecurityToken(token);

            var user = new UserProfile();
            try
            {
                string userInfoJson = secToken.Claims.First(claim => claim.Type == JwtClaimTypes.Subject)?.Value;
                if (!string.IsNullOrEmpty(userInfoJson)) user = ConvertJson.Deserialize<UserProfile>(userInfoJson);
            }
            catch { }

            if (string.IsNullOrEmpty(user?.FullName))
            {
                actionContext.Result = new ContentResult
                {
                    Content = "UnAuthorized - Bạn không có quyền truy cập chức năng này",
                    StatusCode = ApiStatusCode.UnAuthorized
                };

                return;
            }

            actionContext.HttpContext.Items[Constant.USER_INFO] = user;

            await next();
        }
    }
}
