using Admin.NET.Application.Entity;
using Admin.NET.Core;
using System.ComponentModel.DataAnnotations;

namespace Admin.NET.Application;

/// <summary>
/// 工序基础输入参数
/// </summary>
public class ProcessBaseInput
{
    /// <summary>
    /// 编号
    /// </summary>
    public virtual string Code { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    public virtual string Name { get; set; }

    /// <summary>
    /// 计价方式
    /// </summary>
    public virtual long PriceMethod { get; set; }

    /// <summary>
    /// 单价
    /// </summary>
    public virtual decimal UnitPrice { get; set; }

    /// <summary>
    /// 作业者
    /// </summary>
    public virtual List<ProcessWorkerInput> Workers { get; set; }

    /// <summary>
    /// 不良项目
    /// </summary>
    public virtual List<ProcessNgItemInput> NgItems { get; set; }

    /// <summary>
    /// 报工比例
    /// </summary>
    public virtual decimal Ratio { get; set; }

    /// <summary>
    /// 租户Id
    /// </summary>
    public virtual long? TenantId { get; set; }

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
    /// 软删除
    /// </summary>
    public virtual bool IsDelete { get; set; }

}

/// <summary>
/// 工序分页查询输入参数
/// </summary>
public class ProcessInput : BasePageInput
{
    /// <summary>
    /// 关键字查询
    /// </summary>
    public string? SearchKey { get; set; }

    /// <summary>
    /// 编号
    /// </summary>
    public string? Code { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// 计价方式
    /// </summary>
    public PriceMethodEnum? PriceMethod { get; set; }
}

/// <summary>
/// 工序增加输入参数
/// </summary>
public class AddProcessInput : ProcessBaseInput
{
    /// <summary>
    /// 编号
    /// </summary>
    [Required(ErrorMessage = "编号不能为空")]
    public override string Code { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    [Required(ErrorMessage = "名称不能为空")]
    public override string Name { get; set; }

    /// <summary>
    /// 计价方式
    /// </summary>
    [Required(ErrorMessage = "计价方式不能为空")]
    public override long PriceMethod { get; set; }

    /// <summary>
    /// 单价
    /// </summary>
    [Required(ErrorMessage = "单价不能为空")]
    public override decimal UnitPrice { get; set; }

    /// <summary>
    /// 报工比例
    /// </summary>
    [Required(ErrorMessage = "报工比例不能为空")]
    public override decimal Ratio { get; set; }

    /// <summary>
    /// 软删除
    /// </summary>
    [Required(ErrorMessage = "软删除不能为空")]
    public override bool IsDelete { get; set; }

}

    /// <summary>
    /// 工序删除输入参数
    /// </summary>
    public class DeleteProcessInput : BaseIdInput
    {
    }

    /// <summary>
    /// 工序更新输入参数
    /// </summary>
    public class UpdateProcessInput : ProcessBaseInput
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        [Required(ErrorMessage = "主键Id不能为空")]
        public long Id { get; set; }
        
    }

    /// <summary>
    /// 工序主键查询输入参数
    /// </summary>
    public class QueryByIdProcessInput : DeleteProcessInput
    {

    }


/// <summary>
/// 工序作业者输入参数
/// </summary>
public class ProcessWorkerInput
{
    /// <summary>
    /// 作业者用户Id
    /// </summary>
    public long UserId { get; set; }
}


/// <summary>
/// 工序不良项目输入参数
/// </summary>
public class ProcessNgItemInput
{
    /// <summary>
    /// 不良项目Id
    /// </summary>
    public long NgItemId { get; set; }
}
