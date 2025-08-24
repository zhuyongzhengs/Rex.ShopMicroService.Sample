import request from '/@/utils/request';

export function useBillAftersalesApi() {
    return {
        /**
         * 查询售后单(分页)列表信息
         * @param { object } params
         * @returns { DataTableType<RowBillAftersalesType> }
         */
        getBillAftersalesList: (params?: object): Promise<DataTableType<RowBillAftersalesType>> => {
			return request({
				url: '/api/backend/aggregation/billAftersales',
				method: 'get',
                params
			});
		},
        /**
         * 根据ID查询售后单信息
         * @param { string } id
         * @returns { RowBillAftersalesDtlType }
         */
        getBillAftersalesById: (id: string): Promise<RowBillAftersalesDtlType> => {
            return request({
				url: '/api/backend/aggregation/billAftersales/' + id,
				method: 'get'
			});
        },
        /**
         * 查询售后单收货类型
         * @returns { EnumValueType[] }
         */
        getReceiveTypes: (): Promise<EnumValueType[]> => {
			return request({
				url: '/api/order/bill-aftersales/receive-types',
				method: 'get',
			});
		},
        /**
         * 查询售后单状态
         * @returns { EnumValueType[] }
         */
        getBillAftersalesStatus: (): Promise<EnumValueType[]> => {
			return request({
				url: '/api/order/bill-aftersales/status',
				method: 'get',
			});
		},
        /**
         * 审核售后单
         * @param { string } id
         * @param { object } data
         * @returns { boolean }
         */
        auditBillAftersales: (id: string, data: object): Promise<boolean> => {
			return request({
				url: '/api/order/bill-aftersales/' + id + '/audit',
				method: 'put',
                data
			});
		}
    };
}