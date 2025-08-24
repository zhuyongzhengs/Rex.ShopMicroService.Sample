import request from '/@/utils/request';

export function useServiceDescriptionApi() {
    return {
        /**
         * 查询商城服务描述(分页)列表信息
         * @param { object } params
         * @returns { any }
         */
        getServiceDescriptionList: (params?: object): Promise<DataTableType<RowServiceDescriptionType>> => {
			return request({
				url: '/api/good/service-description/page-list',
				method: 'get',
                params
			});
		},

		/**
         * 查询商城服务描述类型信息
         * @returns { any }
         */
        getShopServiceNoteTypes: (): Promise<EnumValueType[]> => {
			return request({
				url: '/api/good/service-description/note-types',
				method: 'get'
			});
		},

        /**
		 * 添加商城服务描述
		 * @param { object } data 
		 * @returns { any }
		 */
		addServiceDescription: (data: object): Promise<RowServiceDescriptionType> => {
			return request({
				url: '/api/good/service-description',
				method: 'post',
				data
			});
		},

		/**
		 * 修改商城服务描述
		 * @param { string } id
		 * @param { object } data
		 * @returns { any }
		 */
		updateServiceDescription: (id: string, data: object): Promise<RowServiceDescriptionType> => {
			return request({
				url: '/api/good/service-description/' + id,
				method: 'put',
				data
			});
		},

		/**
		 * 修改商城服务描述是否显示
		 * @param { string } id
		 * @param { boolean } isShow
		 * @returns { any }
		 */
		updateServiceDescriptionIsShow: (id: string, isShow: boolean): Promise<boolean> => {
			return request({
				url: '/api/good/service-description/isShow/' + id + '/' + isShow,
				method: 'put'
			});
		},

		/**
		 * 删除商城服务描述
		 * @param { string } id 
		 * @returns { any }
		 */
		deleteServiceDescription: (id: string): Promise<boolean> => {
			return request({
				url: '/api/good/service-description/' + id,
				method: 'delete'
			});
		}
    };
}