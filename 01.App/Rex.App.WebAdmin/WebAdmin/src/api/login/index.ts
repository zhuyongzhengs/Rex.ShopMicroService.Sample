import request from '/@/utils/request';

/**
 * （不建议写成 request.post(xxx)，因为这样 post 时，无法 params 与 data 同时传参）
 *
 * 登录api接口集合
 * @method signIn 用户登录
 * @method getApplicationConfig 获取应用程序配置
 * @method getCurrentSysUser 获取用户信息
 */
export function useLoginApi() {
	return {
		signIn: (data: object) => {
			let url = '/connect/token';
			return request({
				url,
				method: 'post',
				headers: {
					'Content-Type': 'application/x-www-form-urlencoded'
				},
				data
			});
		},
		getCurrentSysUser: () => {
			return request({
				url: '/connect/userinfo',
				method: 'get'
			});
		},
		getApplicationConfig: () => {
			return request({
				url: '/api/abp/application-configuration',
				method: 'get'
			});
		}
	};
}
