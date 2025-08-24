import request from '/@/utils/request';

/**
 * （不建议写成 request.post(xxx)，因为这样 post 时，无法 params 与 data 同时传参）
 */
export function useMenuApi() {
	return {
		/**
		 * 获取角色菜单
		 * @returns { any }
		 */
		getRoleMenu: (): Promise<any> => {
			return request({
				url: '/api/base/role-menu/tree',
				method: 'get'
			});
		},

		/**
		 * 获取菜单列表(关键字查询)
		 * @param { object } params
		 * @returns { any }
		 */
		getMenuFilter: (params?: object): Promise<RowMenuType[]> => {
			return request({
				url: '/api/base/menu/filter',
				method: 'get',
				params
			});
		},

		/**
		 * 获取树形菜单(用于添加|修改时选择上级菜单)
		 * @returns { any }
		 */
		getMenuTree: (): Promise<RouteItems> => {
			return request({
				url: '/api/base/menu/tree',
				method: 'get'
			});
		},

		/**
		 * 添加菜单
		 * @param { object } data 
		 * @returns { any }
		 */
		addMenu: (data: object): Promise<RowMenuType> => {
			return request({
				url: '/api/base/menu',
				method: 'post',
				data
			});
		},

		/**
		 * 修改菜单
		 * @param { string } id 
		 * @param { object } data 
		 * @returns { any }
		 */
		updateMenu: (id: string, data: object): Promise<RowMenuType> => {
			return request({
				url: '/api/base/menu/' + id,
				method: 'put',
				data
			});
		},

		/**
		 * 删除菜单
		 * @param { string } id  
		 * @returns { any }
		 */
		deleteMenu: (id: string): Promise<boolean> => {
			return request({
				url: '/api/base/menu/' + id,
				method: 'delete'
			});
		}
	};
}
