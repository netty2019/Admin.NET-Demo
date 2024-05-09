using Admin.NET.Core.Service;
using Admin.NET.Application.Entity;
using Microsoft.AspNetCore.Http;
namespace Admin.NET.Application;
/// <summary>
/// 工单优先级服务
/// </summary>
[ApiDescriptionSettings(ApplicationConst.GroupName, Order = 100)]
public class WoPriService : IDynamicApiController, ITransient
{
    private readonly SqlSugarRepository<WoPri> _rep;
    public WoPriService(SqlSugarRepository<WoPri> rep)
    {
        _rep = rep;
    }

    /// <summary>
    /// 分页查询工单优先级
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "Page")]
    public async Task<SqlSugarPagedList<WoPriOutput>> Page(WoPriInput input)
    {
        var query = _rep.AsQueryable()
            .WhereIF(!string.IsNullOrWhiteSpace(input.SearchKey), u =>
                u.Name.Contains(input.SearchKey.Trim())
                || u.Color.Contains(input.SearchKey.Trim())
            )
            .WhereIF(!string.IsNullOrWhiteSpace(input.Name), u => u.Name.Contains(input.Name.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.Color), u => u.Color.Contains(input.Color.Trim()))
            .WhereIF(input.Sort>0, u => u.Sort == input.Sort)
            .Select<WoPriOutput>();
        return await query.OrderBuilder(input).ToPagedListAsync(input.Page, input.PageSize);
    }

    /// <summary>
    /// 增加工单优先级
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "Add")]
    public async Task<long> Add(AddWoPriInput input)
    {
        await UniqueCheck(input.Name);
        var entity = input.Adapt<WoPri>();
        await _rep.InsertAsync(entity);
        return entity.Id;
    }

    /// <summary>
    /// 删除工单优先级
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "Delete")]
    public async Task Delete(DeleteWoPriInput input)
    {
        var entity = await _rep.GetFirstAsync(u => u.Id == input.Id) ?? throw Oops.Oh(ErrorCodeEnum.D1002);
        //await _rep.FakeDeleteAsync(entity);   //假删除
        await _rep.DeleteAsync(entity);   //真删除
    }

    /// <summary>
    /// 更新工单优先级
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "Update")]
    public async Task Update(UpdateWoPriInput input)
    {
        await UniqueCheck(input.Name,input.Id);
        var entity = input.Adapt<WoPri>();
        await _rep.AsUpdateable(entity).IgnoreColumns(ignoreAllNullColumns: true).ExecuteCommandAsync();
    }

    /// <summary>
    /// 获取工单优先级
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpGet]
    [ApiDescriptionSettings(Name = "Detail")]
    public async Task<WoPri> Detail([FromQuery] QueryByIdWoPriInput input)
    {
        return await _rep.GetFirstAsync(u => u.Id == input.Id);
    }

    /// <summary>
    /// 获取工单优先级列表
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpGet]
    [ApiDescriptionSettings(Name = "List")]
    public async Task<List<WoPriOutput>> List([FromQuery] WoPriInput input)
    {
        return await _rep.AsQueryable().OrderBy(p=>p.Sort).Select<WoPriOutput>().ToListAsync();
    }


    /// <summary>
    /// 拖拽排序
    /// </summary>
    /// <param name="oldIdx"></param>
    /// <param name="newIdx"></param>
    /// <returns></returns>
    [HttpGet]
    [ApiDescriptionSettings(Name = "Sort")]
    public async Task Sort([FromQuery] int oldIdx, [FromQuery] int newIdx)
    {
        var list = await _rep.AsQueryable().OrderBy(p => p.Sort).ToListAsync();
        if (oldIdx < 0 || oldIdx >= list.Count || newIdx < 0 || newIdx >= list.Count)
        {
            throw Oops.Oh("排序的索引超出范围");
        }
        var item = list[oldIdx];
        list.RemoveAt(oldIdx);
        list.Insert(newIdx, item);
        for (var i = 0; i < list.Count(); i++)
        {
            list[i].Sort = i;
        }
        await _rep.AsUpdateable(list).UpdateColumns(p => p.Sort).ExecuteCommandAsync();
    }


    /// <summary>
    /// 唯一性检查
    /// </summary>
    /// <param name="name"></param>
    /// <param name="expectedId"></param>
    /// <returns></returns>
    private async Task UniqueCheck(string name, long? expectedId = null)
    {
        var entity = await _rep.AsQueryable().FirstAsync(p => p.Name == name);
        if (entity != null && (entity.Id != expectedId))
        {
            throw Oops.Oh($"优先级‘{name}’已存在");
        }
    }
}

