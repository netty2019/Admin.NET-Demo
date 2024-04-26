namespace Admin.NET.Application;

/// <summary>
/// 销售订单输出参数
/// </summary>
public class SoDto
{
    /// <summary>
    /// 主键Id
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime? CreateTime { get; set; }

    /// <summary>
    /// 更新时间
    /// </summary>
    public DateTime? UpdateTime { get; set; }

    /// <summary>
    /// 创建者Id
    /// </summary>
    public long? CreateUserId { get; set; }

    /// <summary>
    /// 创建者姓名
    /// </summary>
    public string? CreateUserName { get; set; }

    /// <summary>
    /// 修改者Id
    /// </summary>
    public long? UpdateUserId { get; set; }

    /// <summary>
    /// 修改者姓名
    /// </summary>
    public string? UpdateUserName { get; set; }

    /// <summary>
    /// 创建者部门Id
    /// </summary>
    public long? CreateOrgId { get; set; }

    /// <summary>
    /// 创建者部门名称
    /// </summary>
    public string? CreateOrgName { get; set; }

    /// <summary>
    /// 软删除
    /// </summary>
    public bool IsDelete { get; set; }

    /// <summary>
    /// 编号
    /// </summary>
    public string? Code { get; set; }

    /// <summary>
    /// 客户
    /// </summary>
    public string? Customer { get; set; }


    /// <summary>
    /// 销售订单行
    /// </summary>
    public List<SoItemDto> Items { get; set; }
}


public class SoItemDto
{
    public long? Id { get; set; }

    public long SoId { get; set; }

    /// <summary>
    /// 行号
    /// </summary>
    public int LineNum { get; set; }

    /// <summary>
    /// 产品名称
    /// </summary>
    public string Name { get; set; }
}
