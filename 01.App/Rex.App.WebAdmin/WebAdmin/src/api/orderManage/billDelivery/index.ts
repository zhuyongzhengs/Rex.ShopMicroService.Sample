import request from '/@/utils/request';

export function useBillDeliveryApi() {
    return {
        /**
         * 查询发货单(分页)列表信息
         * @param { object } params
         * @returns { any }
         */
        getBillDeliveryList: (params?: object): Promise<DataTableType<RowBillDeliveryType>> => {
			return request({
				url: '/api/order/bill-delivery',
				method: 'get',
                params
			});
		},
        /**
         * 查询发货单详情信息
         * @param { string } id
         * @param { object } params 
         * @returns { any }
         */
        getBillDeliveryDetail: (id: string, params?: object): Promise<RowBillDeliveryType> => {
			return request({
				url: `/api/order/bill-delivery/${id}`,
				method: 'get',
                params
			});
		},

        /**
         * 更新发货单信息
        * @param { string } id 
        * @param { object } data
         * @returns { any }
         */
        updateOrderDelivery: (id: string, data: object): Promise<boolean> => {
			return request({
				url: '/api/order/bill-delivery/' + id + '/order-delivery',
				method: 'put',
                data
			});
		}
    };
}