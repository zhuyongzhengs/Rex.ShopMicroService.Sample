import request from '/@/utils/request';

export function useBillReshipApi() {
    return {
        /**
         * 查询退货单(分页)列表信息
         * @param { object } params
         * @returns { any }
         */
        getBillReshipList: (params?: object): Promise<DataTableType<RowBillReshipManyType>> => {
			return request({
				url: '/api/backend/aggregation/billReship',
				method: 'get',
                params
			});
		},
        /**
         * 查询退货单状态
         * @returns { any }
         */
        getBillReshipStatus: (): Promise<EnumValueType[]> => {
			return request({
				url: '/api/order/bill-reship/status',
				method: 'get',
			});
		},
        /**
         * 确认收货
         * @param { string } id
         * @returns { any }
         */
        updateConfirmDelivery: (id: string): Promise<boolean> => {
			return request({
				url: `/api/order/bill-reship/${id}/confirm-delivery`,
				method: 'put'
			});
		}
    };
}