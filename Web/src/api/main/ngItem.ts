import request from '/@/utils/request';
enum Api {
  AddNgItem = '/api/ngItem/add',
  DeleteNgItem = '/api/ngItem/delete',
  UpdateNgItem = '/api/ngItem/update',
  PageNgItem = '/api/ngItem/page',
  DetailNgItem = '/api/ngItem/detail',
}

// 增加不良项目
export const addNgItem = (params?: any) =>
	request({
		url: Api.AddNgItem,
		method: 'post',
		data: params,
	});

// 删除不良项目
export const deleteNgItem = (params?: any) => 
	request({
			url: Api.DeleteNgItem,
			method: 'post',
			data: params,
		});

// 编辑不良项目
export const updateNgItem = (params?: any) => 
	request({
			url: Api.UpdateNgItem,
			method: 'post',
			data: params,
		});

// 分页查询不良项目
export const pageNgItem = (params?: any) => 
	request({
			url: Api.PageNgItem,
			method: 'post',
			data: params,
		});

// 详情不良项目
export const detailNgItem = (id: any) => 
	request({
			url: Api.DetailNgItem,
			method: 'get',
			data: { id },
		});


