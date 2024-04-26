using Admin.NET.Core;
using System.ComponentModel.DataAnnotations;

namespace Admin.NET.Application;

/// <summary>
/// 销售订单基础输入参数
/// </summary>
public class SoBaseInput
{
    /// <summary>
    /// 创建时间
    /// </summary>
    public virtual DateTime? CreateTime { get; set; }

    /// <summary>
    /// 更新时间
    /// </summary>
    public virtual DateTime? UpdateTime { get; set; }

    /// <summary>
    /// 创建者Id
    /// </summary>
    public virtual long? CreateUserId { get; set; }

    /// <summary>
    /// 创建者姓名
    /// </summary>
    public virtual string? CreateUserName { get; set; }

    /// <summary>
    /// 修改者Id
    /// </summary>
    public virtual long? UpdateUserId { get; set; }

    /// <summary>
    /// 修改者姓名
    /// </summary>
    public virtual string? UpdateUserName { get; set; }

    /// <summary>
    /// 创建者部门Id
    /// </summary>
    public virtual long? CreateOrgId { get; set; }

    /// <summary>
    /// 创建者部门名称
    /// </summary>
    public virtual string? CreateOrgName { get; set; }

    /// <summary>
    /// 软删除
    /// </summary>
    public virtual bool IsDelete { get; set; }

    /// <summary>
    /// 编号
    /// </summary>
    public virtual string? Code { get; set; }

    /// <summary>
    /// 客户
    /// </summary>
    public virtual string? Customer { get; set; }

}

/// <summary>
/// 销售订单分页查询输入参数
/// </summary>
public class SoInput : BasePageInput
{
    /// <summary>
    /// 关键字查询
    /// </summary>
    public string? SearchKey { get; set; }

    /// <summary>
    /// 创建者部门名称
    /// </summary>
    public string? CreateOrgName { get; set; }

    /// <summary>
    /// 编号
    /// </summary>
    public string? Code { get; set; }

    /// <summary>
    /// 客户
    /// </summary>
    public string? Customer { get; set; }

}

/// <summary>
/// 销售订单增加输入参数
/// </summary>
public class AddSoInput : SoBaseInput
{
    /// <summary>
    /// 软删除
    /// </summary>
    [Required(ErrorMessage = "软删除不能为空")]
    public override bool IsDelete { get; set; }

    /// <summary>
    /// 销售订单行
    /// </summary>
    public List<SoItemInput> Items { get; set; }

}

/// <summary>
/// 销售订单删除输入参数
/// </summary>
public class DeleteSoInput : BaseIdInput
{
}

/// <summary>
/// 销售订单更新输入参数
/// </summary>
public class UpdateSoInput : SoBaseInput
{
    /// <summary>
    /// 主键Id
    /// </summary>
    [Required(ErrorMessage = "主键Id不能为空")]
    public long Id { get; set; }


    /// <summary>
    /// 销售订单行
    /// </summary>
    public List<SoItemInput> Items { get; set; }

}

/// <summary>
/// 销售订单主键查询输入参数
/// </summary>
public class QueryByIdSoInput : DeleteSoInput
{

}



public class SoItemInput
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
