import request from '/@/utils/request';
enum Api {
  AddProcess = '/api/process/add',
  DeleteProcess = '/api/process/delete',
  UpdateProcess = '/api/process/update',
  PageProcess = '/api/process/page',
  DetailProcess = '/api/process/detail',
}

// 增加工序
export const addProcess = (params?: any) =>
	request({
		url: Api.AddProcess,
		method: 'post',
		data: params,
	});

// 删除工序
export const deleteProcess = (params?: any) => 
	request({
			url: Api.DeleteProcess,
			method: 'post',
			data: params,
		});

// 编辑工序
export const updateProcess = (params?: any) => 
	request({
			url: Api.UpdateProcess,
			method: 'post',
			data: params,
		});

// 分页查询工序
export const pageProcess = (params?: any) => 
	request({
			url: Api.PageProcess,
			method: 'post',
			data: params,
		});

// 详情工序
export const detailProcess = (id: any) => 
	request({
			url: Api.DetailProcess,
			method: 'get',
			data: { id },
		});


