using System.Text.Json.Nodes;
using Volo.Abp.Application.Dtos;

namespace Rex.OrderService.Ships
{
    /// <summary>
    /// 地区配送费用Dto
    /// </summary>
    public class AreaFeeDto : EntityDto
    {
        /// <summary>
        /// 地区配送
        /// </summary>
        public string Areas { get; set; }

        /// <summary>
        /// 显示地区
        /// </summary>
        public JsonArray DisplayArea { get; set; }

        /// <summary>
        /// [地区]首重费用
        /// </summary>
        public decimal AreaFirstunitPrice { get; set; }

        /// <summary>
        /// [地区]续重费用
        /// </summary>
        public decimal AreaContinueunitPrice { get; set; }
    }
}