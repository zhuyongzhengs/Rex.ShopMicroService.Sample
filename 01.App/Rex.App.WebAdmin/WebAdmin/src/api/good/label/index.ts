import request from '/@/utils/request';

export function useGoodLabelApi() {
    return {
        /**
		 * 根据ID查询商品标签
		 * @param { object } data 
		 * @returns { any }
		 */
		getLabelByIds: (data: object): Promise<RowLabelType[]> => {
			return request({
				url: '/api/good/label/many-by-ids',
				method: 'get',
				params: {
					ids: data
				}
			});
		}
    };
}