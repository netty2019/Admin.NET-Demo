<template>
  <div class="unit-container">
    <el-card shadow="hover" :body-style="{ paddingBottom: '0' }">
      <el-form :model="queryParams" ref="queryForm" >
        <el-row>
          <el-col :xs="24" :sm="12" :md="12" :lg="6" :xl="6" class="mb10">
            <el-form-item>
                <el-button type="primary" style="margin-left:5px;" icon="ele-Plus" @click="openAddUnit"
                  v-auth="'unit:add'"> 新增 </el-button>
            </el-form-item>
          </el-col>
        </el-row>
      </el-form>
    </el-card>
    <el-card class="full-table" shadow="hover" style="margin-top: 5px">
      <VueDraggable target="tbody" v-model="tableData" :animation="150" handle=".sort-handle" @end="rowSortChange">
        <el-table :data="tableData" style="width: 100%" v-loading="loading" tooltip-effect="light" row-key="id"
          @sort-change="sortChange" border="">
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
          <el-table-column prop="name" label="名称" show-overflow-tooltip="" />
          <el-table-column label="操作" width="140" align="center" fixed="right" show-overflow-tooltip=""
            v-if="auth('unit:update') || auth('unit:delete')">
            <template #default="scope">
              <el-button icon="ele-Edit" size="small" text="" type="primary" @click="openEditUnit(scope.row)"
                v-auth="'unit:update'"> 编辑 </el-button>
              <el-button icon="ele-Delete" size="small" text="" type="primary" @click="delUnit(scope.row)"
                v-auth="'unit:delete'"> 删除 </el-button>
            </template>
          </el-table-column>
        </el-table>
      </VueDraggable>
      <el-pagination v-model:currentPage="tableParams.page" v-model:page-size="tableParams.pageSize"
        :total="tableParams.total" :page-sizes="[10, 20, 50, 100, 200, 500]" small="" background=""
        @size-change="handleSizeChange" @current-change="handleCurrentChange"
        layout="total, sizes, prev, pager, next, jumper" />
      <printDialog ref="printDialogRef" :title="printUnitTitle" @reloadTable="handleQuery" />
      <editDialog ref="editDialogRef" :title="editUnitTitle" @reloadTable="handleQuery" />
    </el-card>
  </div>
</template>

<script lang="ts" setup="" name="unit">
import { ref, onMounted } from "vue";
import { ElMessageBox, ElMessage } from "element-plus";
import { auth } from '/@/utils/authFunction';
import { getDictDataItem as di, getDictDataList as dl } from '/@/utils/dict-utils';
import { formatDate } from '/@/utils/formatTime';


import printDialog from '/@/views/system/print/component/hiprint/preview.vue'
import editDialog from '/@/views/main/unit/component/editDialog.vue'
import { pageUnit, deleteUnit, sortUnit } from '/@/api/main/unit';

import { VueDraggable } from 'vue-draggable-plus'

const showAdvanceQueryUI = ref(false);
const printDialogRef = ref();
const editDialogRef = ref();
const loading = ref(false);
const tableData = ref<any>([]);
const queryParams = ref<any>({});
const tableParams = ref({
  page: 1,
  pageSize: 10,
  total: 0,
});

const printUnitTitle = ref("");
const editUnitTitle = ref("");

// 改变高级查询的控件显示状态
const changeAdvanceQueryUI = () => {
  showAdvanceQueryUI.value = !showAdvanceQueryUI.value;
}


// 查询操作
const handleQuery = async () => {
  loading.value = true;
  var res = await pageUnit(Object.assign(queryParams.value, tableParams.value));
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
const openAddUnit = () => {
  editUnitTitle.value = '添加单位';
  editDialogRef.value.openDialog({});
};

// 打开打印页面
const openPrintUnit = async (row: any) => {
  printUnitTitle.value = '打印单位';
}

// 打开编辑页面
const openEditUnit = (row: any) => {
  editUnitTitle.value = '编辑单位';
  editDialogRef.value.openDialog(row);
};

// 删除
const delUnit = (row: any) => {
  ElMessageBox.confirm(`确定要删除吗?`, "提示", {
    confirmButtonText: "确定",
    cancelButtonText: "取消",
    type: "warning",
  })
    .then(async () => {
      await deleteUnit(row);
      handleQuery();
      ElMessage.success("删除成功");
    })
    .catch(() => { });
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

//行排序事件
const rowSortChange = async (e: any) => {
  loading.value = true;
  await sortUnit(e.oldIndex, e.newIndex);
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
