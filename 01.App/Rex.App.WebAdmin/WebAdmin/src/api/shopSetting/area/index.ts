import request from '/@/utils/request';

export function useGoodAreaApi() {
    return {
        /**
         * 查询区域(分页)列表信息
         * @param { object } params
         * @returns { any }
         */
        getGoodAreaList: (params?: object): Promise<DataTableType<RowAreaType>> => {
			return request({
				url: '/api/good/area/page-list',
				method: 'get',
                params
			});
		},

		/**
         * 查询选中(树形)区域信息
         * @returns { any }
         */
        getAreaTreeActive: (id: number): Promise<AreaTreeType> => {
			return request({
				url: '/api/good/area/tree-active/' + id,
				method: 'get'
			});
		},

		/**
         * 查询(树形)区域信息
         * @returns { any }
         */
        getGoodAreaTree: (): Promise<RowAreaType[]> => {
			return request({
				url: '/api/good/area/tree-many',
				method: 'get'
			});
		},

		/**
         * 获取选中的区域ID
         * @returns { any }
         */
        getAreaTreeActiveIds: (id: number): Promise<number[]> => {
			return request({
				url: '/api/good/area/' + id + '/tree-active-ids',
				method: 'get'
			});
		},
		
		/**
		 * 加载(树形)区域信息
		 * @param parentId 区域父ID
		 * @param depth 深度
		 * @param activeId 选中的ID
		 * @returns { any }
		 */
        loadGoodAreaTree: (parentId: number, depth: number|null = null, activeId: number|null = null): Promise<AreaTreeType[]> => {
			return request({
				url: '/api/good/area/tree',
				method: 'get',
				params: {
					parentId,
					depth,
					activeId
				}
			});
		},

        /**
		 * 添加区域
		 * @param { object } data 
		 * @returns { any }
		 */
		addGoodArea: (data: object): Promise<RowAreaType> => {
			return request({
				url: '/api/good/area',
				method: 'post',
				data
			});
		},

		/**
		 * 修改区域
		 * @param { number } id
		 * @param { object } data
		 * @returns { any }
		 */
		updateGoodArea: (id: number, data: object): Promise<RowAreaType> => {
			return request({
				url: '/api/good/area/' + id,
				method: 'put',
				data
			});
		},

		/**
		 * 删除区域
		 * @param { number } id 
		 * @returns { any }
		 */
		deleteGoodArea: (id: number): Promise<boolean> => {
			return request({
				url: '/api/good/area/' + id,
				method: 'delete'
			});
		}
    };
}