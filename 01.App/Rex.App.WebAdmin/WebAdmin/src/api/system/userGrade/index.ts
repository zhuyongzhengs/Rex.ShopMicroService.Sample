import request from '/@/utils/request';

export function useUserGradeApi() {
    return {
		/**
         * 查询(所有)用户等级信息
         * @returns { any }
         */
        getUserGradeAll: (): Promise<RowUserGradeType[]> => {
			return request({
				url: '/api/base/user-grade/many',
				method: 'get'
			});
		},

        /**
         * 查询用户等级(分页)列表信息
         * @param { object } params
         * @returns { any }
         */
        getUserGradeList: (params?: object): Promise<DataTableType<RowUserGradeType>> => {
			return request({
				url: '/api/base/user-grade/page-list',
				method: 'get',
                params
			});
		},

        /**
		 * 添加用户等级
		 * @param { object } data 
		 * @returns { any } 
		 */
		addUserGrade: (data: object): Promise<RowUserGradeType> => {
			return request({
				url: '/api/base/user-grade',
				method: 'post',
				data
			});
		},

		/**
		 * 修改用户等级
		 * @param { string } id
		 * @param { object } data 
		 * @returns { any } 
		 */
		updateUserGrade: (id: string, data: object): Promise<RowUserGradeType> => {
			return request({
				url: '/api/base/user-grade/' + id,
				method: 'put',
				data
			});
		},

		/**
		 * 删除用户等级
		 * @param { string } id 
		 * @returns { any } 
		 */
		deleteUserGrade: (id: string): Promise<boolean> => {
			return request({
				url: '/api/base/user-grade/' + id,
				method: 'delete'
			});
		}
    };
}