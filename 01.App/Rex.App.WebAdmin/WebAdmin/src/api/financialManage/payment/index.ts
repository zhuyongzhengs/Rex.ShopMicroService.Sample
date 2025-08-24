import request from '/@/utils/request';

export function usePaymentApi() {
    return {
        /**
         * 查询支付方式(分页)列表信息
         * @param { object } params
         * @returns { any }
         */
        getPaymentList: (params?: object): Promise<DataTableType<RowPaymentType>> => {
			return request({
				url: '/api/payment/payments/way',
				method: 'get',
                params
			});
		},
        /**
		 * 修改支付方式是否启用
		 * @param { string } id
		 * @param { boolean } isEnable
		 * @returns { any }
		 */
		updatePaymentIsEnable: (id: string, isEnable: boolean): Promise<boolean> => {
			return request({
				url: '/api/payment/payments/' + id + '/is-enable',
				method: 'put',
				params: {
					isEnable
				}
			});
		},
    };
}