import request from '/@/utils/request';

export function useArticleTypeApi() {
    return {
        /**
         * 查询文章分类(分页)列表信息
         * @param { object } params
         * @returns { any }
         */
        getArticleTypeList: (params?: object): Promise<DataTableType<RowArticleTypeType>> => {
			return request({
				url: '/api/good/article-type/page-list',
				method: 'get',
                params
			});
		},

        /**
		 * 添加文章分类
		 * @param { object } data 
		 * @returns { any }
		 */
		addArticleType: (data: object): Promise<RowArticleTypeType> => {
			return request({
				url: '/api/good/article-type',
				method: 'post',
				data
			});
		},

		/**
		 * 修改文章分类
		 * @param { string } id
		 * @param { object } data
		 * @returns { any }
		 */
		updateArticleType: (id: string, data: object): Promise<RowArticleTypeType> => {
			return request({
				url: '/api/good/article-type/' + id,
				method: 'put',
				data
			});
		},

		/**
		 * 删除文章分类
		 * @param { string } id 
		 * @returns { any }
		 */
		deleteArticleType: (id: string): Promise<boolean> => {
			return request({
				url: '/api/good/article-type/' + id,
				method: 'delete'
			});
		}
    };
}