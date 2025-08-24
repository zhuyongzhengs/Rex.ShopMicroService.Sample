using System;
using Volo.Abp.Application.Dtos;

namespace Rex.BaseService.UserShips
{
    /// <summary>
    /// 创建用户(收货)地址
    /// </summary>
    public class UserShipCreateDto : EntityDto
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// 区域ID
        /// </summary>
        public long AreaId { get; set; }

        /// <summary>
        /// 显示区域信息
        /// </summary>
        public string? DisplayArea { get; set; }

        /// <summary>
        /// 收货详细地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 收货人姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 收货人电话
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 是否默认
        /// </summary>
        public bool IsDefault { get; set; }
    }
}