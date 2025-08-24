using System.ComponentModel.DataAnnotations;

namespace Rex.BaseService.Systems.Menus
{
    /// <summary>
    /// 获取菜单Dto
    /// </summary>
    public class GetMenuInput
    {
        /// <summary>
        /// 排序字段
        /// </summary>
        [Required]
        public string Sorting { get; set; }

        /// <summary>
        /// 查询字段
        /// </summary>
        public string? Filter { get; set; }
    }
}