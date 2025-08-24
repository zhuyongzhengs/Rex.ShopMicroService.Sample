using System.ComponentModel.DataAnnotations.Schema;

namespace Rex.GoodService.Goods
{
    /// <summary>
    /// 商品分类
    /// </summary>
    public partial class GoodCategory
    {
        /// <summary>
        /// 类别名称
        /// </summary>
        [NotMapped]
        public string TypeName { get; set; }
    }
}