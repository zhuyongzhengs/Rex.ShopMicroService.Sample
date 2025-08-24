namespace Rex.Service.Setting
{
    /// <summary>
    /// 设置值Dto
    /// </summary>
    public class SettingValueDo
    {
        /// <summary>
        /// 标签
        /// </summary>
        public string? Label { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public string? Value { get; set; }
    }
}