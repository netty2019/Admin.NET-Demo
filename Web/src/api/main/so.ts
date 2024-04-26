import request from '/@/utils/request';
enum Api {
  AddSo = '/api/so/add',
  DeleteSo = '/api/so/delete',
  UpdateSo = '/api/so/update',
  PageSo = '/api/so/page',
  DetailSo = '/api/so/detail',
  ExportSo="/api/so/export",
  DownloadTemplate = "/api/so/downloadTemplate",
  ImportSo="/api/so/import",
}

// 增加销售订单
export const addSo = (params?: any) =>
	request({
		url: Api.AddSo,
		method: 'post',
		data: params,
	});

// 删除销售订单
export const deleteSo = (params?: any) => 
	request({
			url: Api.DeleteSo,
			method: 'post',
			data: params,
		});

// 编辑销售订单
export const updateSo = (params?: any) => 
	request({
			url: Api.UpdateSo,
			method: 'post',
			data: params,
		});

// 分页查询销售订单
export const pageSo = (params?: any) => 
	request({
			url: Api.PageSo,
			method: 'post',
			data: params,
		});

// 详情销售订单
export const detailSo = (id: any) => 
	request({
			url: Api.DetailSo,
			method: 'get',
			data: { id },
		});

//导出
export const exportSo = (params?:any) =>
	request({
        url:Api.ExportSo,
		method:'post',
        data:params,
		responseType: 'blob',
	});

//下载导入模板
export const downloadTemplate = () =>
	request({
		url:Api.DownloadTemplate,
		method:'post',
		responseType: 'blob',
	});

//导入销售订单
export const importSo = (file?:Blob) =>
	{
		const formData = new FormData();
		formData.append('file', file as any); 

		return request({
			url:Api.ImportSo,
			method:'post',
			data:formData,
			headers: {
				'Content-Type': 'multipart/form-data',
			},
		})
	}
	