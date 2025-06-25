<template>
  <lay-form :size="'md'" :labelWidth="125" isLabelTooltip>
    <lay-row>
      <lay-col :md="12" :sm="24">
        <lay-form-item label="允许自定重定向">
          <lay-radio-button
            v-model="httphandlerAllowautoredirect"
            name="action"
            value="0"
            label="无"
          ></lay-radio-button>
          <lay-radio-button
            v-model="httphandlerAllowautoredirect"
            name="action"
            value="1"
            label="允许"
          ></lay-radio-button>
          <lay-radio-button
            v-model="httphandlerAllowautoredirect"
            name="action"
            value="2"
            label="不允许"
          ></lay-radio-button>
        </lay-form-item>
      </lay-col>
      <lay-col :md="12" :sm="24">
        <lay-form-item label="使用cookie容器">
          <lay-radio-button
            v-model="httphandlerUsecookiecontainer"
            name="action"
            value="0"
            label="无"
          ></lay-radio-button>
          <lay-radio-button
            v-model="httphandlerUsecookiecontainer"
            name="action"
            value="1"
            label="使用"
          ></lay-radio-button>
          <lay-radio-button
            v-model="httphandlerUsecookiecontainer"
            name="action"
            value="2"
            label="不使用"
          ></lay-radio-button>
        </lay-form-item>
      </lay-col>

      <lay-col :md="12" :sm="24">
        <lay-form-item label="使用追踪">
          <lay-radio-button
            v-model="httphandlerUsetracing"
            name="action"
            value="0"
            label="无"
          ></lay-radio-button>
          <lay-radio-button
            v-model="httphandlerUsetracing"
            name="action"
            value="1"
            label="使用"
          ></lay-radio-button>
          <lay-radio-button
            v-model="httphandlerUsetracing"
            name="action"
            value="2"
            label="不使用"
          ></lay-radio-button>
        </lay-form-item>
      </lay-col>

      <lay-col :md="12" :sm="24">
        <lay-form-item label="使用代理">
          <lay-radio-button
            v-model="httphandlerUseproxy"
            name="action"
            value="0"
            label="无"
          ></lay-radio-button>
          <lay-radio-button
            v-model="httphandlerUseproxy"
            name="action"
            value="1"
            label="使用"
          ></lay-radio-button>
          <lay-radio-button
            v-model="httphandlerUseproxy"
            name="action"
            value="2"
            label="不使用"
          ></lay-radio-button>
        </lay-form-item>
      </lay-col>
      <lay-col :md="12" :sm="24">
        <lay-form-item label="服务最大连接数">
          <lay-input-number
            v-model="routeStore.currentRouter.ratelimitPeriod"
            type="text"
            placeholder=""
          />
        </lay-form-item>
      </lay-col>
    </lay-row>
  </lay-form>
</template>
<script lang="ts" setup>
import { ref, computed } from "vue";
import {
  arrayToString,
  stringToArray,
  convertSwitchValue,
  reConvertSwitchValue,
} from "../../../../api/util";
import { useRouterStore } from "../../../../store/ocelotRouter";
const routeStore = useRouterStore();

const httphandlerUseproxy = computed({
  get() {
    return reConvertSwitchValue(routeStore.currentRouter.httphandlerUseproxy);
  },
  set(value: any) {
    routeStore.currentRouter.httphandlerUseproxy = convertSwitchValue(value);
  },
});
const httphandlerUsecookiecontainer = computed({
  get() {
    return reConvertSwitchValue(
      routeStore.currentRouter.httphandlerUsecookiecontainer
    );
  },
  set(value: any) {
    routeStore.currentRouter.httphandlerUsecookiecontainer =
      convertSwitchValue(value);
  },
});
const httphandlerAllowautoredirect = computed({
  get() {
    return reConvertSwitchValue(
      routeStore.currentRouter.httphandlerAllowautoredirect
    );
  },
  set(value: any) {
    routeStore.currentRouter.httphandlerAllowautoredirect =
      convertSwitchValue(value);
  },
});
const httphandlerUsetracing = computed({
  get() {
    return reConvertSwitchValue(routeStore.currentRouter.httphandlerUsetracing);
  },
  set(value: any) {
    routeStore.currentRouter.httphandlerUsetracing = convertSwitchValue(value);
  },
});
</script>
<style scoped></style>
