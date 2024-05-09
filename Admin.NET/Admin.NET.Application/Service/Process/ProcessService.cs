using Admin.NET.Core.Service;
using Admin.NET.Application.Entity;
using Microsoft.AspNetCore.Http;
using Mapster;
namespace Admin.NET.Application;
/// <summary>
/// 工序服务
/// </summary>
[ApiDescriptionSettings(ApplicationConst.GroupName, Order = 100)]
public class ProcessService : IDynamicApiController, ITransient
{
    private readonly SqlSugarRepository<Process> _rep;
    public ProcessService(SqlSugarRepository<Process> rep)
    {
        _rep = rep;
    }

    /// <summary>
    /// 分页查询工序
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "Page")]
    public async Task<SqlSugarPagedList<ProcessOutput>> Page(ProcessInput input)
    {
        var query = _rep.AsQueryable().Includes(p=>p.Workers,x=>x.User).Includes(p=>p.NgItems,x=>x.NgItem)
            .WhereIF(!string.IsNullOrWhiteSpace(input.SearchKey), u =>
                u.Code.Contains(input.SearchKey.Trim())
                || u.Name.Contains(input.SearchKey.Trim())
            )
            .WhereIF(!string.IsNullOrWhiteSpace(input.Code), u => u.Code.Contains(input.Code.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.Name), u => u.Name.Contains(input.Name.Trim()))
            .WhereIF(input.PriceMethod>0, u => u.PriceMethod == input.PriceMethod)
            .Select<Process>();
        var result= await query.OrderBuilder(input).ToPagedListAsync(input.Page, input.PageSize);
        return result.Adapt<SqlSugarPagedList<ProcessOutput>>();
    }

    /// <summary>
    /// 增加工序
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "Add")]
    public async Task<long> Add(AddProcessInput input)
    {
        await UniqueCheck(input.Code);
        var entity = input.Adapt<Process>();
        entity.Workers = input.WorkerIds?.Select(p => new ProcessWorker { UserId = p }).ToList()?? new List<ProcessWorker>();
        entity.NgItems = input.NgItemIds?.Select(p => new ProcessNgItem { NgItemId = p }).ToList() ?? new List<ProcessNgItem>();
        await _rep.AsSugarClient().InsertNav(entity).Include(p => p.Workers).Include(p => p.NgItems).ExecuteCommandAsync();
        return entity.Id;
    }

    /// <summary>
    /// 删除工序
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "Delete")]
    public async Task Delete(DeleteProcessInput input)
    {
        var entity = await _rep.GetFirstAsync(u => u.Id == input.Id) ?? throw Oops.Oh(ErrorCodeEnum.D1002);
        //await _rep.FakeDeleteAsync(entity);   //假删除
        await _rep.AsSugarClient().DeleteNav(entity).Include(p => p.Workers).Include(p => p.NgItems).ExecuteCommandAsync();  //真删除
    }

    /// <summary>
    /// 更新工序
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "Update")]
    public async Task Update(UpdateProcessInput input)
    {
        await UniqueCheck(input.Code,input.Id);
        var entity = input.Adapt<Process>();
        entity.Workers = input.WorkerIds?.Select(p => new ProcessWorker { UserId = p }).ToList() ?? new List<ProcessWorker>();
        entity.NgItems = input.NgItemIds?.Select(p => new ProcessNgItem { NgItemId = p }).ToList() ?? new List<ProcessNgItem>();
        await _rep.AsSugarClient().UpdateNav(entity).Include(p=>p.Workers).Include(p=>p.NgItems).ExecuteCommandAsync();
    }

    /// <summary>
    /// 获取工序
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpGet]
    [ApiDescriptionSettings(Name = "Detail")]
    public async Task<ProcessDto> Detail([FromQuery] QueryByIdProcessInput input)
    {
        var entity = await _rep.AsQueryable().Includes(p => p.Workers).Includes(p => p.NgItems).FirstAsync(u => u.Id == input.Id);
        return entity?.Adapt<ProcessDto>();
    }

    /// <summary>
    /// 获取工序列表
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpGet]
    [ApiDescriptionSettings(Name = "List")]
    public async Task<List<ProcessOutput>> List([FromQuery] ProcessInput input)
    {
        return await _rep.AsQueryable().Select<ProcessOutput>().ToListAsync();
    }


    /// <summary>
    /// 唯一性检查
    /// </summary>
    /// <param name="code"></param>
    /// <param name="expectedId"></param>
    /// <returns></returns>
    private async Task UniqueCheck(string code, long? expectedId = null)
    {
        var entity = await _rep.AsQueryable().FirstAsync(p => p.Code == code);
        if (entity != null && (entity.Id != expectedId))
        {
            throw Oops.Oh($"工序编号‘{code}’已存在");
        }
    }
}

