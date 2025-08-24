namespace Rex.Service.Core.Configurations
{
    /// <summary>
    /// 系统状态码
    /// </summary>
    public static class SystemStatusCode
    {
        /// <summary>
        /// 成功
        /// </summary>
        public const string Success = "00000";

        /// <summary>
        /// 失败
        /// </summary>
        public const string Fail = "00001";

        /// <summary>
        /// 出错
        /// </summary>
        public const string Error = "00002";

        /// <summary>
        /// (后台)网关服务前缀
        /// </summary>
        /// <param name="val">值</param>
        /// <param name="splicer">拼接符</param>
        /// <returns></returns>
        public static string ToWebGatewayServicePrefix(this string val, string splicer = ":")
        {
            if (string.IsNullOrEmpty(val)) return val;
            return $"WebGatewayService{splicer}{val}";
        }

        /// <summary>
        /// (外部)网关服务前缀
        /// </summary>
        /// <param name="val">值</param>
        /// <param name="splicer">拼接符</param>
        /// <returns></returns>
        public static string ToWebPublicGatewayServicePrefix(this string val, string splicer = ":")
        {
            if (string.IsNullOrEmpty(val)) return val;
            return $"WebPublicGatewayService{splicer}{val}";
        }

        /// <summary>
        /// 认证授权服务前缀
        /// </summary>
        /// <param name="val">值</param>
        /// <param name="splicer">拼接符</param>
        /// <returns></returns>
        public static string ToAuthServicePrefix(this string val, string splicer = ":")
        {
            if (string.IsNullOrEmpty(val)) return val;
            return $"AuthService{splicer}{val}";
        }

        /// <summary>
        /// Base服务前缀
        /// </summary>
        /// <param name="val">值</param>
        /// <param name="splicer">拼接符</param>
        /// <returns></returns>
        public static string ToBaseServicePrefix(this string val, string splicer = ":")
        {
            if (string.IsNullOrEmpty(val)) return val;
            return $"BaseService{splicer}{val}";
        }

        /// <summary>
        /// 商品服务前缀
        /// </summary>
        /// <param name="val">值</param>
        /// <param name="splicer">拼接符</param>
        /// <returns></returns>
        public static string ToGoodServicePrefix(this string val, string splicer = ":")
        {
            if (string.IsNullOrEmpty(val)) return val;
            return $"GoodService{splicer}{val}";
        }

        /// <summary>
        /// 促销服务前缀
        /// </summary>
        /// <param name="val">值</param>
        /// <param name="splicer">拼接符</param>
        /// <returns></returns>
        public static string ToPromotionServicePrefix(this string val, string splicer = ":")
        {
            if (string.IsNullOrEmpty(val)) return val;
            return $"PromotionService{splicer}{val}";
        }

        /// <summary>
        /// 订单服务前缀
        /// </summary>
        /// <param name="val">值</param>
        /// <param name="splicer">拼接符</param>
        /// <returns></returns>
        public static string ToOrderServicePrefix(this string val, string splicer = ":")
        {
            if (string.IsNullOrEmpty(val)) return val;
            return $"OrderService{splicer}{val}";
        }

        /// <summary>
        /// 支付服务前缀
        /// </summary>
        /// <param name="val">值</param>
        /// <param name="splicer">拼接符</param>
        /// <returns></returns>
        public static string ToPaymentServicePrefix(this string val, string splicer = ":")
        {
            if (string.IsNullOrEmpty(val)) return val;
            return $"PaymentService{splicer}{val}";
        }

        /// <summary>
        /// 后台聚合服务前缀
        /// </summary>
        /// <param name="val">值</param>
        /// <param name="splicer">拼接符</param>
        /// <returns></returns>
        public static string ToBackendAggregationServicePrefix(this string val, string splicer = ":")
        {
            if (string.IsNullOrEmpty(val)) return val;
            return $"BackendAggregationService{splicer}{val}";
        }

        /// <summary>
        /// 前台聚合服务前缀
        /// </summary>
        /// <param name="val">值</param>
        /// <param name="splicer">拼接符</param>
        /// <returns></returns>
        public static string ToFrontAggregationServicePrefix(this string val, string splicer = ":")
        {
            if (string.IsNullOrEmpty(val)) return val;
            return $"FrontAggregationService{splicer}{val}";
        }
    }
}