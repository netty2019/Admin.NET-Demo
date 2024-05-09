import request from '/@/utils/request';
enum Api {
  AddWoPri = '/api/woPri/add',
  DeleteWoPri = '/api/woPri/delete',
  UpdateWoPri = '/api/woPri/update',
  ListWoPri = '/api/woPri/list',
  DetailWoPri = '/api/woPri/detail',
  SortWoPri = '/api/woPri/sort'
}

// 增加工单优先级
export const addWoPri = (params?: any) =>
	request({
		url: Api.AddWoPri,
		method: 'post',
		data: params,
	});

// 删除工单优先级
export const deleteWoPri = (params?: any) => 
	request({
			url: Api.DeleteWoPri,
			method: 'post',
			data: params,
		});

// 编辑工单优先级
export const updateWoPri = (params?: any) => 
	request({
			url: Api.UpdateWoPri,
			method: 'post',
			data: params,
		});

// 优先级列表
export const listWoPri = (params?: any) => 
	request({
			url: Api.ListWoPri,
			method: 'get',
			data: params,
		});

// 详情工单优先级
export const detailWoPri = (id: any) => 
	request({
			url: Api.DetailWoPri,
			method: 'get',
			data: { id },
		});


//拖拽排序
export const sortWoPri = (oldIdx: any, newIdx:any) => 
	request({
			url: Api.SortWoPri,
			method: 'get',
			data: { oldIdx, newIdx},
		});


