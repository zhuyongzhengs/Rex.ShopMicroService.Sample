import { createSSRApp } from "vue";
import App from "./App.vue";
import * as pinia from 'pinia';
import piniaPluginPersistedstate from 'pinia-plugin-persistedstate'

import uviewPlus from 'uview-plus';

export function createApp() {
  const app = createSSRApp(App);
  app.use(uviewPlus);
  
  const cPinia = pinia.createPinia();
  // 添加Pinia持久化插件
  cPinia.use(piniaPluginPersistedstate);
  app.use(cPinia);
  return {
    app,
    pinia
  };
}
