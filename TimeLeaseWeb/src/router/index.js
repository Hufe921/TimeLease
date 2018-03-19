import Vue from 'vue'
import Router from 'vue-router'
// 以上是基础组件
import Home from '../components/content/home'
import Find from '../components/content/find-time'
import Lease from '../components/content/lease-time'
import Help from '../components/content/help'
import Detail from '../components/content/detail'
import Register from '../components/content/register'
import Shop from '../components/content/shop'
//个人主页
import My from "../components/content/my"
// 以上是一级模块
import Histroy from '../components/content/lease/lease-history'
import Publish from '../components/content/lease/lease-publish'
// 以上是出租二级模块
import MyActivity from "../components/content/my/my-main/my-activity"
import MyApply from "../components/content/my/my-main/my-apply"
import MyLine from "../components/content/my/my-main/my-line"
import MyBP from "../components/content/my/my-main/my-bp"
import MyIcon from "../components/content/my/my-main/my-icon"
import MyOther from "../components/content/my/my-main/my-other"
import MyPassword from "../components/content/my/my-main/my-password"
import MyPlace from "../components/content/my/my-main/my-place"
import MyExchange from "../components/content/my/my-main/my-exchange"
//以上是个人主页二级模块

Vue.use(Router)

export default new Router({
  mode: 'hash',
  routes: [{
      path: '/',
      redirect: '/home'
    },
    {
      path: '/home',
      name: 'home',
      component: Home
    },
    {
      path: '/find',
      name: 'find',
      component: Find
    },
    {
      path: '/lease',
      name: 'lease',
      redirect: '/lease/publish',
      component: Lease,
      children: [{
          path: 'history',
          name: 'history',
          component: Histroy,
        },
        {
          path: 'publish',
          name: 'publish',
          component: Publish,
        }
      ]
    },
    {
      path: '/help',
      name: 'help',
      component: Help
    },
    {
      path: '/detail',
      name: 'detail',
      component: Detail
    },
    {
      path: '/register',
      name: 'register',
      component: Register
    },
    {
      path: '/shop',
      name: 'shop',
      component: Shop
    },
    {
      path: '/my',
      name: 'my',
      component: My,
      redirect: '/my/activity',
      children: [{
          path: 'activity',
          name: 'activity',
          component: MyActivity
        },
        {
          path: 'apply',
          name: 'apply',
          component: MyApply
        },
        {
          path: 'line',
          name: 'line',
          component: MyLine
        },
        {
          path: 'bp',
          name: 'bp',
          component: MyBP
        },
        {
          path: 'icon',
          name: 'icon',
          component: MyIcon
        },
        {
          path: 'other',
          name: 'other',
          component: MyOther
        },
        {
          path: 'password',
          name: 'password',
          component: MyPassword
        },
        {
          path: 'place',
          name: 'place',
          component: MyPlace
        },
        {
          path: 'exchange',
          name: 'exchange',
          component: MyExchange
        }
      ]
    }
  ]
});
