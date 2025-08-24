using System;
using System.ComponentModel;
using System.Reflection;

namespace Rex.Service.Core.Configurations
{
    /// <summary>
    /// 系统常用枚举类
    /// </summary>
    public static class GlobalEnums
    {
        /// <summary>
        /// 获取枚举对象
        /// </summary>
        /// <param name="value">枚举值</param>
        /// <returns></returns>
        public static T GetEnum<T>(this int value)
        {
            T enumVal = (T)Enum.ToObject(typeof(T), value);
            return enumVal;
        }

        /// <summary>
        /// 获取枚举描述
        /// </summary>
        /// <param name="value">枚举</param>
        /// <returns></returns>
        public static string GetDescription<T>(this int value)
        {
            T tEnum = GetEnum<T>(value);
            FieldInfo field = tEnum.GetType().GetField(tEnum.ToString());
            if (field != null)
            {
                if (Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
                {
                    return attribute.Description;
                }
            }
            return value.ToString();
        }

        /// <summary>
        /// 获取枚举描述
        /// </summary>
        /// <param name="value">枚举</param>
        /// <returns></returns>
        public static string GetDescription(this Enum value)
        {
            FieldInfo field = value.GetType().GetField(value.ToString());
            if (field != null)
            {
                if (Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
                {
                    return attribute.Description;
                }
            }
            return value.ToString();
        }

        #region 商品相关

        /// <summary>
        /// 商品参数表类型
        /// </summary>
        public enum GoodParamTypes
        {
            /// <summary>
            /// 文本框
            /// </summary>
            [Description("文本框")]
            Text = 1,

            /// <summary>
            /// 单选
            /// </summary>
            [Description("单选")]
            Radio = 2,

            /// <summary>
            /// 复选框
            /// </summary>
            [Description("复选框")]
            Checkbox = 3
        }

        #endregion 商品相关

        #region 价格相关

        /// <summary>
        /// 价格类型
        /// </summary>
        public enum PriceType
        {
            /// <summary>
            /// 销售价
            /// </summary>
            [Description("销售价")]
            Price = 1,

            /// <summary>
            /// 市场价
            /// </summary>
            [Description("市场价")]
            MktPrice = 2,

            /// <summary>
            /// 成本价
            /// </summary>
            [Description("成本价")]
            CostPrice = 3
        }

        #endregion 价格相关

        #region 服务商品相关

        /// <summary>
        /// 服务项目核销有效期类型
        /// </summary>
        public enum ServicesValidityType
        {
            /// <summary>
            /// 不限，就是不限制，永久可以使用。
            /// </summary>
            [Description("不限")]
            Unlimited = 1,

            /// <summary>
            /// 限时间段  ，就是买了之后，某个时间段才能用。
            /// </summary>
            [Description("限时间段")]
            TimeFrame = 2
        }

        /// <summary>
        /// 服务项目状态
        /// </summary>
        public enum ServicesStatu
        {
            /// <summary>
            /// 上架
            /// </summary>
            [Description("上架")]
            Shelve = 0,

            /// <summary>
            /// 下架
            /// </summary>
            [Description("下架")]
            UnShelve = 1,

            /// <summary>
            /// 售罄
            /// </summary>
            [Description("售罄")]
            SoldOut = 2
        }

        /// <summary>
        /// 服务是否在时间范围内的状态
        /// </summary>
        public enum ServicesOpenStatus
        {
            /// <summary>
            /// 已开始
            /// </summary>
            [Description("已开始")]
            Begin = 1,

            /// <summary>
            /// 未开始
            /// </summary>
            [Description("未开始")]
            NotBegun = 2,

            /// <summary>
            /// 已过期
            /// </summary>
            [Description("已过期")]
            HaveExpired = 3
        }

        #endregion 服务商品相关

        #region 购物车类型

        public enum CartType
        {
            /// <summary>
            /// 普通(商品)购物车
            /// </summary>
            [Description("普通")]
            Common = 1,

            /// <summary>
            /// 秒杀(商品)购物车
            /// </summary>
            [Description("秒杀")]
            Skill = 2
        }

        #endregion 购物车类型

        #region 拼团

        /// <summary>
        /// 拼团记录状态表
        /// </summary>
        public enum PinTuanRecordStatus
        {
            /// <summary>
            /// 拼团中
            /// </summary>
            [Description("拼团中")]
            InProgress = 1,

            /// <summary>
            /// 开团成功
            /// </summary>
            [Description("开团成功")]
            Succeed = 2,

            /// <summary>
            /// 开团失败
            /// </summary>
            [Description("开团失败")]
            Defeated = 3
        }

        /// <summary>
        /// 拼团规则是否在时间范围内的状态
        /// </summary>
        public enum PinTuanRuleStatus
        {
            /// <summary>
            /// 已开始
            /// </summary>
            [Description("已开始")]
            Begin = 1,

            /// <summary>
            /// 未开始
            /// </summary>
            [Description("未开始")]
            NotBegun = 2,

            /// <summary>
            /// 已过期
            /// </summary>
            [Description("已过期")]
            HaveExpired = 3
        }

        #endregion 拼团

        #region 促销类型

        public enum PromotionType
        {
            /// <summary>
            /// 促销
            /// </summary>
            [Description("促销")]
            Promotion = 1,

            /// <summary>
            /// 优惠券
            /// </summary>
            [Description("优惠券")]
            Coupon = 2,

            /// <summary>
            /// 团购
            /// </summary>
            [Description("团购")]
            Group = 3,

            /// <summary>
            /// 秒杀
            /// </summary>
            [Description("秒杀")]
            Seckill = 4
        }

        #endregion 促销类型

        #region Form表单相关

        /// <summary>
        /// 自定义表单类型
        /// </summary>
        public enum FormTypes
        {
            /// <summary>
            /// 订单
            /// </summary>
            [Description("订单")]
            Order = 1,

            /// <summary>
            /// 付款码
            /// </summary>
            [Description("付款码")]
            PaymentCode = 2,

            /// <summary>
            /// 留言
            /// </summary>
            [Description("留言")]
            Comment = 3,

            /// <summary>
            /// 反馈
            /// </summary>
            [Description("反馈")]
            FeedBack = 4,

            /// <summary>
            /// 登记
            /// </summary>
            [Description("登记")]
            Registration = 5,

            /// <summary>
            /// 调研
            /// </summary>
            [Description("调研")]
            Survey = 6
        }

        /// <summary>
        /// 表单头部类型
        /// </summary>
        public enum FormHeadTypes
        {
            /// <summary>
            /// 图片
            /// </summary>
            [Description("图片")]
            Image = 1,

            /// <summary>
            /// 轮播
            /// </summary>
            [Description("轮播")]
            Swiper = 2,

            /// <summary>
            /// 视频
            /// </summary>
            [Description("视频")]
            Video = 3
        }

        /// <summary>
        /// 表单字段类型
        /// </summary>
        public enum FormFieldTypes
        {
            /// <summary>
            /// 单选
            /// </summary>
            [Description("单选")]
            Radio = 1,

            /// <summary>
            /// 复选
            /// </summary>
            [Description("复选")]
            Checbox = 2,

            /// <summary>
            /// 文本框
            /// </summary>
            [Description("文本框")]
            Text = 3,

            /// <summary>
            /// 文本域
            /// </summary>
            [Description("文本域")]
            Textarea = 4,

            /// <summary>
            /// 日期
            /// </summary>
            [Description("日期")]
            Date = 5,

            /// <summary>
            /// 时间
            /// </summary>
            [Description("时间")]
            Time = 6,

            /// <summary>
            /// 商品
            /// </summary>
            [Description("商品")]
            Goods = 7,

            /// <summary>
            /// 金额
            /// </summary>
            [Description("金额")]
            Money = 8,

            /// <summary>
            /// 密码
            /// </summary>
            [Description("密码")]
            Password = 9,

            /// <summary>
            /// 省市区
            /// </summary>
            [Description("省市区")]
            Area = 10,

            /// <summary>
            /// 图片
            /// </summary>
            [Description("图片")]
            Image = 11,

            /// <summary>
            /// 坐标
            /// </summary>
            [Description("坐标")]
            Coordinate = 12
        }

        /// <summary>
        /// 表单验证类型
        /// </summary>
        public enum FormValidationTypes
        {
            /// <summary>
            /// 字符串
            /// </summary>
            [Description("String")]
            String = 1,

            [Description("Number")]
            Number = 2,

            [Description("Integer")]
            Integer = 3,

            [Description("Price")]
            Price = 4,

            [Description("Email")]
            Email = 5,

            [Description("Mobile")]
            Mobile = 6,

            [Description("Array")]
            Array = 7
        }

        #endregion Form表单相关

        #region 商城关键词说明

        /// <summary>
        /// 商城服务描述类型
        /// </summary>
        public enum ShopServiceNoteType
        {
            /// <summary>
            /// 常见问题
            /// </summary>
            [Description("常见问题")]
            CommonQuestion = 1,

            /// <summary>
            /// 服务
            /// </summary>
            [Description("服务")]
            Service = 2,

            /// <summary>
            /// 发货
            /// </summary>
            [Description("发货")]
            Delivery = 3,
        }

        #endregion 商城关键词说明

        #region 账号来源类型

        /// <summary>
        /// 账号来源类型
        /// </summary>
        public enum UserAccountTypes
        {
            [Description("微信公众号")]
            OfficialAccount = 1,

            [Description("微信小程序")]
            MiniProgram = 2
        }

        #endregion 账号来源类型

        #region 订单类型|购物车类型

        /// <summary>
        /// 订单类型|购物车类型
        /// </summary>
        public enum OrderType
        {
            /// <summary>
            /// 普通
            /// </summary>
            [Description("普通")]
            Common = 1,

            /// <summary>
            /// 拼团
            /// </summary>
            [Description("拼团")]
            PinTuan = 2,

            /// <summary>
            /// 团购
            /// </summary>
            [Description("团购")]
            Group = 3,

            /// <summary>
            /// 秒杀
            /// </summary>
            [Description("秒杀")]
            Skill = 4,

            /// <summary>
            /// 砍价
            /// </summary>
            [Description("砍价")]
            Bargain = 6,

            /// <summary>
            /// 赠品
            /// </summary>
            [Description("赠品")]
            Giveaway = 7,

            /// <summary>
            /// 接龙
            /// </summary>
            [Description("接龙")]
            Solitaire = 8,

            /// <summary>
            /// 微信交易组件
            /// </summary>
            [Description("微信交易组件")]
            TransactionComponent = 10,
        }

        #endregion 订单类型|购物车类型

        #region 订单编号类型

        public enum SerialNumberType
        {
            [Description("订单编号")]
            OrderCode = 1,

            [Description("支付单编号")]
            PlayCode = 2,

            [Description("商品编号")]
            GoodCode = 3,

            [Description("货品编号")]
            ProductCode = 4,

            [Description("售后单编号")]
            AfterSaleCode = 5,

            [Description("退款单编号")]
            RefundCode = 6,

            [Description("退货单编号")]
            ReturnCode = 7,

            [Description("发货单编号")]
            DeliveryCode = 8,

            [Description("提货单号")]
            PickProductCode = 9,

            [Description("服务订单编号")]
            ServiceOrderCode = 10,

            [Description("服务券兑换码")]
            ServiceExchangeCode = 11
        }

        #endregion 订单编号类型

        #region 来源

        public enum Source
        {
            [Description("PC页面")]
            PC = 1,

            [Description("H5页面")]
            H5 = 2,

            [Description("微信小程序")]
            MpWeixin = 3,

            [Description("支付宝小程序")]
            MpAlipay = 4,

            [Description("APP")]
            App = 5
        }

        #endregion 来源

        #region 配送区域类型

        public enum ShipAreaType
        {
            /// <summary>
            /// 全部地区
            /// </summary>
            [Description("全部地区")]
            All = 1,

            /// <summary>
            /// 部分地区
            /// </summary>
            [Description("部分地区")]
            Part = 2
        }

        #endregion 配送区域类型

        #region 配送状态启用还是停用

        public enum ShipStatus
        {
            /// <summary>
            /// 启用
            /// </summary>
            [Description("启用")]
            Yes = 1,

            /// <summary>
            /// 停用
            /// </summary>
            [Description("停用")]
            No = 2
        }

        #endregion 配送状态启用还是停用

        #region 配送方式重量

        public enum ShipUnit
        {
            /// <summary>
            /// 500克
            /// </summary>
            [Description("500克")]
            K500 = 500,

            /// <summary>
            /// 1公斤
            /// </summary>
            [Description("1公斤")]
            K1000 = 1000,

            /// <summary>
            /// 1.2公斤
            /// </summary>
            [Description("1.2公斤")]
            K1200 = 1200,

            /// <summary>
            /// 2公斤
            /// </summary>
            [Description("2公斤")]
            K2000 = 2000,

            /// <summary>
            /// 5公斤
            /// </summary>
            [Description("5公斤")]
            K5000 = 5000,

            /// <summary>
            /// 10公斤
            /// </summary>
            [Description("10公斤")]
            K10000 = 10000,

            /// <summary>
            /// 20公斤
            /// </summary>
            [Description("20公斤")]
            K20000 = 20000,

            /// <summary>
            /// 50公斤
            /// </summary>
            [Description("50公斤")]
            K50000 = 50000
        }

        #endregion 配送方式重量

        #region 订单收货方式

        public enum OrderReceiptType
        {
            /// <summary>
            /// 物流快递
            /// </summary>
            [Description("物流快递")]
            Logistics = 1,

            /// <summary>
            /// 同城配送
            /// </summary>
            [Description("同城配送")]
            IntraCityService = 2,

            /// <summary>
            /// 门店自提
            /// </summary>
            [Description("门店自提")]
            SelfDelivery = 3
        }

        #endregion 订单收货方式

        #region 订单状态

        public enum OrderStatus
        {
            /// <summary>
            /// 订单正常
            /// </summary>
            [Description("订单正常")]
            Normal = 1,

            /// <summary>
            /// 订单完成
            /// </summary>
            [Description("订单完成")]
            Complete = 2,

            /// <summary>
            /// 订单取消
            /// </summary>
            [Description("订单取消")]
            Cancel = 3
        }

        #endregion 订单状态

        #region 发货状态

        public enum OrderShipStatus
        {
            /// <summary>
            /// 未发货
            /// </summary>
            [Description("未发货")]
            No = 1,

            /// <summary>
            /// 部分发货
            /// </summary>
            [Description("部分发货")]
            PartialYes = 2,

            /// <summary>
            /// 已发货
            /// </summary>
            [Description("已发货")]
            Yes = 3,

            /// <summary>
            /// 部分退货
            /// </summary>
            [Description("部分退货")]
            PartialNo = 4,

            /// <summary>
            /// 已退货
            /// </summary>
            [Description("已退货")]
            Returned = 5
        }

        #endregion 发货状态

        #region 支付状态

        public enum OrderPayStatus
        {
            /// <summary>
            /// 未付款
            /// </summary>
            [Description("未付款")]
            No = 1,

            /// <summary>
            /// 已付款
            /// </summary>
            [Description("已付款")]
            Yes = 2,

            /// <summary>
            /// 部分付款
            /// </summary>
            [Description("部分付款")]
            PartialYes = 3,

            /// <summary>
            /// 部分退款
            /// </summary>
            [Description("部分退款")]
            PartialNo = 4,

            /// <summary>
            /// 已退款
            /// </summary>
            [Description("已退款")]
            Refunded = 5
        }

        #endregion 支付状态

        #region 付款单状态

        public enum BillPaymentStatus
        {
            /// <summary>
            /// 待支付
            /// </summary>
            [Description("未支付")]
            NoPay = 1,

            /// <summary>
            /// 已支付
            /// </summary>
            [Description("已支付")]
            Payed = 2,

            /// <summary>
            /// 其他
            /// </summary>
            [Description("其他")]
            Other = 3
        }

        #endregion 付款单状态

        #region 退款单状态

        public enum BillRefundStatus
        {
            /// <summary>
            /// 未退款
            /// </summary>
            [Description("未退款")]
            Norefund = 1,

            /// <summary>
            /// 已退款
            /// </summary>
            [Description("已退款")]
            Refund = 2,

            /// <summary>
            /// 同意退款但原路退还失败
            /// </summary>
            [Description("退款失败")]
            Fail = 3,

            /// <summary>
            /// 拒绝退款
            /// </summary>
            [Description("拒绝退款")]
            Refuse = 4
        }

        #endregion 退款单状态

        #region 退货单状态

        public enum BillReshipStatus
        {
            /// <summary>
            /// 待退货
            /// </summary>
            /// <remarks>
            /// 审核通过待发货
            /// </remarks>
            [Description("待退货")]
            PendingReturn = 1,

            /// <summary>
            /// 运输中
            /// </summary>
            /// <remarks>
            /// 已发退货
            /// </remarks>
            [Description("运输中")]
            InTransit = 2,

            /// <summary>
            /// 已收退货
            /// </summary>
            [Description("已收退货")]
            ReceivedReturn = 5
        }

        #endregion 退货单状态

        #region 订单收货状态

        public enum OrderConfirmStatus
        {
            /// <summary>
            /// 未确认收货
            /// </summary>
            [Description("未确认收货")]
            ReceiptNotConfirmed = 1,

            /// <summary>
            /// 已确认收货
            /// </summary>
            [Description("已确认收货")]
            ConfirmReceipt = 2
        }

        #endregion 订单收货状态

        #region 库存变更类型

        public enum OrderChangeStockType
        {
            /// <summary>
            /// 下单
            /// </summary>
            [Description("下单")]
            Order = 1,

            /// <summary>
            /// 发货
            /// </summary>
            [Description("发货")]
            Send = 2,

            /// <summary>
            /// 退款
            /// </summary>
            [Description("退款")]
            Refund = 3,

            /// <summary>
            /// 退货
            /// </summary>
            [Description("退货")]
            Return = 4,

            /// <summary>
            /// 取消订单
            /// </summary>
            [Description("取消订单")]
            Cancel = 5,

            /// <summary>
            /// 完成订单
            /// </summary>
            [Description("完成订单")]
            Complete = 6,
        }

        #endregion 库存变更类型

        #region 订单日志状态

        public enum OrderLogType
        {
            /// <summary>
            /// 订单创建
            /// </summary>
            [Description("订单创建")]
            LogTypeCreate = 1,

            /// <summary>
            /// 订单支付
            /// </summary>
            [Description("订单支付")]
            LogTypePay = 2,

            /// <summary>
            /// 订单发货
            /// </summary>
            [Description("订单发货")]
            LogTypeShip = 3,

            /// <summary>
            /// 订单签收
            /// </summary>
            [Description("订单签收")]
            LogTypeSign = 4,

            /// <summary>
            /// 订单评价
            /// </summary>
            [Description("订单评价")]
            LogTypeEvaluation = 5,

            /// <summary>
            /// 订单完成
            /// </summary>
            [Description("订单完成")]
            LogTypeComplete = 6,

            /// <summary>
            /// 订单取消
            /// </summary>
            [Description("订单取消")]
            LogTypeCancel = 7,

            /// <summary>
            /// 订单编辑
            /// </summary>
            [Description("订单编辑")]
            LogTypeEdit = 8,

            /// <summary>
            /// 订单自动签收
            /// </summary>
            [Description("订单自动签收")]
            LogTypeAutoSign = 9,

            /// <summary>
            /// 订单自动评价
            /// </summary>
            [Description("订单自动评价")]
            LogTypeAutoEvaluation = 10,

            /// <summary>
            /// 订单自动完成
            /// </summary>
            [Description("订单自动完成")]
            LogTypeAutoComplete = 11,

            /// <summary>
            /// 订单自动取消
            /// </summary>
            [Description("订单自动取消")]
            LogTypeAutoCancel = 12
        }

        #endregion 订单日志状态

        #region 支付方式

        public enum PaymentType
        {
            /// <summary>
            /// 微信支付
            /// </summary>
            [Description("微信支付")]
            WechatPay = 1,

            /// <summary>
            /// 支付宝支付
            /// </summary>
            [Description("支付宝支付")]
            AliPay = 2,

            /// <summary>
            /// 线下支付
            /// </summary>
            [Description("线下支付")]
            Offline = 3,

            /// <summary>
            /// 余额支付
            /// </summary>
            [Description("余额支付")]
            BalancePay = 4
        }

        #endregion 支付方式

        #region 付款单类型

        public enum BillPaymentType
        {
            /// <summary>
            /// 订单
            /// </summary>
            [Description("订单")]
            Order = 1,

            /// <summary>
            /// 充值
            /// </summary>
            [Description("充值")]
            Recharge = 2,

            /// <summary>
            /// 表单订单
            /// </summary>
            [Description("表单订单")]
            FormOrder = 3,

            /// <summary>
            /// 表单付款码
            /// </summary>
            [Description("表单付款码")]
            FormPay = 4,

            /// <summary>
            /// 服务订单
            /// </summary>
            [Description("服务订单")]
            ServiceOrder = 5
        }

        #endregion 付款单类型

        #region 退款单类型

        public enum BillRefundType
        {
            /// <summary>
            /// 订单
            /// </summary>
            [Description("订单")]
            Order = 1,

            /// <summary>
            /// 充值
            /// </summary>
            [Description("充值")]
            Recharge = 2,

            /// <summary>
            /// 表单订单
            /// </summary>
            [Description("表单订单")]
            FormOrder = 3,

            /// <summary>
            /// 表单付款码
            /// </summary>
            [Description("表单付款码")]
            FormPay = 4,

            /// <summary>
            /// 服务订单
            /// </summary>
            [Description("服务订单")]
            ServiceOrder = 5,
        }

        #endregion 退款单类型

        #region 售后单类型

        public enum BillAftersalesType
        {
            [Description("售后中")]
            WaitAftersales = 1,

            [Description("售后通过")]
            Success = 2,

            [Description("售后拒绝")]
            Refuse = 3
        }

        #endregion 售后单类型

        #region 售后单状态

        public enum BillAftersalesStatus
        {
            /// <summary>
            /// 等待审核
            /// </summary>
            [Description("等待审核")]
            WaitAudit = 1,

            /// <summary>
            /// 审核通过
            /// </summary>
            [Description("审核通过")]
            Success = 2,

            /// <summary>
            /// 审核拒绝
            /// </summary>
            [Description("审核拒绝")]
            Refuse = 3
        }

        #endregion 售后单状态

        #region 全局订单类型

        public enum GlobalOrderStatusType
        {
            /// <summary>
            /// 待付款
            /// </summary>
            [Description("待付款")]
            PendingPayment = 1,

            /// <summary>
            /// 待发货
            /// </summary>
            [Description("待发货")]
            PendingShipment = 2,

            /// <summary>
            /// 待收货
            /// </summary>
            [Description("待收货")]
            PendingDelivery = 3,

            /// <summary>
            /// 待评价
            /// </summary>
            [Description("待评价")]
            PendingEvaluate = 4,

            /// <summary>
            /// 已评价
            /// </summary>
            [Description("已评价")]
            CompletedEvaluate = 5,

            /// <summary>
            /// 已完成
            /// </summary>
            [Description("已完成")]
            Completed = 6,

            /// <summary>
            /// 已取消
            /// </summary>
            [Description("已取消")]
            Cancel = 7,

            /// <summary>
            /// 部分发货
            /// </summary>
            [Description("部分发货")]
            PartialShipment = 8
        }

        #endregion 全局订单类型

        #region 发货单状态

        public enum BillDeliveryStatus
        {
            /// <summary>
            /// 准备发货
            /// </summary>
            [Description("准备发货")]
            Ready = 1,

            /// <summary>
            /// 已发货
            /// </summary>
            [Description("已发货")]
            Already = 2,

            /// <summary>
            /// 已确认
            /// </summary>
            [Description("已确认")]
            Confirm = 3,

            /// <summary>
            /// 其它
            /// </summary>
            [Description("其它")]
            Other = 4
        }

        #endregion 发货单状态

        #region 是否收货

        public enum BillAftersalesIsReceive
        {
            /// <summary>
            /// 未收到货
            /// </summary>
            [Description("未收到货")]
            Refund = 1,

            /// <summary>
            /// 已收到货
            /// </summary>
            [Description("已收到货")]
            Reship = 2
        }

        #endregion 是否收货

        #region 付款单状态

        public enum BillPaymentsStatus
        {
            /// <summary>
            /// 待支付
            /// </summary>
            [Description("未支付")]
            NoPay = 1,

            /// <summary>
            /// 已支付
            /// </summary>
            [Description("已支付")]
            Payed = 2,

            /// <summary>
            /// 其他
            /// </summary>
            [Description("其他")]
            Other = 3
        }

        #endregion 付款单状态

        #region 库存

        public enum StockType
        {
            /// <summary>
            /// 入库
            /// </summary>
            [Description("入库")]
            In = 1,

            /// <summary>
            /// 出库
            /// </summary>
            [Description("出库")]
            Out = 2,

            /// <summary>
            /// 库存盘点
            /// </summary>
            [Description("库存盘点")]
            CheckGoods = 3,

            /// <summary>
            /// 发货
            /// </summary>
            [Description("发货")]
            DeliverGoods = 4,

            /// <summary>
            /// 退货
            /// </summary>
            [Description("退货")]
            ReturnedGoods = 5
        }

        #endregion 库存

        #region 用户余额类型

        public enum UserBalanceType
        {
            /// <summary>
            /// 用户消费
            /// </summary>
            [Description("用户消费")]
            Pay = 1,

            /// <summary>
            /// 用户退款
            /// </summary>
            [Description("用户退款")]
            Refund = 2,

            /// <summary>
            /// 充值
            /// </summary>
            [Description("充值")]
            Recharge = 3,

            /// <summary>
            /// 提现
            /// </summary>
            [Description("提现")]
            Tocash = 4,

            /// <summary>
            /// 三级分销佣金
            /// </summary>
            [Description("三级分销佣金")]
            Distribution = 5,

            /// <summary>
            /// 平台调整
            /// </summary>
            [Description("平台调整")]
            Admin = 6,

            /// <summary>
            /// 奖励
            /// </summary>
            [Description("奖励")]
            Prize = 7,

            /// <summary>
            /// 服务项目
            /// </summary>
            [Description("服务订单")]
            Service = 8,

            /// <summary>
            /// 代理商提成
            /// </summary>
            [Description("代理商提成")]
            Agent = 9
        }

        #endregion 用户余额类型

        #region 用户积分变动来源类型

        /// <summary>
        /// UserPointLog表type字段
        /// </summary>
        public enum UserPointSourceTypes
        {
            /// <summary>
            /// 签到
            /// </summary>
            [Description("签到")]
            PointTypeSign = 1,

            /// <summary>
            /// 购物返积分
            /// </summary>
            [Description("购物返积分")]
            PointTypeRebate = 2,

            /// <summary>
            /// 购物使用积分
            /// </summary>
            [Description("购物使用积分")]
            PointTypeDiscount = 3,

            /// <summary>
            /// 后台编辑
            /// </summary>
            [Description("后台编辑")]
            PointTypeAdminEdit = 4,

            /// <summary>
            /// 奖励积分
            /// </summary>
            [Description("奖励积分")]
            PointTypePrize = 5,

            /// <summary>
            /// 积分兑换
            /// </summary>
            [Description("积分兑换")]
            PointTypeExchange = 6,

            /// <summary>
            /// 售后退款返还
            /// </summary>
            [Description("售后退款返还")]
            PointRefundReturn = 7,

            /// <summary>
            /// 取消订单返还
            /// </summary>
            [Description("取消订单返还")]
            PointCanCelOrder = 8
        }

        #endregion 用户积分变动来源类型

        #region 后台调度状态

        public enum BackgroundWorkerStatus
        {
            /// <summary>
            /// 待执行
            /// </summary>
            [Description("待执行")]
            Wait = 0,

            /// <summary>
            /// 执行中
            /// </summary>
            [Description("执行中")]
            Executing = 1
        }

        #endregion 后台调度状态
    }
}