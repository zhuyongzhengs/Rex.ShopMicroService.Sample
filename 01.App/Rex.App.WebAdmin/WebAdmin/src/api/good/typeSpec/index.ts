import request from '/@/utils/request';

export function useGoodTypeSpecApi() {
    return {
        /**
         * 查询商品类型规格(分页)列表信息
         * @param { object } params
         * @returns { any }
         */
        getGoodTypeSpecList: (params?: object): Promise<DataTableType<RowGoodTypeSpecType>> => {
			return request({
				url: '/api/good/good-type-spec/page-list',
				method: 'get',
                params
			});
		},

		/**
         * 查询商品类型规格类型
		 * @param { string[] } data
         * @returns { any }
         */
        getGoodTypeSpecTypeByIds: (data: string[]): Promise<RowGoodTypeSpecType[]> => {
			return request({
				url: '/api/good/good-type-spec/many-by-ids',
				method: 'get',
				params: { ids: data }
			});
		},

        /**
		 * 添加商品类型规格
		 * @good-type-spec { object } data 
		 * @returns { any } 
		 */
		addGoodTypeSpec: (data: object): Promise<RowGoodTypeSpecType> => {
			return request({
				url: '/api/good/good-type-spec',
				method: 'post',
				data
			});
		},

		/**
		 * 生成商品类型规格SKU
		 * @good-type-spec { object } data 
		 * @returns { any } 
		 */
		generateGoodTypeSpecSku: (data: object): Promise<RowProductType[]> => {
			return request({
				url: '/api/good/good-type-spec/generate-spec',
				method: 'post',
				data
			});
		},

		/**
		 * 修改商品类型规格
		 * @good-type-spec { string } id
		 * @good-type-spec { object } data
		 * @returns { any } 
		 */
		updateGoodTypeSpec: (id: string, data: object): Promise<RowGoodTypeSpecType> => {
			return request({
				url: '/api/good/good-type-spec/' + id,
				method: 'put',
				data
			});
		},

		/**
		 * 删除商品类型规格
		 * @good-type-spec { string } id
		 * @returns { any } 
		 */
		deleteGoodTypeSpec: (id: string): Promise<boolean> => {
			return request({
				url: '/api/good/good-type-spec/' + id,
				method: 'delete'
			});
		}
    };
}