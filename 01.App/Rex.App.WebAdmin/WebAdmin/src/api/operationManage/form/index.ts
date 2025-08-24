import request from '/@/utils/request';

export function useFormApi() {
    return {
        /**
         * 查询表单(分页)列表信息
         * @param { object } params
         * @returns { any }
         */
        getFormList: (params?: object): Promise<DataTableType<RowFormType>> => {
			return request({
				url: '/api/good/form/page-list',
				method: 'get',
                params
			});
		},

		/**
         * 获取表单类型
         * @returns { any }
         */
        getFormTypes: (): Promise<EnumValueType[]> => {
			return request({
				url: '/api/good/form/form-type',
				method: 'get'
			});
		},

		/**
         * 获取表头类型
         * @returns { any }
         */
        getFormHeadTypes: (): Promise<EnumValueType[]> => {
			return request({
				url: '/api/good/form/form-head-type',
				method: 'get'
			});
		},

		/**
         * 获取表单字段类型
         * @returns { any }
         */
        getFormFieldTypes: (): Promise<EnumValueType[]> => {
			return request({
				url: '/api/good/form/form-field-type',
				method: 'get'
			});
		},

		/**
         * 获取表单字段类型
         * @returns { any }
         */
        getFormValidationTypes: (): Promise<EnumValueType[]> => {
			return request({
				url: '/api/good/form/form-validation-type',
				method: 'get'
			});
		},

        /**
		 * 添加表单
		 * @param { object } data 
		 * @returns { any }
		 */
		addForm: (data: object): Promise<boolean> => {
			return request({
				url: '/api/good/form',
				method: 'post',
				data
			});
		},

		/**
		 * 修改表单
		 * @param { string } id
		 * @param { object } data
		 * @returns { any }
		 */
		updateForm: (id: string, data: object): Promise<boolean> => {
			return request({
				url: '/api/good/form/' + id,
				method: 'put',
				data
			});
		},

		/**
		 * 修改表单是否登录
		 * @param { string } id
		 * @param { boolean } isLogin
		 * @returns { any }
		 */
		updateFormIsLogin: (id: string, isLogin: boolean): Promise<boolean> => {
			return request({
				url: '/api/good/form/isLogin/' + id + '/' + isLogin,
				method: 'put'
			});
		},

		/**
		 * 删除表单
		 * @param { string } id 
		 * @returns { any }
		 */
		deleteForm: (id: string): Promise<boolean> => {
			return request({
				url: '/api/good/form/' + id,
				method: 'delete'
			});
		}
    };
}