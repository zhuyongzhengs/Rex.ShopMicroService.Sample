import request from '/@/utils/request';

export function usePageDesignApi() {
    return {
        /**
         * 查询页面设计(分页)列表信息
         * @param { object } params
         * @returns { any }
         */
        getPageDesignList: (params?: object): Promise<DataTableType<RowPageDesignType>> => {
			return request({
				url: '/api/good/page-design/page-list',
				method: 'get',
                params
			});
		},

		/**
         * 查询版面设计数据
         * @param { string } pageDesignId
         * @returns { any }
         */
        getLayoutDesigns: (pageDesignId: string): Promise<LayoutDesignType> => {
			return request({
				url: '/api/good/page-design/layout-design/' + pageDesignId,
				method: 'get'
			});
		},

        /**
		 * 添加页面设计
		 * @param { object } data 
		 * @returns { any }
		 */
		addPageDesign: (data: object): Promise<RowPageDesignType> => {
			return request({
				url: '/api/good/page-design',
				method: 'post',
				data
			});
		},

		/**
		 * 修改页面设计
		 * @param { string } id
		 * @param { object } data
		 * @returns { any }
		 */
		updatePageDesign: (id: string, data: object): Promise<RowPageDesignType> => {
			return request({
				url: '/api/good/page-design/' + id,
				method: 'put',
				data
			});
		},

		/**
		 * 修改是否默认
		 * @param { string } id
		 * @param { number } type
		 * @returns { any }
		 */
		updatePageDesignIsType: (id: string, type: number): Promise<boolean> => {
			return request({
				url: '/api/good/page-design/' + id + '/is-type',
				method: 'put',
				params: {
					type
				}
			});
		},

		/**
		 * 删除页面设计
		 * @param { string } id 
		 * @returns { any }
		 */
		deletePageDesign: (id: string): Promise<boolean> => {
			return request({
				url: '/api/good/page-design/' + id,
				method: 'delete'
			});
		}
    };
}