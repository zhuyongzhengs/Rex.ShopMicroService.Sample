import request from '/@/utils/request';

export function useSecurityLogApi() {
    return {
        /**
         * 查询安全日志(分页)列表信息
         * @param { object } params
         * @returns { any }
         */
        getSecurityLogList: (params?: object): Promise<DataTableType<RowSecurityLogType>> => {
			return request({
				url: '/api/base/security-log',
				method: 'get',
                params
			});
		},
    };
}