using Rex.Service.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Rex.BaseService.Systems.SysUsers
{
    /// <summary>
    /// 系统用户仓储接口
    /// </summary>
    public interface ISysUserRepository : IRepository<SysUser, Guid>
    {
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
        Task<long> GetPageCountAsync(
            string name = "",
            string phoneNumber = "",
            Int16? gender = null,
            Guid? gradeId = null,
            bool? isActive = null,
            DateTime? beginDate = null,
            DateTime? endDate = null,
            CancellationToken cancellationToken = default
            );

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
        Task<List<SysUser>> GetPageListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string name = "",
            string phoneNumber = "",
            Int16? gender = null,
            Guid? gradeId = null,
            bool? isActive = null,
            DateTime? beginDate = null,
            DateTime? endDate = null,
            CancellationToken cancellationToken = default
            );

        /// <summary>
        /// 更新用户积分
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="pointType">积分类型</param>
        /// <param name="point">积分</param>
        /// <param name="remark">备注</param>
        Task UpdateUserChangePointAsync(Guid userId, int pointType, int point, string? remark, CancellationToken cancellationToken = default);

        /// <summary>
        /// 获取当前用户信息
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        Task<CurrentSysUserEto> GetCurrentUserAsync(Guid userId);

        /// <summary>
        /// 更新当前用户信息
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        Task<CurrentSysUserEto> UpdateCurrentUserAsync(Guid userId);
    }
}