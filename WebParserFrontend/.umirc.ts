import { defineConfig } from '@umijs/max';

export default defineConfig({
  antd: {},
  locale:{
    antd: true,
    default: "ru"
  },
  access: {},
  model: {},
  initialState: {},
  request: {},
  layout: {
    title: '@umijs/max',
  },
  routes: [
    {
      path: '/',
      redirect: '/home',
    },
    {
      name: 'Shops Parser',
      path: '/home',
      component: './Home',
    },
    {
      name: 'Магазины',
      path: '/shops',
      component: './Shops',
    },
    {
      name: "Товары",
      path: '/goods',
      component: './Goods'
    }
  ],
  npmClient: 'npm',
});

