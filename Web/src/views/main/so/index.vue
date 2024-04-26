<template>
  <div class="so-container">
    <el-card shadow="hover" :body-style="{ paddingBottom: '0' }"> 
      <el-form :model="queryParams" ref="queryForm" labelWidth="90">
        <el-row>
          <el-col :xs="24" :sm="12" :md="12" :lg="8" :xl="4" class="mb10">
            <el-form-item label="关键字">
              <el-input v-model="queryParams.searchKey" clearable="" placeholder="请输入模糊查询关键字"/>
              
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="12" :md="12" :lg="8" :xl="4" class="mb10" v-if="showAdvanceQueryUI">
            <el-form-item label="编号">
              <el-input v-model="queryParams.code" clearable="" placeholder="请输入编号"/>
              
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="12" :md="12" :lg="8" :xl="4" class="mb10" v-if="showAdvanceQueryUI">
            <el-form-item label="客户">
              <el-input v-model="queryParams.customer" clearable="" placeholder="请输入客户"/>
              
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="12" :md="12" :lg="8" :xl="4" class="mb10" v-if="showAdvanceQueryUI">
            <el-form-item label="创建者部门名称">
              <el-input v-model="queryParams.createOrgName" clearable="" placeholder="请输入创建者部门名称"/>
              
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="12" :md="12" :lg="6" :xl="6" class="mb10">
            <el-form-item>
              <el-button-group style="display: flex; align-items: center;">
                <el-button type="primary"  icon="ele-Search" @click="handleQuery" v-auth="'so:page'"> 查询 </el-button>
                      <el-button icon="ele-Refresh" @click="() => queryParams = {}"> 重置 </el-button>
                        <el-button icon="ele-ZoomIn" @click="changeAdvanceQueryUI" v-if="!showAdvanceQueryUI" style="margin-left:5px;"> 高级查询 </el-button>
                        <el-button icon="ele-ZoomOut" @click="changeAdvanceQueryUI" v-if="showAdvanceQueryUI" style="margin-left:5px;"> 隐藏 </el-button>
                <el-button type="primary" style="margin-left:5px;" icon="ele-Plus" @click="openAddSo" v-auth="'so:add'"> 新增 </el-button>

						     <el-button icon="ele-UploadFilled" style="margin-left:5px;" v-auth="'so:import'"  @click="dlgImportVisible=true" >导入</el-button>
                 <el-button icon="ele-FolderOpened" style="margin-left: 5px;" v-auth="'so:export'" @click="soExport" > 导出 </el-button>
                
              </el-button-group>
            </el-form-item>
            
          </el-col>
        </el-row>
      </el-form>
    </el-card>

    <el-row :gutter="8" style="width: 100%; height: 100%; flex: 1">
      <!-- 头表 -->
			<el-col :span="12" :xs="24" style="display: flex; height: 100%; flex: 1">
        <el-card class="full-table" shadow="hover" style="margin-top: 5px">
          <el-table
            :data="tableData"
            style="width: 100%"
            v-loading="loading"
            tooltip-effect="light"
                            row-key="id"
                    @row-click="rowClick"
                    @sort-change="sortChange"
            border="">
            <el-table-column type="index" label="序号" width="55" align="center"/>
            <el-table-column prop="code" label="编号"  show-overflow-tooltip="" />
            <el-table-column prop="customer" label="客户"  show-overflow-tooltip="" />
            <el-table-column prop="createOrgName" label="创建者部门名称"  show-overflow-tooltip="" />
            <el-table-column label="操作" width="140" align="center" fixed="right" show-overflow-tooltip="" v-if="auth('so:update') || auth('so:delete')">
              <template #default="scope">
                <el-button icon="ele-Edit" size="small" text="" type="primary" @click="openEditSo(scope.row)" v-auth="'so:update'"> 编辑 </el-button>
                <el-button icon="ele-Delete" size="small" text="" type="primary" @click="delSo(scope.row)" v-auth="'so:delete'"> 删除 </el-button>
              </template>
            </el-table-column>
          </el-table>
          <el-pagination
            v-model:currentPage="tableParams.page"
            v-model:page-size="tableParams.pageSize"
            :total="tableParams.total"
            :page-sizes="[10, 20, 50, 100, 200, 500]"
            small=""
            background=""
            @size-change="handleSizeChange"
            @current-change="handleCurrentChange"
            layout="total, sizes, prev, pager, next, jumper"
      />
          <printDialog
            ref="printDialogRef"
            :title="printSoTitle"
            @reloadTable="handleQuery" />
          <editDialog
            ref="editDialogRef"
            :title="editSoTitle"
            @reloadTable="handleQuery"
          />
        </el-card>
     </el-col>
     <!-- 行表 -->
     <el-col :span="12" :xs="24" style="display: flex; height: 100%; flex: 1">
        <el-card class="full-table" shadow="hover" style="margin-top: 5px">
          <el-table
            :data="selectRow.items"
            style="width: 100%"
            v-loading="loading"
            tooltip-effect="light"
                            row-key="id"
            border="">
            <el-table-column prop="lineNum" label="行号"  show-overflow-tooltip="" />
            <el-table-column prop="name" label="物料"  show-overflow-tooltip="" />
          </el-table>
        </el-card>
     </el-col>

   </el-row>
   <el-dialog  
      v-model="dlgImportVisible"
      width="30%" draggable="">
      <template #header>
				<div style="color: #fff">
					<span>导入</span>
				</div>
			</template>
      <el-link type="primary" @click="soDownloadTemplate">下载导入模板</el-link>
      <el-upload  ref="soImportRef" drag action=""   	:limit="1"
						:show-file-list="false"
						:auto-upload="false" :on-change="soImport" style="margin-top: 5px;">
           <el-icon class="el-icon--upload">
            <upload-filled />
           </el-icon>
              <div class="el-upload__text">
                 拖拽文件或<em>点击上传</em>
               </div>
          <template #tip>
            <div class="el-upload__tip">
              只支持.xlsx格式文件
            </div>
          </template>
        </el-upload>
    </el-dialog> 
  </div>
</template>

<script lang="ts" setup="" name="so">
  import { ref } from "vue";
  import { ElMessageBox, ElMessage } from "element-plus";
  import { auth } from '/@/utils/authFunction';
  import { getDictDataItem as di, getDictDataList as dl } from '/@/utils/dict-utils';
  import { formatDate } from '/@/utils/formatTime';

  import type { UploadInstance} from 'element-plus';

  import { UploadFilled } from '@element-plus/icons-vue'
  import printDialog from '/@/views/system/print/component/hiprint/preview.vue'
  import editDialog from '/@/views/main/so/component/editDialog.vue'
  import { pageSo, deleteSo ,exportSo,downloadTemplate,importSo} from '/@/api/main/so';
  
  import { downloadByData, getFileName } from '/@/utils/download';

  const showAdvanceQueryUI = ref(false);
  const printDialogRef = ref();
  const editDialogRef = ref();
  const loading = ref(false);
  const tableData = ref<any>([]);
  const selectRow = ref<any>({});
  const queryParams = ref<any>({});
  const tableParams = ref({
    page: 1,
    pageSize: 10,
    total: 0,
  });

  const printSoTitle = ref("");
  const editSoTitle = ref("");

  const dlgImportVisible = ref(false);

  // 改变高级查询的控件显示状态
  const changeAdvanceQueryUI = () => {
    showAdvanceQueryUI.value = !showAdvanceQueryUI.value;
  }
  

  // 查询操作
  const handleQuery = async () => {
    loading.value = true;
    var res = await pageSo(Object.assign(queryParams.value, tableParams.value));
    tableData.value = res.data.result?.items ?? [];
    tableParams.value.total = res.data.result?.total;
    loading.value = false;
  };

  // 列排序
  const sortChange = async (column: any) => {
	queryParams.value.field = column.prop;
	queryParams.value.order = column.order;
	await handleQuery();
  };

  // 打开新增页面
  const openAddSo = () => {
    editSoTitle.value = '添加销售订单';
    editDialogRef.value.openDialog({});
  };

  // 打开打印页面
  const openPrintSo = async (row: any) => {
    printSoTitle.value = '打印销售订单';
  }
  
  // 打开编辑页面
  const openEditSo = (row: any) => {
    editSoTitle.value = '编辑销售订单';
    editDialogRef.value.openDialog(row);
  };

  // 删除
  const delSo = (row: any) => {
    ElMessageBox.confirm(`确定要删除吗?`, "提示", {
    confirmButtonText: "确定",
    cancelButtonText: "取消",
    type: "warning",
  })
  .then(async () => {
    await deleteSo(row);
    handleQuery();
    ElMessage.success("删除成功");
  })
  .catch(() => {});
  };

  // 改变页面容量
  const handleSizeChange = (val: number) => {
    tableParams.value.pageSize = val;
    handleQuery();
  };

  // 改变页码序号
  const handleCurrentChange = (val: number) => {
    tableParams.value.page = val;
    handleQuery();
  };

  //头表行选中
  const rowClick =(row: any, event: any, column: any)=>{
      console.log(row);
      selectRow.value=row;
  };

  //导出
  const soExport = async ()=>{
      loading.value=true;
      var res = await exportSo(queryParams.value);
      loading.value=false;
      var fileName = getFileName(res.headers);
      downloadByData(res.data as any, fileName);
  };

  //下载导入模板
  const soDownloadTemplate=async ()=>{
     loading.value=true;
      var res = await downloadTemplate();
      loading.value=false;
      var fileName = getFileName(res.headers);
      downloadByData(res.data as any, fileName);
  }

  //销售订单导入
  const soImportRef = ref<UploadInstance>();
  const soImport= async(file:any)=>{
      loading.value=true;
      soImportRef.value?.clearFiles();
      importSo(file.raw).then(async ()=>{
        loading.value=false;
        ElMessage.success('导入成功');
        dlgImportVisible.value=false;
      }).catch((e)=>{
           debugger
           /*
           ElMessageBox({message: e,  
             type: 'info',  
             dangerouslyUseHTMLString: true  });*/
        });
      }
  handleQuery();
</script>
<style scoped>
:deep(.el-ipnut),
:deep(.el-select),
:deep(.el-input-number) {
	width: 100%;
}
</style>

