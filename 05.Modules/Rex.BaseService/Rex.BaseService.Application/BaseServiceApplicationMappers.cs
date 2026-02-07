using Rex.BaseService.Systems.AuditLoggings;
using Rex.BaseService.Systems.Menus;
using Rex.BaseService.Systems.OrganizationUnitRoles;
using Rex.BaseService.Systems.OrganizationUnits;
using Rex.BaseService.Systems.RoleMenus;
using Rex.BaseService.Systems.SecurityLogs;
using Rex.BaseService.Systems.SysUsers;
using Rex.BaseService.Systems.UserBalances;
using Rex.BaseService.Systems.UserGrades;
using Rex.BaseService.Systems.UserOrganizationUnits;
using Rex.BaseService.Systems.UserWeChats;
using Rex.BaseService.UserShips;
using Rex.Service.Core.Models;
using Rex.Service.Setting;
using Riok.Mapperly.Abstractions;
using System.Text.Json;
using Volo.Abp.AuditLogging;
using Volo.Abp.Identity;
using Volo.Abp.Mapperly;
using Volo.Abp.Settings;

namespace Rex.BaseService;

/* 在此处添加自己的映射关系 */

#region Menus

[Mapper]
public partial class MenuCreateDtoToMenuMapper : MapperBase<MenuCreateDto, Menu>
{
    public override partial Menu Map(MenuCreateDto mcd);

    public override partial void Map(MenuCreateDto mcd, Menu mu);

    public override void AfterMap(MenuCreateDto source, Menu destination)
    {
        destination.MetaInfo = MapMetaInfo(source);
    }

    private string? MapMetaInfo(MenuCreateDto mcd)
    {
        return mcd.Meta is not null ? JsonSerializer.Serialize(mcd.Meta) : null;
    }
}

[Mapper]
public partial class MenuUpdateDtoToMenuMapper : MapperBase<MenuUpdateDto, Menu>
{
    public override partial Menu Map(MenuUpdateDto mud);

    public override partial void Map(MenuUpdateDto mud, Menu mu);

    public override void AfterMap(MenuUpdateDto source, Menu destination)
    {
        destination.MetaInfo = MapMetaInfo(source);
    }

    private string? MapMetaInfo(MenuUpdateDto mud)
    {
        return mud.Meta is not null ? JsonSerializer.Serialize(mud.Meta) : null;
    }
}

[Mapper]
public partial class MenuMapper : TwoWayMapperBase<MenuDto, Menu>
{
    public override partial Menu Map(MenuDto md);

    public override partial void Map(MenuDto md, Menu mu);

    public override void AfterMap(MenuDto source, Menu destination)
    {
        destination.MetaInfo = MapMetaInfo(source);
    }

    private string? MapMetaInfo(MenuDto md)
    {
        return md.Meta != null ? JsonSerializer.Serialize(md.Meta) : null;
    }

    public override partial MenuDto ReverseMap(Menu mu);

    public override partial void ReverseMap(Menu mu, MenuDto md);

    public override void AfterReverseMap(Menu destination, MenuDto source)
    {
        source.Meta = MapMeta(destination);
    }

    private MenuMeta? MapMeta(Menu mu)
    {
        return !string.IsNullOrEmpty(mu.MetaInfo) ? JsonSerializer.Deserialize<MenuMeta>(mu.MetaInfo)
            : new MenuMeta();
    }
}

[Mapper]
public partial class MenuToMenuTreeDtoMapper : MapperBase<Menu, MenuTreeDto>
{
    public override partial MenuTreeDto Map(Menu mu);

    public override partial void Map(Menu mu, MenuTreeDto mtd);

    public override void AfterMap(Menu source, MenuTreeDto destination)
    {
        destination.Meta = MapMeta(source);
    }

    private MenuMeta? MapMeta(Menu mu)
    {
        return !string.IsNullOrEmpty(mu.MetaInfo) ? JsonSerializer.Deserialize<MenuMeta>(mu.MetaInfo) : new MenuMeta();
    }
}

#endregion Menus

#region RoleMenus

[Mapper]
public partial class RoleMenuCreateDtoToRoleMenuMapper : MapperBase<RoleMenuCreateDto, RoleMenu>
{
    public override partial RoleMenu Map(RoleMenuCreateDto rmcd);

    public override partial void Map(RoleMenuCreateDto rmcd, RoleMenu rm);
}

[Mapper]
public partial class RoleMenuUpdateDtoToRoleMenuMapper : MapperBase<RoleMenuUpdateDto, RoleMenu>
{
    public override partial RoleMenu Map(RoleMenuUpdateDto rmud);

    public override partial void Map(RoleMenuUpdateDto rmud, RoleMenu rm);
}

[Mapper]
public partial class RoleMenuToRoleMenuDtoMapper : MapperBase<RoleMenu, RoleMenuDto>
{
    public override partial RoleMenuDto Map(RoleMenu rm);

    public override partial void Map(RoleMenu rm, RoleMenuDto rmd);
}

#endregion RoleMenus

#region OrganizationUnits

[Mapper]
public partial class OrganizationUnitCreateDtoToOrganizationUnitMapper : MapperBase<OrganizationUnitCreateDto, OrganizationUnit>
{
    public override partial OrganizationUnit Map(OrganizationUnitCreateDto oucd);

    public override partial void Map(OrganizationUnitCreateDto oucd, OrganizationUnit ou);
}

[Mapper]
public partial class OrganizationUnitUpdateDtoToOrganizationUnitMapper : MapperBase<OrganizationUnitUpdateDto, OrganizationUnit>
{
    public override partial OrganizationUnit Map(OrganizationUnitUpdateDto ouud);

    public override partial void Map(OrganizationUnitUpdateDto ouud, OrganizationUnit ou);
}

[Mapper]
public partial class OrganizationUnitToOrganizationUnitDtoMapper : MapperBase<OrganizationUnit, OrganizationUnitDto>
{
    public override partial OrganizationUnitDto Map(OrganizationUnit ou);

    public override partial void Map(OrganizationUnit ou, OrganizationUnitDto oud);
}

[Mapper]
public partial class OrganizationUnitMapper : TwoWayMapperBase<OrganizationUnit, OrganizationUnitTreeDto>
{
    public override partial OrganizationUnitTreeDto Map(OrganizationUnit ou);

    public override partial void Map(OrganizationUnit ou, OrganizationUnitTreeDto outd);

    public override partial OrganizationUnit ReverseMap(OrganizationUnitTreeDto outd);

    public override partial void ReverseMap(OrganizationUnitTreeDto outd, OrganizationUnit ou);
}

#endregion OrganizationUnits

#region UserOrganizationUnits

[Mapper]
public partial class UserOrganizationUnitCreateDtoToIdentityUserOrganizationUnitMapper
    : MapperBase<UserOrganizationUnitCreateDto, IdentityUserOrganizationUnit>
{
    public override partial IdentityUserOrganizationUnit Map(UserOrganizationUnitCreateDto source);

    public override partial void Map(UserOrganizationUnitCreateDto source, IdentityUserOrganizationUnit destination);
}

[Mapper]
public partial class UserOrganizationUnitUpdateDtoToIdentityUserOrganizationUnitMapper
    : MapperBase<UserOrganizationUnitUpdateDto, IdentityUserOrganizationUnit>
{
    public override partial IdentityUserOrganizationUnit Map(UserOrganizationUnitUpdateDto source);

    public override partial void Map(UserOrganizationUnitUpdateDto source, IdentityUserOrganizationUnit destination);
}

[Mapper]
public partial class IdentityUserOrganizationUnitToUserOrganizationUnitDtoMapper
    : MapperBase<IdentityUserOrganizationUnit, UserOrganizationUnitDto>
{
    public override partial UserOrganizationUnitDto Map(IdentityUserOrganizationUnit source);

    public override partial void Map(IdentityUserOrganizationUnit source, UserOrganizationUnitDto destination);
}

#endregion UserOrganizationUnits

#region OrganizationUnitRoles

[Mapper]
public partial class OrganizationUnitRoleCreateDtoToOrganizationUnitRoleMapper
    : MapperBase<OrganizationUnitRoleCreateDto, OrganizationUnitRole>
{
    public override partial OrganizationUnitRole Map(OrganizationUnitRoleCreateDto source);

    public override partial void Map(OrganizationUnitRoleCreateDto source, OrganizationUnitRole destination);
}

[Mapper]
public partial class OrganizationUnitRoleUpdateDtoToOrganizationUnitRoleMapper
    : MapperBase<OrganizationUnitRoleUpdateDto, OrganizationUnitRole>
{
    public override partial OrganizationUnitRole Map(OrganizationUnitRoleUpdateDto source);

    public override partial void Map(OrganizationUnitRoleUpdateDto source, OrganizationUnitRole destination);
}

[Mapper]
public partial class OrganizationUnitRoleToOrganizationUnitRoleDtoMapper
    : MapperBase<OrganizationUnitRole, OrganizationUnitRoleDto>
{
    public override partial OrganizationUnitRoleDto Map(OrganizationUnitRole source);

    public override partial void Map(OrganizationUnitRole source, OrganizationUnitRoleDto destination);
}

#endregion OrganizationUnitRoles

#region AuditLogs

[Mapper]
public partial class AuditLogToAuditLogDtoMapper : MapperBase<AuditLog, AuditLogDto>
{
    public override partial AuditLogDto Map(AuditLog source);

    public override partial void Map(AuditLog source, AuditLogDto destination);
}

[Mapper]
public partial class EntityChangeToEntityChangeDtoMapper : MapperBase<EntityChange, EntityChangeDto>
{
    public override partial EntityChangeDto Map(EntityChange source);

    public override partial void Map(EntityChange source, EntityChangeDto destination);
}

[Mapper]
public partial class AuditLogActionToAuditLogActionDtoMapper : MapperBase<AuditLogAction, AuditLogActionDto>
{
    public override partial AuditLogActionDto Map(AuditLogAction source);

    public override partial void Map(AuditLogAction source, AuditLogActionDto destination);
}

[Mapper]
public partial class EntityPropertyChangeToEntityPropertyChangeDtoMapper : MapperBase<EntityPropertyChange, EntityPropertyChangeDto>
{
    public override partial EntityPropertyChangeDto Map(EntityPropertyChange source);

    public override partial void Map(EntityPropertyChange source, EntityPropertyChangeDto destination);
}

#endregion AuditLogs

#region SecurityLogs

[Mapper]
public partial class SecurityLogMapper : MapperBase<IdentitySecurityLog, SecurityLogDto>
{
    public override partial SecurityLogDto Map(IdentitySecurityLog source);

    public override partial void Map(IdentitySecurityLog source, SecurityLogDto destination);
}

#endregion SecurityLogs

#region UserWeChats

[Mapper]
public partial class UserWeChatCreateDtoToUserWeChatMapper : MapperBase<UserWeChatCreateDto, UserWeChat>
{
    public override partial UserWeChat Map(UserWeChatCreateDto source);

    public override partial void Map(UserWeChatCreateDto source, UserWeChat destination);
}

[Mapper]
public partial class UserWeChatUpdateDtoToUserWeChatMapper : MapperBase<UserWeChatUpdateDto, UserWeChat>
{
    public override partial UserWeChat Map(UserWeChatUpdateDto source);

    public override partial void Map(UserWeChatUpdateDto source, UserWeChat destination);
}

// 双向：UserWeChatDto ⇄ UserWeChat
[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.None, UseReferenceHandling = true)]
public partial class UserWeChatMapper : TwoWayMapperBase<UserWeChatDto, UserWeChat>
{
    public override partial UserWeChat Map(UserWeChatDto source);

    public override partial void Map(UserWeChatDto source, UserWeChat destination);

    public override partial UserWeChatDto ReverseMap(UserWeChat source);

    public override partial void ReverseMap(UserWeChat source, UserWeChatDto destination);
}

#endregion UserWeChats

#region UserGrades

[Mapper]
public partial class UserGradeCreateDtoToUserGradeMapper : MapperBase<UserGradeCreateDto, UserGrade>
{
    public override partial UserGrade Map(UserGradeCreateDto source);

    public override partial void Map(UserGradeCreateDto source, UserGrade destination);
}

[Mapper]
public partial class UserGradeUpdateDtoToUserGradeMapper : MapperBase<UserGradeUpdateDto, UserGrade>
{
    public override partial UserGrade Map(UserGradeUpdateDto source);

    public override partial void Map(UserGradeUpdateDto source, UserGrade destination);
}

// 双向：UserGradeDto ⇄ UserGrade
[Mapper]
public partial class UserGradeDtoMapper : TwoWayMapperBase<UserGradeDto, UserGrade>
{
    public override partial UserGrade Map(UserGradeDto source);

    public override partial void Map(UserGradeDto source, UserGrade destination);

    public override partial UserGradeDto ReverseMap(UserGrade source);

    public override partial void ReverseMap(UserGrade source, UserGradeDto destination);
}

// 单向：UserGrade → GradeDto
[Mapper]
public partial class UserGradeToGradeDtoMapper : MapperBase<UserGrade, GradeDto>
{
    public override partial GradeDto Map(UserGrade source);

    public override partial void Map(UserGrade source, GradeDto destination);
}

// 双向：UserGrade ⇄ GradeEto
[Mapper]
public partial class UserGradeEtoMapper : TwoWayMapperBase<UserGrade, GradeEto>
{
    public override partial GradeEto Map(UserGrade source);

    public override partial void Map(UserGrade source, GradeEto destination);

    public override partial UserGrade ReverseMap(GradeEto source);

    public override partial void ReverseMap(GradeEto source, UserGrade destination);
}

#endregion UserGrades

#region SysUsers

[Mapper]
public partial class SysUserCreateDtoToSysUserMapper : MapperBase<SysUserCreateDto, SysUser>
{
    public override partial SysUser Map(SysUserCreateDto source);

    public override partial void Map(SysUserCreateDto source, SysUser destination);
}

[Mapper]
public partial class SysUserUpdateDtoToSysUserMapper : MapperBase<SysUserUpdateDto, SysUser>
{
    public override partial SysUser Map(SysUserUpdateDto source);

    public override partial void Map(SysUserUpdateDto source, SysUser destination);
}

// 双向：SysUserDto ⇄ SysUser
[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.None, UseReferenceHandling = true)]
public partial class SysUserMapper : TwoWayMapperBase<SysUserDto, SysUser>
{
    public override partial SysUser Map(SysUserDto source);

    public override partial void Map(SysUserDto source, SysUser destination);

    public override partial SysUserDto ReverseMap(SysUser source);

    public override partial void ReverseMap(SysUser source, SysUserDto destination);
}

// 双向：UserPointLogDto ⇄ UserPointLog
[Mapper]
public partial class UserPointLogMapper : TwoWayMapperBase<UserPointLogDto, UserPointLog>
{
    public override partial UserPointLog Map(UserPointLogDto source);

    public override partial void Map(UserPointLogDto source, UserPointLog destination);

    public override partial UserPointLogDto ReverseMap(UserPointLog source);

    public override partial void ReverseMap(UserPointLog source, UserPointLogDto destination);
}

#endregion SysUsers

#region SettingValues

[Mapper]
public partial class SettingValueToSettingValueDoMapper : MapperBase<SettingValue, SettingValueDo>
{
    public override partial SettingValueDo Map(SettingValue source);

    public override partial void Map(SettingValue source, SettingValueDo destination);
}

#endregion SettingValues

#region UserShips

[Mapper]
public partial class UserShipCreateDtoToUserShipMapper : MapperBase<UserShipCreateDto, UserShip>
{
    public override partial UserShip Map(UserShipCreateDto source);

    public override partial void Map(UserShipCreateDto source, UserShip destination);
}

[Mapper]
public partial class UserShipUpdateDtoToUserShipMapper : MapperBase<UserShipUpdateDto, UserShip>
{
    public override partial UserShip Map(UserShipUpdateDto source);

    public override partial void Map(UserShipUpdateDto source, UserShip destination);
}

// 双向：UserShipDto ⇄ UserShip
[Mapper]
public partial class UserShipMapper : TwoWayMapperBase<UserShipDto, UserShip>
{
    public override partial UserShip Map(UserShipDto source);

    public override partial void Map(UserShipDto source, UserShip destination);

    public override partial UserShipDto ReverseMap(UserShip source);

    public override partial void ReverseMap(UserShip source, UserShipDto destination);
}

#endregion UserShips

#region UserBalances

// 双向：UserBalanceDto ⇄ UserBalance
[Mapper]
public partial class UserBalanceMapper : TwoWayMapperBase<UserBalanceDto, UserBalance>
{
    public override partial UserBalance Map(UserBalanceDto source);

    public override partial void Map(UserBalanceDto source, UserBalance destination);

    public override partial UserBalanceDto ReverseMap(UserBalance source);

    public override partial void ReverseMap(UserBalance source, UserBalanceDto destination);
}

#endregion UserBalances