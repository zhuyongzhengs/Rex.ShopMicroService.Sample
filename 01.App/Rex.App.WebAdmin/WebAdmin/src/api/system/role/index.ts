import request from '/@/utils/request';

export function useRoleApi() {
    return {
        /**
         * 查询所有的角色信息
         * @returns { any }
         */
        getRoleAll: (): Promise<any> => {
			return request({
				url: '/api/identity/roles/all',
				method: 'get'
			});
		},

        /**
         * 查询角色(分页)列表信息
         * @param { object } params
         * @returns { any }
         */
        getRoleList: (params?: object): Promise<DataTableType<RowRoleType>> => {
			return request({
				url: '/api/identity/roles',
				method: 'get',
                params
			});
		},

        /**
		 * 添加角色
		 * @param { object } data
		 * @returns { any }
		 */
		addRole: (data: object): Promise<RowRoleType> => {
			return request({
				url: '/api/identity/roles',
				method: 'post',
				data
			});
		},

		/**
		 * 修改角色
		 * @param { string } id 
		 * @param { object } data 
		 * @returns { any }
		 */
		updateRole: (id: string, data: object): Promise<RowRoleType> => {
			return request({
				url: '/api/identity/roles/' + id,
				method: 'put',
				data
			});
		},

		/**
		 * 删除角色
		 * @param { string } id 
		 * @returns { any }
		 */
		deleteRole: (id: string): Promise<boolean> => {
			return request({
				url: '/api/identity/roles/' + id,
				method: 'delete'
			});
		}
    };
}