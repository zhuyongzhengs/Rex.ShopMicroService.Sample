import request from '/@/utils/request';

export function usePermissionApi() {
    return {
        /**
         * 查询权限信息
		 * @param { object } params
         * @returns { any }
         */
        getPermissions: (params?: object): Promise<any> => {
			return request({
				url: '/api/permission-management/permissions',
				method: 'get',
                params
			});
		},

		/**
		 * 修改权限信息
		 * @param { object } params 
		 * @param { object } data 
		 * @returns { any }
		 */
		updatePermissions: (params?: object, data?: object): Promise<any> => {
			return request({
				url: '/api/permission-management/permissions',
				method: 'put',
                params,
				data
			});
		}
    };
}