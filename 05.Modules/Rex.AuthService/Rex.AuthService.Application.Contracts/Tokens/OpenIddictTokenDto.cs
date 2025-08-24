using System;
using Volo.Abp.Application.Dtos;

namespace Rex.AuthService.Tokens;

/// <summary>
/// 授权Token Dto
/// </summary>
public class OpenIddictTokenDto : EntityDto<Guid>
{
    /// <summary>
    /// 应用程序ID
    /// </summary>
    public Guid? ApplicationId { get; set; }

    /// <summary>
    /// 授权信息ID
    /// </summary>
    public Guid? AuthorizationId { get; set; }

    /// <summary>
    /// Token创建的时间.
    /// </summary>
    public DateTime? CreationDate { get; set; }

    /// <summary>
    /// 当前令牌的(UTC)过期日期。
    /// </summary>
    public DateTime? ExpirationDate { get; set; }

    /// <summary>
    /// 当前令牌的有效负载（如果适用）
    /// </summary>
    /// <remarks>
    /// 注意：此属性仅用于引用令牌，出于安全原因可以被加密
    /// </remarks>
    public string Payload { get; set; }

    /// <summary>
    /// 扩展属性
    /// </summary>
    public string Properties { get; set; }

    /// <summary>
    /// 当前令牌的(UTC)兑换日期
    /// </summary>
    public DateTime? RedemptionDate { get; set; }

    /// <summary>
    /// 关联的引用标识符
    /// </summary>
    /// <remarks>
    /// 该属性仅用于引用令牌，并且出于安全原因可以被散列或加密
    /// </remarks>
    public string ReferenceId { get; set; }

    /// <summary>
    /// 当前令牌的状态
    /// </summary>
    public string Status { get; set; }

    /// <summary>
    /// 当前令牌关联的主题
    /// </summary>
    public string Subject { get; set; }

    /// <summary>
    /// 当前令牌的类型
    /// </summary>
    public string Type { get; set; }
}