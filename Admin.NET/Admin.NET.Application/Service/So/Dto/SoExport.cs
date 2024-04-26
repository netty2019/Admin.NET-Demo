﻿// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

using Admin.NET.Application.Entity;
using Magicodes.ExporterAndImporter.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.NET.Application.Service.So.Dto;
public class SoExport
{
    /// <summary>
    /// 编号
    /// </summary>
    [ExporterHeader("订单编号")]
    public string? Code { get; set; }

    /// <summary>
    /// 客户
    /// </summary>
    [ExporterHeader("客户名称")]
    public string? Customer { get; set; }


    /// <summary>
    /// 行号
    /// </summary>
    [ExporterHeader("行号")]
    public int LineNum { get; set; }

    /// <summary>
    /// 产品名称
    /// </summary>
    [ExporterHeader("产品名称")]
    public string Name { get; set; }
}


public class SoExportMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<SoItem, SoExport>()
            .Map(t => t.Code, o => o.So.Code)
            .Map(t => t.Customer, o => o.So.Customer);
    }
}