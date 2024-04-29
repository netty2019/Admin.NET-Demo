import request from '/@/utils/request';
enum Api {
  AddUnit = '/api/unit/add',
  DeleteUnit = '/api/unit/delete',
  UpdateUnit = '/api/unit/update',
  PageUnit = '/api/unit/page',
  DetailUnit = '/api/unit/detail',
}

// 增加单位
export const addUnit = (params?: any) =>
	request({
		url: Api.AddUnit,
		method: 'post',
		data: params,
	});

// 删除单位
export const deleteUnit = (params?: any) => 
	request({
			url: Api.DeleteUnit,
			method: 'post',
			data: params,
		});

// 编辑单位
export const updateUnit = (params?: any) => 
	request({
			url: Api.UpdateUnit,
			method: 'post',
			data: params,
		});

// 分页查询单位
export const pageUnit = (params?: any) => 
	request({
			url: Api.PageUnit,
			method: 'post',
			data: params,
		});

// 详情单位
export const detailUnit = (id: any) => 
	request({
			url: Api.DetailUnit,
			method: 'get',
			data: { id },
		});


