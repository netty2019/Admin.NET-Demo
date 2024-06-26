﻿// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

namespace Admin.NET.Core.Service;

/// <summary>
/// 微信支付服务 🧩
/// </summary>
[ApiDescriptionSettings(Order = 210)]
public class SysWechatPayService : IDynamicApiController, ITransient
{
    private readonly SqlSugarRepository<SysWechatPay> _sysWechatPayUserRep;

    private readonly WechatPayOptions _wechatPayOptions;
    private readonly PayCallBackOptions _payCallBackOptions;

    private readonly WechatTenpayClient _wechatTenpayClient;

    public SysWechatPayService(SqlSugarRepository<SysWechatPay> sysWechatPayUserRep,
        IOptions<WechatPayOptions> wechatPayOptions,
        IOptions<PayCallBackOptions> payCallBackOptions)
    {
        _sysWechatPayUserRep = sysWechatPayUserRep;
        _wechatPayOptions = wechatPayOptions.Value;
        _payCallBackOptions = payCallBackOptions.Value;

        _wechatTenpayClient = CreateTenpayClient();
    }

    /// <summary>
    /// 初始化微信支付客户端
    /// </summary>
    /// <returns></returns>
    private WechatTenpayClient CreateTenpayClient()
    {
        var cerFilePath = App.WebHostEnvironment.ContentRootPath + _wechatPayOptions.MerchantCertificatePrivateKey;

        var tenpayClientOptions = new WechatTenpayClientOptions()
        {
            MerchantId = _wechatPayOptions.MerchantId,
            MerchantV3Secret = _wechatPayOptions.MerchantV3Secret,
            MerchantCertificateSerialNumber = _wechatPayOptions.MerchantCertificateSerialNumber,
            MerchantCertificatePrivateKey = File.Exists(cerFilePath) ? File.ReadAllText(cerFilePath) : "",
            PlatformCertificateManager = new InMemoryCertificateManager()
        };
        return new WechatTenpayClient(tenpayClientOptions);
    }

    /// <summary>
    /// 生成JSAPI调起支付所需参数 🔖
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("生成JSAPI调起支付所需参数")]
    public dynamic GenerateParametersForJsapiPay(WechatPayParaInput input)
    {
        return _wechatTenpayClient.GenerateParametersForJsapiPayRequest(_wechatPayOptions.AppId, input.PrepayId);
    }

    /// <summary>
    /// 微信支付统一下单获取Id(商户直连) 🔖
    /// </summary>
    [DisplayName("微信支付统一下单获取Id(商户直连)")]
    public async Task<dynamic> CreatePayTransaction([FromBody] WechatPayTransactionInput input)
    {
        var request = new CreatePayTransactionJsapiRequest()
        {
            OutTradeNumber = DateTimeOffset.Now.ToString("yyyyMMddHHmmssfff") + (new Random()).Next(100, 1000), // 订单号
            AppId = _wechatPayOptions.AppId,
            Description = input.Description,
            Attachment = input.Attachment,
            GoodsTag = input.GoodsTag,
            ExpireTime = DateTimeOffset.Now.AddMinutes(10),
            NotifyUrl = _payCallBackOptions.WechatPayUrl,
            Amount = new CreatePayTransactionJsapiRequest.Types.Amount() { Total = input.Total },
            Payer = new CreatePayTransactionJsapiRequest.Types.Payer() { OpenId = input.OpenId }
        };
        var response = await _wechatTenpayClient.ExecuteCreatePayTransactionJsapiAsync(request);
        if (!response.IsSuccessful())
            throw Oops.Oh(response.ErrorMessage);

        // 保存订单信息
        var wechatPay = new SysWechatPay()
        {
            AppId = _wechatPayOptions.AppId,
            MerchantId = _wechatPayOptions.MerchantId,
            OutTradeNumber = request.OutTradeNumber,
            Description = input.Description,
            Attachment = input.Attachment,
            GoodsTag = input.GoodsTag,
            Total = input.Total,
            OpenId = input.OpenId,
            TransactionId = ""
        };
        await _sysWechatPayUserRep.InsertAsync(wechatPay);

        return new
        {
            response.PrepayId,
            request.OutTradeNumber
        };
    }

    /// <summary>
    /// 微信支付统一下单获取Id(服务商模式) 🔖
    /// </summary>
    [DisplayName("微信支付统一下单获取Id(服务商模式)")]
    public async Task<dynamic> CreatePayPartnerTransaction([FromBody] WechatPayTransactionInput input)
    {
        var request = new CreatePayPartnerTransactionJsapiRequest()
        {
            OutTradeNumber = DateTimeOffset.Now.ToString("yyyyMMddHHmmssfff") + (new Random()).Next(100, 1000), // 订单号
            AppId = _wechatPayOptions.AppId,
            MerchantId = _wechatPayOptions.MerchantId,
            SubAppId = _wechatPayOptions.AppId,
            SubMerchantId = _wechatPayOptions.MerchantId,
            Description = input.Description,
            Attachment = input.Attachment,
            GoodsTag = input.GoodsTag,
            ExpireTime = DateTimeOffset.Now.AddMinutes(10),
            NotifyUrl = _payCallBackOptions.WechatPayUrl,
            Amount = new CreatePayPartnerTransactionJsapiRequest.Types.Amount() { Total = input.Total },
            Payer = new CreatePayPartnerTransactionJsapiRequest.Types.Payer() { OpenId = input.OpenId }
        };
        var response = await _wechatTenpayClient.ExecuteCreatePayPartnerTransactionJsapiAsync(request);
        if (!response.IsSuccessful())
            throw Oops.Oh(response.ErrorMessage);

        // 保存订单信息
        var wechatPay = new SysWechatPay()
        {
            AppId = _wechatPayOptions.AppId,
            MerchantId = _wechatPayOptions.MerchantId,
            SubAppId = _wechatPayOptions.AppId,
            SubMerchantId = _wechatPayOptions.MerchantId,
            OutTradeNumber = request.OutTradeNumber,
            Description = input.Description,
            Attachment = input.Attachment,
            GoodsTag = input.GoodsTag,
            Total = input.Total,
            OpenId = input.OpenId,
            TransactionId = ""
        };
        await _sysWechatPayUserRep.InsertAsync(wechatPay);

        return new
        {
            response.PrepayId,
            request.OutTradeNumber
        };
    }

    /// <summary>
    /// 获取支付订单详情 🔖
    /// </summary>
    /// <param name="tradeId"></param>
    /// <returns></returns>
    [DisplayName("获取支付订单详情")]
    public async Task<SysWechatPay> GetPayInfo(string tradeId)
    {
        return await _sysWechatPayUserRep.GetFirstAsync(u => u.OutTradeNumber == tradeId);
    }

    /// <summary>
    /// 微信支付成功回调(商户直连) 🔖
    /// </summary>
    /// <returns></returns>
    [AllowAnonymous]
    [DisplayName("微信支付成功回调(商户直连)")]
    public async Task<WechatPayOutput> PayCallBack()
    {
        using var ms = new MemoryStream();
        await App.HttpContext.Request.Body.CopyToAsync(ms);
        var b = ms.ToArray();
        var callbackJson = Encoding.UTF8.GetString(b);

        var callbackModel = _wechatTenpayClient.DeserializeEvent(callbackJson);
        if ("TRANSACTION.SUCCESS".Equals(callbackModel.EventType))
        {
            var callbackResource = _wechatTenpayClient.DecryptEventResource<TransactionResource>(callbackModel);

            // 修改订单支付状态
            var wechatPay = await _sysWechatPayUserRep.GetFirstAsync(u => u.OutTradeNumber == callbackResource.OutTradeNumber
                && u.MerchantId == callbackResource.MerchantId);
            if (wechatPay == null) return null;
            //wechatPay.OpenId = callbackResource.Payer.OpenId; // 支付者标识
            //wechatPay.MerchantId = callbackResource.MerchantId; // 微信商户号
            //wechatPay.OutTradeNumber = callbackResource.OutTradeNumber; // 商户订单号
            wechatPay.TransactionId = callbackResource.TransactionId; // 支付订单号
            wechatPay.TradeType = callbackResource.TradeType; // 交易类型
            wechatPay.TradeState = callbackResource.TradeState; // 交易状态
            wechatPay.TradeStateDescription = callbackResource.TradeStateDescription; // 交易状态描述
            wechatPay.BankType = callbackResource.BankType; // 付款银行类型
            wechatPay.Total = callbackResource.Amount.Total; // 订单总金额
            wechatPay.PayerTotal = callbackResource.Amount.PayerTotal; // 用户支付金额
            wechatPay.SuccessTime = callbackResource.SuccessTime; // 支付完成时间

            await _sysWechatPayUserRep.AsUpdateable(wechatPay).IgnoreColumns(true).ExecuteCommandAsync();

            return new WechatPayOutput()
            {
                Total = wechatPay.Total,
                Attachment = wechatPay.Attachment,
                GoodsTag = wechatPay.GoodsTag
            };
        }

        return null;
    }

    /// <summary>
    /// 微信支付成功回调(服务商模式) 🔖
    /// </summary>
    /// <returns></returns>
    [AllowAnonymous]
    [DisplayName("微信支付成功回调(服务商模式)")]
    public async Task PayPartnerCallBack()
    {
        using var ms = new MemoryStream();
        await App.HttpContext.Request.Body.CopyToAsync(ms);
        var b = ms.ToArray();
        var callbackJson = Encoding.UTF8.GetString(b);

        var callbackModel = _wechatTenpayClient.DeserializeEvent(callbackJson);
        if ("TRANSACTION.SUCCESS".Equals(callbackModel.EventType))
        {
            var callbackResource = _wechatTenpayClient.DecryptEventResource<PartnerTransactionResource>(callbackModel);

            // 修改订单支付状态
            var wechatPay = await _sysWechatPayUserRep.GetFirstAsync(u => u.OutTradeNumber == callbackResource.OutTradeNumber
                && u.MerchantId == callbackResource.MerchantId);
            if (wechatPay == null) return;
            //wechatPay.OpenId = callbackResource.Payer.OpenId; // 支付者标识
            //wechatPay.MerchantId = callbackResource.MerchantId; // 微信商户号
            //wechatPay.OutTradeNumber = callbackResource.OutTradeNumber; // 商户订单号
            wechatPay.TransactionId = callbackResource.TransactionId; // 支付订单号
            wechatPay.TradeType = callbackResource.TradeType; // 交易类型
            wechatPay.TradeState = callbackResource.TradeState; // 交易状态
            wechatPay.TradeStateDescription = callbackResource.TradeStateDescription; // 交易状态描述
            wechatPay.BankType = callbackResource.BankType; // 付款银行类型
            wechatPay.Total = callbackResource.Amount.Total; // 订单总金额
            wechatPay.PayerTotal = callbackResource.Amount.PayerTotal; // 用户支付金额
            wechatPay.SuccessTime = callbackResource.SuccessTime; // 支付完成时间

            await _sysWechatPayUserRep.AsUpdateable(wechatPay).IgnoreColumns(true).ExecuteCommandAsync();
        }
    }
}