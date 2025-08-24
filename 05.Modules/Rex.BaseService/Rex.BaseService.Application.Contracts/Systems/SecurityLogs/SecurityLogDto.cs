using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.MultiTenancy;

namespace Rex.BaseService.Systems.SecurityLogs
{
    /// <summary>
    /// 安全日志Dto
    /// </summary>
    public class SecurityLogDto : EntityDto<Guid>, IMultiTenant
    {
        /// <summary>
        /// 租户ID
        /// </summary>
        public Guid? TenantId { get; set; }

        /// <summary>
        /// 应用程序名称
        /// </summary>
        public string ApplicationName { get; set; }

        /// <summary>
        /// 执行操作的用户身份
        /// </summary>
        public string Identity { get; set; }

        /// <summary>
        /// 执行的操作
        /// </summary>
        /// <remarks>
        /// 例如登：录、注销等
        /// </remarks>
        public string Action { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid? UserId { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 租户名称
        /// </summary>
        public string TenantName { get; set; }

        /// <summary>
        /// 客户端ID
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// 客户端IP地址
        /// </summary>
        public string ClientIpAddress { get; set; }

        /// <summary>
        /// 浏览器信息
        /// </summary>
        public string BrowserInfo { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreationTime { get; set; }
    }
}