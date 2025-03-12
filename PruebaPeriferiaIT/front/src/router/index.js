import NotFound from '@/view/notFound.vue';
import Vue from 'vue';
import VueRouter from 'vue-router';
import MainView from '../view/main.vue';

Vue.use(VueRouter);

const routes = [
  { path: '/', component: MainView },
  {
    path: "*",
    component: NotFound,
  },
];

const router = new VueRouter({
  mode: 'history',
  routes,
});

export default router;
