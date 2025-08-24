import request from '/@/utils/request';

export function useServiceGoodApi() {
    return {
        /**
         * 查询(分页)服务商品列表信息
         * @param { object } params
         * @returns { any }
         */
        getServiceGoodList: (params?: object): Promise<DataTableType<RowServiceGoodType>> => {
			return request({
				url: '/api/good/service-good/page-list',
				method: 'get',
                params
			});
		},

		/**
         * 获取项目状态
         * @returns { any }
         */
        getServiceGoodStatus: (): Promise<EnumValueType[]> => {
			return request({
				url: '/api/good/service-good/status',
				method: 'get'
			});
		},

		/**
         * 获取核销有效期类型
         * @returns { any }
         */
        getServiceValidityTypes: (): Promise<EnumValueType[]> => {
			return request({
				url: '/api/good/service-good/validity-types',
				method: 'get'
			});
		},

		/**
         * 根据ID查询服务商品信息
         * @param { string } id
         * @returns { any }
         */
        getServiceGoodById: (id: string): Promise<any> => {
			return request({
				url: '/api/good/service-good/' + id,
				method: 'get'
			});
		},

        /**
		 * 添加
		 * @param { object } data 
		 * @returns { any }
		 */
		addServiceGood: (data: object): Promise<RowServiceGoodType> => {
			return request({
				url: '/api/good/service-good',
				method: 'post',
				data
			});
		},

		/**
		 * 修改
		 * @param { string } id 
		 * @param { object } data 
		 * @returns { any }
		 */
		updateServiceGood: (id: string, data: object): Promise<RowServiceGoodType> => {
			return request({
				url: '/api/good/service-good/' + id,
				method: 'put',
				data
			});
		},
		
		/**
		 * 删除
		 * @param { string } id 
		 * @returns { any }
		 */
		deleteServiceGood: (id: string): Promise<boolean> => {
			return request({
				url: '/api/good/service-good/' + id,
				method: 'delete'
			});
		},
    };
}