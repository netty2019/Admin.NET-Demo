using Admin.NET.Core.Service;
using Admin.NET.Application.Entity;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
namespace Admin.NET.Application;
/// <summary>
/// 单位服务
/// </summary>
[ApiDescriptionSettings(ApplicationConst.GroupName, Order = 100)]
public class UnitService : IDynamicApiController, ITransient
{
    private readonly SqlSugarRepository<Unit> _rep;
    public UnitService(SqlSugarRepository<Unit> rep)
    {
        _rep = rep;
    }

    /// <summary>
    /// 分页查询单位
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "Page")]
    public async Task<SqlSugarPagedList<UnitOutput>> Page(UnitInput input)
    {
        var query = _rep.AsQueryable()
            .WhereIF(!string.IsNullOrWhiteSpace(input.SearchKey), u =>
                u.Name.Contains(input.SearchKey.Trim())
            )
            .WhereIF(!string.IsNullOrWhiteSpace(input.Name), u => u.Name.Contains(input.Name.Trim()))
            .OrderBy(p=>p.Sort)
            .Select<UnitOutput>();
        return await query.OrderBuilder(input).ToPagedListAsync(input.Page, input.PageSize);
    }

    /// <summary>
    /// 增加单位
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "Add")]
    public async Task<long> Add(AddUnitInput input)
    {
        var entity = input.Adapt<Unit>();
        var maxSort = _rep.AsQueryable().Max(p => p.Sort);
        entity.Sort = maxSort + 1;
        await _rep.InsertAsync(entity);
        return entity.Id;
    }

    /// <summary>
    /// 删除单位
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "Delete")]
    public async Task Delete(DeleteUnitInput input)
    {
        var entity = await _rep.GetFirstAsync(u => u.Id == input.Id) ?? throw Oops.Oh(ErrorCodeEnum.D1002);
       // await _rep.FakeDeleteAsync(entity);   //假删除
        await _rep.DeleteAsync(entity);   //真删除
    }

    /// <summary>
    /// 更新单位
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "Update")]
    public async Task Update(UpdateUnitInput input)
    {
        var entity = input.Adapt<Unit>();
        await _rep.AsUpdateable(entity).IgnoreColumns(ignoreAllNullColumns: true).ExecuteCommandAsync();
    }

    /// <summary>
    /// 获取单位
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpGet]
    [ApiDescriptionSettings(Name = "Detail")]
    public async Task<Unit> Detail([FromQuery] QueryByIdUnitInput input)
    {
        return await _rep.GetFirstAsync(u => u.Id == input.Id);
    }

    /// <summary>
    /// 获取单位列表
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpGet]
    [ApiDescriptionSettings(Name = "List")]
    public async Task<List<UnitOutput>> List([FromQuery] UnitInput input)
    {
        return await _rep.AsQueryable().OrderBy(p=>p.Sort).Select<UnitOutput>().ToListAsync();
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
            Oops.Oh("排序的索引超出范围");
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
}

