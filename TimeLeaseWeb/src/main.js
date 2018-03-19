import Vue from 'vue'
import Vuex from 'vuex'
import router from './router/index.js'
import App from './App'

Vue.use(Vuex);
Vue.config.debug = true;

//状态管理
const store = new Vuex.Store({
  state: {
    isLogin: false,
    url: "http://120.76.145.211:8013", //api地址
    user: getCookie("user") ? JSON.parse(getCookie("user")) : "" //用户信息
  },
  mutations: {
    getUser: function (state) {
      state.user = getCookie("user") ? JSON.parse(getCookie("user")) : "";
    },
    changeLogin: function (state, msg) {
      state.isLogin = msg;
    }
  }
});

//路由守卫
router.beforeEach((to, from, next) => {
  if (!store.state.user && (to.path.indexOf("my") >= 0 || to.name == "detail" || to.name == "publish")) {
    showDialog("请登录！");
    next(false);
  } else {
    next(); //放行
  }
})

//vue实例
new Vue({
  el: '#app',
  store: store,
  router: router,
  components: {
    'time-lease-app': App
  },
  mounted() {
    this.$store.commit("getUser");
    this.routerChange();
  },
  watch: {
    $route() {
      this.routerChange();
    }
  },
  methods: {
    routerChange() {
      var path = this.$route.path;
      if (path.indexOf("my") >= 0) {
        var name = this.$route.name;
        $(".my-menu li").removeClass("my-current");
        $(".m-" + name + "").addClass("my-current");
      } else {
        $(".menu-container li").removeClass("current");
        if (path.indexOf("home") >= 0) {
          $(".home").addClass("current");
        } else if (path.indexOf("find") >= 0) {
          $(".find").addClass("current");
        } else if (path.indexOf("lease") >= 0) {
          $(".lease").addClass("current");
        } else if (path.indexOf("help") >= 0) {
          $(".help").addClass("current");
        } else if (path.indexOf("shop") >= 0) {
          $(".shop").addClass("current");
        }
      }
    }
  }
});
