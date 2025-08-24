import request from '/@/utils/request';

export function useUserWeChatApi() {
    return {
        /**
         * 查询微信用户(分页)列表信息
         * @param { object } params
         * @returns { any }
         */
        getUserWeChatList: (params?: object): Promise<DataTableType<RowUserWeChatType>> => {
			return request({
				url: '/api/base/user-we-chat/page-list',
				method: 'get',
                params
			});
		},
        /**
         * 获取第三方登录类型
         * @returns { any }
         */
        getUserAccountTypes: (): Promise<EnumValueType[]> => {
			return request({
				url: '/api/base/user-we-chat/user-account-type',
				method: 'get'
			});
		},
    };
}