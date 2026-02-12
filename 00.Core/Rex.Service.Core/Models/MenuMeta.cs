using System;

namespace Rex.Service.Core.Models
{
    /// <summary>
    /// 元信息
    /// </summary>
    [Serializable]
    public class MenuMeta
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 是否隐藏
        /// </summary>
        public bool IsHide { get; set; }

        /// <summary>
        /// 是否缓存
        /// </summary>
        public bool IsKeepAlive { get; set; }

        /// <summary>
        /// 是否固定
        /// </summary>
        public bool IsAffix { get; set; }

        /// <summary>
        /// 外链地址
        /// </summary>
        public string Link { get; set; }

        /// <summary>
        /// 是否内嵌
        /// </summary>
        public bool IsIframe { get; set; }
    }
}