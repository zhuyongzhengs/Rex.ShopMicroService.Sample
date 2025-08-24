using System;
using System.Collections.Generic;
using System.Text;

namespace Rex.OrderService.Statistics
{
    /// <summary>
    /// 订单销售总额统计Dto
    /// </summary>
    [Serializable]
    public class OrderAmountStatisticsDto
    {
        /// <summary>
        /// 订单日期
        /// </summary>
        public DateTime OrderDate { get; set; }

        /// <summary>
        /// 销售总额
        /// </summary>
        public decimal? TotalAmount { get; set; }
    }
}