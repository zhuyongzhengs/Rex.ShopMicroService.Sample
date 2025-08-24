using Rex.Service.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Rex.GoodService.Forms
{
    /// <summary>
    /// 表单服务接口
    /// </summary>
    public interface IFormAppService : IApplicationService
    {
        /// <summary>
        /// 获取(分页)表单列表
        /// </summary>
        /// <param name="input">查询条件</param>
        /// <returns></returns>
        Task<PagedResultDto<FormDto>> GetPageListAsync(GetFormInput input);

        /// <summary>
        /// 添加表单
        /// </summary>
        /// <param name="input">表单信息</param>
        /// <returns></returns>
        Task CreateAsync(FormSubmitCreateDto input);

        /// <summary>
        /// 修改表单
        /// </summary>
        /// <param name="id">表单ID</param>
        /// <param name="input">表单信息</param>
        /// <returns></returns>
        Task UpdateAsync(Guid id, FormSubmitUpdateDto input);

        /// <summary>
        /// 修改表单是否需要登录
        /// </summary>
        /// <param name="id">表单ID</param>
        /// <param name="isLogin">是否登录</param>
        /// <returns></returns>
        Task UpdateIsLoginAsync(Guid id, bool isLogin);

        /// <summary>
        /// 删除表单
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        Task DeleteAsync(Guid id);

        /// <summary>
        /// 获取表单类型
        /// </summary>
        /// <returns></returns>
        Task<List<EnumEntity>> GetFormTypeAsync();

        /// <summary>
        /// 获取表头类型
        /// </summary>
        /// <returns></returns>
        Task<List<EnumEntity>> GetFormHeadTypeAsync();

        /// <summary>
        /// 获取表单字段类型
        /// </summary>
        /// <returns></returns>
        Task<List<EnumEntity>> GetFormFieldTypeAsync();

        /// <summary>
        /// 获取表单验证类型
        /// </summary>
        /// <returns></returns>
        Task<List<EnumEntity>> GetFormValidationTypeAsync();
    }
}