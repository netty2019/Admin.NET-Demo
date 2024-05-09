using Admin.NET.Core;
using System.ComponentModel.DataAnnotations;

namespace Admin.NET.Application;

    /// <summary>
    /// 不良项目基础输入参数
    /// </summary>
    public class NgItemBaseInput
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
    /// 不良项目分页查询输入参数
    /// </summary>
    public class NgItemInput : BasePageInput
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
    /// 不良项目增加输入参数
    /// </summary>
    public class AddNgItemInput : NgItemBaseInput
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
    /// 不良项目删除输入参数
    /// </summary>
    public class DeleteNgItemInput : BaseIdInput
    {
    }

    /// <summary>
    /// 不良项目更新输入参数
    /// </summary>
    public class UpdateNgItemInput : NgItemBaseInput
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        [Required(ErrorMessage = "主键Id不能为空")]
        public long Id { get; set; }
        
    }

    /// <summary>
    /// 不良项目主键查询输入参数
    /// </summary>
    public class QueryByIdNgItemInput : DeleteNgItemInput
    {

    }
