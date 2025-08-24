using Microsoft.AspNetCore.Authorization;
using Rex.Service.Core.Configurations;
using Rex.Service.Permission.BaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Rex.BaseService.Systems.UserGrades
{
    /// <summary>
    /// 用户等级服务
    /// </summary>
    [RemoteService]
    [Authorize(BaseServicePermissions.UserGrades.Default)]
    public class UserGradeAppService : CrudAppService<UserGrade, UserGradeDto, Guid, PagedAndSortedResultRequestDto, UserGradeCreateDto, UserGradeUpdateDto>, IUserGradeAppService
    {
        private readonly IUserGradeRepository _userGradeRepository;

        public UserGradeAppService(IUserGradeRepository repository) : base(repository)
        {
            _userGradeRepository = repository;
        }

        /// <summary>
        /// 获取(分页)用户等级列表
        /// </summary>
        /// <param name="input">查询条件</param>
        /// <returns></returns>
        public async Task<PagedResultDto<UserGradeDto>> GetPageListAsync(GetUserGradeInput input)
        {
            // 查询数量
            long totalCount = (await _userGradeRepository.GetQueryableAsync())
                .WhereIf(!input.Title.IsNullOrWhiteSpace(), p => p.Title.Contains(input.Title))
                .WhereIf(input.IsDefault.HasValue, p => p.IsDefault == input.IsDefault.Value).LongCount();

            // 查询数据列表
            if (totalCount < 1)
            {
                return new PagedResultDto<UserGradeDto>();
            }
            List<UserGrade> userGradeList = (await _userGradeRepository.GetQueryableAsync())
                    .WhereIf(!input.Title.IsNullOrWhiteSpace(), p => p.Title.Contains(input.Title))
                    .WhereIf(input.IsDefault.HasValue, p => p.IsDefault == input.IsDefault.Value)
                    .OrderByIf<UserGrade, IQueryable<UserGrade>>(!input.Sorting.IsNullOrWhiteSpace(), input.Sorting)
                    .PageBy(input.SkipCount, input.MaxResultCount)
                    .ToList();

            return new PagedResultDto<UserGradeDto>(
                totalCount,
                ObjectMapper.Map<List<UserGrade>, List<UserGradeDto>>(userGradeList)
            );
        }

        /// <summary>
        /// 创建用户等级
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize(BaseServicePermissions.UserGrades.Create)]
        public override async Task<UserGradeDto> CreateAsync(UserGradeCreateDto input)
        {
            UserGrade uGrade = await _userGradeRepository.FindAsync(p => p.Title.Equals(input.Title));
            if (uGrade != null)
            {
                throw new UserFriendlyException($"等级[{input.Title}]已存在，请重新输入！", SystemStatusCode.Fail.ToBaseServicePrefix(), "等级唯一，不允许重复。").WithData("Title", input.Title);
            }
            if (input.IsDefault)
            {
                List<UserGrade> userGradeList = await _userGradeRepository.GetListAsync(p => p.IsDefault);
                foreach (var userGrade in userGradeList)
                {
                    userGrade.IsDefault = false;
                }
            }
            UserGradeDto userGradeDto = await base.CreateAsync(input);
            await CurrentUnitOfWork.SaveChangesAsync();
            return userGradeDto;
        }

        /// <summary>
        /// 修改用户等级
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize(BaseServicePermissions.UserGrades.Update)]
        public override async Task<UserGradeDto> UpdateAsync(Guid id, UserGradeUpdateDto input)
        {
            UserGrade uGrade = await _userGradeRepository.FindAsync(p => p.Title.Equals(input.Title) && p.Id != id);
            if (uGrade != null)
            {
                throw new UserFriendlyException($"等级[{input.Title}]已存在，请重新输入！", SystemStatusCode.Fail.ToBaseServicePrefix(), "等级唯一，不允许重复。").WithData("Title", input.Title);
            }
            if (input.IsDefault)
            {
                List<UserGrade> userGradeList = await _userGradeRepository.GetListAsync(p => p.IsDefault);
                foreach (var userGrade in userGradeList)
                {
                    userGrade.IsDefault = false;
                }
            }
            UserGradeDto userGradeDto = await base.UpdateAsync(id, input);
            await CurrentUnitOfWork.SaveChangesAsync();
            return userGradeDto;
        }

        /// <summary>
        /// 查询用户等级
        /// </summary>
        /// <returns></returns>
        public async Task<List<UserGradeDto>> GetManyAsync()
        {
            List<UserGrade> userGradeList = (await _userGradeRepository.GetQueryableAsync()).OrderByDescending(p => p.CreationTime).ToList();
            return ObjectMapper.Map<List<UserGrade>, List<UserGradeDto>>(userGradeList);
        }

        /// <summary>
        /// 删除用户等级
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        [Authorize(BaseServicePermissions.UserGrades.Delete)]
        public override async Task DeleteAsync(Guid id)
        {
            await base.DeleteAsync(id);
        }
    }
}