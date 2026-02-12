using System;
using static Rex.Service.Core.Configurations.GlobalEnums;

namespace Rex.Service.Core.Models
{
    /// <summary>
    /// 后台工作状态缓存
    /// </summary>
    [Serializable]
    public class WorkerStatusRc
    {
        /// <summary>
        /// 状态
        /// </summary>
        public BackgroundWorkerStatus? Status { get; set; }
    }
}