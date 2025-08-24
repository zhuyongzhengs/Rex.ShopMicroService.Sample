import request from '/@/utils/request';

export function usePinTuanRuleApi() {
    return {
        /**
         * 查询拼团规则(分页)列表信息
         * @param { object } params
         * @returns { any }
         */
        getPinTuanRuleList: (params?: object): Promise<DataTableType<RowPinTuanRuleType>> => {
			return request({
				url: '/api/promotion/pin-tuan-rule/page-list',
				method: 'get',
                params
			});
		},

		/**
         * 查询拼团规则(分页)列表信息
		 * 注：会加载商品关联信息(较慢)
         * @param { object } params
         * @returns { any }
         */
        getPinTuanRuleLoadGoodList: (params?: object): Promise<DataTableType<RowPinTuanRuleType>> => {
			return request({
				url: '/api/backend/aggregation/pinTuan/rule',
				method: 'get',
                params
			});
		},

		/**
         * 根据拼团规则ID获取拼团商品
         * @param { string[] } ruleIds
         * @returns { any }
         */
        getPinTuanGoodByRuleId: (ruleIds: string[]): Promise<any> => {
			return request({
				url: '/api/backend/aggregation/pinTuan/good',
				method: 'post',
				data: ruleIds
			});
		},

        /**
		 * 添加拼团规则
		 * @param { object } data 
		 * @returns { any }
		 */
		addPinTuanRule: (data: object): Promise<RowPinTuanRuleType> => {
			return request({
				url: '/api/promotion/pin-tuan-rule',
				method: 'post',
				data
			});
		},

		/**
		 * 修改拼团规则
		 * @param { string } id
		 * @param { object } data
		 * @returns { any }
		 */
		updatePinTuanRule: (id: string, data: object): Promise<RowPinTuanRuleType> => {
			return request({
				url: '/api/promotion/pin-tuan-rule/' + id,
				method: 'put',
				data
			});
		},

		/**
		 * 修改拼团规则-是否开启
		 * @param { string } id
		 * @param { bool } isStatusOpen
		 * @returns { any }
		 */
		updatePinTuanRuleIsStatusOpen: (id: string, isStatusOpen: boolean): Promise<boolean> => {
			return request({
				url: `/api/promotion/pin-tuan-rule/${ id }/is-status-open`,
				method: 'put',
				params: {
					isStatusOpen
				}
			});
		},

		/**
		 * 删除拼团规则
		 * @param { string } id 
		 * @returns { any }
		 */
		deletePinTuanRule: (id: string): Promise<boolean> => {
			return request({
				url: '/api/promotion/pin-tuan-rule/' + id,
				method: 'delete'
			});
		}
    };
}