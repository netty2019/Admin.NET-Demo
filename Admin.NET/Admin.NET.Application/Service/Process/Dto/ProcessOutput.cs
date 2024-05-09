namespace Admin.NET.Application;

/// <summary>
/// 工序输出参数
/// </summary>
public class ProcessOutput
{
    /// <summary>
    /// 主键Id
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 编号
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 计价方式
    /// </summary>
    public long PriceMethod { get; set; }

    /// <summary>
    /// 单价
    /// </summary>
    public decimal? UnitPrice { get; set; }


    /// <summary>
    /// 作业者
    /// </summary>
    public string Workers { get; set; }

    /// <summary>
    /// 不良项目
    /// </summary>
    public string NgItems { get; set; }

    /// <summary>
    /// 报工比例
    /// </summary>
    public decimal Ratio { get; set; }

    /// <summary>
    /// 租户Id
    /// </summary>
    public long? TenantId { get; set; }

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
    /// 软删除
    /// </summary>
    public bool IsDelete { get; set; }
}
 

