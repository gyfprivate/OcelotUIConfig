<template>
  <lay-card class="container">
    <div v-if="routerStore.currentRouter.projectId">
      <lay-form
        v-model="routerStore.currentRouter"
        :size="'md'"
        :labelWidth="95"
        isLabelTooltip
      >
        <lay-row>
          <lay-col :sm="24">
            <lay-button @click="routerStore.onSaveRouter" type="primary"
              >保存</lay-button
            >
          </lay-col>

          <lay-col :md="8" :sm="24">
            <lay-form-item label="上游请求模板">
              <lay-input
                v-model="routerStore.currentRouter.upstreamPathTemplate"
              ></lay-input>
            </lay-form-item>
          </lay-col>
          <lay-col :md="8" :sm="24">
            <lay-form-item label="下游路由模板">
              <lay-input
                v-model="routerStore.currentRouter.downstreamPathTemplate"
              ></lay-input>
            </lay-form-item>
          </lay-col>
          <lay-col :md="8" :sm="24">
            <lay-form-item label="排序索引">
              <lay-input
                v-model.number="routerStore.currentRouter.sort"
              ></lay-input>
            </lay-form-item>
          </lay-col>

          <lay-col :md="8" :sm="24">
            <lay-form-item label="上游请求HTTP方法">
              <lay-checkbox
                skin="primary"
                v-model="allselect"
                @change="changeMethod"
                value="all"
                >All</lay-checkbox
              >
              <lay-checkbox-group
                style="display: inline"
                v-model="upstreamHttpMethod"
                @change="groupChange"
              >
                <lay-checkbox
                  v-for="item in HttpMethodNames"
                  :key="item"
                  skin="primary"
                  :value="item"
                  >{{ item }}</lay-checkbox
                >
              </lay-checkbox-group>
            </lay-form-item>
          </lay-col>

          <lay-col :md="8" :sm="24">
            <lay-form-item label="下游协议">
              <lay-select
                v-model="routerStore.currentRouter.downstreamScheme"
                :allow-clear="true"
              >
                <lay-select-option
                  value="Http"
                  label="Http"
                ></lay-select-option>
                <lay-select-option
                  value="Https"
                  label="Https"
                ></lay-select-option>
              </lay-select>
            </lay-form-item>
          </lay-col>

          <lay-col :md="8" :sm="24">
            <lay-form-item label="服务发现名称">
              <lay-input
                v-model.number="routerStore.currentRouter.serviceName"
              ></lay-input>
            </lay-form-item>
          </lay-col>
          <lay-col :md="8" :sm="24">
            <lay-form-item label="下游请求HTTP方法">
              <lay-select
                :allow-clear="true"
                v-model="routerStore.currentRouter.downstreamHttpMethod"
              >
                <lay-select-option
                  v-for="item in HttpMethodNames"
                  :key="item"
                  :value="item"
                  :label="item"
                ></lay-select-option>
              </lay-select>
            </lay-form-item>
          </lay-col>
          <lay-col :md="8" :sm="24">
            <lay-form-item label="缓存过期时间">
              <lay-input
                v-model.number="routerStore.currentRouter.cacheTtlseconds"
              >
                <template #suffix>秒</template>
              </lay-input>
            </lay-form-item>
          </lay-col>

          <lay-col :md="8" :sm="24">
            <lay-form-item label="上游站点">
              <lay-input
                v-model.number="routerStore.currentRouter.upstreamHost"
              ></lay-input>
            </lay-form-item>
          </lay-col>
          <lay-col :md="8" :sm="24">
            <lay-form-item label="下游HTTP版本">
              <lay-select
                :allow-clear="true"
                v-model="routerStore.currentRouter.downstreamHttpMethod"
              >
                <lay-select-option value="1.0" label="1.0"></lay-select-option>
                <lay-select-option value="1.1" label="1.1"></lay-select-option>
                <lay-select-option value="2.0" label="2.0"></lay-select-option>
              </lay-select>
            </lay-form-item>
          </lay-col>

          <lay-col :md="8" :sm="24">
            <lay-form-item label="请求Key">
              <lay-input
                v-model.number="routerStore.currentRouter.requestIdKey"
              ></lay-input>
            </lay-form-item>
          </lay-col>
          <lay-col :md="8" :sm="24">
            <lay-form-item label="缓存区域">
              <lay-input
                v-model.number="routerStore.currentRouter.cacheRegion"
              ></lay-input>
            </lay-form-item>
          </lay-col>

          <lay-col :md="8" :sm="24">
            <lay-form-item label="服务发现命名空间">
              <lay-input
                v-model.number="routerStore.currentRouter.serviceNamespace"
              ></lay-input>
            </lay-form-item>
          </lay-col>

          <lay-col :md="8" :sm="24">
            <lay-form-item label="路由优先级">
              <lay-input-number
                v-model.number="routerStore.currentRouter.serviceNamespace"
              ></lay-input-number>
            </lay-form-item>
          </lay-col>
          <lay-col :md="8" :sm="24">
            <lay-form-item label="路由超时时间">
              <lay-input v-model.number="routerStore.currentRouter.timeout">
                <template #suffix>毫秒</template></lay-input
              >
            </lay-form-item>
          </lay-col>

          <lay-col :md="8" :sm="24">
            <lay-form-item label="route.key">
              <lay-input
                v-model.number="routerStore.currentRouter.key"
              ></lay-input>
            </lay-form-item>
          </lay-col>
          <lay-col :md="8" :sm="24">
            <lay-form-item label="route.Sort">
              <lay-input-number
                v-model.number="routerStore.currentRouter.sort"
              ></lay-input-number>
            </lay-form-item>
          </lay-col>
          <lay-col :md="8" :sm="24">
            <lay-form-item label="评估危险服务验证">
              <lay-radio-button
                v-model="dangerousAcceptAnyServerCertificateValidator"
                name="action"
                value="0"
                label="无"
              ></lay-radio-button>
              <lay-radio-button
                v-model="dangerousAcceptAnyServerCertificateValidator"
                name="action"
                value="1"
                label="使用"
              ></lay-radio-button>
              <lay-radio-button
                v-model="dangerousAcceptAnyServerCertificateValidator"
                name="action"
                value="2"
                label="不使用"
              ></lay-radio-button>
            </lay-form-item>
          </lay-col>
          <lay-col :md="8" :sm="24">
            <lay-form-item label="上下游路由模板大小写匹配">
              <lay-radio-button
                v-model="routeIsCaseSensitive"
                name="action"
                value="0"
                label="无"
              ></lay-radio-button>
              <lay-radio-button
                v-model="routeIsCaseSensitive"
                name="action"
                value="1"
                label="使用"
              ></lay-radio-button>
              <lay-radio-button
                v-model="routeIsCaseSensitive"
                name="action"
                value="2"
                label="不使用"
              ></lay-radio-button>
            </lay-form-item>
          </lay-col>
          <lay-col :md="8" :sm="24">
            <lay-form-item label="委托处理">
              <!-- v-model="routerStore.currentRouter.delegatingHandlers" -->
              <lay-tag-input
                v-model="delegatingHandlers"
                v-model:inputValue="delegatingHandlerInput"
                allowClear
                placeholder="输入后按回车确认"
              >
              </lay-tag-input>
            </lay-form-item>
          </lay-col>
        </lay-row>
      </lay-form>
      <lay-line theme="red"></lay-line>
      <lay-tab v-model="currentPage" type="brief">
        <lay-tab-item title="Host端口" id="1"
          ><div style="padding: 0">
            <hostedit /></div
        ></lay-tab-item>
        <lay-tab-item title="断路器配置" id="2"
          ><div style="padding: 20px"><routerBreakingEdit /></div
        ></lay-tab-item>
        <lay-tab-item title="流量限制" id="3"
          ><div style="padding: 20px"><routerRateLimitEdit /></div
        ></lay-tab-item>
        <lay-tab-item title="鉴权管理" id="4"
          ><div style="padding: 20px"><routerAuthEdit /></div
        ></lay-tab-item>
        <lay-tab-item title="http处理" id="5"
          ><div style="padding: 20px"><routerHttpHandlerEdit /></div
        ></lay-tab-item>
        <lay-tab-item title="负载均衡" id="6"
          ><div style="padding: 20px"><routerLoadBalanEdit /></div
        ></lay-tab-item>
      </lay-tab>
    </div>
    <div v-else>
      <!-- //没有选择的时候什么都不显示 -->
    </div>
  </lay-card>
</template>
<script lang="ts" setup>
import { reactive, ref, watch, computed } from "vue";
import hostedit from "./components/routerHostEdit.vue";
import routerBreakingEdit from "./components/routerBreakingEdit.vue";
import routerRateLimitEdit from "./components/routerRateLimitEdit.vue";
import routerAuthEdit from "./components/routerAuthEdit.vue";
import routerHttpHandlerEdit from "./components/routerHttpHandlerEdit.vue";
import routerLoadBalanEdit from "./components/routerLoadBalanEdit.vue";
import { useRouterStore } from "../../../store/ocelotRouter";
import { layer } from "@layui/layui-vue";
import {
  get_router_info,
  add_router,
  update_router,
} from "../../../api/module/router";
import {
  stringToArray,
  arrayToString,
  convertSwitchValue,
  reConvertSwitchValue,
} from "../../../api/util";
import router from "@/router";
import { set } from "nprogress";

const HttpMethodNames = ["Get", "Post", "Put", "Deleted", "Patch", "Option"];

const routerStore = useRouterStore();
const allselect = ref(false);
// 草稿数据
let draftData = {};

const upstreamHttpMethod = computed({
  get() {
    const r = stringToArray(routerStore.currentRouter.upstreamHttpMethod);
    allselect.value = r.length === HttpMethodNames.length;
    return r;
  },
  set(value: string[]) {
    // console.log(value);
    routerStore.currentRouter.upstreamHttpMethod = arrayToString(value);
    allselect.value = value.length === HttpMethodNames.length;
  },
});

const routeIsCaseSensitive = computed({
  get() {
    return reConvertSwitchValue(routerStore.currentRouter.routeIsCaseSensitive);
  },
  set(value: string) {
    routerStore.currentRouter.routeIsCaseSensitive = convertSwitchValue(value);
  },
});

const dangerousAcceptAnyServerCertificateValidator = computed({
  get() {
    return reConvertSwitchValue(
      routerStore.currentRouter.dangerousAcceptAnyServerCertificateValidator
    );
  },
  set(value: string) {
    routerStore.currentRouter.dangerousAcceptAnyServerCertificateValidator =
      convertSwitchValue(value);
  },
});

// // 复选组变化
// const groupChange = () => {
//   console.log(upstreamHttpMethod.value);
//   routerStore.currentRouter.upstreamHttpMethod = arrayToString(
//     upstreamHttpMethod.value
//   );
// };

// 全选变化
const changeMethod = (isChecked: any) => {
  //   console.log(isChecked);
  if (isChecked)
    upstreamHttpMethod.value = [
      "Get",
      "Post",
      "Put",
      "Deleted",
      "Patch",
      "Option",
    ];
  else upstreamHttpMethod.value = [];
};

const delegatingHandlerInput = ref("");

const currentPage = ref("1");
const delegatingHandlers = computed({
  get() {
    return stringToArray(routerStore.currentRouter.delegatingHandlers);
  },
  set(value: any) {
    console.log(value);
    routerStore.currentRouter.delegatingHandlers = arrayToString(value);
    console.log(routerStore.currentRouter.delegatingHandlers);
  },
});

const form: any = ref({});
</script>
<style scoped>
.container {
  margin: 5px;
  width: 100%;
}
</style>
