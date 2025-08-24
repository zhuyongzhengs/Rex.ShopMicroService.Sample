import request from '/@/utils/request';

export function useOrganizationUnitApi() {
	return {
		/**
		 * 获取（树形）组织单位
		 * @param { object } params 
		 * @returns { any }
		 */
		getOrganizationUnitTree: (params?: object): Promise<any> => {
			return request({
				url: '/api/base/organization-unit/tree',
				method: 'get',
				params
			});
		},

		/**
		 * 添加组织单位
		 * @param { object } data 
		 * @returns { any }
		 */
		addOrganizationUnit: (data: object): Promise<boolean> => {
			return request({
				url: '/api/base/organization-unit',
				method: 'post',
				data
			});
		},

		/**
		 * 修改组织单位
		 * @param { string } id 
		 * @param { object } data 
		 * @returns { any }
		 */
		updateOrganizationUnit: (id: string, data: object): Promise<boolean> => {
			return request({
				url: '/api/base/organization-unit/'+id,
				method: 'put',
				data
			});
		},

		/**
		 * 删除菜单
		 * @param { string } id 
		 * @returns { any }
		 */
		deleteOrganizationUnit: (id: string): Promise<boolean> => {
			return request({
				url: '/api/base/organization-unit/' + id,
				method: 'delete'
			});
		}
	};
}
