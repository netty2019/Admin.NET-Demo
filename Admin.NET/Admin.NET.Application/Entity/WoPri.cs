using Admin.NET.Core;
namespace Admin.NET.Application.Entity;

/// <summary>
/// 工单优先级
/// </summary>
[SugarTable("WoPri","工单优先级")]
public class WoPri  : EntityTenant
{
    /// <summary>
    /// 名称
    /// </summary>
    [Required]
    [SugarColumn(ColumnName = "Name", ColumnDescription = "名称", Length = 32)]
    public string Name { get; set; }

    /// <summary>
    /// 颜色
    /// </summary>
    [Required]
    [SugarColumn(ColumnName = "Color", ColumnDescription = "颜色", Length = 32)]
    public string Color { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    [SugarColumn(ColumnName = "Sort", ColumnDescription = "排序")]
    public int? Sort { get; set; }
    
}
