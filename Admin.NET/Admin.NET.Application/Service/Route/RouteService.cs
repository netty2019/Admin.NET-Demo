using Admin.NET.Core.Service;
using Admin.NET.Application.Entity;
using Microsoft.AspNetCore.Http;
namespace Admin.NET.Application;
/// <summary>
/// 工艺路线服务
/// </summary>
[ApiDescriptionSettings(ApplicationConst.GroupName, Order = 100)]
public class RouteService : IDynamicApiController, ITransient
{
    private readonly SqlSugarRepository<Route> _rep;
    public RouteService(SqlSugarRepository<Route> rep)
    {
        _rep = rep;
    }

    /// <summary>
    /// 分页查询工艺路线
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "Page")]
    public async Task<SqlSugarPagedList<RouteOutput>> Page(RouteInput input)
    {
        var query = _rep.AsQueryable().Includes(p=>p.Processes,x=>x.Process)
            .WhereIF(!string.IsNullOrWhiteSpace(input.SearchKey), u =>
                u.Code.Contains(input.SearchKey.Trim())
                || u.Name.Contains(input.SearchKey.Trim())
            )
            .WhereIF(!string.IsNullOrWhiteSpace(input.Code), u => u.Code.Contains(input.Code.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.Name), u => u.Name.Contains(input.Name.Trim()))
            .Select<Route>();
        var result= await query.OrderBuilder(input).ToPagedListAsync(input.Page, input.PageSize);
        return result.Adapt<SqlSugarPagedList<RouteOutput>>();
    }

    /// <summary>
    /// 增加工艺路线
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "Add")]
    public async Task<long> Add(AddRouteInput input)
    {
        await UniqueCheck(input.Code);
        var entity = input.Adapt<Route>();
        UpdateSort(entity);
        await _rep.AsSugarClient().InsertNav(entity).Include(p => p.Processes).ExecuteCommandAsync();
        return entity.Id;
    }

    /// <summary>
    /// 删除工艺路线
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "Delete")]
    public async Task Delete(DeleteRouteInput input)
    {
        var entity = await _rep.GetFirstAsync(u => u.Id == input.Id) ?? throw Oops.Oh(ErrorCodeEnum.D1002);
        //await _rep.FakeDeleteAsync(entity);   //假删除
        await _rep.AsSugarClient().DeleteNav(entity).Include(p=>p.Processes).ExecuteCommandAsync();   //真删除
    }

    /// <summary>
    /// 更新工艺路线
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "Update")]
    public async Task Update(UpdateRouteInput input)
    {
        await UniqueCheck(input.Code,input.Id);
        var entity = input.Adapt<Route>();
        UpdateSort(entity);
        await _rep.AsSugarClient().UpdateNav(entity).Include(p=>p.Processes).ExecuteCommandAsync();
    }

    /// <summary>
    /// 获取工艺路线
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpGet]
    [ApiDescriptionSettings(Name = "Detail")]
    public async Task<RouteDto> Detail([FromQuery] QueryByIdRouteInput input)
    {
        return (await _rep.AsQueryable().Includes(p=>p.Processes,x=>x.Process).FirstAsync(u => u.Id == input.Id)).Adapt<RouteDto>();
    }

    /// <summary>
    /// 获取工艺路线列表
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpGet]
    [ApiDescriptionSettings(Name = "List")]
    public async Task<List<RouteOutput>> List([FromQuery] RouteInput input)
    {
        return await _rep.AsQueryable().Select<RouteOutput>().ToListAsync();
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
            throw Oops.Oh($"不良项目编号‘{code}’已存在");
        }
    }

    
    /// <summary>
    /// 更新工序的排序值
    /// </summary>
    /// <param name="route"></param>
    private void UpdateSort(Route route)
    {
        if (route.Processes != null)
        {
            for (var i = 0; i < route.Processes.Count; i++)
            {
                route.Processes[i].Sort = i;
            }
        }
    }
}

