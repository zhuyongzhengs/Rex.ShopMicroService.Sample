import request from '/@/utils/request';

export function useArticleApi() {
    return {
        /**
         * 查询文章(分页)列表信息
         * @param { object } params
         * @returns { any }
         */
        getArticleList: (params?: object): Promise<DataTableType<RowArticleType>> => {
			return request({
				url: '/api/good/article',
				method: 'get',
                params
			});
		},

		/**
		 * 修改文章是否显示
		 * @param { string } id
		 * @param { boolean } isPub
		 * @returns { any }
		 */
		updateArticleIsPub: (id: string, isPub: boolean): Promise<boolean> => {
			return request({
				url: '/api/good/article/' + id + '/isPub',
				method: 'put',
				params: {
					isPub: isPub
				}
			});
		},

        /**
		 * 添加文章
		 * @param { object } data 
		 * @returns { any }
		 */
		addArticle: (data: object): Promise<RowArticleType> => {
			return request({
				url: '/api/good/article',
				method: 'post',
				data
			});
		},

		/**
		 * 修改文章
		 * @param { string } id
		 * @param { object } data
		 * @returns { any }
		 */
		updateArticle: (id: string, data: object): Promise<RowArticleType> => {
			return request({
				url: '/api/good/article/' + id,
				method: 'put',
				data
			});
		},

		/**
		 * 删除文章
		 * @param { string } id 
		 * @returns { any }
		 */
		deleteArticle: (id: string): Promise<boolean> => {
			return request({
				url: '/api/good/article/' + id,
				method: 'delete'
			});
		}
    };
}