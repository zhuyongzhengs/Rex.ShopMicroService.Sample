using AutoMapper;
using Rex.AuthService.Applications;
using Rex.AuthService.Authorizations;
using Rex.AuthService.Scopes;
using Rex.AuthService.Tokens;
using Rex.Service.Setting;
using System.Linq;
using System.Text.Json;
using Volo.Abp.OpenIddict.Applications;
using Volo.Abp.OpenIddict.Authorizations;
using Volo.Abp.OpenIddict.Scopes;
using Volo.Abp.OpenIddict.Tokens;
using Volo.Abp.Settings;

namespace Rex.AuthService;

public class AuthServiceApplicationAutoMapperProfile : Profile
{
    public AuthServiceApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        #region Applications

        CreateMap<OpenIddictApplicationCreateDto, OpenIddictApplication>()
            .AfterMap((coia, oiam) =>
             {
                 oiam.Permissions = coia.Permissions.Any() ? JsonSerializer.Serialize(coia.Permissions) : null;
                 oiam.PostLogoutRedirectUris = coia.PostLogoutRedirectUris.Any() ? JsonSerializer.Serialize(coia.PostLogoutRedirectUris) : null;
                 oiam.RedirectUris = coia.RedirectUris.Any() ? JsonSerializer.Serialize(coia.RedirectUris) : null;
                 oiam.Requirements = coia.Requirements.Any() ? JsonSerializer.Serialize(coia.Requirements) : null;
             });
        CreateMap<OpenIddictApplicationUpdateDto, OpenIddictApplication>()
            .AfterMap((uoia, oia) =>
             {
                 oia.Permissions = uoia.Permissions.Any() ? JsonSerializer.Serialize(uoia.Permissions) : null;
                 oia.PostLogoutRedirectUris = uoia.PostLogoutRedirectUris.Any() ? JsonSerializer.Serialize(uoia.PostLogoutRedirectUris) : null;
                 oia.RedirectUris = uoia.RedirectUris.Any() ? JsonSerializer.Serialize(uoia.RedirectUris) : null;
                 oia.Requirements = uoia.Requirements.Any() ? JsonSerializer.Serialize(uoia.Requirements) : null;
             });

        CreateMap<OpenIddictApplicationCreateDto, AbpApplicationDescriptor>();
        CreateMap<OpenIddictApplicationUpdateDto, OpenIddictApplicationModel>()
            .AfterMap((uoia, oiam) =>
            {
                oiam.Permissions = uoia.Permissions.Any() ? JsonSerializer.Serialize(uoia.Permissions) : null;
                oiam.PostLogoutRedirectUris = uoia.PostLogoutRedirectUris.Any() ? JsonSerializer.Serialize(uoia.PostLogoutRedirectUris) : null;
                oiam.RedirectUris = uoia.RedirectUris.Any() ? JsonSerializer.Serialize(uoia.RedirectUris) : null;
                oiam.Requirements = uoia.Requirements.Any() ? JsonSerializer.Serialize(uoia.Requirements) : null;
            });

        CreateMap<AbpApplicationDescriptor, OpenIddictApplicationDto>()
            .AfterMap((ad, oiad) =>
            {
                oiad.Permissions = ad.Permissions.Any() ? JsonSerializer.Serialize(ad.Permissions) : null;
                oiad.PostLogoutRedirectUris = ad.PostLogoutRedirectUris.Any() ? JsonSerializer.Serialize(ad.PostLogoutRedirectUris) : null;
                oiad.RedirectUris = ad.RedirectUris.Any() ? JsonSerializer.Serialize(ad.RedirectUris) : null;
                oiad.Requirements = ad.Requirements.Any() ? JsonSerializer.Serialize(ad.Requirements) : null;
                oiad.Properties = ad.Properties.Any() ? JsonSerializer.Serialize(ad.Properties) : null;
            });
        CreateMap<OpenIddictApplication, OpenIddictApplicationDto>();

        #endregion Applications

        #region Authorizations

        CreateMap<OpenIddictAuthorizationCreateDto, OpenIddictAuthorization>();
        CreateMap<OpenIddictAuthorizationUpdateDto, OpenIddictAuthorization>();
        CreateMap<OpenIddictAuthorization, OpenIddictAuthorizationDto>();

        #endregion Authorizations

        #region Tokens

        CreateMap<OpenIddictTokenCreateDto, OpenIddictToken>();
        CreateMap<OpenIddictTokenUpdateDto, OpenIddictToken>();
        CreateMap<OpenIddictToken, OpenIddictTokenDto>();

        #endregion Tokens

        #region Scopes

        CreateMap<OpenIddictScopeCreateDto, OpenIddictScope>()
            .AfterMap((cois, ois) =>
             {
                 ois.Resources = cois.Resources.Any() ? JsonSerializer.Serialize(cois.Resources) : null;
             });
        CreateMap<OpenIddictScopeUpdateDto, OpenIddictScope>()
            .AfterMap((uois, ois) =>
            {
                ois.Resources = uois.Resources.Any() ? JsonSerializer.Serialize(uois.Resources) : null;
            });
        CreateMap<OpenIddictScope, OpenIddictScopeDto>();

        #endregion Scopes

        #region SettingValues

        CreateMap<SettingValue, SettingValueDo>();

        #endregion SettingValues
    }
}