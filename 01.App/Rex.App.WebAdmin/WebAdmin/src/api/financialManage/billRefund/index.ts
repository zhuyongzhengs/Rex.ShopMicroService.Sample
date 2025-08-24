import request from '/@/utils/request';

export function useBillRefundApi() {
    return {
        /**
         * 查询退款单(分页)列表信息
         * @param { object } params
         * @returns { any }
         */
        getBillRefundList: (params?: object): Promise<DataTableType<RowBillRefundManyType>> => {
			return request({
				url: '/api/backend/aggregation/billRefund',
				method: 'get',
                params
			});
		},
        /**
         * 查询退款单类型
         * @returns { any }
         */
        getBillRefundType: (): Promise<EnumValueType[]> => {
			return request({
				url: '/api/payment/bill-refund/bill-refund-type',
				method: 'get'
			});
		},
        /**
         * 查询退款单状态
         * @returns { any }
         */
        getBillRefundStatus: (): Promise<EnumValueType[]> => {
			return request({
				url: '/api/payment/bill-refund/bill-refund-status',
				method: 'get'
			});
		},
        /**
         * 审核退款
         * @param { string } id
         * @param { object } data
         * @returns { any }
         */
        updateAuditRefund: (id: string, data: object): Promise<boolean> => {
			return request({
				url: `/api/payment/bill-refund/${id}/audit`,
				method: 'put',
				data
			});
		}
    };
}