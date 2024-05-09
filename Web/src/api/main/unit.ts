import request from '/@/utils/request';
enum Api {
  AddUnit = '/api/unit/add',
  DeleteUnit = '/api/unit/delete',
  UpdateUnit = '/api/unit/update',
  ListUnit = '/api/unit/list',
  DetailUnit = '/api/unit/detail',
  SortUnit = '/api/unit/sort',
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

// 单位列表
export const listUnit = (params?: any) => 
	request({
			url: Api.ListUnit,
			method: 'get',
			data: params,
		});

// 详情单位
export const detailUnit = (id: any) => 
	request({
			url: Api.DetailUnit,
			method: 'get',
			data: { id },
		});


//拖拽排序
export const sortUnit = (oldIdx: any, newIdx:any) => 
	request({
			url: Api.SortUnit,
			method: 'get',
			data: { oldIdx, newIdx},
		});


