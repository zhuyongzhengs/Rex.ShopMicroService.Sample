using Microsoft.AspNetCore.Authorization;
using Rex.Service.Core.Configurations;
using Rex.Service.Core.Helper;
using Rex.Service.Core.Models;
using Rex.Service.Permission.GoodServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Rex.GoodService.Forms
{
    /// <summary>
    /// 表单服务
    /// </summary>
    [RemoteService]
    [Authorize(GoodServicePermissions.Forms.Default)]
    public class FormAppService : ApplicationService, IFormAppService
    {
        private readonly IFormRepository _formRepository;
        public IRepository<FormItem, Guid> FormItemRepository { get; set; }

        public FormAppService(IFormRepository repository)
        {
            _formRepository = repository;
        }

        /// <summary>
        /// 获取(分页)表单列表
        /// </summary>
        /// <param name="input">查询条件</param>
        /// <returns></returns>
        public async Task<PagedResultDto<FormDto>> GetPageListAsync(GetFormInput input)
        {
            // 查询数量
            long totalCount = await _formRepository.GetPageCountAsync(input.Name, input.Type);
            if (totalCount < 1)
            {
                return new PagedResultDto<FormDto>();
            }
            // 查询数据列表
            List<Form> formList = await _formRepository.GetPageListAsync(input.SkipCount, input.MaxResultCount, input.Sorting, input.Name, input.Type);
            foreach (var form in formList)
            {
                form.FormItems = form.FormItems.OrderBy(p => p.Sort).ToList();
            }
            return new PagedResultDto<FormDto>(
                totalCount,
                ObjectMapper.Map<List<Form>, List<FormDto>>(formList)
            );
        }

        /// <summary>
        /// 添加表单
        /// </summary>
        /// <param name="input">表单信息</param>
        /// <returns></returns>
        public async Task CreateAsync(FormSubmitCreateDto input)
        {
            #region 保存表单

            Form form = ObjectMapper.Map<FormCreateDto, Form>(input.Form);
            await _formRepository.InsertAsync(form);

            #endregion 保存表单

            #region 保存表单项

            if (input.FormItems.Any())
            {
                List<FormItem> formItems = ObjectMapper.Map<List<FormItemCreateDto>, List<FormItem>>(input.FormItems);
                formItems.ForEach(item => item.FormId = form.Id);
                await FormItemRepository.InsertManyAsync(formItems);
            }

            #endregion 保存表单项
        }

        /// <summary>
        /// 修改表单
        /// </summary>
        /// <param name="id">表单ID</param>
        /// <param name="input">表单信息</param>
        /// <returns></returns>
        public async Task UpdateAsync(Guid id, FormSubmitUpdateDto input)
        {
            #region 修改表单

            Form form = await _formRepository.GetAsync(id);
            form.Name = input.Form.Name;
            form.Type = input.Form.Type;
            form.Sort = input.Form.Sort;
            form.Images = input.Form.Images;
            form.VideoPath = input.Form.VideoPath;
            form.Description = input.Form.Description;
            form.HeadType = input.Form.HeadType;
            form.HeadTypeValue = input.Form.HeadTypeValue;
            form.HeadTypeVideo = input.Form.HeadTypeVideo;
            form.ButtonName = input.Form.ButtonName;
            form.ButtonColor = input.Form.ButtonColor;
            form.Times = input.Form.Times;
            form.Qrcode = input.Form.Qrcode;
            form.ReturnMsg = input.Form.ReturnMsg;
            form.EndDateTime = input.Form.EndDateTime;

            #endregion 修改表单

            #region 修改表单项

            List<FormItem> formItems = await FormItemRepository.GetListAsync(p => p.FormId == id);

            #region Edit

            List<Guid> editIds = input.FormItems.Where(p => p.Id != null && p.Id != Guid.Empty).Select(p => p.Id.Value).ToList();
            List<FormItem> editFormItems = formItems.Where(p => editIds.Contains(p.Id)).ToList();
            foreach (var editFormItem in editFormItems)
            {
                FormItemUpdateDto formItemUpdate = input.FormItems.Where(p => p.Id == editFormItem.Id).FirstOrDefault();
                if (formItemUpdate == null) continue;
                editFormItem.Name = formItemUpdate.Name;
                editFormItem.Type = formItemUpdate.Type;
                editFormItem.ValidationType = formItemUpdate.ValidationType;
                editFormItem.Value = formItemUpdate.Value;
                editFormItem.DefaultValue = formItemUpdate.DefaultValue;
                editFormItem.FormId = id;
                editFormItem.Required = formItemUpdate.Required;
                editFormItem.Sort = formItemUpdate.Sort;
            }

            #endregion Edit

            #region Delete

            List<FormItem> delFormItems = formItems.Where(p => !editIds.Contains(p.Id)).ToList();
            if (delFormItems.Any())
            {
                await FormItemRepository.DeleteManyAsync(delFormItems);
            }

            #endregion Delete

            #region Create

            List<FormItemUpdateDto> formItemUpdates = input.FormItems.Where(p => p.Id == null || p.Id == Guid.Empty).ToList();
            List<FormItem> formItemCreates = new List<FormItem>();
            foreach (var formItemUpdate in formItemUpdates)
            {
                FormItem formItem = new FormItem();
                formItem.Name = formItemUpdate.Name;
                formItem.Type = formItemUpdate.Type;
                formItem.ValidationType = formItemUpdate.ValidationType;
                formItem.Value = formItemUpdate.Value;
                formItem.DefaultValue = formItemUpdate.DefaultValue;
                formItem.FormId = id;
                formItem.Required = formItemUpdate.Required;
                formItem.Sort = formItemUpdate.Sort;
                formItemCreates.Add(formItem);
            }
            if (formItemCreates.Any()) await FormItemRepository.InsertManyAsync(formItemCreates);

            #endregion Create

            #endregion 修改表单项
        }

        /// <summary>
        /// 修改表单是否需要登录
        /// </summary>
        /// <param name="id">表单ID</param>
        /// <param name="isLogin">是否登录</param>
        /// <returns></returns>
        public async Task UpdateIsLoginAsync(Guid id, bool isLogin)
        {
            Form form = await _formRepository.GetAsync(id);
            if (form != null)
            {
                form.IsLogin = isLogin;
            }
        }

        /// <summary>
        /// 删除表单
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        public async Task DeleteAsync(Guid id)
        {
            await _formRepository.DeleteAsync(id);
        }

        /// <summary>
        /// 获取表单类型
        /// </summary>
        /// <returns></returns>
        public async Task<List<EnumEntity>> GetFormTypeAsync()
        {
            return await Task.FromResult(EnumHelper.EnumToList<GlobalEnums.FormTypes>());
        }

        /// <summary>
        /// 获取表头类型
        /// </summary>
        /// <returns></returns>
        public async Task<List<EnumEntity>> GetFormHeadTypeAsync()
        {
            return await Task.FromResult(EnumHelper.EnumToList<GlobalEnums.FormHeadTypes>());
        }

        /// <summary>
        /// 获取表单字段类型
        /// </summary>
        /// <returns></returns>
        public async Task<List<EnumEntity>> GetFormFieldTypeAsync()
        {
            return await Task.FromResult(EnumHelper.EnumToList<GlobalEnums.FormFieldTypes>());
        }

        /// <summary>
        /// 获取表单验证类型
        /// </summary>
        /// <returns></returns>
        public async Task<List<EnumEntity>> GetFormValidationTypeAsync()
        {
            return await Task.FromResult(EnumHelper.EnumToList<GlobalEnums.FormValidationTypes>());
        }
    }
}