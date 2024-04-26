using Admin.NET.Core;
using Admin.NET.Application.Entity;

namespace Admin.NET.Application.SeedData;

/// <summary>
///  表种子数据
/// </summary>
public class PlantSeedData: ISqlSugarEntitySeedData<Plant>
{
    /// <summary>
    /// 种子数据
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Plant> HasData()
    {
        string recordsJSON = @"
            [
			  {
			    ""Code"": ""1101"",
			    ""Name"": ""球阀厂"",
			    ""CreateOrgId"": 0,
			    ""CreateOrgName"": """",
			    ""CreateTime"": ""2024-04-19 08:54:32"",
			    ""UpdateTime"": null,
			    ""CreateUserId"": 1300000000101,
			    ""CreateUserName"": ""超级管理员"",
			    ""UpdateUserId"": null,
			    ""UpdateUserName"": null,
			    ""IsDelete"": false,
			    ""Id"": 537898477523013
			  }
			]
        ";
        List<Plant> records = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Plant>>(recordsJSON);
        
        // 后处理数据的特殊字段
		//for (int i = 0; i < records.Count; i++) { }

        return records;
    }
}