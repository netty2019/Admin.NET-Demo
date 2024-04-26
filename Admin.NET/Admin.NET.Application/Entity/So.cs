using Admin.NET.Core;
namespace Admin.NET.Application.Entity;

/// <summary>
/// 
/// </summary>
[SugarTable("So","销售订单")]
public class So  : EntityBaseData
{
    /// <summary>
    /// 
    /// </summary>
    [SugarColumn(ColumnName = "Code", ColumnDescription = "编号", Length = 32)]
    public string? Code { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    [SugarColumn(ColumnName = "Customer", ColumnDescription = "客户", Length = 32)]
    public string? Customer { get; set; }

    [Navigate(NavigateType.OneToMany,nameof(SoItem.SoId))]
    public List<SoItem>  Items { get; set; }
}
