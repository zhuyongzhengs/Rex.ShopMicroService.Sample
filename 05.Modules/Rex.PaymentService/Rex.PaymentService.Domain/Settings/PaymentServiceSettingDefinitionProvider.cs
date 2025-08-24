using EasyAbp.Abp.WeChat.MiniProgram.Options;
using EasyAbp.Abp.WeChat.MiniProgram.Settings;
using EasyAbp.Abp.WeChat.Pay.Options;
using EasyAbp.Abp.WeChat.Pay.Settings;
using Microsoft.Extensions.Options;
using Rex.PaymentService.Localization;
using Volo.Abp.Localization;
using Volo.Abp.Security.Encryption;
using Volo.Abp.Settings;

namespace Rex.PaymentService.Settings;

public class PaymentServiceSettingDefinitionProvider : SettingDefinitionProvider
{
    private readonly IStringEncryptionService _stringEncryptionService;
    private readonly AbpWeChatMiniProgramOptions _miniProgramOptions;
    private readonly AbpWeChatPayOptions _payOptions;

    public PaymentServiceSettingDefinitionProvider(
        IStringEncryptionService stringEncryptionService,
        IOptions<AbpWeChatMiniProgramOptions> miniProgramOptions,
        IOptions<AbpWeChatPayOptions> payOptions)
    {
        _stringEncryptionService = stringEncryptionService;
        _miniProgramOptions = miniProgramOptions.Value;
        _payOptions = payOptions.Value;
    }

    public override void Define(ISettingDefinitionContext context)
    {
        #region 微信小程序设置

        context.Add(new SettingDefinition(
            AbpWeChatMiniProgramSettings.AppId,
            _miniProgramOptions.AppId,
            L($"DisplayName:{AbpWeChatMiniProgramSettings.AppId}"),
            L($"Description:{AbpWeChatMiniProgramSettings.AppId}")
        ));

        context.Add(new SettingDefinition(
            AbpWeChatMiniProgramSettings.AppSecret,
            _stringEncryptionService.Encrypt(_miniProgramOptions.AppSecret),
            L($"DisplayName:{AbpWeChatMiniProgramSettings.AppSecret}"),
            L($"Description:{AbpWeChatMiniProgramSettings.AppSecret}"),
            isEncrypted: true
        ));

        context.Add(new SettingDefinition(
            AbpWeChatMiniProgramSettings.Token,
            _stringEncryptionService.Encrypt(_miniProgramOptions.Token),
            L($"DisplayName:{AbpWeChatMiniProgramSettings.Token}"),
            L($"Description:{AbpWeChatMiniProgramSettings.Token}"),
            isEncrypted: true
        ));

        context.Add(new SettingDefinition(
            AbpWeChatMiniProgramSettings.EncodingAesKey,
            _stringEncryptionService.Encrypt(_miniProgramOptions.EncodingAesKey),
            L($"DisplayName:{AbpWeChatMiniProgramSettings.EncodingAesKey}"),
            L($"Description:{AbpWeChatMiniProgramSettings.EncodingAesKey}"),
            isEncrypted: true
        ));

        #endregion 微信小程序设置

        #region 微信支付设置

        context.Add(new SettingDefinition(
            AbpWeChatPaySettings.MchId,
            _payOptions.MchId,
            L($"DisplayName:{AbpWeChatPaySettings.MchId}"),
            L($"Description:{AbpWeChatPaySettings.MchId}")
        ));

        context.Add(new SettingDefinition(
            AbpWeChatPaySettings.ApiKey,
            _stringEncryptionService.Encrypt(_payOptions.ApiV3Key),
            L($"DisplayName:{AbpWeChatPaySettings.ApiKey}"),
            L($"Description:{AbpWeChatPaySettings.ApiKey}"),
            isEncrypted: true
        ));

        context.Add(new SettingDefinition(
            AbpWeChatPaySettings.IsSandBox,
            _payOptions.IsSandBox.ToString(),
            L($"DisplayName:{AbpWeChatPaySettings.IsSandBox}"),
            L($"Description:{AbpWeChatPaySettings.IsSandBox}")
        ));

        context.Add(new SettingDefinition(
            AbpWeChatPaySettings.NotifyUrl,
            _payOptions.NotifyUrl,
            L($"DisplayName:{AbpWeChatPaySettings.NotifyUrl}"),
            L($"Description:{AbpWeChatPaySettings.NotifyUrl}")
        ));

        context.Add(new SettingDefinition(
            AbpWeChatPaySettings.RefundNotifyUrl,
            _payOptions.RefundNotifyUrl,
            L($"DisplayName:{AbpWeChatPaySettings.RefundNotifyUrl}"),
            L($"Description:{AbpWeChatPaySettings.RefundNotifyUrl}")
        ));

        context.Add(new SettingDefinition(
            AbpWeChatPaySettings.CertificateBlobContainerName,
            _payOptions.CertificateBlobContainerName,
            L($"DisplayName:{AbpWeChatPaySettings.CertificateBlobContainerName}"),
            L($"Description:{AbpWeChatPaySettings.CertificateBlobContainerName}")
        ));

        context.Add(new SettingDefinition(
            AbpWeChatPaySettings.CertificateBlobName,
            _payOptions.CertificateBlobName,
            L($"DisplayName:{AbpWeChatPaySettings.CertificateBlobName}"),
            L($"Description:{AbpWeChatPaySettings.CertificateBlobName}")
        ));

        context.Add(new SettingDefinition(
            AbpWeChatPaySettings.CertificateSecret,
            _stringEncryptionService.Encrypt(_payOptions.CertificateSecret),
            L($"DisplayName:{AbpWeChatPaySettings.CertificateSecret}"),
            L($"Description:{AbpWeChatPaySettings.CertificateSecret}"),
            isEncrypted: true
        ));

        context.Add(new SettingDefinition(
            AbpWeChatPaySettings.AcceptLanguage,
            _payOptions.AcceptLanguage,
            L($"DisplayName:{AbpWeChatPaySettings.AcceptLanguage}"),
            L($"Description:{AbpWeChatPaySettings.AcceptLanguage}")
        ));

        #endregion 微信支付设置
    }

    /// <summary>
    /// 本地化字符串
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<PaymentServiceResource>(name);
    }
}