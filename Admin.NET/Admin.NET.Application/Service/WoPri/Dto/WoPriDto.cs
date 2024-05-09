namespace Admin.NET.Application;

    /// <summary>
    /// 工单优先级输出参数
    /// </summary>
    public class WoPriDto
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        public long Id { get; set; }
        
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
        
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// 颜色
        /// </summary>
        public string Color { get; set; }
        
        /// <summary>
        /// 排序
        /// </summary>
        public int? Sort { get; set; }
        
    }
