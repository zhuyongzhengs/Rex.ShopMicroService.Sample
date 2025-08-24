namespace Rex.Service.Core.Models
{
    /// <summary>
    /// 枚举实体
    /// </summary>
    public class EnumEntity
    {
        /// <summary>
        /// 枚举的描述
        /// </summary>
        public string Description { set; get; }

        /// <summary>
        /// 枚举名称
        /// </summary>
        public string Title { set; get; }

        /// <summary>
        /// 枚举对象的值
        /// </summary>
        public int Value { set; get; }
    }
}