using Volo.Abp.Application.Dtos;

namespace Rex.GoodService.Forms
{
    /// <summary>
    /// 查询表单
    /// </summary>
    public class GetFormInput : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// 表单名称
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// 表单类型
        /// </summary>
        public int? Type { get; set; }
    }
}