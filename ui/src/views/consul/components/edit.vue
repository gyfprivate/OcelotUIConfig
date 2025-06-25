<template>
  <lay-layer
    v-model="visible"
    :shade="false"
    :area="['800px', '600px']"
    :btn="actions"
    :title="title"
  >
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
import { add_consul, edit_consul } from "../../../api/module/consul";
const title = ref("编辑Consul配置");

let formType = 0; // 0: 新增, 1: 编辑

// 定义触发的事件类型
const emits = defineEmits<{
  (event: "refresh"): void;
}>();
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
        add_consul(form)
          .then((res) => {
            layer.msg("操作成功");
            emits("refresh");
            visible.value = false;
          })
          .catch((err) => {
            layer.msg("网络异常，请稍后再试");
          });
      }
      if (formType == 1) {
        edit_consul(form)
          .then((res) => {
            layer.msg("操作成功");
            emits("refresh");
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
const form = reactive({});
// 表单验证规则
const schema = reactive({
  name: {
    label: "名称",
    type: "input",
    props: {
      type: "text",
      placeholder: "请输入名称",
    },
    colProps: {
      md: 12,
      sm: 12,
    },
  },
  type: {
    label: "类型",
    type: "select",
    props: {
      options: [
        { label: "Consul", value: 1 },
        // { label: "类型二", value: 2 },
        // { label: "类型三", value: 3 },
      ],
      placeholder: "请选择类型",
    },
    colProps: {
      sm: 12,
      md: 12,
    },
  },
  host: {
    label: "IP",
    type: "input",
    props: {
      type: "text",
      placeholder: "请输入IP地址",
    },
    colProps: {
      sm: 12,
      md: 12,
    },
  },
  port: {
    label: "端口",
    type: "input",
    props: {
      type: "number",
      placeholder: "请输入端口号",
    },
    colProps: {
      sm: 12,
      md: 12,
    },
  },
  key: {
    label: "所属位置",
    type: "input",
    props: {
      type: "text",
      placeholder: "请输入Consul的dc",
    },
    colProps: {
      sm: 12,
      md: 12,
    },
  },
  path: {
    label: "路径",
    type: "input",
    props: {
      type: "text",
      placeholder: "请输入配置保存的路径",
    },
    colProps: {
      sm: 12,
      md: 12,
    },
  },
  remark: {
    label: "备注",
    type: "textarea",
    props: {
      placeholder: "请输入备注信息",
    },
    colProps: {
      sm: 24,
      md: 24,
    },
  },

  enabled: {
    label: "是否启用",
    type: "switch",
    props: {
      activeText: "启用",
      inactiveText: "禁用",
    },
    colProps: {
      sm: 24,
      md: 24,
    },
  },
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
</style>
