using AutoMapper;
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
using System;
using System.Text.Json;
using Volo.Abp.AuditLogging;
using Volo.Abp.Identity;
using Volo.Abp.Settings;

namespace Rex.BaseService;

public class BaseServiceApplicationAutoMapperProfile : Profile
{
    public BaseServiceApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        #region Menus

        CreateMap<MenuCreateDto, Menu>()
            .AfterMap((cmd, mu) =>
             {
                 mu.MetaInfo = cmd.Meta != null ? JsonSerializer.Serialize(cmd.Meta) : null;
             });
        CreateMap<MenuUpdateDto, Menu>()
            .AfterMap((umd, mu) =>
            {
                mu.MetaInfo = umd.Meta != null ? JsonSerializer.Serialize(umd.Meta) : null;
            });
        CreateMap<MenuDto, Menu>()
            .AfterMap((md, mu) =>
            {
                mu.MetaInfo = md.Meta != null ? JsonSerializer.Serialize(md.Meta) : null;
            });
        CreateMap<Menu, MenuDto>()
            .AfterMap((mu, md) =>
            {
                md.Meta = mu.MetaInfo.IsNullOrEmpty() ? null : JsonSerializer.Deserialize<MenuMeta>(mu.MetaInfo);
            });
        CreateMap<Menu, MenuTreeDto>()
            .AfterMap((mu, mtd) =>
            {
                mtd.Meta = mu.MetaInfo.IsNullOrEmpty() ? null : JsonSerializer.Deserialize<MenuMeta>(mu.MetaInfo);
            });

        #endregion Menus

        #region RoleMenus

        CreateMap<RoleMenuCreateDto, RoleMenu>();
        CreateMap<RoleMenuUpdateDto, RoleMenu>();
        CreateMap<RoleMenu, RoleMenuDto>();

        #endregion RoleMenus

        #region OrganizationUnits

        CreateMap<OrganizationUnitCreateDto, OrganizationUnit>();
        CreateMap<OrganizationUnitUpdateDto, OrganizationUnit>();
        CreateMap<OrganizationUnit, OrganizationUnitDto>();

        CreateMap<OrganizationUnit, OrganizationUnitTreeDto>();
        CreateMap<OrganizationUnitTreeDto, OrganizationUnit>();

        #endregion OrganizationUnits

        #region UserOrganizationUnits

        CreateMap<UserOrganizationUnitCreateDto, IdentityUserOrganizationUnit>();
        CreateMap<UserOrganizationUnitUpdateDto, IdentityUserOrganizationUnit>();
        CreateMap<IdentityUserOrganizationUnit, UserOrganizationUnitDto>();

        #endregion UserOrganizationUnits

        #region OrganizationUnitRoles

        CreateMap<OrganizationUnitRoleCreateDto, OrganizationUnitRole>();
        CreateMap<OrganizationUnitRoleUpdateDto, OrganizationUnitRole>();
        CreateMap<OrganizationUnitRole, OrganizationUnitRoleDto>();

        #endregion OrganizationUnitRoles

        #region AuditLogs

        CreateMap<AuditLog, AuditLogDto>()
                .ForMember(t => t.EntityChanges, option => option.MapFrom(l => l.EntityChanges))
                .ForMember(t => t.Actions, option => option.MapFrom(l => l.Actions));
        CreateMap<EntityChange, EntityChangeDto>()
             .ForMember(t => t.PropertyChanges, option => option.MapFrom(l => l.PropertyChanges));

        CreateMap<AuditLogAction, AuditLogActionDto>();
        CreateMap<EntityPropertyChange, EntityPropertyChangeDto>();

        #endregion AuditLogs

        #region SecurityLogs

        CreateMap<IdentitySecurityLog, SecurityLogDto>();
        CreateMap<SecurityLogDto, IdentitySecurityLog>();

        #endregion SecurityLogs

        #region UserWeChats

        CreateMap<UserWeChatCreateDto, UserWeChat>();
        CreateMap<UserWeChatUpdateDto, UserWeChat>();
        CreateMap<UserWeChatDto, UserWeChat>();
        CreateMap<UserWeChat, UserWeChatDto>();

        #endregion UserWeChats

        #region UserGrades

        CreateMap<UserGradeCreateDto, UserGrade>();
        CreateMap<UserGradeUpdateDto, UserGrade>();
        CreateMap<UserGradeDto, UserGrade>();
        CreateMap<UserGrade, UserGradeDto>();
        CreateMap<UserGrade, GradeDto>();
        CreateMap<UserGrade, GradeEto>();
        CreateMap<GradeEto, UserGrade>();

        #endregion UserGrades

        #region SysUsers

        CreateMap<SysUserCreateDto, SysUser>();
        CreateMap<SysUserUpdateDto, SysUser>();
        CreateMap<SysUserDto, SysUser>();
        CreateMap<SysUser, SysUserDto>();

        CreateMap<UserPointLogDto, UserPointLog>();
        CreateMap<UserPointLog, UserPointLogDto>();

        #endregion SysUsers

        #region SettingValues

        CreateMap<SettingValue, SettingValueDo>();

        #endregion SettingValues

        #region UserShips

        CreateMap<UserShipCreateDto, UserShip>();
        CreateMap<UserShipUpdateDto, UserShip>();
        CreateMap<UserShipDto, UserShip>();
        CreateMap<UserShip, UserShipDto>();

        #endregion UserShips

        #region UserBalances

        CreateMap<UserBalanceDto, UserBalance>();
        CreateMap<UserBalance, UserBalanceDto>();

        #endregion UserBalances
    }
}