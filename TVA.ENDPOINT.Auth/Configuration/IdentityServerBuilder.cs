using Microsoft.Extensions.DependencyInjection;

namespace TVA.ENDPOINT.Auth.Configuration
{
    public static class IdentityServerBuilder
    {
        public static IIdentityServerBuilder AddCustomResourceOwnerPassword(this IIdentityServerBuilder builder)
        {
            builder.AddResourceOwnerValidator<ResourceOwnerPasswordValidator>();

            return builder;
        }

        public static IIdentityServerBuilder AddCustomSocialNetworkPassword(this IIdentityServerBuilder builder)
        {
            builder.AddExtensionGrantValidator<DelegationGrantValidator>();

            return builder;
        }
    }
}
