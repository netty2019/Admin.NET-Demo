<template>
  <div class="woPri-container">
    <el-card shadow="hover" :body-style="{ paddingBottom: '0' }"> 
      <el-form :model="queryParams" ref="queryForm">
        <el-row>
          <el-col :xs="24" :sm="12" :md="12" :lg="6" :xl="6" class="mb10">
            <el-form-item>
              <el-button-group style="display: flex; align-items: center;">
                <el-button type="primary" style="margin-left:5px;" icon="ele-Plus" @click="openAddWoPri" v-auth="'woPri:add'"> 新增 </el-button>
              </el-button-group>
            </el-form-item>
            
          </el-col>
        </el-row>
      </el-form>
    </el-card>
    <el-card class="full-table" shadow="hover" style="margin-top: 5px">
      <VueDraggable target="tbody" v-model="tableData" :animation="150" handle=".sort-handle" @end="rowSortChange">
      <el-table
				:data="tableData"
				style="width: 800px"
				v-loading="loading"
				tooltip-effect="light"
                				row-key="id"
                @sort-change="sortChange"
				border="">
        <el-table-column label="排序" width="80" align="center">
            <template #default="scope">
              <div style="display: flex; justify-content: center;align-items: center" class="sort-handle">
                <svg t="1715178347829" class="icon" viewBox="0 0 1024 1024" version="1.1"
                  xmlns="http://www.w3.org/2000/svg" p-id="4274" width="23" height="23">
                  <path
                    d="M904 160H120c-4.4 0-8 3.6-8 8v64c0 4.4 3.6 8 8 8h784c4.4 0 8-3.6 8-8v-64c0-4.4-3.6-8-8-8zM904 784H120c-4.4 0-8 3.6-8 8v64c0 4.4 3.6 8 8 8h784c4.4 0 8-3.6 8-8v-64c0-4.4-3.6-8-8-8zM904 472H120c-4.4 0-8 3.6-8 8v64c0 4.4 3.6 8 8 8h784c4.4 0 8-3.6 8-8v-64c0-4.4-3.6-8-8-8z"
                    p-id="4275" fill="#1296db"></path>
                </svg>
              </div>
            </template>
          </el-table-column>
        <el-table-column prop="name" label="工单优先级"  show-overflow-tooltip="" />
        <el-table-column prop="color" label="颜色"  show-overflow-tooltip="" >
          <template #default="scope">
            <div style="width: 23px;height: 23px;border-radius: 23px;" :style="{ backgroundColor: scope.row.color }"  ></div>
          </template>
        </el-table-column>
        <el-table-column label="操作" width="140" align="center" fixed="right" show-overflow-tooltip="" v-if="auth('woPri:update') || auth('woPri:delete')">
          <template #default="scope">
            <el-button icon="ele-Edit" size="small" text="" type="primary" @click="openEditWoPri(scope.row)" v-auth="'woPri:update'"> 编辑 </el-button>
            <el-button icon="ele-Delete" size="small" text="" type="primary" @click="delWoPri(scope.row)" v-auth="'woPri:delete'"> 删除 </el-button>
          </template>
        </el-table-column>
      </el-table>
    </VueDraggable>
      <printDialog
        ref="printDialogRef"
        :title="printWoPriTitle"
        @reloadTable="handleQuery" />
      <editDialog
        ref="editDialogRef"
        :title="editWoPriTitle"
        @reloadTable="handleQuery"
      />
    </el-card>
  </div>
</template>

<script lang="ts" setup="" name="woPri">
  import { ref } from "vue";
  import { ElMessageBox, ElMessage } from "element-plus";
  import { auth } from '/@/utils/authFunction';
  import { getDictDataItem as di, getDictDataList as dl } from '/@/utils/dict-utils';
  import { formatDate } from '/@/utils/formatTime';


  import printDialog from '/@/views/system/print/component/hiprint/preview.vue'
  import editDialog from '/@/views/main/woPri/component/editDialog.vue'
  import { listWoPri, deleteWoPri,sortWoPri } from '/@/api/main/woPri';

  import { VueDraggable } from 'vue-draggable-plus'

  const showAdvanceQueryUI = ref(false);
  const printDialogRef = ref();
  const editDialogRef = ref();
  const loading = ref(false);
  const tableData = ref<any>([]);
  const queryParams = ref<any>({});

  const printWoPriTitle = ref("");
  const editWoPriTitle = ref("");

  // 改变高级查询的控件显示状态
  const changeAdvanceQueryUI = () => {
    showAdvanceQueryUI.value = !showAdvanceQueryUI.value;
  }
  

  // 查询操作
  const handleQuery = async () => {
    loading.value = true;
    var res = await listWoPri(Object.assign(queryParams.value));
    tableData.value = res.data.result ?? [];
    loading.value = false;
  };

  // 列排序
  const sortChange = async (column: any) => {
	queryParams.value.field = column.prop;
	queryParams.value.order = column.order;
	await handleQuery();
  };

  // 打开新增页面
  const openAddWoPri = () => {
    editWoPriTitle.value = '添加工单优先级';
    editDialogRef.value.openDialog({});
  };

  // 打开打印页面
  const openPrintWoPri = async (row: any) => {
    printWoPriTitle.value = '打印工单优先级';
  }
  
  // 打开编辑页面
  const openEditWoPri = (row: any) => {
    editWoPriTitle.value = '编辑工单优先级';
    editDialogRef.value.openDialog(row);
  };

  // 删除
  const delWoPri = (row: any) => {
    ElMessageBox.confirm(`确定要删除吗?`, "提示", {
    confirmButtonText: "确定",
    cancelButtonText: "取消",
    type: "warning",
  })
  .then(async () => {
    await deleteWoPri(row);
    handleQuery();
    ElMessage.success("删除成功");
  })
  .catch(() => {});
  };

//行排序事件
const rowSortChange = async (e: any) => {
  loading.value = true;
  await sortWoPri(e.oldIndex, e.newIndex);
  loading.value = false;
};

  handleQuery();
</script>
<style scoped>
:deep(.el-ipnut),
:deep(.el-select),
:deep(.el-input-number) {
	width: 100%;
}
</style>

