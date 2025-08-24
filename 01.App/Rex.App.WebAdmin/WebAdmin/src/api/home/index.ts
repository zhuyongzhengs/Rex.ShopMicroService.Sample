import request from '/@/utils/request';

export function useHomeApi() {
  return {
      /**
       * 获取待办事项数量
       * @returns { WaitItemCountType }
       */
      getWaitItemCount: (): Promise<WaitItemCountType> => {
        return request({
            url: '/api/backend/aggregation/common/waitItemCount',
            method: 'get'
        });
      },
      /**
       * 获取本周订单销售总额
       * @returns { any }
       */
      getWeekOrderAmountStatistics: (): Promise<any> => {
        return request({
            url: '/api/order/common/week-order-amount-statistics',
            method: 'get'
        });
      }
  }
}