import request from '/@/utils/request';

export function useNoticeApi() {
    return {
        /**
         * 查询公告(分页)列表信息
         * @param { object } params
         * @returns { any }
         */
        getNoticeList: (params?: object): Promise<DataTableType<RowNoticeType>> => {
			return request({
				url: '/api/good/notice/page-list',
				method: 'get',
                params
			});
		},

        /**
		 * 添加公告
		 * @param { object } data 
		 * @returns { any }
		 */
		addNotice: (data: object):Promise<RowNoticeType> => {
			return request({
				url: '/api/good/notice',
				method: 'post',
				data
			});
		},

		/**
		 * 修改公告
		 * @param { string } id
		 * @param { object } data
		 * @returns { any }
		 */
		updateNotice: (id: string, data: object): Promise<RowNoticeType> => {
			return request({
				url: '/api/good/notice/' + id,
				method: 'put',
				data
			});
		},

		/**
		 * 删除公告
		 * @param { string } id 
		 * @returns { any }
		 */
		deleteNotice: (id: string): Promise<boolean> => {
			return request({
				url: '/api/good/notice/' + id,
				method: 'delete'
			});
		}
    };
}