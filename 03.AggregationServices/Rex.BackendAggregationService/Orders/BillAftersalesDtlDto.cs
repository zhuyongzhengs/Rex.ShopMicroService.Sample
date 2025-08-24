using Rex.Service.Core.Configurations;
using Volo.Abp.Application.Dtos;

namespace Rex.BackendAggregationService.Orders
{
    /// <summary>
    /// 售后单明细Dto
    /// </summary>
    public class BillAftersalesDtlDto : EntityDto<Guid>
    {
        /// <summary>
        /// 售后单号
        /// </summary>
        public string No { get; set; }

        /// <summary>
        /// 订单ID
        /// </summary>
        public Guid OrderId { get; set; }

        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 售后类型
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 显示售后类型
        /// </summary>
        public string TypeDisplay
        {
            get
            {
                return this.Type.GetDescription<GlobalEnums.BillAftersalesIsReceive>();
            }
        }

        /// <summary>
        /// 退款金额
        /// </summary>
        public decimal RefundAmount { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 显示审核状态
        /// </summary>
        public string StatusDisplay
        {
            get
            {
                return this.Status.GetDescription<GlobalEnums.BillAftersalesStatus>();
            }
        }

        /// <summary>
        /// 退款原因
        /// </summary>
        public string Reason { get; set; }

        /// <summary>
        /// 卖家备注
        /// </summary>
        public string? Mark { get; set; }

        /// <summary>
        /// 凭证图片
        /// </summary>
        public List<string> Images { get; set; } = new();

        /// <summary>
        /// 售后单明细
        /// </summary>
        public List<BillAftersalesDtlItemDto> ProductItems { get; set; } = new();
    }
}