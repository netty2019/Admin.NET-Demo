using Admin.NET.Core.Service;
using Admin.NET.Application.Entity;
using Microsoft.AspNetCore.Http;
namespace Admin.NET.Application;
/// <summary>
/// 不良项目服务
/// </summary>
[ApiDescriptionSettings(ApplicationConst.GroupName, Order = 100)]
public class NgItemService : IDynamicApiController, ITransient
{
    private readonly SqlSugarRepository<NgItem> _rep;
    public NgItemService(SqlSugarRepository<NgItem> rep)
    {
        _rep = rep;
    }

    /// <summary>
    /// 分页查询不良项目
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "Page")]
    public async Task<SqlSugarPagedList<NgItemOutput>> Page(NgItemInput input)
    {
        var query = _rep.AsQueryable()
            .WhereIF(!string.IsNullOrWhiteSpace(input.SearchKey), u =>
                u.Code.Contains(input.SearchKey.Trim())
                || u.Name.Contains(input.SearchKey.Trim())
            )
            .WhereIF(!string.IsNullOrWhiteSpace(input.Code), u => u.Code.Contains(input.Code.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.Name), u => u.Name.Contains(input.Name.Trim()))
            .Select<NgItemOutput>();
        return await query.OrderBuilder(input).ToPagedListAsync(input.Page, input.PageSize);
    }

    /// <summary>
    /// 增加不良项目
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "Add")]
    public async Task<long> Add(AddNgItemInput input)
    {
        await UniqueCheck(input.Code);
        var entity = input.Adapt<NgItem>();
        await _rep.InsertAsync(entity);
        return entity.Id;
    }

    /// <summary>
    /// 删除不良项目
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "Delete")]
    public async Task Delete(DeleteNgItemInput input)
    {
        var entity = await _rep.GetFirstAsync(u => u.Id == input.Id) ?? throw Oops.Oh(ErrorCodeEnum.D1002);
        //await _rep.FakeDeleteAsync(entity);   //假删除
        await _rep.DeleteAsync(entity);   //真删除
    }

    /// <summary>
    /// 更新不良项目
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "Update")]
    public async Task Update(UpdateNgItemInput input)
    {
        await UniqueCheck(input.Code,input.Id);
        var entity = input.Adapt<NgItem>();
        await _rep.AsUpdateable(entity).IgnoreColumns(ignoreAllNullColumns: true).ExecuteCommandAsync();
    }

    /// <summary>
    /// 获取不良项目
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpGet]
    [ApiDescriptionSettings(Name = "Detail")]
    public async Task<NgItem> Detail([FromQuery] QueryByIdNgItemInput input)
    {
        return await _rep.GetFirstAsync(u => u.Id == input.Id);
    }

    /// <summary>
    /// 获取不良项目列表
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpGet]
    [ApiDescriptionSettings(Name = "List")]
    public async Task<List<NgItemOutput>> List([FromQuery] NgItemInput input)
    {
        return await _rep.AsQueryable().Select<NgItemOutput>().ToListAsync();
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
}

