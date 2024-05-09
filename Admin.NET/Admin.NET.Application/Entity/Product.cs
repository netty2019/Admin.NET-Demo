﻿// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
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
/// 产品
/// </summary>
[SugarTable("Product", "产品")]
public class Product: EntityTenant
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
    [SugarColumn(ColumnName = "Name", ColumnDescription = "名称", Length = 50)]
    public string Name { get; set; }


    /// <summary>
    /// 规格
    /// </summary>
    [SugarColumn(ColumnName = "Spec", ColumnDescription = "规格", Length = 80)]
    public string Spec { get; set; }


    /// <summary>
    /// 材质
    /// </summary>
    [SugarColumn(ColumnName = "Cz", ColumnDescription = "材质", Length = 32)]
    public string Cz { get; set; }


    /// <summary>
    /// 图号
    /// </summary>
    [SugarColumn(ColumnName = "DrawNo", ColumnDescription = "图号", Length = 32)]
    public string DrawNo { get; set; }


    /// <summary>
    /// 单位
    /// </summary>
    [Required]
    [SugarColumn(ColumnName = "Unit", ColumnDescription = "单位", Length = 32)]
    public string Unit { get; set; }


    [SugarColumn(ColumnName = "RouteId", ColumnDescription = "工艺路线Id")]
    public long RouteId { get; set; }

}