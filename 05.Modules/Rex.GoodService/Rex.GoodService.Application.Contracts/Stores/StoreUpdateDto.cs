using System;
using Volo.Abp.Application.Dtos;

namespace Rex.GoodService.Stores
{
    /// <summary>
    /// 修改门店Dto
    /// </summary>
    public class StoreUpdateDto : EntityDto
    {
        /// <summary>
        /// 门店名称
        /// </summary>
        public string StoreName { get; set; }

        /// <summary>
        /// 门店电话/手机号
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 门店联系人
        /// </summary>
        public string LinkMan { get; set; }

        /// <summary>
        /// 门店logo
        /// </summary>
        public string LogoImage { get; set; }

        /// <summary>
        /// 门店地区id
        /// </summary>
        public long AreaId { get; set; }

        /// <summary>
        /// 门店详细地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 坐标位置
        /// </summary>
        public string Coordinate { get; set; }

        /// <summary>
        /// 纬度
        /// </summary>
        public string Latitude { get; set; }

        /// <summary>
        /// 经度
        /// </summary>
        public string Longitude { get; set; }

        /// <summary>
        /// 是否默认
        /// </summary>
        public bool IsDefault { get; set; }

        /// <summary>
        /// 距离
        /// </summary>
        public decimal Distance { get; set; }

        /// <summary>
        /// 并发(控制)戳
        /// </summary>
        public string ConcurrencyStamp { get; set; }
    }
}