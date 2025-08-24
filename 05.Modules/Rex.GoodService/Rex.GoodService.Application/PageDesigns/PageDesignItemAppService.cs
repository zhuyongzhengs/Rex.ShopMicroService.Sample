using Microsoft.AspNetCore.Authorization;
using Rex.Service.Core.Configurations;
using Rex.Service.Permission.GoodServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Rex.GoodService.PageDesigns
{
    /// <summary>
    /// 页面设计项服务
    /// </summary>
    [RemoteService]
    [Authorize(GoodServicePermissions.PageDesigns.Default)]
    public class PageDesignItemAppService : CrudAppService<PageDesignItem, PageDesignItemDto, Guid, PagedAndSortedResultRequestDto, PageDesignItemCreateDto, PageDesignItemUpdateDto>, IPageDesignItemAppService
    {
        private readonly IPageDesignRepository _pageDesignRepository;
        private readonly IPageDesignItemRepository _pageDesignItemRepository;

        public PageDesignItemAppService(IPageDesignItemRepository repository, IPageDesignRepository pageDesignRepository) : base(repository)
        {
            _pageDesignItemRepository = repository;
            _pageDesignRepository = pageDesignRepository;
        }

        /// <summary>
        /// 获取布局设计数据
        /// </summary>
        /// <param name="pageDesignId">设计ID</param>
        /// <returns></returns>
        public async Task<dynamic> GetLayoutAsync(Guid pageDesignId)
        {
            PageDesign pageDesign = await _pageDesignRepository.GetAsync(pageDesignId);
            if (pageDesign == null)
                throw new UserFriendlyException($"不存在该页面设计(或已被删除)，请检查！", SystemStatusCode.Fail.ToGoodServicePrefix());
            List<PageDesignItem> pageDesignItems = (await _pageDesignItemRepository.GetQueryableAsync()).OrderBy(p => p.Sort).Where(p => p.PageCode.Equals(pageDesign.Code)).ToList();
            string[] noObjArr = { "textarea" };
            var pageConfig = pageDesignItems.Select(p => new
            {
                Type = p.WidgetCode,
                Value = noObjArr.Contains(p.WidgetCode) ? p.Parameters : JsonSerializer.Deserialize<dynamic>(p.Parameters)
            }).ToList();
            return pageConfig;
        }

        /// <summary>
        /// 提交页面设计
        /// </summary>
        /// <param name="pageDesignId">页面设计ID</param>
        /// <param name="pageDesignItems">页面设计项</param>
        /// <returns></returns>
        public async Task SubmitAsync(Guid pageDesignId, List<PageDesignItemDto> pageDesignItems)
        {
            if (!pageDesignItems.Any())
                throw new UserFriendlyException($"您还未设计新的页面布局，请检查！", SystemStatusCode.Fail.ToGoodServicePrefix(), "未设计新的页面布局。");

            PageDesign pageDesign = await _pageDesignRepository.GetAsync(pageDesignId);
            if (pageDesign == null)
                throw new UserFriendlyException($"不存在该页面设计信息，请检查！", SystemStatusCode.Fail.ToGoodServicePrefix(), "不存在该页面设计信息。").WithData("PageDesign.Id", pageDesignId);

            #region 删除原有的页面设计项

            await _pageDesignItemRepository.DeleteAsync(p => p.PageCode == pageDesign.Code);

            #endregion 删除原有的页面设计项

            #region 新增新的页面设计项

            List<PageDesignItem> insertPageDesignItems = ObjectMapper.Map<List<PageDesignItemDto>, List<PageDesignItem>>(pageDesignItems);
            for (int i = 0; i < insertPageDesignItems.Count; i++)
            {
                insertPageDesignItems[i].PageCode = pageDesign.Code;
                insertPageDesignItems[i].PositionId = i;
                insertPageDesignItems[i].Sort = i + 1;
                insertPageDesignItems[i].ConcurrencyStamp = null;
            }
            await _pageDesignItemRepository.InsertManyAsync(insertPageDesignItems);

            #endregion 新增新的页面设计项
        }
    }
}