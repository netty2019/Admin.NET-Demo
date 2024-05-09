using Admin.NET.Core;
using System.ComponentModel.DataAnnotations;

namespace Admin.NET.Application;

/// <summary>
/// 工艺路线基础输入参数
/// </summary>
public class RouteBaseInput
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
    /// 工序
    /// </summary>
    public virtual List<RouteProcessInput> Processes { get; set; }

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
    /// 工艺路线分页查询输入参数
    /// </summary>
    public class RouteInput : BasePageInput
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
        
    }

    /// <summary>
    /// 工艺路线增加输入参数
    /// </summary>
    public class AddRouteInput : RouteBaseInput
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
        /// 软删除
        /// </summary>
        [Required(ErrorMessage = "软删除不能为空")]
        public override bool IsDelete { get; set; }
        
    }

    /// <summary>
    /// 工艺路线删除输入参数
    /// </summary>
    public class DeleteRouteInput : BaseIdInput
    {
    }

    /// <summary>
    /// 工艺路线更新输入参数
    /// </summary>
    public class UpdateRouteInput : RouteBaseInput
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        [Required(ErrorMessage = "主键Id不能为空")]
        public long Id { get; set; }
        
    }

    /// <summary>
    /// 工艺路线主键查询输入参数
    /// </summary>
    public class QueryByIdRouteInput : DeleteRouteInput
    {

    }



/// <summary>
/// 工序路线工序
/// </summary>
public class RouteProcessInput
{
    /// <summary>
    /// 工序Id
    /// </summary>
    public long ProcessId { get; set; }
}