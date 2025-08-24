import { createApp } from 'vue';
import pinia from '/@/stores/index';
import App from '/@/App.vue';
import router from '/@/router';
import { directive } from '/@/directive/index';
import { i18n } from '/@/i18n/index';
import other from '/@/utils/other';

import ElementPlus from 'element-plus';
import '/@/theme/index.scss';
import Tmap from '@map-component/vue-tmap';
// import * as ElementPlusIconsVue from '@element-plus/icons-vue'
import VueGridLayout from 'vue-grid-layout';

const app = createApp(App);

// 全局注册Icon
// for (const [key, component] of Object.entries(ElementPlusIconsVue)) {
//     app.component(key, component)
// }

directive(app);
other.elSvg(app);

app.use(pinia).use(router).use(ElementPlus).use(Tmap).use(i18n).use(VueGridLayout).mount('#app');
