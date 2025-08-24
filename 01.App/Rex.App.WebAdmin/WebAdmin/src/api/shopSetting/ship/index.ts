import request from '/@/utils/request';

export function useShipApi() {
    return {
        /**
         * 查询配送方式(分页)列表信息
         * @param { object } params
         * @returns { any }
         */
        getShipList: (params?: object): Promise<DataTableType<RowShipType>> => {
			return request({
				url: '/api/order/ship/page-list',
				method: 'get',
                params
			});
		},

		/**
         * 查询获取配送方式重量
         * @returns { any }
         */
        getShipUnitList: (): Promise<EnumValueType[]> => {
			return request({
				url: '/api/order/ship/ship-units',
				method: 'get'
			});
		},

        /**
		 * 添加配送方式
		 * @param { object } data 
		 * @returns { any }
		 */
		addShip: (data: object): Promise<RowShipType> => {
			return request({
				url: '/api/order/ship',
				method: 'post',
				data
			});
		},

		/**
		 * 修改配送方式
		 * @param { string } id
		 * @param { object } data
		 * @returns { any }
		 */
		updateShip: (id: string, data: object): Promise<RowShipType> => {
			return request({
				url: '/api/order/ship/' + id,
				method: 'put',
				data
			});
		},

		/**
		 * 修改配送方式是否默认
		 * @param { string } id
		 * @param { boolean } isDefault
		 * @returns { any }
		 */
		updateShipIsDefault: (id: string, isDefault: boolean): Promise<boolean> => {
			return request({
				url: '/api/order/ship/' + id + '/is-default',
				method: 'put',
				params: {
					isDefault
				}
			});
		},

		/**
		 * 修改是否包邮
		 * @param { string } id
		 * @param { boolean } isFreePostage
		 * @returns { any }
		 */
		updateShipIsfreePostage: (id: string, isFreePostage: boolean): Promise<boolean> => {
			return request({
				url: '/api/order/ship/' + id + '/isfree-postage',
				method: 'put',
				params: {
					isFreePostage
				}
			});
		},

		/**
		 * 删除配送方式
		 * @param { string } id 
		 * @returns { any }
		 */
		deleteShip: (id: string): Promise<boolean> => {
			return request({
				url: '/api/order/ship/' + id,
				method: 'delete'
			});
		}
    };
}