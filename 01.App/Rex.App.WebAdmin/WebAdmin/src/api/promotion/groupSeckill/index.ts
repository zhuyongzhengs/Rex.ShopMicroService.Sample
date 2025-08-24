import request from '/@/utils/request';

export function useGroupSeckillApi() {
    return {
        /**
         * 查询商品秒杀(分页)列表信息
         * @param { object } params
         * @returns { any }
         */
        getGroupSeckillList: (params?: object): Promise<DataTableType<RowPromotionType>> => {
			return request({
				url: '/api/promotion/groupSeckill',
				method: 'get',
                params
			});
		},

		/**
         * 获取(分页) 常用促销列表
         * @param { object } params
         * @returns { any }
         */
        getPromotionCommonTagList: (params?: object): Promise<DataTableType<RowPromotionType>> => {
			return request({
				url: '/api/promotion/promotion-common-tag',
				method: 'get',
                params
			});
		},

		/**
         * 查询商品秒杀结果类型
         * @returns { any }
         */
        getGroupSeckillResultType: (): Promise<CommonKeyValueType[]> => {
			return request({
				url: '/api/promotion/groupSeckill/resultType',
				method: 'get'
			});
		},

        /**
		 * 添加商品秒杀
		 * @param { object } data 
		 * @returns { any }
		 */
		addGroupSeckill: (data: object): Promise<RowPromotionType> => {
			return request({
				url: '/api/promotion/groupSeckill',
				method: 'post',
				data
			});
		},

		/**
		 * 修改商品秒杀
		 * @param { string } id
		 * @param { object } data
		 * @returns { any }
		 */
		updateGroupSeckill: (id: string, data: object): Promise<RowPromotionType> => {
			return request({
				url: '/api/promotion/groupSeckill/' + id,
				method: 'put',
				data
			});
		},

		/**
		 * 修改是否开启
		 * @param { string } promotionId
		 * @param { boolean } isEnable
		 * @returns { any }
		 */
		updateGroupSeckillIsEnable: (promotionId: string, isEnable: boolean): Promise<boolean> => {
			return request({
				url: `/api/promotion/groupSeckill/${ promotionId }/is-enable`,
				method: 'put',
				params: {
					isEnable
				}
			});
		},

		/**
		 * 删除商品秒杀
		 * @param { string } id 
		 * @returns { any }
		 */
		deleteGroupSeckill: (id: string): Promise<boolean> => {
			return request({
				url: '/api/promotion/groupSeckill/' + id,
				method: 'delete'
			});
		}
    };
}