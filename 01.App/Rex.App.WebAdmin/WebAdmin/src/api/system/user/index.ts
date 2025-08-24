import request from '/@/utils/request';

export function useUserApi() {
    return {
        /**
         * 查询用户(分页)列表信息
         * @param { object } params
         * @returns { any }
         */
        getUserList: (params?: object): Promise<DataTableType<RowUserType>> => {
			return request({
				url: '/api/base/admin-user/page-list',
				method: 'get',
                params
			});
		},

        /**
         * 查询用户角色信息
         * @param { string } id
         * @returns { any }
         */
        getUserRoles: (id: string): Promise<any> => {
			return request({
				url: `/api/identity/users/${ id }/roles`,
				method: 'get'
			});
		},

        /**
		 * 添加用户
		 * @param { object } data 
		 * @returns { any } 
		 */
		addUser: (data: object): Promise<any> => {
			return request({
				url: '/api/identity/users',
				method: 'post',
				data
			});
		},

		/**
		 * 修改用户
		 * @param { string } id
		 * @param { object } data 
		 * @returns { any } 
		 */
		updateUser: (id: string, data: object): Promise<any> => {
			return request({
				url: '/api/identity/users/' + id,
				method: 'put',
				data
			});
		},

		/**
		 * 删除用户
		 * @param { string } id
		 * @returns { any } 
		 */
		deleteUser: (id: string): Promise<boolean> => {
			return request({
				url: '/api/identity/users/' + id,
				method: 'delete'
			});
		}
    };
}