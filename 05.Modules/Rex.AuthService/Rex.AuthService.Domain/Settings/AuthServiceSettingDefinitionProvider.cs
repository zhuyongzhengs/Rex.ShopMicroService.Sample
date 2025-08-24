using EasyAbp.Abp.WeChat.MiniProgram.Options;
using EasyAbp.Abp.WeChat.MiniProgram.Settings;
using Microsoft.Extensions.Options;
using Rex.AuthService.Localization;
using Rex.Service.Setting.BaseServices;
using Volo.Abp.Localization;
using Volo.Abp.Security.Encryption;
using Volo.Abp.Settings;

namespace Rex.AuthService.Settings;

public class AuthServiceSettingDefinitionProvider : SettingDefinitionProvider
{
    private readonly IStringEncryptionService _stringEncryptionService;
    private readonly AbpWeChatMiniProgramOptions _options;

    public AuthServiceSettingDefinitionProvider(
        IStringEncryptionService stringEncryptionService,
        IOptions<AbpWeChatMiniProgramOptions> options)
    {
        _stringEncryptionService = stringEncryptionService;
        _options = options.Value;
    }

    public override void Define(ISettingDefinitionContext context)
    {
        #region 平台设置

        context.AddBasePlatformSettingDefine(val => L(val));

        #endregion 平台设置

        #region 微信小程序设置

        context.Add(new SettingDefinition(
            AbpWeChatMiniProgramSettings.AppId,
            _options.AppId,
            L($"DisplayName:{AbpWeChatMiniProgramSettings.AppId}"),
            L($"Description:{AbpWeChatMiniProgramSettings.AppId}")
        ));

        context.Add(new SettingDefinition(
            AbpWeChatMiniProgramSettings.AppSecret,
            _stringEncryptionService.Encrypt(_options.AppSecret),
            L($"DisplayName:{AbpWeChatMiniProgramSettings.AppSecret}"),
            L($"Description:{AbpWeChatMiniProgramSettings.AppSecret}"),
            isEncrypted: true
        ));

        context.Add(new SettingDefinition(
            AbpWeChatMiniProgramSettings.Token,
            _stringEncryptionService.Encrypt(_options.Token),
            L($"DisplayName:{AbpWeChatMiniProgramSettings.Token}"),
            L($"Description:{AbpWeChatMiniProgramSettings.Token}"),
            isEncrypted: true
        ));

        context.Add(new SettingDefinition(
            AbpWeChatMiniProgramSettings.EncodingAesKey,
            _stringEncryptionService.Encrypt(_options.EncodingAesKey),
            L($"DisplayName:{AbpWeChatMiniProgramSettings.EncodingAesKey}"),
            L($"Description:{AbpWeChatMiniProgramSettings.EncodingAesKey}"),
            isEncrypted: true
        ));

        #endregion 微信小程序设置
    }

    /// <summary>
    /// 本地化字符串
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<AuthServiceResource>(name);
    }
}