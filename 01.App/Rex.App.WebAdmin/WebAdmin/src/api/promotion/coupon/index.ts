import request from '/@/utils/request';

export function useCouponApi() {
    return {
        /**
         * 查询优惠劵(分页)列表信息
         * @param { object } params
         * @returns { any }
         */
        getCouponList: (params?: object): Promise<DataTableType<RowPromotionType>> => {
			return request({
				url: '/api/promotion/promotion-global/page-list',
				method: 'get',
                params
			});
		},

		/**
         * 查询卷码(分页)列表信息
         * @param { object } params
         * @returns { any }
         */
        getCouponListTables: (params?: object): Promise<DataTableType<RowCouponType>> => {
			return request({
				url: 'api/backend/aggregation/promotion/coupon/list',
				method: 'get',
                params
			});
		},

		/**
         * 查询优惠劵条件类型
         * @returns { any }
         */
        getCouponConditionType: ():Promise<CommonKeyValueType[]> => {
			return request({
				url: '/api/promotion/promotion-global/promotion-condition-type',
				method: 'get'
			});
		},

		/**
         * 查询优惠劵结果类型
         * @returns { any }
         */
        getCouponResultType: (): Promise<CommonKeyValueType[]> => {
			return request({
				url: '/api/promotion/promotion-global/promotion-result-type',
				method: 'get'
			});
		},

        /**
		 * 添加优惠劵
		 * @param { object } data 
		 * @returns { any }
		 */
		addCoupon: (data: object): Promise<RowCouponType> => {
			return request({
				url: '/api/promotion/promotion-global',
				method: 'post',
				data
			});
		},

		/**
		 * 修改优惠劵
		 * @param { string } id
		 * @param { object } data
		 * @returns { any }
		 */
		updateCoupon: (id: string, data: object): Promise<RowCouponType> => {
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
		updateCouponIsExclusive: (promotionId: string, isExclusive: boolean): Promise<boolean> => {
			return request({
				url: `/api/promotion/coupon/isExclusive/${ promotionId }/${ isExclusive }`,
				method: 'put'
			});
		},

		/**
		 * 修改是否开启
		 * @param { string } promotionId
		 * @param { boolean } isEnable
		 * @returns { any }
		 */
		updateCouponIsEnable: (promotionId: string, isEnable: boolean): Promise<boolean> => {
			return request({
				url: `/api/promotion/coupon/isEnable/${ promotionId }/${ isEnable }`,
				method: 'put'
			});
		},

		/**
		 * 删除优惠劵
		 * @param { string } id 
		 * @returns { any }
		 */
		deleteCoupon: (id: string): Promise<boolean> => {
			return request({
				url: '/api/promotion/promotion-global/' + id,
				method: 'delete'
			});
		}
    };
}