import request from '/@/utils/request';

export function useGoodBrandApi() {
    return {
        /**
         * 查询品牌(分页)列表信息
         * @param { object } params
         * @returns { DataTableType<RowGoodBrandType> }
         */
        getGoodBrandList: (params?: object): Promise<DataTableType<RowGoodBrandType>> => {
			return request({
				url: '/api/good/brand/page-list',
				method: 'get',
                params
			});
		},

		/**
         * 查询品牌信息
         * @param { object } params
         * @returns { RowGoodBrandType[] }
         */
        getGoodBrandShow: (params?: object): Promise<RowGoodBrandType[]> => {
			return request({
				url: '/api/good/brand/show',
				method: 'get',
				params
			});
		},

        /**
		 * 添加品牌
		 * @param { object } data 
		 * @returns { RowGoodBrandType }
		 */
		addGoodBrand: (data: object): Promise<RowGoodBrandType> => {
			return request({
				url: '/api/good/brand',
				method: 'post',
				data
			});
		},

		/**
		 * 修改品牌
		 * @param { string } id
		 * @param { object } data
		 * @returns { any }
		 */
		updateGoodBrand: (id: string, data: object): Promise<RowGoodBrandType> => {
			return request({
				url: '/api/good/brand/' + id,
				method: 'put',
				data
			});
		},

		/**
		 * 修改品牌是否显示
		 * @param { string } id
		 * @param { boolean } isShow
		 * @returns { any }
		 */
		updateGoodBrandIsShow: (id: string, isShow: boolean): Promise<boolean> => {
			return request({
				url: '/api/good/brand/' + id + '/is-show',
				method: 'put',
				params: {
					isShow
				}
			});
		},

		/**
		 * 删除品牌
		 * @param { string } id 
		 * @returns { any }
		 */
		deleteGoodBrand: (id: string): Promise<boolean> => {
			return request({
				url: '/api/good/brand/' + id,
				method: 'delete'
			});
		}
    };
}