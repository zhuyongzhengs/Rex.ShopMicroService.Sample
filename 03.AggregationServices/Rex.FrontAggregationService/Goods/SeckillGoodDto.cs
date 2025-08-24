using Rex.Service.Core.Configurations;
using Volo.Abp.Application.Dtos;

namespace Rex.FrontAggregationService.Goods
{
    /// <summary>
    /// 秒杀商品Dto
    /// </summary>
    public class SeckillGoodDto : EntityDto<Guid>
    {
        /// <summary>
        /// 商品条码
        /// </summary>
        public string BarCode { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 品牌名称
        /// </summary>
        public string? BrandName { get; set; }

        /// <summary>
        /// 商品简介
        /// </summary>
        public string? Brief { get; set; }

        /// <summary>
        /// 缩略图
        /// </summary>
        public string? Image { get; set; }

        /// <summary>
        /// 图集
        /// </summary>
        public string? Images { get; set; }

        /// <summary>
        /// 视频
        /// </summary>
        public string? Video { get; set; }

        /// <summary>
        /// sku序列
        /// </summary>
        public string? GoodSkuIds { get; set; }

        /// <summary>
        /// 参数序列
        /// </summary>
        public string? GoodParamsIds { get; set; }

        /// <summary>
        /// 是否虚拟商品
        /// </summary>
        public bool IsNomalVirtual { get; set; }

        /// <summary>
        /// 商品单位
        /// </summary>
        public string? Unit { get; set; }

        /// <summary>
        /// 商品详情
        /// </summary>
        public string? Intro { get; set; }

        /// <summary>
        /// 商品规格序列号存储
        /// </summary>
        public string? SpesDesc { get; set; }

        /// <summary>
        /// 参数序列化
        /// </summary>
        public string? Parameters { get; set; }

        /// <summary>
        /// 评论次数
        /// </summary>
        public int CommentsCount { get; set; }

        /// <summary>
        /// 浏览次数
        /// </summary>
        public int ViewCount { get; set; }

        /// <summary>
        /// 购买次数
        /// </summary>
        public int BuyCount { get; set; }

        /// <summary>
        /// 商品排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 自定义规格名称
        /// </summary>
        public string? NewSpec { get; set; }

        /// <summary>
        /// 开启规则
        /// </summary>
        public int OpenSpec { get; set; }

        /// <summary>
        /// 是否推荐
        /// </summary>
        public bool IsRecommend { get; set; }

        /// <summary>
        /// 是否热门
        /// </summary>
        public bool IsHot { get; set; }

        /// <summary>
        /// 是否收藏
        /// </summary>
        public bool IsFav { get; set; }

        /// <summary>
        /// 销售价
        /// </summary>
        public decimal Price { get; set; } = 0;

        /// <summary>
        /// 市场价
        /// </summary>
        public decimal MktPrice { get; set; } = 0;

        /// <summary>
        /// 货品信息
        /// </summary>
        public List<SeckillProductDto> Products { get; set; }

        /// <summary>
        /// 促销ID
        /// </summary>
        public Guid PromotionId { get; set; }

        /// <summary>
        /// 促销类型
        /// </summary>
        public int PromotionType { get; set; }

        /// <summary>
        /// 促销是否启用
        /// </summary>
        public bool PromotionIsEnable { get; set; }

        /// <summary>
        /// 促销(当前)时间
        /// </summary>
        public DateTime PromotionTime { get; set; }

        /// <summary>
        /// 促销开始时间
        /// </summary>
        public DateTime PromotionStartTime { get; set; }

        /// <summary>
        /// 促销结束时间
        /// </summary>
        public DateTime PromotionEndTime { get; set; }

        /// <summary>
        /// 开始状态
        /// </summary>
        public int StartStatus { get; set; }

        /// <summary>
        /// 显示类型
        /// </summary>
        public string StartStatusDisplay
        {
            get
            {
                return this.StartStatus.GetDescription<GlobalEnums.PinTuanRuleStatus>();
            }
        }

        /// <summary>
        /// 是否过期
        /// </summary>
        public bool IsOverdue { get; set; }

        /// <summary>
        /// 促销剩余时间(秒)
        /// </summary>
        public int PromotionSeconds { get; set; }

        /// <summary>
        /// 秒杀价格
        /// </summary>
        public decimal GoodSeckillMoney { get; set; }

        /// <summary>
        /// 秒杀优惠价格
        /// </summary>
        public decimal SeckillDiscountAmount { get; set; }

        /// <summary>
        /// (总)库存数量
        /// </summary>
        public int TotalStock { get; set; }

        /// <summary>
        /// (总)冻结库存数量
        /// </summary>
        public int TotalFreezeStock { get; set; }
    }
}