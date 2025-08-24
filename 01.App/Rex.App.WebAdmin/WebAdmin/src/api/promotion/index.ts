import request from '/@/utils/request';

export function usePromotionApi() {
    return {
		/**
         * 查询促销(分页)列表信息
         * @param { object } params
         * @returns { any }
         */
        getPromotionList: (params?: object): Promise<DataTableType<RowPromotionType>> => {
			return request({
				url: '/api/promotion/promotion-global/page-list',
				method: 'get',
                params
			});
		},

		/**
         * 查询促销条件类型
         * @returns { any }
         */
        getPromotionConditionType: (): Promise<CommonKeyValueType[]> => {
			return request({
				url: '/api/promotion/promotion-global/promotion-condition-type',
				method: 'get'
			});
		},

		/**
         * 查询促销结果类型
         * @returns { any }
         */
        getPromotionResultType: (): Promise<CommonKeyValueType[]> => {
			return request({
				url: '/api/promotion/promotion-global/promotion-result-type',
				method: 'get'
			});
		},

        /**
		 * 添加促销
		 * @param { object } data 
		 * @returns { any }
		 */
		addPromotion: (data: object): Promise<RowPromotionType> => {
			return request({
				url: '/api/promotion/promotion-global',
				method: 'post',
				data
			});
		},

		/**
		 * 修改促销
		 * @param { string } id
		 * @param { object } data
		 * @returns { any }
		 */
		updatePromotion: (id: string, data: object): Promise<RowPromotionType> => {
			return request({
				url: '/api/promotion/promotion-global/' + id,
				method: 'put',
				data
			});
		},

		/**
		 * 修改是否排他
		 * @param { string } promotionId
		 * @param { boolean } isExclusive
		 * @returns { any }
		 */
		updatePromotionIsExclusive: (promotionId: string, isExclusive: boolean): Promise<boolean> => {
			return request({
				url: `/api/promotion/promotion-global/isExclusive/${ promotionId }/${ isExclusive }`,
				method: 'put'
			});
		},

		/**
		 * 修改是否开启
		 * @param { string } promotionId
		 * @param { boolean } isEnable
		 * @returns { any }
		 */
		updatePromotionIsEnable: (promotionId: string, isEnable: boolean): Promise<boolean> => {
			return request({
				url: `/api/promotion/common/isEnable/${ promotionId }/${ isEnable }`,
				method: 'put'
			});
		},

		/**
		 * 删除促销
		 * @param { string } id 
		 * @returns { any }
		 */
		deletePromotion: (id: string): Promise<boolean> => {
			return request({
				url: '/api/promotion/promotion-global/' + id,
				method: 'delete'
			});
		}
    };
}