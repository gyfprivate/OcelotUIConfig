<template>
  <div class="flex">
    <lay-card title="基础参数"
      ><lay-json-schema-form
        :model="form"
        :schema="schema1"
        :space="space"
        :labelWidth="labelWidth"
      ></lay-json-schema-form
    ></lay-card>
    <lay-card title="服务发现"
      ><lay-json-schema-form
        :model="form"
        :schema="schema4"
        :space="space"
        :labelWidth="labelWidth"
      >
      </lay-json-schema-form
    ></lay-card>
    <lay-card title="下游协议"
      ><lay-json-schema-form
        :model="form"
        :schema="schema2"
        :space="space"
        :labelWidth="labelWidth"
      ></lay-json-schema-form>
      <template #selectcloseable>
        123
        <lay-input :allow-clear="true">
          <lay-select-option value="Http" label="Http"></lay-select-option>
        </lay-input>
      </template>
    </lay-card>
    <lay-card title="负载均衡"
      ><lay-json-schema-form
        :model="form"
        :schema="schema3"
        :space="space"
        :labelWidth="labelWidth"
      ></lay-json-schema-form
    ></lay-card>
    <lay-card title="断路器配置"
      ><lay-json-schema-form
        :model="form"
        :schema="schema5"
        :space="space"
        :labelWidth="labelWidth"
      >
      </lay-json-schema-form
    ></lay-card>
    <lay-card title="流量限制"
      ><lay-json-schema-form
        :model="form"
        :schema="schema6"
        :space="space"
        :labelWidth="labelWidth"
      ></lay-json-schema-form
    ></lay-card>
    <lay-card title="http处理"
      ><lay-json-schema-form
        :model="form"
        :schema="schema7"
        :space="space"
        :labelWidth="labelWidth"
      ></lay-json-schema-form
    ></lay-card>

    <!-- 最底部的提交按钮 -->
    <div style="width: 100%">
      <lay-affix
        :target="target"
        :offset="40"
        position="bottom"
        v-if="target"
        style="width: 100%"
      >
        <div class="right-flex">
          <lay-button type="normal" @click="onSave">保存</lay-button>
        </div>
      </lay-affix>
    </div>
  </div>
</template>
<script lang="ts" setup>
import { ref, reactive, onMounted, computed, nextTick } from "vue";
import { useProjStore } from "../../store/proj";
import {
  get_global_configuration,
  modify_global_configuration,
} from "../../api/module/globalconfiguration";
import { layer } from "@layui/layui-vue";

const target = ref();
const space = ref(5);
const labelWidth = ref(150);

onMounted(() => {
  get_global_configuration()
    .then(({ data }) => {
      console.log(data);
      Object.assign(form, data); // 合并属性，保留响应式
    })
    .catch((err) => {});
});

const onSave = () => {
  modify_global_configuration("0", form)
    .then(({ data }) => {
      console.log(data);
      layer.msg("保存完毕");
    })
    .catch((err) => {});
};

nextTick(() => {
  target.value = document.querySelector(".flex");
});
const projStore = useProjStore();
const form = reactive({});
const schema1 = reactive({
  enabled: {
    label: "启动/禁用",
    type: "radio",
    props: {
      options: [
        { label: "启用", value: true },
        { label: "禁用", value: false },
      ],
    },
    colProps: {
      lg: 6,
      md: 12,
    },
  },
  baseUrl: {
    label: "基础域名",
    type: "input",
    props: {
      type: "text",
    },
    colProps: {
      lg: 6,
      md: 12,
    },
  },
  requestIdKey: {
    label: "基础请求Id",
    type: "input",
    props: {
      type: "text",
    },
    colProps: {
      lg: 6,
      md: 12,
    },
  },
});
const schema2 = reactive({
  downstreamScheme: {
    label: "下游协议",
    type: "select",
    props: {
      options: [
        { label: "请选择", value: null },
        { label: "http", value: "http" },
        { label: "https", value: "https" },
      ],
      // placeholder: "",
    },
    slot: {
      customRender: "selectcloseable",
    },
    colProps: {
      lg: 6,
      md: 12,
    },
  },
  downstreamHttpVersion: {
    label: "Http版本",
    type: "select",
    props: {
      options: [
        { label: "请选择", value: null },
        { label: "1.0", value: "1.0" },
        { label: "1.1", value: "1.1" },
        { label: "2.0", value: "2.0" },
      ],
      placeholder: "",
    },
    slot: {
      customRender: "selectcloseable",
    },
    colProps: {
      lg: 6,
      md: 12,
    },
  },
});
const schema3 = reactive({
  loadbalancerType: {
    label: "负载均衡类型",
    type: "select",
    props: {
      options: [
        { label: "请选择", value: null },
        { label: "最少连接数", value: "LeastConnection" },
        { label: "轮询", value: "RoundRobin" },
        { label: "不适用负载均衡", value: "NoLoadBalance" },
      ],
      placeholder: "",
    },
    colProps: {
      lg: 6,
      md: 12,
    },
  },
  loadbalancerKey: {
    label: "负载均衡Key",
    type: "input",
    props: {
      type: "text",
    },
    colProps: {
      lg: 6,
      md: 12,
    },
  },
  loadbalancerExpiry: {
    label: "负载均衡Expiry",
    type: "input",
    props: {
      type: "text",
    },
    colProps: {
      lg: 6,
      md: 12,
    },
  },
});
const schema4 = reactive({
  serviceDiscoveryEnabled: {
    label: "启动/禁用",
    type: "radio",
    props: {
      options: [
        { label: "启用", value: true },
        { label: "禁用", value: false },
      ],
    },
    colProps: {
      lg: 6,
      md: 12,
    },
  },
  servicediscoveryScheme: {
    label: "Http协议",
    type: "select",
    props: {
      options: [
        { label: "请选择", value: null },
        { label: "http", value: "http" },
        { label: "https", value: "https" },
      ],
      placeholder: "",
    },
    colProps: {
      lg: 6,
      md: 12,
    },
  },

  servicediscoveryHost: {
    label: "服务发现站点",
    type: "input",
    props: {
      type: "text",
    },
    colProps: {
      lg: 6,
      md: 12,
    },
  },
  servicediscoveryPort: {
    label: "服务发现端口",
    type: "input",
    props: {
      type: "number",
    },
    colProps: {
      lg: 6,
      md: 12,
    },
  },
  servicediscoveryType: {
    label: "服务发现类型",
    type: "select",
    props: {
      options: [
        { label: "请选择", value: null },
        { label: "Consul", value: "Consul" },
        { label: "PollConsul", value: "PollConsul" },
        { label: "Eureka", value: "Eureka" },
      ],
      placeholder: "",
    },
    colProps: {
      lg: 6,
      md: 12,
    },
  },
  servicediscoveryToken: {
    label: "Token",
    type: "input",
    props: {
      type: "text",
    },
    colProps: {
      lg: 6,
      md: 12,
    },
  },
  servicediscoveryConfigurationkey: {
    label: "配置Key",
    type: "input",
    props: {
      type: "text",
    },
    colProps: {
      lg: 6,
      md: 12,
    },
  },
  servicediscoveryNamespace: {
    label: "命名空间",
    type: "input",
    props: {
      type: "text",
    },
    colProps: {
      lg: 6,
      md: 12,
    },
  },
  servicediscoveryPollinginterval: {
    label: "轮询间隔",
    type: "input",
    props: {
      type: "number",
    },
    slots: {
      suffix: () => "ms",
    },
    colProps: {
      lg: 6,
      md: 12,
    },
  },
});
const schema5 = reactive({
  qosEnabled: {
    label: "启动/禁用",
    type: "radio",
    props: {
      options: [
        { label: "启用", value: true },
        { label: "禁用", value: false },
      ],
    },
    colProps: {
      lg: 6,
      md: 12,
    },
  },
  qosExceptionsallowedbeforebreaking: {
    label: "允许的例外数量",
    type: "input",
    props: {
      type: "number",
    },
    colProps: {
      lg: 6,
      md: 12,
    },
  },
  qosDurationofbreak: {
    label: "恢复时间间隔",
    type: "input",
    props: {
      type: "number",
    },
    slots: {
      suffix: () => "ms",
    },
    colProps: {
      lg: 6,
      md: 12,
    },
  },
  qosTimeoutvalue: {
    label: "请求超时时间",
    type: "input",
    props: {
      type: "number",
    },
    slots: {
      suffix: () => "ms",
    },
    colProps: {
      lg: 6,
      md: 12,
    },
  },
});
const schema6 = reactive({
  ratelimitDisableratelimitheaders: {
    label: "包含X-Rate-Limit和Rety-After",
    type: "radio",
    props: {
      options: [
        { label: "启用", value: true },
        { label: "禁用", value: false },
      ],
    },
    colProps: {
      lg: 6,
      md: 12,
    },
  },
  ratelimitClientidheader: {
    label: "客户Header",
    type: "input",
    props: {
      type: "text",
    },
    colProps: {
      lg: 6,
      md: 12,
    },
  },
  ratelimitQuotaexceededmessage: {
    label: "超过限制提示语",
    type: "input",
    props: {
      type: "text",
    },
    colProps: {
      lg: 6,
      md: 12,
    },
  },
  ratelimitHttpstatuscode: {
    label: "超过限制Http状态码",
    type: "input",
    props: {
      type: "number",
    },
    colProps: {
      lg: 6,
      md: 12,
    },
  },
});
const schema7 = reactive({
  httphandlerAllowautoredirect: {
    label: "允许自定重定向",
    type: "radio",
    props: {
      options: [
        { label: "允许", value: true },
        { label: "不允许", value: false },
      ],
    },
    colProps: {
      lg: 6,
      md: 12,
    },
  },
  httphandlerUsecookiecontainer: {
    label: "使用cookie容器",
    type: "radio",
    props: {
      options: [
        { label: "使用", value: true },
        { label: "不使用", value: false },
      ],
    },
    colProps: {
      lg: 6,
      md: 12,
    },
  },
  httphandlerUsetracing: {
    label: "使用追踪",
    type: "radio",
    props: {
      options: [
        { label: "使用", value: true },
        { label: "不使用", value: false },
      ],
    },
    colProps: {
      lg: 6,
      md: 12,
    },
  },
  httphandlerUseproxy: {
    label: "使用代理",
    type: "radio",
    props: {
      options: [
        { label: "使用", value: true },
        { label: "不使用", value: false },
      ],
    },
    colProps: {
      lg: 6,
      md: 12,
    },
  },
  httphandlerMaxconnectionsperserver: {
    label: "服务最大连接数",
    type: "input",
    props: {
      type: "number",
    },
    colProps: {
      lg: 6,
      md: 12,
    },
  },
});
</script>
<style scoped>
.flex {
  margin: 5px;
  padding-bottom: 50px;
}
.right-flex {
  display: flex;
  flex-direction: row-reverse;
  padding-right: 280px;
}
.layui-card {
  margin-bottom: 5px;
}
</style>
