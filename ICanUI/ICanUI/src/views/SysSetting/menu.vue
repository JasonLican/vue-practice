<template>
  <div>
    <el-card class="tableHeard">
      <el-form :inline="true" :model="param">
        <el-form-item label="请输入">
          <el-input v-model="param.name" placeholder="请输入菜单名" clearable />
        </el-form-item>
        <el-form-item>
          <el-button type="primary" @click="handleSearch">查询</el-button>
        </el-form-item>
        <el-form-item>
          <el-button type="primary" :icon="CirclePlusFilled" @click="handleAdd"
            >新增</el-button
          >
        </el-form-item>
      </el-form>
    </el-card>
    <el-card class="tableList">
      <el-table
        :data="menuData"
        style="width: 100%; margin-bottom: 20px"
        row-key="id"
        border
      >
        <el-table-column
          v-for="item in columns"
          :key="item.prop"
          :prop="item.prop"
          :label="item.label"
        />
        <el-table-column fixed="right" label="操作" width="240">
          <template #default="scope">
            <el-button
              type="primary"
              size="small"
              @click="handleEdit(scope.row)"
              >修改</el-button
            >
            <el-button
              type="danger"
              size="small"
              @click="handleDelete(scope.row)"
              >删除</el-button
            >
          </template>
        </el-table-column>
      </el-table>
    </el-card>
  </div>
  <div class="tableOption">
    <el-dialog
      v-model="dialogVisible"
      :title="dialogTitle"
      width="45%"
      :before-close="handleClose"
    >
      <el-form
        :inline="true"
        :model="formModule"
        ref="menuForm"
        label-width="auto"
      >
        <el-row>
          <el-col :span="12">
            <el-form-item label="菜单编码" prop="code">
              <el-input
                v-model="formModule.code"
                placeholder="请输入菜单名称"
                clearable
              ></el-input>
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="菜单名称" prop="label">
              <el-input
                v-model="formModule.label"
                placeholder="请输入菜单名称"
                clearable
              ></el-input>
            </el-form-item>
          </el-col>
        </el-row>
        <el-row>
          <el-col :span="12">
            <el-form-item label="图标" prop="icon">
              <el-input
                v-model="formModule.icon"
                placeholder="请输入图标名称"
                clearable
              ></el-input>
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="路由" prop="url">
              <el-input
                v-model="formModule.url"
                placeholder="请输入路由"
                clearable
              ></el-input>
            </el-form-item>
          </el-col>
        </el-row>
        <el-row>
          <el-col :span="12">
            <el-form-item label="组件" prop="component">
              <el-input
                v-model="formModule.component"
                placeholder="请输入组件名称"
                clearable
              ></el-input>
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="排序" prop="sort">
              <el-input
                v-model="formModule.sort"
                placeholder="排序"
                clearable
              ></el-input>
            </el-form-item>
          </el-col>
        </el-row>
      </el-form>
      <template #footer>
        <div class="dialog-footer">
          <el-button @click="onCancel('menuForm')">取消</el-button>
          <el-button type="primary" @click="onSubmit">确定</el-button>
        </div>
      </template>
    </el-dialog>
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted, reactive,getCurrentInstance  } from "vue";
import { ElMessage } from "element-plus";
import { CirclePlusFilled } from "@element-plus/icons-vue";
import { GetMenuList } from "@/api/Menu";

const dialogVisible = ref(false);
const dialogTitle = ref(null);
const formModule = reactive({
  code: "",
  label: "",
  icon: "",
  url: "",
  component: "",
  sort: "",
});
let { proxy } = getCurrentInstance();

interface SerchForm {
  name: string;
}

const param = reactive<SerchForm>({
  name: null,
});
const menuData = ref([]);
onMounted(async () => {
  let res = await GetMenuList();
  if ((res.code = 200)) menuData.value = res.data;
});

// 表格相关
let columns = ref([
  { prop: "label", label: "菜单名称", align: "left" },
  { prop: "icon", label: "图标" },
  { prop: "url", label: "路由路径" },
]);

const getOptions = (data: any) => {
  console.log(data);
  return data.map((item) => {
    const a: any = {
      label: item.title,
      value: item.id,
    };
    if (item.children) {
      a.children = getOptions(item.children);
    }
    return a;
  });
};

const rowData = ref<any>({});

const handleSearch = () => {};
const handleAdd = () => {
  dialogVisible.value = true;
  dialogTitle.value = "新增";
};

const handleEdit = (row: any) => {};

// 删除相关
const handleDelete = (row: any) => {
  ElMessage.success("删除成功");
};

const handleClose = (done: () => void) => {
  dialogVisible.value = false;
};

const onCancel = (formName:any) => {
  proxy.$refs.menuForm.resetFields();
  dialogVisible.value = false;
};
const onSubmit = () => {};
</script>

<style scoped></style>