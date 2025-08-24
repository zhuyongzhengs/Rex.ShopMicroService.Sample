import request from '/@/utils/request';

export function useGoodParamApi() {
    return {
        /**
         * 查询参数[模型](分页)列表信息
         * @param { object } params
         * @returns { any }
         */
        getGoodParamList: (params?: object): Promise<DataTableType<RowGoodParamType>> => {
			return request({
				url: '/api/good/good-param/page-list',
				method: 'get',
                params
			});
		},

		/**
         * 查询参数[模型]类型
         * @returns { any }
         */
        getGoodParamTypes: (): Promise<EnumValueType[]> => {
			return request({
				url: '/api/good/good-param/types',
				method: 'get'
			});
		},

		/**
         * 查询商品类型规格类型
		 * @param { string[] } data
         * @returns { any }
         */
        getGoodParamTypeByIds: (data: string[]): Promise<RowGoodParamType[]> => {
			return request({
				url: '/api/good/good-param/many-by-ids',
				method: 'get',
				params: { ids: data }
			});
		},

        /**
		 * 添加参数[模型]
		 * @param { object } data 
		 * @returns { any } 
		 */
		addGoodParam: (data: object): Promise<RowGoodParamType> => {
			return request({
				url: '/api/good/good-param',
				method: 'post',
				data
			});
		},

		/**
		 * 修改参数[模型]
		 * @param { string } id
		 * @param { object } data
		 * @returns { any } 
		 */
		updateGoodParam: (id: string, data: object): Promise<RowGoodParamType> => {
			return request({
				url: '/api/good/good-param/' + id,
				method: 'put',
				data
			});
		},

		/**
		 * 删除参数[模型]
		 * @param { string } id
		 * @returns { any } 
		 */
		deleteGoodParam: (id: string): Promise<boolean> => {
			return request({
				url: '/api/good/good-param/' + id,
				method: 'delete'
			});
		}
    };
}