using Admin.NET.Core.Service;
using Admin.NET.Application.Entity;
using Microsoft.AspNetCore.Http;
using Mapster;
using OfficeOpenXml.ConditionalFormatting.Contracts;
using Magicodes.ExporterAndImporter.Excel;
using System.IO;
using Admin.NET.Application.Service.So.Dto;
namespace Admin.NET.Application;
/// <summary>
/// 销售订单服务
/// </summary>
[ApiDescriptionSettings(ApplicationConst.GroupName, Order = 100)]
public class SoService : IDynamicApiController, ITransient
{
    private readonly SqlSugarRepository<So> _rep;
    public SoService(SqlSugarRepository<So> rep)
    {
        _rep = rep;
    }

    /// <summary>
    /// 分页查询销售订单
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "Page")]
    public async Task<SqlSugarPagedList<SoOutput>> Page(SoInput input)
    {
        var query = _rep.AsQueryable().Includes(p=>p.Items)
            .WhereIF(!string.IsNullOrWhiteSpace(input.SearchKey), u =>
                u.Code.Contains(input.SearchKey.Trim())
                || u.Customer.Contains(input.SearchKey.Trim())
                || u.CreateOrgName.Contains(input.SearchKey.Trim())
            )
            .WhereIF(!string.IsNullOrWhiteSpace(input.Code), u => u.Code.Contains(input.Code.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.Customer), u => u.Customer.Contains(input.Customer.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.CreateOrgName), u => u.CreateOrgName.Contains(input.CreateOrgName.Trim()))
            .Select<So>();
        var result =await query.OrderBuilder(input).ToPagedListAsync(input.Page, input.PageSize);
        return result.Adapt<SqlSugarPagedList<SoOutput>>();
    }

    /// <summary>
    /// 增加销售订单
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "Add")]
    public async Task<long> Add(AddSoInput input)
    {
        var entity = input.Adapt<So>();
        await _rep.AsSugarClient().InsertNav(entity).Include(p=>p.Items).ExecuteCommandAsync();
        //throw Oops.Oh("异常测试");
        return entity.Id;
    }

    /// <summary>
    /// 删除销售订单
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "Delete")]
    public async Task Delete(DeleteSoInput input)
    {
        var entity = await _rep.GetFirstAsync(u => u.Id == input.Id) ?? throw Oops.Oh(ErrorCodeEnum.D1002);
       // await _rep.FakeDeleteAsync(entity);   //假删除
        await _rep.AsSugarClient().DeleteNav(entity).Include(p=>p.Items).ExecuteCommandAsync();   //真删除
    }

    /// <summary>
    /// 更新销售订单
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "Update")]
    public async Task Update(UpdateSoInput input)
    {
        var entity = input.Adapt<So>();
        await _rep.AsSugarClient()
            .UpdateNav(entity, new UpdateNavRootOptions
            {
                IsIgnoreAllNullColumns = true
            })
            .Include(p => p.Items, new SqlSugar.UpdateNavOptions
            {
                IgnoreNullColumns = true,
                OneToManyInsertOrUpdate = true,
            })
            .ExecuteCommandAsync();
    }

    /// <summary>
    /// 获取销售订单
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpGet]
    [ApiDescriptionSettings(Name = "Detail")]
    public async Task<SoDto> Detail([FromQuery] QueryByIdSoInput input)
    {
        var entity= await _rep.AsQueryable().Includes(p=>p.Items).FirstAsync(u => u.Id == input.Id);
        return entity.Adapt<SoDto>();
    }

    /// <summary>
    /// 获取销售订单列表
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpGet]
    [ApiDescriptionSettings(Name = "List")]
    public async Task<List<SoOutput>> List([FromQuery] SoInput input)
    {
        var list= await _rep.AsQueryable().Includes(p=>p.Items).ToListAsync();
        return list.Adapt<List<SoOutput>>();
    }

    /// <summary>
    /// 下载Excle导入模板
    /// </summary>
    /// <returns></returns>
    [ApiDescriptionSettings(Name = "DownloadTemplate"), NonUnify]
    public  Task<IActionResult> DownloadTemplate()
    {
       return  CommonUtil.ExportExcelTemplate("销售订单导入模板", new SoImport());
    }


    /// <summary>
    /// 导入
    /// </summary>
    /// <param name="file"></param>
    /// <returns></returns>
    [ApiDescriptionSettings(Name = "Import")]
    public async Task Import([Required] IFormFile file)
    {
        var list = (await CommonUtil.ImportExcelData(file, new SoImport()));
    }


    [ApiDescriptionSettings(Name ="Export"),NonUnify]
    public async Task<IActionResult> Export(SoInput input)
    {
        var list =(await _rep.ChangeRepository<SqlSugarRepository<SoItem>>().AsQueryable().Includes(p => p.So).ToListAsync()).Adapt<List<SoExport>>();
        IExcelExporter excelExporter = new ExcelExporter();
        var res = await excelExporter.ExportAsByteArray(list);
        return new FileStreamResult(new MemoryStream(res), "application/octet-stream") { FileDownloadName = DateTime.Now.ToString("yyyyMMddHHmm") + "销售订单导出.xlsx" };
    }
}

