using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Rex.GoodService.Stores
{
    /// <summary>
    /// 门店表
    /// </summary>
    public class Store : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public Store()
        {
        }

        public Store(Guid id)
        {
            this.Id = id;
        }

        /// <summary>
        /// 租户ID
        /// </summary>
        public Guid? TenantId { get; set; }

        /// <summary>
        /// 门店名称
        /// </summary>
        [StringLength(125)]
        [Required]
        public string StoreName { get; set; }

        /// <summary>
        /// 门店电话/手机号
        /// </summary>
        [StringLength(13)]
        [Required]
        public string Mobile { get; set; }

        /// <summary>
        /// 门店联系人
        /// </summary>
        [StringLength(50)]
        [Required]
        public string LinkMan { get; set; }

        /// <summary>
        /// 门店logo
        /// </summary>
        [StringLength(255)]
        [Required]
        public string LogoImage { get; set; }

        /// <summary>
        /// 门店地区id
        /// </summary>
        [Required]
        public long AreaId { get; set; }

        /// <summary>
        /// 门店详细地址
        /// </summary>
        [StringLength(255)]
        [Required]
        public string Address { get; set; }

        /// <summary>
        /// 坐标位置
        /// </summary>
        [StringLength(50)]
        [Required]
        public string Coordinate { get; set; }

        /// <summary>
        /// 纬度
        /// </summary>
        [StringLength(40)]
        [Required]
        public string Latitude { get; set; }

        /// <summary>
        /// 经度
        /// </summary>
        [StringLength(40)]
        [Required]
        public string Longitude { get; set; }

        /// <summary>
        /// 是否默认
        /// </summary>
        [Required]
        public bool IsDefault { get; set; }

        /// <summary>
        /// 距离
        /// </summary>
        [Required]
        public decimal Distance { get; set; }
    }
}