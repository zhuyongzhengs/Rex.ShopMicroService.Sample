import request from '/@/utils/request';

export function useOrderApi() {
    return {
        /**
         * 查询订单(分页)列表信息
         * @param { object } params
         * @returns { any }
         */
        getOrderList: (params?: object): Promise<DataTableType<RowOrderType>> => {
			return request({
				url: '/api/order/orders',
				method: 'get',
                params
			});
		},

        /**
         * 更新卖家备注
         * @param { string } orderId
         * @param { string } mark
         * @returns { any }
         */
        updateOrderMark: (orderId: string, mark: string): Promise<boolean> => {
			return request({
				url: '/api/order/orders/mark/' + orderId,
				method: 'put',
                data: mark
			});
		},

        /**
         * 更新订单收货信息
         * @param { string } orderId
         * @param { object } data
         * @returns { any }
         */
        updateOrderShip: (orderId: string, data: object): Promise<boolean> => {
			return request({
				url: '/api/order/orders/ship/' + orderId,
				method: 'put',
                data
			});
		},

        /**
         * 取消订单
         * @param { object } data
         * @returns { any }
         */
        cancelOrder: (data: object): Promise<boolean> => {
			return request({
				url: '/api/order/orders/cancel',
				method: 'put',
                data
			});
		},

        /**
         * 删除订单
         * @param { object } data
         * @returns { any }
         */
        deleteOrder: (data: object): Promise<boolean> => {
			return request({
				url: '/api/order/orders/many',
				method: 'delete',
                params: {
                    ids: data
                }
			});
		},

        /**
         * 完成订单
         * @param { string } orderId
         * @returns { any }
         */
        completeOrder: (orderId: string): Promise<boolean> => {
            return request({
                url: '/api/order/orders/complete/' + orderId,
                method: 'put'
            });
        },

        /**
         * 查询订单数量
         * @param { object } params
         * @returns { any }
         */
        getStatusQuantity: (): Promise<any> => {
			return request({
				url: '/api/order/orders/status-quantity',
				method: 'get'
			});
		},

        /**
         * 查询订单明细
         * @param { object } params
         * @returns { any }
         */
        getOrderDetail: (orderId: string): Promise<OrderDetailType> => {
			return request({
				url: '/api/backend/aggregation/order/detail/' + orderId,
				method: 'get'
			});
		},

        /**
         * 查询订单发货信息
         * @param { string } orderId
         * @returns { any }
         */
        getOrderDelivery: (orderId: string): Promise<any> => {
			return request({
				url: '/api/backend/aggregation/order/delivery/' + orderId,
				method: 'get'
			});
		},

        /**
         * 根据订单ID获取售后单状态
         * @param { string } orderId
         * @param { number } status
         * @returns { any }
         */
        getBillAftersalesByStatusAsync: (orderId: string, status: number): Promise<RowBillAftersalesType> => {
			return request({
				url: '/api/order/bill-aftersales/order-status/' + orderId,
				method: 'get',
                params: {
                    status
                }
			});
		},

        /**
         * 查询订单类型
         * @returns { any }
         */
        getOrderType: (): Promise<EnumValueType[]> => {
			return request({
				url: '/api/order/orders/order-type',
				method: 'get',
			});
		},

        /**
         * 查询发货状态
         * @returns { any }
         */
        getOrderShipStatus: (): Promise<EnumValueType[]> => {
			return request({
				url: '/api/order/orders/ship-status',
				method: 'get',
			});
		},

        /**
         * 查询售后状态
         * @returns { any }
         */
        getOrderConfirmStatus: (): Promise<EnumValueType[]> => {
			return request({
				url: '/api/order/orders/confirm-status',
				method: 'get',
			});
		},

        /**
         * 查询订单状态
         * @returns { any }
         */
        getOrderStatus: (): Promise<EnumValueType[]> => {
			return request({
				url: '/api/order/orders/status',
				method: 'get',
			});
		},

        /**
         * 查询订单来源
         * @returns { any }
         */
        getOrderSource: (): Promise<EnumValueType[]> => {
			return request({
				url: '/api/order/orders/source',
				method: 'get',
			});
		},

        /**
         * 查询订单来源
         * @returns { any }
         */
        getOrderPayStatus: (): Promise<EnumValueType[]> => {
			return request({
				url: '/api/order/orders/pay-status',
				method: 'get',
			});
		},

        /**
         * 查询收货方式
         * @returns { any }
         */
        getOrderReceiptType: (): Promise<EnumValueType[]> => {
			return request({
				url: '/api/order/orders/receipt-type',
				method: 'get',
			});
		},

        /**
         * 订单发货
         * @param { object } data
         * @returns { any }
         */
        orderDelivery: (data: object): Promise<boolean> => {
			return request({
				url: '/api/order/bill-delivery/order-delivery',
				method: 'post',
                data
			});
		}
    };
}