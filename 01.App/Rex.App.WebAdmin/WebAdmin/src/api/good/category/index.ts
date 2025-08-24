import request from '/@/utils/request';

/**
 * （不建议写成 request.post(xxx)，因为这样 post 时，无法 params 与 data 同时传参）
 */
export function useGoodCategoryApi() {
	return {
		/**
		 * 获取(显示)商品分类
		 * @param { object } params
		 * @returns { any }
		 */
		getGoodCategoryShow: (params?: object): Promise<RowGoodCategoryType[]> => {
			return request({
				url: '/api/good/good-category/show',
				method: 'get',
				params
			});
		},

		/**
		 * 获取树形商品分类(用于添加|修改时选择上级商品分类)
		 * @returns { any }
		 */
		getGoodCategoryTree: (): Promise<RowGoodCategoryType[]> => {
			return request({
				url: '/api/good/good-category/tree',
				method: 'get'
			});
		},

		/**
		 * 添加商品分类
		 * @param { object } data
		 * @returns { any }
		 */
		addGoodCategory: (data: object): Promise<RowGoodCategoryType> => {
			return request({
				url: '/api/good/good-category',
				method: 'post',
				data
			});
		},

		/**
		 * 修改商品分类
		 * @param { string } id 
		 * @param { object } data 
		 * @returns { any }
		 */
		updateGoodCategory: (id: string, data: object): Promise<RowGoodCategoryType> => {
			return request({
				url: '/api/good/good-category/' + id,
				method: 'put',
				data
			});
		},

		/**
		 * 修改商品分类是否显示
		 * @param { string } id 
		 * @param { boolean } isShow 
		 * @returns { any }
		 */
		updateGoodCategoryIsShow: (id: string, isShow: boolean): Promise<boolean> => {
			return request({
				url: '/api/good/good-category/' + id + '/is-show',
				method: 'put',
				params: { isShow }
			});
		},

		/**
		 * 删除商品分类
		 * @param { string } id 
		 * @returns { any }
		 */
		deleteGoodCategory: (id: string): Promise<boolean> => {
			return request({
				url: '/api/good/good-category/' + id,
				method: 'delete'
			});
		}
	};
}
