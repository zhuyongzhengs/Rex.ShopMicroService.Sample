import request from '/@/utils/request';

export function usePageDesignItemApi() {
    return {
        /**
         * 查询(存储)页面设计项
         * @param { string } pageDesignId
         * @returns { any }
         */
        getPageDesignItemLayout: (pageDesignId: string): Promise<LayoutItem[]> => {
			return request({
				url: '/api/good/page-design-item/layout/'+pageDesignId,
				method: 'get'
			});
		},

        /**
		 * 添加页面设计项
		 * @param { object } data 
		 * @returns { any }
		 */
		addPageDesignItem: (data: object): Promise<RowPageDesignItemType> => {
			return request({
				url: '/api/good/page-design-item',
				method: 'post',
				data
			});
		},

		/**
		 * 提交页面设计项
		 * @param { string } pageDesignId 
		 * @param { object } data 
		 * @returns { any }
		 */
		submitPageDesignItem: (pageDesignId: string, data: object): Promise<RowPageDesignItemType> => {
			return request({
				url: '/api/good/page-design-item/submit/' + pageDesignId,
				method: 'post',
				data
			});
		},

		/**
		 * 修改页面设计项
		 * @param { string } id
		 * @param { object } data
		 * @returns { any }
		 */
		updatePageDesignItem: (id: string, data: object): Promise<boolean> => {
			return request({
				url: '/api/good/page-design-item/' + id,
				method: 'put',
				data
			});
		},

		/**
		 * 删除页面设计项
		 * @param { string } id 
		 * @returns { any }
		 */
		deletePageDesignItem: (id: string): Promise<boolean> => {
			return request({
				url: '/api/good/page-design-item/' + id,
				method: 'delete'
			});
		}
    };
}