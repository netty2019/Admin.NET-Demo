﻿using Admin.NET.Application.Entity;

namespace Admin.NET.Application;

/// <summary>
/// 工序输出参数
/// </summary>
public class ProcessDto
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
    public decimal UnitPrice { get; set; }

    /// <summary>
    /// 作业者
    /// </summary>
    public List<ProcessWorkerDto> Workers { get; set; }


    /// <summary>
    /// 不良项目
    /// </summary>
    public List<ProcessNgItemDto>  NgItems { get; set; }


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


/// <summary>
/// 工序作业者
/// </summary>
public class ProcessWorkerDto
{
    /// <summary>
    /// 作业者用户Id
    /// </summary>
    public long UserId { get; set; }

    /// <summary>
    /// 账户
    /// </summary>
    public string Account { get; set; }


    /// <summary>
    /// 真实姓名
    /// </summary>
    public string RealName { get; set; }
}


/// <summary>
/// 工序不良项目
/// </summary>
public class ProcessNgItemDto
{
    /// <summary>
    /// 不良项目Id
    /// </summary>
    public long NgItemId { get; set; }

    /// <summary>
    /// 不良项目编号
    /// </summary>
    public string Code { get; set; }


    /// <summary>
    /// 不良项目名称
    /// </summary>
    public string Name { get; set; }
}