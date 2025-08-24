import request from '/@/utils/request';

export function useGoodStoreClerkApi() {
    return {
        /**
         * 查询店铺店员关联(分页)列表信息
         * @param { object } params
         * @returns { any }
         */
        getGoodStoreClerkList: (params?: object): Promise<DataTableType<RowStoreClerkType>> => {
			return request({
				url: '/api/backend/aggregation/storeClerk',
				method: 'get',
                params
			});
		},

        /**
		 * 添加店铺店员关联
		 * @param { object } data 
		 * @returns { any }
		 */
		addGoodStoreClerk: (data: object): Promise<RowStoreClerkType> => {
			return request({
				url: '/api/backend/aggregation/storeClerk',
				method: 'post',
				data
			});
		},

		/**
		 * 修改店铺店员关联
		 * @param { string } id
		 * @param { object } data
		 * @returns { any }
		 */
		updateGoodStoreClerk: (id: string, data: object): Promise<RowStoreClerkType> => {
			return request({
				url: '/api/backend/aggregation/storeClerk/' + id,
				method: 'put',
				data
			});
		},

		/**
		 * 删除店铺店员关联
		 * @param { string } id 
		 * @returns { any }
		 */
		deleteGoodStoreClerk: (id: string): Promise<boolean> => {
			return request({
				url: '/api/good/store-clerk/' + id,
				method: 'delete'
			});
		},

		/**
		 * 批量删除店铺店员关联
		 * @param { object } data 
		 * @returns { any }
		 */
		deleteGoodStoreClerkMany: (data: object): Promise<boolean> => {
			return request({
				url: '/api/good/store-clerk/many',
				method: 'delete',
				params: {
					ids: data
				}
			});
		}
    };
}