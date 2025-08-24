import request from '/@/utils/request';

export function useLogisticsApi() {
    return {
        /**
         * 查询物流(分页)列表信息
         * @param { object } params
         * @returns { any }
         */
        getLogisticsList: (params?: object): Promise<DataTableType<RowLogisticsType>> => {
			return request({
				url: '/api/order/logistics/page-list',
				method: 'get',
                params
			});
		},

		/**
         * 查询物流列表信息
         * @param { object } params
         * @returns { any }
         */
        searchLogisticsList: (params?: object): Promise<RowLogisticsType[]> => {
			return request({
				url: '/api/order/logistics/filters',
				method: 'get',
                params
			});
		},

        /**
		 * 添加物流
		 * @param { object } data 
		 * @returns { any }
		 */
		addLogistics: (data: object): Promise<RowLogisticsType> => {
			return request({
				url: '/api/order/logistics',
				method: 'post',
				data
			});
		},

		/**
		 * 修改物流
		 * @param { string } id
		 * @param { object } data
		 * @returns { any }
		 */
		updateLogistics: (id: string, data: object): Promise<RowLogisticsType> => {
			return request({
				url: '/api/order/logistics/' + id,
				method: 'put',
				data
			});
		},

		/**
		 * 删除物流
		 * @param { string } id 
		 * @returns { any }
		 */
		deleteLogistics: (id: string): Promise<boolean> => {
			return request({
				url: '/api/order/logistics/' + id,
				method: 'delete'
			});
		}
    };
}