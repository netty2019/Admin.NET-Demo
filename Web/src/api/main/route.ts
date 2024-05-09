import request from '/@/utils/request';
enum Api {
  AddRoute = '/api/route/add',
  DeleteRoute = '/api/route/delete',
  UpdateRoute = '/api/route/update',
  PageRoute = '/api/route/page',
  DetailRoute = '/api/route/detail',
}

// 增加工艺路线
export const addRoute = (params?: any) =>
	request({
		url: Api.AddRoute,
		method: 'post',
		data: params,
	});

// 删除工艺路线
export const deleteRoute = (params?: any) => 
	request({
			url: Api.DeleteRoute,
			method: 'post',
			data: params,
		});

// 编辑工艺路线
export const updateRoute = (params?: any) => 
	request({
			url: Api.UpdateRoute,
			method: 'post',
			data: params,
		});

// 分页查询工艺路线
export const pageRoute = (params?: any) => 
	request({
			url: Api.PageRoute,
			method: 'post',
			data: params,
		});

// 详情工艺路线
export const detailRoute = (id: any) => 
	request({
			url: Api.DetailRoute,
			method: 'get',
			data: { id },
		});


