import { createApp } from 'vue'
import Router from './router'
import Store from './store'
import App from './App.vue'
import { permission } from "./directives/permission";
import './mockjs'

import LayJsonSchemaForm from "@layui/json-schema-form";
import "@layui/json-schema-form/lib/index.css";


const app = createApp(App)

app.use(Store);
app.use(Router);

app.directive("permission",permission);
app.use(LayJsonSchemaForm);
app.mount('#app');
