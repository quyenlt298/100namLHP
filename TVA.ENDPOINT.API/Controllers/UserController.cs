
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TVA.ENDPOINT.API.Auth;
using TVA.MODEL.Account;
using TVA.SERVICES.Domain.Account;

namespace TVA.ENDPOINT.API.Controllers
{
    //[Authorize] //Các endpoint không cần đăng nhập
    //[Authorization] //Các endpoint cần đăng nhập cho thêm bên dưới [Authorize]
    [Route("api/[controller]")]
    public class UsersController : BaseController
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // GET api/users
        [HttpGet]
        public ActionResult Get()
        {
            return Ok();
        }

        // POST api/users
        [HttpPost]
        public ActionResult Post([FromBody]UserProfile user)
        {
            return Ok(_userRepository.UpdateUserInfoFromSocialNetwork(user));
        }
    }
}
