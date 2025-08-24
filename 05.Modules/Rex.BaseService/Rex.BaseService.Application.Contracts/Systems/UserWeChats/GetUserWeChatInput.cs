using Volo.Abp.Application.Dtos;

namespace Rex.BaseService.Systems.UserWeChats
{
    /// <summary>
    /// 查询微信用户
    /// </summary>
    public class GetUserWeChatInput : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// 昵称
        /// </summary>
        public string? NickName { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string? Mobile { get; set; }

        /// <summary>
        /// OpenId
        /// </summary>
        public string? OpenId { get; set; }
    }
}