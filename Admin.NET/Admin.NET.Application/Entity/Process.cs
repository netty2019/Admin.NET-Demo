// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.NET.Application.Entity;

/// <summary>
/// 工序
/// </summary>
[SugarTable("Process", "工序")]
public class Process : EntityTenant
{
    /// <summary>
    /// 编号
    /// </summary>
    [Required]
    [SugarColumn(ColumnName = "Code", ColumnDescription = "编号", Length = 32)]
    public string Code { get; set; }


    /// <summary>
    /// 名称
    /// </summary>
    [Required]
    [SugarColumn(ColumnName = "Name", ColumnDescription = "名称", Length = 80)]
    public string Name { get; set; }


    /// <summary>
    /// 计价方式
    /// </summary>
    [Required]
    [SugarColumn(ColumnName = "PriceMethod", ColumnDescription = "计价方式")]
    public PriceMethodEnum PriceMethod { get; set; } = PriceMethodEnum.Piece;


    /// <summary>
    /// 单价
    /// </summary>
    [SugarColumn(ColumnName = "UnitPrice", ColumnDescription = "单价")]
    public decimal UnitPrice { get; set; }


    /// <summary>
    /// 作业者
    /// </summary>
    [Navigate(NavigateType.OneToMany, nameof(ProcessWorker.ProcessId))]
    public List<ProcessWorker> Workers { get; set; }


    /// <summary>
    /// 不良项目
    /// </summary>
    [Navigate(NavigateType.OneToMany, nameof(ProcessNgItem.ProcessId))]
    public List<ProcessNgItem> NgItems { get; set; }


    /// <summary>
    /// 报工比例
    /// </summary>
    [SugarColumn(ColumnName = "Ratio", ColumnDescription = "报工比例")]
    public decimal Ratio { get; set; } = 1;
}



/// <summary>
/// 计价方式
/// </summary>
[Description("计价方式")]
public enum PriceMethodEnum
{
    /// <summary>
    /// 计件
    /// </summary>
    [Description("计件")]
    Piece = 111,

    /// <summary>
    /// 计时
    /// </summary>
    [Description("计时")]
    Time = 222
}
