using Rex.AuthService.Applications;
using Rex.AuthService.Authorizations;
using Rex.AuthService.Scopes;
using Rex.AuthService.Tokens;
using Rex.Service.Setting;
using Riok.Mapperly.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Volo.Abp.Mapperly;
using Volo.Abp.OpenIddict.Applications;
using Volo.Abp.OpenIddict.Authorizations;
using Volo.Abp.OpenIddict.Scopes;
using Volo.Abp.OpenIddict.Tokens;
using Volo.Abp.Settings;

namespace Rex.AuthService;

#region Applications

[Mapper]
public partial class OpenIddictApplicationCreateDtoToOpenIddictApplicationMapper
    : MapperBase<OpenIddictApplicationCreateDto, OpenIddictApplication>
{
    public override partial OpenIddictApplication Map(OpenIddictApplicationCreateDto source);

    public override partial void Map(OpenIddictApplicationCreateDto source, OpenIddictApplication destination);

    public override void AfterMap(OpenIddictApplicationCreateDto source, OpenIddictApplication destination)
    {
        destination.Permissions = MapPermissions(source.Permissions);
        destination.PostLogoutRedirectUris = MapPostLogoutRedirectUris(source.PostLogoutRedirectUris);
        destination.RedirectUris = MapRedirectUris(source.RedirectUris);
        destination.Requirements = MapRequirements(source.Requirements);
    }

    private string? MapPermissions(HashSet<string> permissions) =>
        permissions != null && permissions.Any() ? JsonSerializer.Serialize(permissions) : null;

    private string? MapPostLogoutRedirectUris(HashSet<Uri> uris) =>
        uris != null && uris.Any() ? JsonSerializer.Serialize(uris) : null;

    private string? MapRedirectUris(HashSet<Uri> uris) =>
        uris != null && uris.Any() ? JsonSerializer.Serialize(uris) : null;

    private string? MapRequirements(HashSet<string> requirements) =>
        requirements != null && requirements.Any() ? JsonSerializer.Serialize(requirements) : null;
}

[Mapper]
public partial class OpenIddictApplicationUpdateDtoToOpenIddictApplicationMapper
    : MapperBase<OpenIddictApplicationUpdateDto, OpenIddictApplication>
{
    public override partial OpenIddictApplication Map(OpenIddictApplicationUpdateDto source);

    public override partial void Map(OpenIddictApplicationUpdateDto source, OpenIddictApplication destination);

    public override void AfterMap(OpenIddictApplicationUpdateDto source, OpenIddictApplication destination)
    {
        destination.Permissions = MapPermissions(source.Permissions);
        destination.PostLogoutRedirectUris = MapPostLogoutRedirectUris(source.PostLogoutRedirectUris);
        destination.RedirectUris = MapRedirectUris(source.RedirectUris);
        destination.Requirements = MapRequirements(source.Requirements);
    }

    private string? MapPermissions(HashSet<string> permissions) =>
        permissions != null && permissions.Any() ? JsonSerializer.Serialize(permissions) : null;

    private string? MapPostLogoutRedirectUris(HashSet<Uri> uris) =>
        uris != null && uris.Any() ? JsonSerializer.Serialize(uris) : null;

    private string? MapRedirectUris(HashSet<Uri> uris) =>
        uris != null && uris.Any() ? JsonSerializer.Serialize(uris) : null;

    private string? MapRequirements(HashSet<string> requirements) =>
        requirements != null && requirements.Any() ? JsonSerializer.Serialize(requirements) : null;
}

[Mapper]
public partial class OpenIddictApplicationCreateDtoToAbpApplicationDescriptorMapper
    : MapperBase<OpenIddictApplicationCreateDto, AbpApplicationDescriptor>
{
    public override partial AbpApplicationDescriptor Map(OpenIddictApplicationCreateDto source);

    public override partial void Map(OpenIddictApplicationCreateDto source, AbpApplicationDescriptor destination);
}

[Mapper]
public partial class OpenIddictApplicationUpdateDtoToOpenIddictApplicationModelMapper
    : MapperBase<OpenIddictApplicationUpdateDto, OpenIddictApplicationModel>
{
    public override partial OpenIddictApplicationModel Map(OpenIddictApplicationUpdateDto source);

    public override partial void Map(OpenIddictApplicationUpdateDto source, OpenIddictApplicationModel destination);

    public override void AfterMap(OpenIddictApplicationUpdateDto source, OpenIddictApplicationModel destination)
    {
        destination.Permissions = MapPermissions(source.Permissions);
        destination.PostLogoutRedirectUris = MapPostLogoutRedirectUris(source.PostLogoutRedirectUris);
        destination.RedirectUris = MapRedirectUris(source.RedirectUris);
        destination.Requirements = MapRequirements(source.Requirements);
    }

    private string? MapPermissions(HashSet<string> permissions) =>
        permissions != null && permissions.Any() ? JsonSerializer.Serialize(permissions) : null;

    private string? MapPostLogoutRedirectUris(HashSet<Uri> uris) =>
        uris != null && uris.Any() ? JsonSerializer.Serialize(uris) : null;

    private string? MapRedirectUris(HashSet<Uri> uris) =>
        uris != null && uris.Any() ? JsonSerializer.Serialize(uris) : null;

    private string? MapRequirements(HashSet<string> requirements) =>
        requirements != null && requirements.Any() ? JsonSerializer.Serialize(requirements) : null;
}

[Mapper]
public partial class AbpApplicationDescriptorToOpenIddictApplicationDtoMapper
    : MapperBase<AbpApplicationDescriptor, OpenIddictApplicationDto>
{
    public override partial OpenIddictApplicationDto Map(AbpApplicationDescriptor source);

    public override partial void Map(AbpApplicationDescriptor source, OpenIddictApplicationDto destination);

    public override void AfterMap(AbpApplicationDescriptor source, OpenIddictApplicationDto destination)
    {
        destination.Permissions = MapPermissions(source.Permissions);
        destination.PostLogoutRedirectUris = MapPostLogoutRedirectUris(source.PostLogoutRedirectUris);
        destination.RedirectUris = MapRedirectUris(source.RedirectUris);
        destination.Requirements = MapRequirements(source.Requirements);
    }

    private string? MapPermissions(HashSet<string> permissions) =>
        permissions != null && permissions.Any() ? JsonSerializer.Serialize(permissions) : null;

    private string? MapPostLogoutRedirectUris(HashSet<Uri> uris) =>
        uris != null && uris.Any() ? JsonSerializer.Serialize(uris) : null;

    private string? MapRedirectUris(HashSet<Uri> uris) =>
        uris != null && uris.Any() ? JsonSerializer.Serialize(uris) : null;

    private string? MapRequirements(HashSet<string> requirements) =>
        requirements != null && requirements.Any() ? JsonSerializer.Serialize(requirements) : null;
}

[Mapper]
public partial class OpenIddictApplicationToOpenIddictApplicationDtoMapper
    : MapperBase<OpenIddictApplication, OpenIddictApplicationDto>
{
    public override partial OpenIddictApplicationDto Map(OpenIddictApplication source);

    public override partial void Map(OpenIddictApplication source, OpenIddictApplicationDto destination);
}

#endregion Applications

#region Authorizations

[Mapper]
public partial class OpenIddictAuthorizationCreateDtoToOpenIddictAuthorizationMapper
    : MapperBase<OpenIddictAuthorizationCreateDto, OpenIddictAuthorization>
{
    public override partial OpenIddictAuthorization Map(OpenIddictAuthorizationCreateDto source);

    public override partial void Map(OpenIddictAuthorizationCreateDto source, OpenIddictAuthorization destination);
}

[Mapper]
public partial class OpenIddictAuthorizationUpdateDtoToOpenIddictAuthorizationMapper
    : MapperBase<OpenIddictAuthorizationUpdateDto, OpenIddictAuthorization>
{
    public override partial OpenIddictAuthorization Map(OpenIddictAuthorizationUpdateDto source);

    public override partial void Map(OpenIddictAuthorizationUpdateDto source, OpenIddictAuthorization destination);
}

[Mapper]
public partial class OpenIddictAuthorizationToOpenIddictAuthorizationDtoMapper
    : MapperBase<OpenIddictAuthorization, OpenIddictAuthorizationDto>
{
    public override partial OpenIddictAuthorizationDto Map(OpenIddictAuthorization source);

    public override partial void Map(OpenIddictAuthorization source, OpenIddictAuthorizationDto destination);
}

#endregion Authorizations

#region Tokens

[Mapper]
public partial class OpenIddictTokenCreateDtoToOpenIddictTokenMapper
    : MapperBase<OpenIddictTokenCreateDto, OpenIddictToken>
{
    public override partial OpenIddictToken Map(OpenIddictTokenCreateDto source);

    public override partial void Map(OpenIddictTokenCreateDto source, OpenIddictToken destination);
}

[Mapper]
public partial class OpenIddictTokenUpdateDtoToOpenIddictTokenMapper
    : MapperBase<OpenIddictTokenUpdateDto, OpenIddictToken>
{
    public override partial OpenIddictToken Map(OpenIddictTokenUpdateDto source);

    public override partial void Map(OpenIddictTokenUpdateDto source, OpenIddictToken destination);
}

[Mapper]
public partial class OpenIddictTokenToOpenIddictTokenDtoMapper
    : MapperBase<OpenIddictToken, OpenIddictTokenDto>
{
    public override partial OpenIddictTokenDto Map(OpenIddictToken source);

    public override partial void Map(OpenIddictToken source, OpenIddictTokenDto destination);
}

#endregion Tokens

#region Scopes

[Mapper]
public partial class OpenIddictScopeCreateDtoToOpenIddictScopeMapper
    : MapperBase<OpenIddictScopeCreateDto, OpenIddictScope>
{
    public override partial OpenIddictScope Map(OpenIddictScopeCreateDto source);

    public override partial void Map(OpenIddictScopeCreateDto source, OpenIddictScope destination);

    public override void AfterMap(OpenIddictScopeCreateDto source, OpenIddictScope destination)
    {
        destination.Resources = MapResources(source.Resources);
    }

    private string? MapResources(List<string> resources) =>
        resources != null && resources.Any() ? JsonSerializer.Serialize(resources) : null;
}

[Mapper]
public partial class OpenIddictScopeUpdateDtoToOpenIddictScopeMapper
    : MapperBase<OpenIddictScopeUpdateDto, OpenIddictScope>
{
    public override partial OpenIddictScope Map(OpenIddictScopeUpdateDto source);

    public override partial void Map(OpenIddictScopeUpdateDto source, OpenIddictScope destination);

    public override void AfterMap(OpenIddictScopeUpdateDto source, OpenIddictScope destination)
    {
        destination.Resources = MapResources(source.Resources);
    }

    private string? MapResources(List<string> resources) =>
        resources != null && resources.Any() ? JsonSerializer.Serialize(resources) : null;
}

[Mapper]
public partial class OpenIddictScopeToOpenIddictScopeDtoMapper
    : MapperBase<OpenIddictScope, OpenIddictScopeDto>
{
    public override partial OpenIddictScopeDto Map(OpenIddictScope source);

    public override partial void Map(OpenIddictScope source, OpenIddictScopeDto destination);
}

#endregion Scopes

#region SettingValues

[Mapper]
public partial class SettingValueToSettingValueDoMapper : MapperBase<SettingValue, SettingValueDo>
{
    public override partial SettingValueDo Map(SettingValue source);

    public override partial void Map(SettingValue source, SettingValueDo destination);
}

#endregion SettingValues