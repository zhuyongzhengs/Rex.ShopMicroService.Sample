using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace Rex.GoodService.Forms
{
    /// <summary>
    /// 修改表单Dto
    /// </summary>
    public class FormUpdateDto : EntityDto
    {
        /// <summary>
        /// 表单名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 表单类型
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 表单排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 图集
        /// </summary>
        public string? Images { get; set; }

        /// <summary>
        /// 视频地址
        /// </summary>
        public string? VideoPath { get; set; }

        /// <summary>
        /// 表单描述
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// 表头类型
        /// </summary>
        public int HeadType { get; set; }

        /// <summary>
        /// 表单头值
        /// </summary>
        public string? HeadTypeValue { get; set; }

        /// <summary>
        /// 表单视频
        /// </summary>
        public string? HeadTypeVideo { get; set; }

        /// <summary>
        /// 表单提交按钮名称
        /// </summary>
        public string? ButtonName { get; set; }

        /// <summary>
        /// 表单按钮颜色
        /// </summary>
        public string? ButtonColor { get; set; }

        /// <summary>
        /// 是否需要登录
        /// </summary>
        public bool IsLogin { get; set; }

        /// <summary>
        /// 可提交次数
        /// </summary>
        public int Times { get; set; }

        /// <summary>
        /// 二维码图片地址
        /// </summary>
        public string? Qrcode { get; set; }

        /// <summary>
        /// 提交后提示语
        /// </summary>
        public string? ReturnMsg { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndDateTime { get; set; }

        /// <summary>
        /// 并发(控制)戳
        /// </summary>
        public string ConcurrencyStamp { get; set; }
    }

    /// <summary>
    /// 修改表单提交Dto
    /// </summary>
    public class FormSubmitUpdateDto : EntityDto
    {
        /// <summary>
        /// From表单
        /// </summary>
        public FormUpdateDto Form { get; set; }

        /// <summary>
        /// Form(子集)表单项
        /// </summary>
        public List<FormItemUpdateDto> FormItems { get; set; }
    }
}