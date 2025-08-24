import request from '/@/utils/request';

export function useOrganizationUnitRoleApi() {
	return {
		/**
		 * 获取[角色]组织单元
		 * @param { object } params
		 * @returns { any }
		 */
		getOrganizationUnitRoleList: (params?: object): Promise<DataTableType<RowOrganizationUnitRoleType>> => {
			return request({
				url: '/api/base/organization-unit-role',
				method: 'get',
				params
			});
		},

		/**
		 * 获取选择[角色]组织单元
		 * @param { string } organizationUnitId
		 * @param { object } params
		 * @returns { any }
		 */
		getSelectOuRoleList: (organizationUnitId: string, params?: object): Promise<DataTableType<RowRoleType>> => {
			return request({
				url: '/api/base/organization-unit-role/select-role/' + organizationUnitId,
				method: 'get',
				params
			});
		},

		/**
		 * 添加[角色]组织单元
		 * @param { object } data
		 * @returns { any }
		 */
		addOrganizationUnitRole: (data?: object): Promise<boolean> => {
			return request({
				url: '/api/base/organization-unit-role/many',
				method: 'post',
				data
			});
		},

		/**
		 * 删除[角色]组织单元
		 * @param { string } ouId 
		 * @param { string[] } data 
		 * @returns { any } 
		 */
		deleteOrganizationUnitRole: (ouId: string, data: string[]): Promise<boolean> => {
			return request({
				url: '/api/base/organization-unit-role/role-id/' + ouId,
				method: 'delete',
				params: {
					roleIds: data
				}
			});
		}
	};
}
