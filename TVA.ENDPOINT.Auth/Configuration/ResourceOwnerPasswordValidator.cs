using IdentityModel;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Authentication;
using System;
using System.Threading.Tasks;
using TVA.SERVICES.Domain.Account;
using TVA.COMMON.Library.Convert;

namespace TVA.ENDPOINT.Auth.Configuration
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly IUserRepository _userRepository;

        public ResourceOwnerPasswordValidator(IUserRepository userRepository, ISystemClock systemClock)
        {
            _userRepository = userRepository;
        }

        public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            if (string.IsNullOrWhiteSpace(context.UserName) || string.IsNullOrWhiteSpace(context.Password))
            {
                context.Result = new GrantValidationResult
                {
                    IsError = true,
                    Error = OidcConstants.TokenErrors.InvalidRequest,
                    ErrorDescription = "Bạn cần truyền đủ tên đăng nhập và mật khẩu",
                };

                return Task.FromResult(0);
            }

            var user = _userRepository.GetUserInfo(context.UserName, context.Password);

            if (user?.UserId > 0)
            {
                context.Result = new GrantValidationResult(ConvertJson.Serialize(user),
                                                            OidcConstants.AuthenticationMethods.Password, 
                                                            DateTime.UtcNow.ToVnTime(),
                                                            user?.Claims);

                return Task.FromResult(0);
            }

            context.Result = new GrantValidationResult
            {
                IsError = true,
                Error = OidcConstants.TokenErrors.InvalidClient,
                ErrorDescription = "Tên đăng nhập hoặc mật khẩu của bạn không đúng"
            };

            return Task.FromResult(0);
        }
    }
}
