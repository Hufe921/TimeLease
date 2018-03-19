<template>
  <div class="head-container">
    <div class="head-main">
      <!--存放logo和菜单-->
      <div class="head-left">
        <div class="logo">
          <div class="clock-container">
            <img src="static/img/logo.png" />
          </div>
          <router-link to="/home">TimeLease</router-link>
        </div>
        <div class="menu-container">
          <ul>
            <li class="home" data-menu='home'>
              <a href="javascript:void(0)">
                <i class="fa fa-home"></i>
                <span>首页</span>
              </a>
            </li>
            <li data-menu="find" class="find">
              <a href="javascript:void(0)">
                <i class="fa fa-clock-o"></i>
                <span>寻找时间</span>
              </a>
            </li>
            <li data-menu="lease" class="lease">
              <a href="javascript:void(0)">
                <i class="fa fa-cny"></i>
                <span>出租时间</span>
              </a>
            </li>
            <li data-menu="help" class="help">
              <a href="javascript:void(0)">
                <i class="fa fa-heartbeat"></i>
                <span>爱心公益</span>
              </a>
            </li>
            <li data-menu="shop" class="shop">
              <a href="javascript:void(0)">
                <i class="fa fa-shopping-cart"></i>
                <span>积分商城</span>
              </a>
            </li>
          </ul>
        </div>
      </div>
      <!--存放用户信息 -->
      <div class="head-right">
        <ul>
          <template v-if="user">
            <li @click="intoMy"><img :src="url+user.Icon"/></li>
            <li @click="intoMy">{{user.Name}}</li>
            <li>|</li>
            <li @click="headExit">退出</li>
          </template>
          <template v-else>
            <li @click="intoMy"><img src="static/img/user.png"/></li>
            <li @click="headLogin">登录</li>
          </template>
        </ul>
      </div>
      <!-- 切换按钮 -->
      <div class="small-button" @click="changeMenu">
          <i class="fa fa-bars"></i>
      </div>
    </div>
    <!-- 小屏幕适配 -->
    <div class="small-menu">
      <ul>
        <template v-if="user">
          <li data-menu="my"><a href="#">{{user.Name}}</a></li>
          <li @click="headExit" class="small-login"><a href="#">退出</a></li>
        </template>
        <template v-else>
          <li @click="headLogin" class="small-login"><a href="#">登录</a></li>
        </template>
        <li data-menu="home"><a href="#">首页</a></li>
        <li data-menu="find"><a href="#">寻找时间</a></li>
        <li data-menu="lease"><a href="#">出租时间</a></li>
        <li data-menu="help"><a href="#">爱心公益</a></li>
        <li data-menu="shop"><a href="#">积分商城</a></li>
      </ul>
    </div>

  </div>
</template>

<script>
import router from "../../router/index";
export default {
  data: function() {
    return {
      url: this.$store.state.url
    };
  },
  mounted: function() {
    var self = this;
    //编程式导航
    $(".menu-container li,.small-menu li:not(.small-login)").click(function() {
      self.into(this);
    });
  },
  watch: {},
  methods: {
    changeMenu() {
      $(".small-menu").slideToggle(500); //小屏幕餐单按钮
    },
    into(obj) {
      //路由切换
      var name = $(obj).attr("data-menu");
      router.push({ name: name });
    },
    headLogin() {
      //打开登录
      this.$store.commit("changeLogin", true);
    },
    intoMy() {
      //进入我的主页
      router.push({ name: "my" });
    },
    headExit() {
      //用户退出
      var self = this;
      $.confirm({
        title: "通知!",
        content: "你确定退出登录？",
        buttons: {
          取消: function() {},
          确定: {
            btnClass: "btn-blue",
            action: function() {
              delCookie("user");
              self.$store.commit("getUser");
              if (self.$route.path.indexOf("my") >= 0) {
                router.push({ name: "home" });
              }
            }
          }
        }
      });
    }
  },
  computed: {
    user() {
      return this.$store.state.user;
    }
  }
};
</script>

<style>
.head-container {
  width: 100%;
  height: 80px;
  background-color: #fff;
  box-shadow: 0 4px 8px 0 rgba(7, 17, 27, 0.1);
}
.head-main {
  width: 92%;
  margin: 0px auto;
}
.head-left {
  height: 100%;
  float: left;
}
.logo {
  float: left;
}
.clock-container {
  float: left;
  cursor: pointer;
  text-align: center;
  margin-right: 5px;
}
.clock-container img {
  width: 35px;
  height: 35px;
  margin-top: 23px;
}
.logo > a {
  color: #2c3e50;
  font-size: 33px;
  float: left;

  line-height: 80px;
}
.menu-container {
  float: left;
}
.menu-container li {
  width: 90px;
  float: left;
  height: 100%;
  cursor: pointer;
  margin-left: 30px;
  line-height: 80px;
  text-align: center;
}
.menu-container li i {
  color: gray;
  font-size: 18px;
}
.menu-container li a {
  color: #454545;
  font-size: 15px;
}
.current {
  background-color: #f6f6f6;
}
.head-right {
  float: right;
}
.head-right li {
  float: left;
  cursor: pointer;
  margin-left: 5px;
  line-height: 80px;
}
.head-right li:nth-child(1) img {
  width: 35px;
  height: 35px;
  border-radius: 50%;
}
/* 以上是针对大屏幕 */
.small-menu {
  width: 100%;
  opacity: 0.7;
  top: 80px;
  display: none;
  z-index: 99999;
  position: absolute;
  background-color: #fff;
}
.small-menu li {
  width: 100%;
  line-height: 35px;
  text-align: center;
  border-bottom: 1px solid #f3f3f3;
}
.small-menu li a {
  color: #333;
}
.small-button {
  width: 30px;
  height: 28px;
  float: right;
  display: none;
  cursor: pointer;
  margin-top: 27px;
  text-align: center;
  margin-left: 10px;
  background-color: #528c2e;
  border-radius: 3px;
}
.small-button i {
  color: #fff;
  line-height: 28px;
  font-size: 18px;
}
/* 以下是小屏幕适配 */
@media only screen and (max-width: 768px) {
  .head-right {
    display: none;
  }
  .menu-container {
    display: none;
  }
  .small-button {
    display: inline-block;
  }
}
</style>


