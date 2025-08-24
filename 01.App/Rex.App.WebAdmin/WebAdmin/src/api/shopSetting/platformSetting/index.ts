import request from '/@/utils/request';

export function usePlatformSettingApi() {
    return {
		/**
		 * 获取平台设置
		 * @returns { any }
		 */
		getPlatformSetting: (): Promise<PlatformSettingType> => {
			return request({
				url: '/api/base/platform-setting',
				method: 'get'
			});
		},
		
        /**
		 * 保存平台设置
		 * @param { object } data 
		 * @returns { any }
		 */
		setPlatformSetting: (data: object): Promise<any> => {
			return request({
				url: '/api/backend/aggregation/common/savePlatformSetting',
				method: 'post',
				data
			});
		}
    };
}