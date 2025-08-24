namespace Rex.GoodService.StoreClerks
{
    /// <summary>
    /// 查询店铺店员关联
    /// </summary>
    public partial class GetStoreClerkInput
    {
        /// <summary>
        /// 用户昵称
        /// </summary>
        public string? UserName { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string? PhoneNumber { get; set; }
    }
}