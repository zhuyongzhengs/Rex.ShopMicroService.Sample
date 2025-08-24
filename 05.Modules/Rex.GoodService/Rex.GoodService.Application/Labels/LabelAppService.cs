using Microsoft.AspNetCore.Authorization;
using Rex.Service.Core.Configurations;
using Rex.Service.Permission.GoodServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Rex.GoodService.Labels
{
    /// <summary>
    /// 标签服务
    /// </summary>
    [RemoteService]
    [Authorize(GoodServicePermissions.Goods.Default)]
    public class LabelAppService : CrudAppService<Label, LabelDto, Guid, PagedAndSortedResultRequestDto, LabelCreateDto, LabelUpdateDto>, ILabelAppService
    {
        private readonly ILabelRepository _labelRepository;

        public LabelAppService(ILabelRepository repository) : base(repository)
        {
            _labelRepository = repository;
        }

        /// <summary>
        /// 新增标签
        /// </summary>
        /// <returns></returns>
        [Authorize(GoodServicePermissions.Goods.Create)]
        public override async Task<LabelDto> CreateAsync(LabelCreateDto input)
        {
            Label label = await _labelRepository.FindAsync(p => p.Name.Equals(input.Name));
            if (label != null)
            {
                throw new UserFriendlyException($"标签名称[{input.Name}]已存在，请重新输入！", SystemStatusCode.Fail.ToGoodServicePrefix(), "标签名称唯一，不允许重复。").WithData("Name", input.Name);
            }
            return await base.CreateAsync(input);
        }

        /// <summary>
        /// 修改标签
        /// </summary>
        /// <returns></returns>
        [Authorize(GoodServicePermissions.Goods.Update)]
        public override async Task<LabelDto> UpdateAsync(Guid id, LabelUpdateDto input)
        {
            Label label = await _labelRepository.FindAsync(p => p.Name.Equals(input.Name) && p.Id != id);
            if (label != null)
            {
                throw new UserFriendlyException($"标签名称[{input.Name}]已存在，请重新输入！", SystemStatusCode.Fail.ToGoodServicePrefix(), "标签名称唯一，不允许重复。").WithData("Name", input.Name);
            }
            return await base.UpdateAsync(id, input);
        }

        /// <summary>
        /// 根据标签名称查询标签
        /// </summary>
        /// <param name="names">标签名称</param>
        /// <returns></returns>
        public async Task<List<LabelDto>> GetGoodLabelByNameAsync(string[] names)
        {
            List<Label> labels = await _labelRepository.GetListAsync(p => names.Contains(p.Name));
            return ObjectMapper.Map<List<Label>, List<LabelDto>>(labels);
        }

        /// <summary>
        /// 根据标签名称查询标签
        /// </summary>
        /// <param name="ids">ID</param>
        /// <returns></returns>
        public async Task<List<LabelDto>> GetManyByIdsAsync(Guid[] ids)
        {
            List<Label> labels = await _labelRepository.GetListAsync(p => ids.Contains(p.Id));
            return ObjectMapper.Map<List<Label>, List<LabelDto>>(labels);
        }
    }
}