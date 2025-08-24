using Rex.Service.Core.Configurations;
using System;
using Yitter.IdGenerator;

namespace Rex.Service.Core.Helper
{
    /// <summary>
    /// 通用帮助类
    /// </summary>
    public static class CommonHelper
    {
        /// <summary>
        /// 获取随机编码
        /// </summary>
        /// <param name="prefix">前缀</param>
        /// <returns></returns>
        public static string GetRandomCode(string prefix = "")
        {
            return prefix + YitIdHelper.NextId();
        }

        /// <summary>
        /// 返回当前的毫秒时间戳
        /// </summary>
        public static string Msectime()
        {
            long timeTicks = (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000;
            return timeTicks.ToString();
        }

        /// <summary>
        /// 获取多种数据编号
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public static string GetSerialNumberType(int type)
        {
            string str;
            Random rand = new Random();
            switch (type)
            {
                case (int)GlobalEnums.SerialNumberType.OrderCode:         //订单编号
                    str = type + Msectime() + rand.Next(0, 9);
                    break;

                case (int)GlobalEnums.SerialNumberType.PlayCode:         //支付单编号
                    str = type + Msectime() + rand.Next(0, 9);
                    break;

                case (int)GlobalEnums.SerialNumberType.GoodCode:         //商品编号
                    str = 'G' + Msectime() + rand.Next(0, 5);
                    break;

                case (int)GlobalEnums.SerialNumberType.ProductCode:         //货品编号
                    str = 'P' + Msectime() + rand.Next(0, 5);
                    break;

                case (int)GlobalEnums.SerialNumberType.AfterSaleCode:         //售后单编号
                    str = type + Msectime() + rand.Next(0, 9);
                    break;

                case (int)GlobalEnums.SerialNumberType.RefundCode:         //退款单编号
                    str = type + Msectime() + rand.Next(0, 9);
                    break;

                case (int)GlobalEnums.SerialNumberType.ReturnCode:         //退货单编号
                    str = type + Msectime() + rand.Next(0, 9);
                    break;

                case (int)GlobalEnums.SerialNumberType.DeliveryCode:         //发货单编号
                    str = type + Msectime() + rand.Next(0, 9);
                    break;

                case (int)GlobalEnums.SerialNumberType.ServiceOrderCode:         //服务订单编号
                    str = type + Msectime() + rand.Next(0, 9);
                    break;

                case (int)GlobalEnums.SerialNumberType.PickProductCode:         //提货单号
                    var charsStr = new[] { 'Q', 'W', 'E', 'R', 'T', 'Y', 'U', 'P', 'A', 'S', 'D', 'F', 'G', 'H', 'J', 'K', 'L', 'Z', 'X', 'C', 'V', 'B', 'N', 'M', '2', '3', '4', '5', '6', '7', '8', '9' };
                    var charsLen = charsStr.Length - 1;
                    str = "";
                    for (int i = 0; i < 6; i++)
                    {
                        str += charsStr[rand.Next(0, charsLen)];
                    }
                    break;

                case (int)GlobalEnums.SerialNumberType.ServiceExchangeCode:         //服务券兑换码
                    var charsStr2 = new[] { 'Q', 'W', 'E', 'R', 'T', 'Y', 'U', 'P', 'A', 'S', 'D', 'F', 'G', 'H', 'J', 'K', 'L', 'Z', 'X', 'C', 'V', 'B', 'N', 'M', '2', '3', '4', '5', '6', '7', '8', '9' };
                    var charsLen2 = charsStr2.Length - 1;
                    str = "";
                    for (int i = 0; i < 6; i++)
                    {
                        str += charsStr2[rand.Next(0, charsLen2)];
                    }
                    break;

                default:
                    str = 'T' + Msectime() + rand.Next(0, 9);
                    break;
            }
            return str;
        }

        /// <summary>
        /// 获取金额来源备注
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="money">金额</param>
        /// <param name="cateMoney">手续费</param>
        /// <returns></returns>
        public static string GetUserBalanceTypeMemo(int type, decimal money, decimal? cateMoney = 0)
        {
            decimal result = Math.Truncate(money * 100) / 100;
            string moneyVal = result.ToString("F2");
            string str = type switch
            {
                (int)GlobalEnums.UserBalanceType.Pay => $"消费了{moneyVal}元",
                (int)GlobalEnums.UserBalanceType.Refund => $"收到了退款{moneyVal}元",
                (int)GlobalEnums.UserBalanceType.Recharge => $"充值了{moneyVal}元",
                (int)GlobalEnums.UserBalanceType.Tocash => $"提现了{moneyVal}元" +
                    (cateMoney.HasValue && cateMoney > 0 ? $",手续费{cateMoney:F2}元" : string.Empty),
                (int)GlobalEnums.UserBalanceType.Distribution => $"佣金{moneyVal}元",
                (int)GlobalEnums.UserBalanceType.Agent => $"佣金{moneyVal}元",
                (int)GlobalEnums.UserBalanceType.Admin => $"后台操作{moneyVal}元",
                (int)GlobalEnums.UserBalanceType.Prize => $"抽奖活动奖励{moneyVal}元",
                (int)GlobalEnums.UserBalanceType.Service => $"购买服务消费了{moneyVal}元",
                _ => string.Empty
            };
            return str;
        }
    }
}