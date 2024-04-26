using Admin.NET.Core;
using System.ComponentModel.DataAnnotations;

namespace Admin.NET.Application;

    /// <summary>
    /// Plant基础输入参数
    /// </summary>
    public class PlantBaseInput
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
        /// 
        /// </summary>
        public virtual string? Code { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public virtual string? Name { get; set; }
        
    }

    /// <summary>
    /// Plant分页查询输入参数
    /// </summary>
    public class PlantInput : BasePageInput
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
        /// 
        /// </summary>
        public string? Code { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string? Name { get; set; }
        
    }

    /// <summary>
    /// Plant增加输入参数
    /// </summary>
    public class AddPlantInput : PlantBaseInput
    {
        /// <summary>
        /// 软删除
        /// </summary>
        [Required(ErrorMessage = "软删除不能为空")]
        public override bool IsDelete { get; set; }
        
    }

    /// <summary>
    /// Plant删除输入参数
    /// </summary>
    public class DeletePlantInput : BaseIdInput
    {
    }

    /// <summary>
    /// Plant更新输入参数
    /// </summary>
    public class UpdatePlantInput : PlantBaseInput
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        [Required(ErrorMessage = "主键Id不能为空")]
        public long Id { get; set; }
        
    }

    /// <summary>
    /// Plant主键查询输入参数
    /// </summary>
    public class QueryByIdPlantInput : DeletePlantInput
    {

    }
