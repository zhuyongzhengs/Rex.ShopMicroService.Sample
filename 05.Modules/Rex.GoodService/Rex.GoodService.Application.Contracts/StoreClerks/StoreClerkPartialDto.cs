namespace Rex.GoodService.StoreClerks
{
    /// <summary>
    /// 店铺店员关联Dto
    /// </summary>
    public partial class StoreClerkDto
    {
        /// <summary>
        /// 头像
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 店员昵称
        /// </summary>
        public string UserName { get; set; }
    }
}