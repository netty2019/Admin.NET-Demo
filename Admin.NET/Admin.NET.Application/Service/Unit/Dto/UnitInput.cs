using Admin.NET.Core;
using System.ComponentModel.DataAnnotations;

namespace Admin.NET.Application;

    /// <summary>
    /// 单位基础输入参数
    /// </summary>
    public class UnitBaseInput
    {
        /// <summary>
        /// 名称
        /// </summary>
        public virtual string? Name { get; set; }
        
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
        public virtual bool? IsDelete { get; set; }
        
    }

    /// <summary>
    /// 单位分页查询输入参数
    /// </summary>
    public class UnitInput : BasePageInput
    {
        /// <summary>
        /// 关键字查询
        /// </summary>
        public string? SearchKey { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string? Name { get; set; }
        
    }

    /// <summary>
    /// 单位增加输入参数
    /// </summary>
    public class AddUnitInput : UnitBaseInput
    {
    }

    /// <summary>
    /// 单位删除输入参数
    /// </summary>
    public class DeleteUnitInput : BaseIdInput
    {
    }

    /// <summary>
    /// 单位更新输入参数
    /// </summary>
    public class UpdateUnitInput : UnitBaseInput
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        [Required(ErrorMessage = "主键Id不能为空")]
        public long Id { get; set; }
        
    }

    /// <summary>
    /// 单位主键查询输入参数
    /// </summary>
    public class QueryByIdUnitInput : DeleteUnitInput
    {

    }
