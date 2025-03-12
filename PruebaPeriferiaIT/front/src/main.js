import Vue from 'vue';
import App from './App.vue';

import router from './router';

import { createPinia, PiniaVuePlugin } from 'pinia';
Vue.use(PiniaVuePlugin);
const pinia = createPinia();

import Quasar from 'quasar';
import 'quasar/dist/quasar.min.css';
Vue.use(Quasar);

Vue.config.productionTip = false;

new Vue({
  router, 
  pinia, 
  render: h => h(App),
}).$mount('#app');
