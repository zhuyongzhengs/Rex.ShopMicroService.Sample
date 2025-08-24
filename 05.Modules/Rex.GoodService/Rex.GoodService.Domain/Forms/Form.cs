using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Rex.GoodService.Forms
{
    /// <summary>
    /// 表单
    /// </summary>
    public class Form : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        /// <summary>
        /// 租户ID
        /// </summary>
        public Guid? TenantId { get; set; }

        /// <summary>
        /// 表单名称
        /// </summary>
        [StringLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// 表单类型
        /// </summary>
        [Required]
        public int Type { get; set; }

        /// <summary>
        /// 表单排序
        /// </summary>
        [Required]
        public int Sort { get; set; }

        /// <summary>
        /// 图集
        /// </summary>
        public string? Images { get; set; }

        /// <summary>
        /// 视频地址
        /// </summary>
        [StringLength(255)]
        public string? VideoPath { get; set; }

        /// <summary>
        /// 表单描述
        /// </summary>
        [StringLength(255)]
        public string? Description { get; set; }

        /// <summary>
        /// 表头类型
        /// </summary>
        [Required]
        public int HeadType { get; set; }

        /// <summary>
        /// 表单头值
        /// </summary>
        [StringLength(200)]
        public string? HeadTypeValue { get; set; }

        /// <summary>
        /// 表单视频
        /// </summary>
        [StringLength(200)]
        public string? HeadTypeVideo { get; set; }

        /// <summary>
        /// 表单提交按钮名称
        /// </summary>
        [StringLength(50)]
        public string? ButtonName { get; set; }

        /// <summary>
        /// 表单按钮颜色
        /// </summary>
        [StringLength(30)]
        public string? ButtonColor { get; set; }

        /// <summary>
        /// 是否需要登录
        /// </summary>
        [Required]
        public bool IsLogin { get; set; }

        /// <summary>
        /// 可提交次数
        /// </summary>
        [Required]
        public int Times { get; set; }

        /// <summary>
        /// 二维码图片地址
        /// </summary>
        [StringLength(200)]
        public string? Qrcode { get; set; }

        /// <summary>
        /// 提交后提示语
        /// </summary>
        [StringLength(200)]
        public string? ReturnMsg { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        [Required]
        public DateTime EndDateTime { get; set; }

        /// <summary>
        /// (子集)表单项
        /// </summary>
        public List<FormItem> FormItems { get; set; }
    }
}