import request from '/@/utils/request';

export function useGoodCommentApi() {
    return {
        /**
         * 查询商品评价(分页)列表信息
         * @param { object } params
         * @returns { any }
         */
        getGoodCommentList: (params?: object): Promise<DataTableType<RowGoodCommentType>> => {
			return request({
				url: '/api/backend/aggregation/good/comment/pageList',
				method: 'get',
                params
			});
		},

        /**
		 * 更新卖家回复
		 * @param { string } id
		 * @param { string } content
		 * @returns { any }
		 */
		updateSellerReply: (id: string, content: string): Promise<boolean> => {
			return request({
				url: '/api/good/good-comment/' + id + '/seller-reply',
				method: 'put',
                params: { content }
			});
		},

		/**
		 * 更新是否前台显示
		 * @param { string } id
		 * @param { boolean } isDisplay
		 * @returns { any }
		 */
		updateIsDisplay: (id: string, isDisplay: boolean): Promise<boolean> => {
			return request({
				url: '/api/good/good-comment/' + id + '/is-display',
				method: 'put',
                params: { isDisplay }
			});
		},

        /**
		 * 批量删除
		 * @param { string[] } data 
		 * @returns { any }
		 */
		deleteGoodsMany: (data: string[]): Promise<boolean> => {
			return request({
				url: '/api/good/good-comment',
				method: 'delete',
				params: { ids: data }
			});
		}
    };
}