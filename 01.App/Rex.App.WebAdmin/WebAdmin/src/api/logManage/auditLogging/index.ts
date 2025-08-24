import request from '/@/utils/request';

export function useAuditLoggingApi() {
    return {
        /**
         * 查询审计日志(分页)列表信息
         * @param { object } params
         * @returns { any }
         */
        getAuditLoggingList: (params?: object): Promise<DataTableType<RowAuditLoggingType>> => {
			return request({
				url: '/api/base/audit-logging',
				method: 'get',
                params
			});
		},
    };
}