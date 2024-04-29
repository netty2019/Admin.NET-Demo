using Admin.NET.Core;
namespace Admin.NET.Application.Entity;

/// <summary>
/// 
/// </summary>
[SugarTable("Plant","工厂")]
public class Plant  : EntityBaseData
{
    /// <summary>
    /// 
    /// </summary>
    [SugarColumn(ColumnName = "Code", ColumnDescription = "编号", Length = 32)]
    public string? Code { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    [SugarColumn(ColumnName = "Name", ColumnDescription = "描述", Length = 100)]
    public string? Name { get; set; }


    /// <summary>
    /// 
    /// </summary>
    [SugarColumn(ColumnName = "Test", ColumnDescription = "测试", Length = 100)]
    public string? Test { get; set; }



    /// <summary>
    /// 
    /// </summary>
    [SugarColumn(ColumnName = "Test1", ColumnDescription = "测试1", Length = 100)]
    public string? Test1 { get; set; }

}
