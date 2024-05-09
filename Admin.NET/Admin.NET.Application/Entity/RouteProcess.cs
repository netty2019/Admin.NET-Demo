// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

using SKIT.FlurlHttpClient.Wechat.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.NET.Application.Entity;

/// <summary>
/// 工艺路线工序
/// </summary>
[SugarTable("RouteProcess", "工艺路线工序")]
public class RouteProcess: EntityTenant
{
    /// <summary>
    /// 工艺路线Id
    /// </summary>
    [SugarColumn(ColumnName = "RouteId", ColumnDescription = "工艺路线Id")]
    public long RouteId { get; set; }

    /// <summary>
    /// 工序Id
    /// </summary>
    [SugarColumn(ColumnName = "ProcessId", ColumnDescription = "工序Id")]
    public long ProcessId { get; set; }

    /// <summary>
    /// 工序
    /// </summary>
    [Navigate(NavigateType.OneToOne,nameof(RouteProcess.ProcessId))]
    public Process Process { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    [SugarColumn(ColumnName = "Sort", ColumnDescription = "排序")]
    public int Sort { get; set; }
}
