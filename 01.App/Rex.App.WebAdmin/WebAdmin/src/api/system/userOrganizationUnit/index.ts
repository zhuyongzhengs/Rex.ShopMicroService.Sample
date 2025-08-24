import request from '/@/utils/request';

export function useUserOrganizationUnitApi() {
	return {
		/**
		 * 获取[用户]组织单元
		 * @param { object } params
		 * @returns { any }
		 */
		getUserOrganizationUnitList: (params?: object): Promise<DataTableType<RowUserOrganizationUnitType>> => {
			return request({
				url: '/api/base/user-organization-unit',
				method: 'get',
				params
			});
		},

		/**
         * 选择组织单元用户(分页)列表信息
         * @param { object } params
         * @returns { any }
         */
        getSelectUserOuList: (params?: object): Promise<DataTableType<RowUserType>> => {
			return request({
				url: '/api/base/user-organization-unit/select-user',
				method: 'get',
                params
			});
		},

		/**
		 * 添加[用户]组织单元
		 * @param { object } data
		 * @returns { any }
		 */
		addUserOrganizationUnit: (data?: object): Promise<boolean> => {
			return request({
				url: '/api/base/user-organization-unit/many',
				method: 'post',
				data
			});
		},

		/**
		 * 删除[用户]组织单元
		 * @param { string } ouId 
		 * @param { string[] } data 
		 * @returns { any } 
		 */
		deleteUserOrganizationUnit: (ouId: string, data: string[]): Promise<boolean> => {
			return request({
				url: '/api/base/user-organization-unit/by-ou-id/' + ouId,
				method: 'delete',
				params: {
					userIds: data
				}
			});
		}
	};
}
