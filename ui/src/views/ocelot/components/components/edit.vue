<template>
  <lay-layer
    v-model="visible"
    :shade="false"
    :area="['600px', '300px']"
    :btn="actions"
    :title="title"
  >
    <div class="flex-container">
      <div class="fixed-width">创建时间</div>
      <div class="flexible-width">{{ form.createTime }}</div>
      <div class="fixed-width" style="margin-left: 5px">编辑时间</div>
      <div class="flexible-width">{{ form.updateTime }}</div>
    </div>
    <lay-json-schema-form
      :model="form"
      :schema="schema"
      :space="space"
    ></lay-json-schema-form>
  </lay-layer>
</template>
<script lang="ts" setup>
import { ref, reactive } from "vue";
import { layer } from "@layui/layui-vue";
import { add_asyn, edit_asyn } from "../../../../api/module/proj";
import { formatTimestamp } from "../../../../api/timeutil";
import { useProjStore } from "../../../../store/proj";
const projStore = useProjStore();
const title = ref("");

let formType = 0; // 0: 新增, 1: 编辑

const space = ref(5);
// 弹窗显示状态
const visible = ref(false);
// 弹窗按键
const actions = ref([
  {
    text: "确认",
    callback: () => {
      console.log("提交表单数据", form);
      if (formType == 0) {
        add_asyn(form)
          .then((res) => {
            layer.msg("操作成功");
            projStore.refreshProjList();
            visible.value = false;
          })
          .catch((err) => {
            layer.msg("网络异常，请稍后再试");
          });
      }
      if (formType == 1) {
        edit_asyn(form.id, form)
          .then((res) => {
            layer.msg("操作成功");
            projStore.refreshProjList();
            visible.value = false;
          })
          .catch((err) => {
            layer.msg("网络异常，请稍后再试");
          });
      }
    },
  },
  {
    text: "取消",
    callback: () => {
      visible.value = false;
    },
  },
]);
// 表单数据
const form = reactive({ id: "", createTime: "", updateTime: "" });
// 表单验证规则
const schema = reactive({
  projectName: {
    label: "项目名称",
    type: "input",
    props: {
      type: "text",
      placeholder: "请输入项目名称",
    },
    rules: [{ required: true, message: "请输入项目名称", trigger: "blur" }],
    colProps: {
      md: 12,
      sm: 12,
    },
  },
  orderIndex: {
    label: "排序",
    type: "input",
    props: {
      placeholder: "请输入排序字段",
      min: 0,
      type: "number",
    },
    colProps: {
      md: 12,
      sm: 12,
    },
  },
  // enabled: {
  //   label: "启用",
  //   type: "switch",
  //   props: {
  //     activeText: "是",
  //     inactiveText: "否",
  //   },
  //   colProps: {
  //     md: 12,
  //     sm: 12,
  //   },
  // },
});
const show = (s: any) => {
  title.value = s.title;
  formType = s.type; // 0: 新增, 1: 编辑
  Object.assign(form, s.data);
  visible.value = true;
};

// 暴露方法给父组件
defineExpose({
  show,
});
</script>
<style scoped>
.layui-form {
  margin: 10px;
  padding: 0;
}

.time-title {
  width: 110px;
  height: 38px;
  padding-right: 15px;
  text-align: right;
}

.flex-container {
  display: flex;
  flex-wrap: wrap;
  margin: 10px;
  height: 38px;
}
.fixed-width {
  width: 110px; /* 定宽 */
  text-align: right;
  padding-right: 15px;
  line-height: 38px;
}
.flexible-width {
  flex: 1; /* 自适应剩余空间 */
  line-height: 38px;
}
</style>
