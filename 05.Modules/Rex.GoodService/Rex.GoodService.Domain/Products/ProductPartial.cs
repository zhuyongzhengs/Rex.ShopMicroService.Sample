using System.ComponentModel.DataAnnotations.Schema;

namespace Rex.GoodService.Products
{
    /// <summary>
    /// 货品
    /// </summary>
    public partial class Product
    {
        /// <summary>
        /// 一级佣金
        /// </summary>
        [NotMapped]
        public decimal LevelOne { get; set; } = 0;

        /// <summary>
        /// 二级佣金
        /// </summary>
        [NotMapped]
        public decimal LevelTwo { get; set; } = 0;

        /// <summary>
        /// 三级佣金
        /// </summary>
        [NotMapped]
        public decimal LevelThree { get; set; } = 0;
    }
}