using Rex.GoodService.Products;
using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace Rex.GoodService.Goods
{
    /// <summary>
    /// 商品明细Dto
    /// </summary>
    public class GoodDetailDto : EntityDto
    {
        /// <summary>
        /// 基础信息
        /// </summary>
        public GoodBasicInfoDto BasicInfo { get; set; }

        /// <summary>
        /// 图集/视频
        /// </summary>
        public GoodImageOrVideoDto ImageOrVideo { get; set; }

        /// <summary>
        /// SKU/货品设置
        /// </summary>
        public GoodSetSkuProductDto SetSkuProduct { get; set; }

        /// <summary>
        /// 参数设置
        /// </summary>
        public GoodParameterDto SetParam { get; set; }

        /// <summary>
        /// 会员折扣
        /// </summary>
        public List<GoodGradeDto> GoodGrades { get; set; }

        /// <summary>
        /// 商品详情
        /// </summary>
        public string? Intro { get; set; }
    }

    /// <summary>
    /// 商品基础信息
    /// </summary>
    public class GoodBasicInfoDto : EntityDto<Guid>
    {
        /// <summary>
        /// 商品名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 商品分类
        /// </summary>
        public Guid GoodCategoryId { get; set; }

        /// <summary>
        /// 商品分类扩展
        /// </summary>
        public string? GoodCategoryIdExtends { get; set; }

        /// <summary>
        /// 品牌
        /// </summary>
        public Guid BrandId { get; set; }

        /// <summary>
        /// 商品条码
        /// </summary>
        public string BarCode { get; set; }

        /// <summary>
        /// 商品简介
        /// </summary>
        public string? Brief { get; set; }

        /// <summary>
        /// 商品单位
        /// </summary>
        public string? Unit { get; set; }

        /// <summary>
        /// 商品排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 是否上架
        /// </summary>
        public bool IsMarketable { get; set; }

        /// <summary>
        /// 是否虚拟商品
        /// </summary>
        public bool IsNomalVirtual { get; set; }

        /// <summary>
        /// 是否推荐
        /// </summary>
        public bool IsRecommend { get; set; }

        /// <summary>
        /// 是否热门
        /// </summary>
        public bool IsHot { get; set; }
    }

    /// <summary>
    /// 商品图集/视频
    /// </summary>
    public class GoodImageOrVideoDto : EntityDto
    {
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
    }

    /// <summary>
    /// 商品SKU/货品设置
    /// </summary>
    public class GoodSetSkuProductDto : EntityDto
    {
        /// <summary>
        /// SKU规格ID
        /// </summary>
        public string? GoodSkuIds { get; set; }

        /// <summary>
        /// 商品规格序列号存储
        /// </summary>
        public string? SpesDesc { get; set; }

        /// <summary>
        /// 自定义规格名称
        /// </summary>
        public string? NewSpec { get; set; }

        /// <summary>
        /// 佣金分配方式
        /// </summary>
        public int ProductsDistributionType { get; set; }

        /// <summary>
        /// 开启规则
        /// </summary>
        public int OpenSpec { get; set; }

        /// <summary>
        /// 货品信息
        /// </summary>
        public List<ProductDto> Products { get; set; }
    }

    /// <summary>
    /// 商品参数设置
    /// </summary>
    public class GoodParameterDto : EntityDto
    {
        /// <summary>
        /// 参数ID
        /// </summary>
        public string? GoodParamsIds { get; set; }

        /// <summary>
        /// 参数序列化
        /// </summary>
        public string? Parameters { get; set; }
    }
}