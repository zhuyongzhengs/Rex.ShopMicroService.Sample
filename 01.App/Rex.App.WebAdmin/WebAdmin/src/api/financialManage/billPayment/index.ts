import request from '/@/utils/request';

export function useBillPaymentApi() {
    return {
        /**
         * 查询支付单(分页)列表信息
         * @param { object } params
         * @returns { any }
         */
        getBillPaymentList: (params?: object): Promise<DataTableType<RowBillPaymentType>> => {
			return request({
				url: '/api/backend/aggregation/billPayment',
				method: 'get',
                params
			});
		},

        /**
         * 查询支付状态
         * @returns { any }
         */
        getBillPaymentStatus: (): Promise<EnumValueType[]> => {
			return request({
				url: '/api/payment/bill-payment/status',
				method: 'get'
			});
		},

        /**
         * 查询单据类型
         * @returns { any }
         */
        getBillPaymentType: (): Promise<EnumValueType[]> => {
			return request({
				url: '/api/payment/bill-payment/types',
				method: 'get'
			});
		},
        /**
		 * 订单支付
		 * @param { object } data 
		 * @returns { any }
		 */
		orderPay: (data: object): Promise<boolean> => {
			return request({
				url: '/api/backend/aggregation/billPayment/orderPay',
				method: 'post',
				data
			});
		},
    };
}