﻿// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

namespace Admin.NET.Core.Service;

/// <summary>
/// 微信小程序服务 🧩
/// </summary>
[ApiDescriptionSettings(Order = 240)]
public class SysWxOpenService : IDynamicApiController, ITransient
{
    private readonly SqlSugarRepository<SysWechatUser> _sysWechatUserRep;
    private readonly SysConfigService _sysConfigService;
    private readonly WechatApiClient _wechatApiClient;

    public SysWxOpenService(SqlSugarRepository<SysWechatUser> sysWechatUserRep,
        SysConfigService sysConfigService,
        WechatApiClientFactory wechatApiClientFactory)
    {
        _sysWechatUserRep = sysWechatUserRep;
        _sysConfigService = sysConfigService;
        _wechatApiClient = wechatApiClientFactory.CreateWxOpenClient();
    }

    /// <summary>
    /// 获取微信用户OpenId 🔖
    /// </summary>
    /// <param name="input"></param>
    [AllowAnonymous]
    [DisplayName("获取微信用户OpenId")]
    public async Task<WxOpenIdOutput> GetWxOpenId([FromQuery] JsCode2SessionInput input)
    {
        var reqJsCode2Session = new SnsJsCode2SessionRequest()
        {
            JsCode = input.JsCode,
        };
        var resCode2Session = await _wechatApiClient.ExecuteSnsJsCode2SessionAsync(reqJsCode2Session);
        if (resCode2Session.ErrorCode != (int)WechatReturnCodeEnum.请求成功)
            throw Oops.Oh(resCode2Session.ErrorMessage + " " + resCode2Session.ErrorCode);

        var wxUser = await _sysWechatUserRep.GetFirstAsync(p => p.OpenId == resCode2Session.OpenId);
        if (wxUser == null)
        {
            wxUser = new SysWechatUser
            {
                OpenId = resCode2Session.OpenId,
                UnionId = resCode2Session.UnionId,
                SessionKey = resCode2Session.SessionKey,
                PlatformType = PlatformTypeEnum.微信小程序
            };
            wxUser = await _sysWechatUserRep.AsInsertable(wxUser).ExecuteReturnEntityAsync();
        }
        else
        {
            await _sysWechatUserRep.AsUpdateable(wxUser).IgnoreColumns(true).ExecuteCommandAsync();
        }

        return new WxOpenIdOutput
        {
            OpenId = resCode2Session.OpenId
        };
    }

    /// <summary>
    /// 获取微信用户电话号码 🔖
    /// </summary>
    /// <param name="input"></param>
    [AllowAnonymous]
    [DisplayName("获取微信用户电话号码")]
    public async Task<WxPhoneOutput> GetWxPhone([FromQuery] WxPhoneInput input)
    {
        var accessToken = await GetCgibinToken();
        var reqUserPhoneNumber = new WxaBusinessGetUserPhoneNumberRequest()
        {
            Code = input.Code,
            AccessToken = accessToken,
        };
        var resUserPhoneNumber = await _wechatApiClient.ExecuteWxaBusinessGetUserPhoneNumberAsync(reqUserPhoneNumber);
        if (resUserPhoneNumber.ErrorCode != (int)WechatReturnCodeEnum.请求成功)
            throw Oops.Oh(resUserPhoneNumber.ErrorMessage + " " + resUserPhoneNumber.ErrorCode);

        return new WxPhoneOutput
        {
            PhoneNumber = resUserPhoneNumber.PhoneInfo?.PhoneNumber
        };
    }

    /// <summary>
    /// 微信小程序登录OpenId 🔖
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [DisplayName("微信小程序登录OpenId")]
    public async Task<dynamic> WxOpenIdLogin(WxOpenIdLoginInput input)
    {
        var wxUser = await _sysWechatUserRep.GetFirstAsync(p => p.OpenId == input.OpenId);
        if (wxUser == null)
            throw Oops.Oh("微信小程序登录失败");

        var tokenExpire = await _sysConfigService.GetTokenExpire();
        return new
        {
            wxUser.Avatar,
            accessToken = JWTEncryption.Encrypt(new Dictionary<string, object>
            {
                { ClaimConst.UserId, wxUser.Id },
                { ClaimConst.RealName, wxUser.NickName },
                { ClaimConst.LoginMode, LoginModeEnum.APP },
            }, tokenExpire)
        };
    }

    /// <summary>
    /// 获取订阅消息模板列表 🔖
    /// </summary>
    [DisplayName("获取订阅消息模板列表")]
    public async Task<dynamic> GetMessageTemplateList()
    {
        var accessToken = await GetCgibinToken();
        var reqTemplate = new WxaApiNewTemplateGetTemplateRequest()
        {
            AccessToken = accessToken
        };
        var resTemplate = await _wechatApiClient.ExecuteWxaApiNewTemplateGetTemplateAsync(reqTemplate);
        if (resTemplate.ErrorCode != (int)WechatReturnCodeEnum.请求成功)
            throw Oops.Oh(resTemplate.ErrorMessage + " " + resTemplate.ErrorCode);

        return resTemplate.TemplateList;
    }

    /// <summary>
    /// 发送订阅消息 🔖
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("发送订阅消息")]
    public async Task<dynamic> SendSubscribeMessage(SendSubscribeMessageInput input)
    {
        var accessToken = await GetCgibinToken();
        var reqMessage = new CgibinMessageSubscribeSendRequest()
        {
            AccessToken = accessToken,
            TemplateId = input.TemplateId,
            ToUserOpenId = input.ToUserOpenId,
            Data = input.Data,
            MiniProgramState = input.MiniprogramState,
            Language = input.Language,
            MiniProgramPagePath = input.MiniProgramPagePath
        };
        var resMessage = await _wechatApiClient.ExecuteCgibinMessageSubscribeSendAsync(reqMessage);
        return resMessage;
    }

    /// <summary>
    /// 增加订阅消息模板 🔖
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [ApiDescriptionSettings(Name = "AddSubscribeMessageTemplate"), HttpPost]
    [DisplayName("增加订阅消息模板")]
    public async Task<dynamic> AddSubscribeMessageTemplate(AddSubscribeMessageTemplateInput input)
    {
        var accessToken = await GetCgibinToken();
        var reqMessage = new WxaApiNewTemplateAddTemplateRequest()
        {
            AccessToken = accessToken,
            TemplateTitleId = input.TemplateTitleId,
            KeyworkIdList = input.KeyworkIdList,
            SceneDescription = input.SceneDescription
        };
        var resTemplate = await _wechatApiClient.ExecuteWxaApiNewTemplateAddTemplateAsync(reqMessage);
        return resTemplate;
    }

    /// <summary>
    /// 获取Access_token
    /// </summary>
    private async Task<string> GetCgibinToken()
    {
        var reqCgibinToken = new CgibinTokenRequest();
        var resCgibinToken = await _wechatApiClient.ExecuteCgibinTokenAsync(reqCgibinToken);
        if (resCgibinToken.ErrorCode != (int)WechatReturnCodeEnum.请求成功)
            throw Oops.Oh(resCgibinToken.ErrorMessage + " " + resCgibinToken.ErrorCode);
        return resCgibinToken.AccessToken;
    }
}