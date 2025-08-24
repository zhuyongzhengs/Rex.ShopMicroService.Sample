using Microsoft.EntityFrameworkCore;
using Rex.BaseService.EntityFrameworkCore;
using Rex.BaseService.Systems.UserGrades;
using Rex.BaseService.Systems.UserPointLogs;
using Rex.BaseService.Systems.UserWeChats;
using Rex.Service.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Caching;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Rex.BaseService.Systems.SysUsers
{
    /// <summary>
    /// 系统用户仓储
    /// </summary>
    public class SysUserRepository : EfCoreRepository<BaseServiceDbContext, SysUser, Guid>, ISysUserRepository
    {
        public BaseServiceDbContext bServiceDbContext { get; set; }
        public IDistributedCache<CurrentSysUserEto> CurrentSysUserDistributedCache { get; set; }
        private readonly IUserPointLogRepository _userPointLogRepository;
        private readonly IUserGradeRepository _userGradeRepository;
        private readonly IUserWeChatRepository _userWeChatRepository;

        public SysUserRepository(IDbContextProvider<BaseServiceDbContext> dbContextProvider, IUserPointLogRepository userPointLogRepository, IUserGradeRepository userGradeRepository, IUserWeChatRepository userWeChatRepository) : base(dbContextProvider)
        {
            _userPointLogRepository = userPointLogRepository;
            _userGradeRepository = userGradeRepository;
            _userWeChatRepository = userWeChatRepository;
        }

        /// <summary>
        /// 获取(分页) 系统[注册]用户列表总数
        /// </summary>
        /// <param name="name">昵称</param>
        /// <param name="phoneNumber">手机号码</param>
        /// <param name="gender">性别</param>
        /// <param name="gradeId">用户等级</param>
        /// <param name="isActive">是否激活</param>
        /// <param name="beginDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns></returns>
        public async Task<long> GetPageCountAsync(string name = "", string phoneNumber = "", short? gender = null, Guid? gradeId = null, bool? isActive = null, DateTime? beginDate = null, DateTime? endDate = null, CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync())
                .WhereIf(!name.IsNullOrWhiteSpace(), p => p.Name.Contains(name))
                .WhereIf(!phoneNumber.IsNullOrWhiteSpace(), p => p.PhoneNumber.Equals(phoneNumber))
                .WhereIf(gender.HasValue, p => p.Gender == gender)
                .WhereIf(gradeId.HasValue, p => p.GradeId == gradeId)
                .WhereIf(isActive.HasValue, p => p.IsActive == isActive)
                .WhereIf(beginDate.HasValue && endDate.HasValue, p => p.CreationTime >= beginDate.Value && p.CreationTime <= endDate.Value)
                .LongCountAsync(GetCancellationToken(cancellationToken));
        }

        /// <summary>
        /// 获取(分页) 系统[注册]用户列表
        /// </summary>
        /// <param name="skipCount">跳过数</param>
        /// <param name="maxResultCount">最大结果数</param>
        /// <param name="sorting">排序</param>
        /// <param name="name">昵称</param>
        /// <param name="phoneNumber">手机号码</param>
        /// <param name="gender">性别</param>
        /// <param name="gradeId">用户等级</param>
        /// <param name="isActive">是否激活</param>
        /// <param name="beginDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns></returns>
        public async Task<List<SysUser>> GetPageListAsync(int skipCount, int maxResultCount, string sorting, string name = "", string phoneNumber = "", short? gender = null, Guid? gradeId = null, bool? isActive = null, DateTime? beginDate = null, DateTime? endDate = null, CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync())
                    .WhereIf(!name.IsNullOrWhiteSpace(), p => p.Name.Contains(name))
                    .WhereIf(!phoneNumber.IsNullOrWhiteSpace(), p => p.PhoneNumber.Equals(phoneNumber))
                    .WhereIf(gender.HasValue, p => p.Gender == gender)
                    .WhereIf(gradeId.HasValue, p => p.GradeId == gradeId)
                    .WhereIf(isActive.HasValue, p => p.IsActive == isActive)
                    .WhereIf(beginDate.HasValue && endDate.HasValue, p => p.CreationTime >= beginDate.Value && p.CreationTime <= endDate.Value)
                    .Include(p => p.Grade)
                    .OrderByIf<SysUser, IQueryable<SysUser>>(!sorting.IsNullOrWhiteSpace(), sorting)
                    .PageBy(skipCount, maxResultCount)
                    .ToListAsync(GetCancellationToken(cancellationToken));
        }

        /// <summary>
        /// 更新用户积分
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="pointType">积分类型</param>
        /// <param name="point">积分</param>
        /// <param name="remark">备注</param>
        public async Task UpdateUserChangePointAsync(Guid userId, int pointType, int point, string? remark, CancellationToken cancellationToken = default)
        {
            SysUser? sysUser = await (await GetDbSetAsync()).FindAsync(userId, cancellationToken);
            if (sysUser == null) return;
            sysUser.Point = point + sysUser.Point;

            // 插入记录
            UserPointLog uPointLog = new UserPointLog();
            uPointLog.UserId = userId;
            uPointLog.Type = pointType;
            uPointLog.Num = point;
            uPointLog.Balance = sysUser.Point;
            uPointLog.Remark = remark;
            await _userPointLogRepository.InsertAsync(uPointLog, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// 获取当前用户信息
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        public async Task<CurrentSysUserEto> GetCurrentUserAsync(Guid userId)
        {
            string currUserKey = userId.ToString();
            return await CurrentSysUserDistributedCache.GetOrAddAsync(currUserKey, async () =>
            {
                CurrentSysUserEto currSysUserEto = new CurrentSysUserEto();
                SysUser sysUser = await (await GetDbSetAsync()).SingleOrDefaultAsync(x => x.Id == userId);
                if (sysUser != null)
                {
                    currSysUserEto.Id = sysUser.Id;
                    currSysUserEto.TenantId = sysUser.TenantId;
                    currSysUserEto.UserName = sysUser.UserName;
                    currSysUserEto.Name = sysUser.Name;
                    currSysUserEto.Email = sysUser.Email;
                    currSysUserEto.Balance = sysUser.Balance;
                    currSysUserEto.Point = sysUser.Point;
                    currSysUserEto.Gender = sysUser.Gender;
                    currSysUserEto.Avatar = sysUser.Avatar;
                    currSysUserEto.BirthDate = sysUser.BirthDate;
                    if (sysUser.GradeId.HasValue)
                    {
                        UserGrade userGrade = await _userGradeRepository.GetAsync(x => x.Id == sysUser.GradeId);
                        currSysUserEto.Grade = new GradeEto()
                        {
                            Id = userGrade.Id,
                            Title = userGrade.Title
                        };
                    }
                    currSysUserEto.ParentId = sysUser.ParentId;
                }
                UserWeChat userWeChat = await _userWeChatRepository.GetAsync(x => x.UserId == userId);
                if (userWeChat != null)
                {
                    currSysUserEto.NickName = userWeChat.NickName;
                    currSysUserEto.PhoneNumber = userWeChat.Mobile;
                    currSysUserEto.Avatar = userWeChat.Avatar;
                    currSysUserEto.Gender = userWeChat.Gender;
                    currSysUserEto.OpenId = userWeChat.OpenId;
                    currSysUserEto.BirthDate = userWeChat.BirthDate;
                }
                return currSysUserEto;
            });
        }

        /// <summary>
        /// 更新当前用户信息
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        public async Task<CurrentSysUserEto> UpdateCurrentUserAsync(Guid userId)
        {
            string currUserKey = userId.ToString();
            await CurrentSysUserDistributedCache.RemoveAsync(currUserKey);
            return await GetCurrentUserAsync(userId);
        }
    }
}