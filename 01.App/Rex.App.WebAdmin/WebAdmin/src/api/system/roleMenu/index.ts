import request from '/@/utils/request';

/**
 * （不建议写成 request.post(xxx)，因为这样 post 时，无法 params 与 data 同时传参）
 */
export function useRoleMenuApi() {
	return {
		/**
		 * 根据角色ID获取角色菜单
		 * @param { string } roleId 
		 * @returns { any }
		 */
		getRoleMenuByRoleId: (roleId: string): Promise<any> => {
			return request({
				url: '/api/base/role-menu/role-id/' + roleId,
				method: 'get'
			});
		},

		/**
		 * 批量修改角色菜单菜单
		 * @param { object } data 
		 * @returns { any }
		 */
		updateManyRoleMenu: (data: object): Promise<any> => {
			return request({
				url: '/api/base/role-menu/many',
				method: 'put',
				data
			});
		}
	};
}
