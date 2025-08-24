import request from '/@/utils/request';

export function useGoodStoreApi() {
    return {
        /**
         * 查询门店(分页)列表信息
         * @param { object } params
         * @returns { any }
         */
        getGoodStoreList: (params?: object): Promise<DataTableType<RowStoreType>> => {
			return request({
				url: '/api/good/store/page-list',
				method: 'get',
                params
			});
		},

		/**
         * 查询门店(所有)信息
         * @param { object } params  
		 * @returns { any }
         */
        getGoodStoreAll: (params?: object): Promise<RowStoreType[]> => {
			return request({
				url: '/api/good/store/many',
				method: 'get',
				params
			});
		},

        /**
		 * 添加门店
		 * @param { object } data 
		 * @returns { any }
		 */
		addGoodStore: (data: object): Promise<RowStoreType> => {
			return request({
				url: '/api/good/store',
				method: 'post',
				data
			});
		},

		/**
		 * 修改门店
		 * @param { number } id
		 * @param { object } data
		 * @returns { any }
		 */
		updateGoodStore: (id: number, data: object): Promise<RowStoreType> => {
			return request({
				url: '/api/good/store/' + id,
				method: 'put',
				data
			});
		},

		/**
		 * 删除门店
		 * @param { number } id 
		 * @returns { any }
		 */
		deleteGoodStore: (id: number): Promise<boolean> => {
			return request({
				url: '/api/good/store/' + id,
				method: 'delete'
			});
		}
    };
}