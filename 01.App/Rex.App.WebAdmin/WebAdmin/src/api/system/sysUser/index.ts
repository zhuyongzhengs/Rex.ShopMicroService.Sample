import request from '/@/utils/request';

export function useSysUserApi() {
    return {
        /**
         * 查询[注册]用户(分页)列表信息
         * @param { object } params
         * @returns { any }
         */
        getSysUserList: (params?: object): Promise<DataTableType<RowSysUserType>> => {
			return request({
				url: '/api/base/sys-user/page-list',
				method: 'get',
                params
			});
		},

        /**
		 * 添加[注册]用户
		 * @param { object } data 
		 * @returns { any } 
		 */
		addSysUser: (data: object): Promise<RowSysUserType> => {
			return request({
				url: '/api/base/sys-user',
				method: 'post',
				data
			});
		},

		/**
		 * 修改[注册]用户
		 * @param { string } id
		 * @param { object } data 
		 * @returns { any } 
		 */
		updateSysUser: (id: string, data: object): Promise<RowSysUserType> => {
			return request({
				url: '/api/base/sys-user/' + id,
				method: 'put',
				data
			});
		},

		/**
		 * 删除[注册]用户
		 * @param { string } id 
		 * @returns { any } 
		 */
		deleteSysUser: (id: string): Promise<boolean> => {
			return request({
				url: '/api/base/sys-user/' + id,
				method: 'delete'
			});
		}
    };
}