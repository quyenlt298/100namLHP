using IdentityModel;
using IdentityServer4.Validation;
using System;
using System.Threading.Tasks;
using TVA.SERVICES.Domain.Account;
using TVA.SERVICES.External.Base;
using TVA.SERVICES.External.Type;
using TVA.COMMON.Library.Convert;

namespace TVA.ENDPOINT.Auth.Configuration
{
    public class DelegationGrantValidator : IExtensionGrantValidator
    {
        private readonly ISocialNetworkProvider _socialNetworkProvider;
        private readonly IUserRepository _userRepository;

        public DelegationGrantValidator(ISocialNetworkProvider socialNetworkProvider, IUserRepository userRepository)
        {
            _socialNetworkProvider = socialNetworkProvider;
            _userRepository = userRepository;
        }

        public async Task ValidateAsync(ExtensionGrantValidationContext context)
        {
            var token = context.Request.Raw.Get("token");
            var socialNetworkType = context.Request.Raw.Get("social_network_type");

            if (string.IsNullOrWhiteSpace(token) || string.IsNullOrWhiteSpace(socialNetworkType))
            {
                context.Result = new GrantValidationResult
                {
                    IsError = true,
                    Error = OidcConstants.TokenErrors.InvalidRequest,
                    ErrorDescription = "Tham số truyền vào chưa đủ",
                };

                return;
            }

            var user = await _socialNetworkProvider.GetUserSocialNetworkByTokenAsync(GetSocialNetworkType(socialNetworkType), token);

            if (user == null || string.IsNullOrWhiteSpace(user.FullName))
            {
                context.Result = new GrantValidationResult
                {
                    IsError = true,
                    Error = OidcConstants.TokenErrors.InvalidRequest,
                    ErrorDescription = "Có lỗi khi kết nối đến tài khoản mạng xã hội của bạn",
                };

                return;
            }

            user.AccountType = socialNetworkType;
            user = _userRepository.UpdateUserInfoFromSocialNetwork(user);

            if (user == null || user.UserId <= 0)
            {
                context.Result = new GrantValidationResult
                {
                    IsError = true,
                    Error = OidcConstants.TokenErrors.InvalidRequest,
                    ErrorDescription = "Cập nhật tài khoản vào hệ thống thất bại, vui lòng thử lại sau",
                };

                return;
            }

            context.Result = new GrantValidationResult(ConvertJson.Serialize(user),
                                                        OidcConstants.AuthenticationMethods.Password, 
                                                        DateTime.UtcNow.ToVnTime(),
                                                        user?.Claims);
        }

        private SocialNetworkEnumType GetSocialNetworkType(string socialNetworkType)
        {
            switch (socialNetworkType.ToLower())
            {
                case "facebook":
                    return SocialNetworkEnumType.Facebook;
                case "google":
                    return SocialNetworkEnumType.Google;
                default:
                    return SocialNetworkEnumType.Facebook;
            }
        }

        public string GrantType => "delegation";
    }
}
