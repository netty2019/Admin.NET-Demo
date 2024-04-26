import request from '/@/utils/request';
enum Api {
  AddPlant = '/api/plant/add',
  DeletePlant = '/api/plant/delete',
  UpdatePlant = '/api/plant/update',
  PagePlant = '/api/plant/page',
  DetailPlant = '/api/plant/detail',
}

// 增加Plant
export const addPlant = (params?: any) =>
	request({
		url: Api.AddPlant,
		method: 'post',
		data: params,
	});

// 删除Plant
export const deletePlant = (params?: any) => 
	request({
			url: Api.DeletePlant,
			method: 'post',
			data: params,
		});

// 编辑Plant
export const updatePlant = (params?: any) => 
	request({
			url: Api.UpdatePlant,
			method: 'post',
			data: params,
		});

// 分页查询Plant
export const pagePlant = (params?: any) => 
	request({
			url: Api.PagePlant,
			method: 'post',
			data: params,
		});

// 详情Plant
export const detailPlant = (id: any) => 
	request({
			url: Api.DetailPlant,
			method: 'get',
			data: { id },
		});


