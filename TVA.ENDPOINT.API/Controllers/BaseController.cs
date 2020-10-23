using Microsoft.AspNetCore.Mvc;
using TVA.MODEL.Account;
using TVA.MODEL.Base;

namespace TVA.ENDPOINT.API.Controllers
{
    public class BaseController : Controller
    {
        public UserContract UserInfo
        {
            get
            {
                if (HttpContext.Items.Keys.Contains(Constant.USER_INFO))
                {
                    return (UserContract)HttpContext.Items[Constant.USER_INFO];
                }

                return new UserContract();
            }
        }
    }
}
