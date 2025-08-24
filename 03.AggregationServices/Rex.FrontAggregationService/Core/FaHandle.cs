using Rex.OrderService.Ships;
using System.Text.Json;
using static Rex.Service.Core.Configurations.GlobalEnums;

namespace Rex.FrontAggregationService.Core
{
    /// <summary>
    /// 处理类
    /// </summary>
    public static class FaHandle
    {
        /// <summary>
        /// 获取配送费用
        /// </summary>
        /// <param name="shipDto">配送方式</param>
        /// <param name="weight">重量</param>
        /// <param name="goodAmount">商品总价</param>
        /// <param name="areaId">区域ID</param>
        /// <returns></returns>
        public static decimal GetShipCost(ShipDto shipDto, decimal weight, decimal goodAmount, long areaId)
        {
            if (shipDto == null || shipDto.IsfreePostage) return 0;
            bool isArea = false;
            decimal postFee = 0;
            if (shipDto != null && shipDto.AreaType == (int)ShipAreaType.Part)
            {
                if (shipDto.AreaFees != null && shipDto.AreaFees.Count > 0)
                {
                    foreach (var areaFee in shipDto.AreaFees)
                    {
                        if (string.IsNullOrWhiteSpace(areaFee.Areas)) continue;
                        long[][] areaArr = JsonSerializer.Deserialize<long[][]>(areaFee.Areas);
                        long[] areas = areaArr.SelectMany(row => row).ToArray();
                        if (areas.Contains(areaId))
                        {
                            isArea = true;
                            decimal calcTotal = CalcFee(shipDto, weight, goodAmount, areaFee.AreaFirstunitPrice);
                            postFee = Math.Round(calcTotal, 2);
                            break;
                        }
                    }
                }
            }
            if (!isArea)
            {
                decimal calcTotal = CalcFee(shipDto, weight, goodAmount);
                postFee = Math.Round(calcTotal, 2);
            }
            return postFee;
        }

        /// <summary>
        /// 计算运费
        /// </summary>
        /// <param name="ship">配送方式</param>
        /// <param name="weight">订单重量</param>
        /// <param name="totalMoney">商品总价</param>
        /// <param name="areaFirstunitPrice">[区域]首重费用</param>
        /// <returns></returns>
        public static decimal CalcFee(ShipDto ship, decimal weight, decimal totalMoney = 0, decimal areaFirstunitPrice = 0)
        {
            // 满多少免运费
            if (ship.GoodsMoney > 0 && totalMoney >= ship.GoodsMoney) return 0;
            // 重量大于首重则进行判断
            if (weight > 0 && weight > ship.FirstUnit)
            {
                return ship.FirstunitPrice + (Math.Ceiling(Math.Abs(weight - ship.FirstUnit) / ship.ContinueUnit) * ship.ContinueunitPrice);
            }
            if (ship.FirstunitPrice > 0) return ship.FirstunitPrice;
            return areaFirstunitPrice;
        }
    }
}