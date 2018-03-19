<template>
  <div class="login-container" v-if="isLogin">
      <!-- 遮罩 -->
      <div class="login-wrapper" @click="close"></div>
      <!-- 登录框 -->
      <div class="login-main">
        <div class="input-container">
            <h4>登录</h4>
            <p @click="intoRegister"><a href="javascript:void(0)">注册</a></p>
        </div>
        <div class="input-container">
            <input type="text" class="input-control" placeholder="手机号" v-model="phone">
        </div>
        <div class="input-container">
            <input type="password" class="input-control" placeholder="密码" v-model="password">
        </div>
        <div class="input-container">
            <input type="button" class="btn-control btn-info" value="登录" @click="userLogin">
        </div>
        <div class="input-container login-loader">
            <login-loader :isLoader="isLoader"></login-loader>
            <div class="tip-container" v-if="isTips">
                <i class="fa fa-exclamation-circle"></i>
                <p>提示：{{tip}}</p>
            </div>
            <p class="tip-start"></p>
        </div>
      </div>
  </div>
</template>

<script>
import router from "../../router/index.js";
import Loading from "../Tip/loading.vue";
import axios from "axios";
export default {
  data: function() {
    return {
      url: this.$store.state.url,
      phone: "",
      password: "",
      isLoader: false,
      isTips: false,
      tip: ""
    };
  },
  methods: {
    _clear() {
      //还原原始设计
      this.isLoader = false;
      this.isTips = false;
      this.password = "";
    },
    _tip(data) {
      //提示
      this.isTips = true;
      this.tip = data;
      showDialog(data);
    },
    userLogin() {
      //登录
      var self = this;
      if (self.phone == "") {
        self._tip("请输入手机号！");
      } else if (self.password == "") {
        self._tip("请输入密码！");
      } else {
        self.isLoader = true;
        self.isTips = false;
        axios
          .get(self.url + "/api/User/login", {
            params: {
              phone: self.phone,
              password: self.password
            }
          })
          .then(res => {
            setCookie("user", JSON.stringify(res.data), "d3"); //设置3天cookie
            self.$store.commit("changeLogin", false); //关闭登录框
            self._clear();
            self.$store.commit("getUser");
          })
          .catch(err => {
            if (err.response.status) {
              self._clear();
              self._tip(err.response.data.Reason);
              showDialog(err.response.data.Reason);
            }
          });
      }
    },
    intoRegister() {
      this.$store.commit("changeLogin", false);
      router.push({ name: "register" });
    },
    close() {
      this.$store.commit("changeLogin", false);
    }
  },
  computed: {
    isLogin() {
      return this.$store.state.isLogin;
    }
  },
  components: {
    "login-loader": Loading
  }
};
</script>

<style>
.login-loader .loader-container {
  margin: 0px auto !important;
}
.login-container {
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  z-index: 99999;
  position: fixed;
}
.login-wrapper {
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  z-index: 99999;
  position: fixed;
  opacity: 0.3;
  background-color: #444;
}
.login-main {
  width: 28%;
  height: 330px;
  left: 35%;
  top: 200px;
  position: fixed;
  z-index: 999999;
  border-radius: 5px;
  background-color: #fff;
  box-shadow: 0 2px 6px rgba(0, 0, 0, 0.2);
}
.input-container {
  width: 90%;
  margin: 18px auto;
  text-align: center;
}
.input-control {
  width: 100%;
  height: 40px;
  line-height: 40px;
  margin: 0px auto;
  padding: 0px 16px 0px 14px;
  border: 1px solid #dcdcdc;
}
.btn-control {
  width: 100%;
  height: 40px;
  border: 0px;
  outline: none;
  border-radius: 5px;
  margin: 10px auto;
}
/* 以上是登录主体 */
.tip-container {
  width: 100%;
  height: 33px;
  position: relative;
  display: table;
  border: solid 1px #ffc2b3;
  background: #ffd1ca;
  border-radius: 2px;
}
.tip-container i {
  width: 16px;
  height: 16px;
  display: inline-block;
  position: absolute;
  top: 10px;
  left: 10px;
  color: red;
}
.tip-container p {
  color: #666;
  font-size: 12px;
  padding-right: 25px;
  padding-left: 35px;
  display: table-cell;
  vertical-align: middle;
}

/*以上是错误提示*/
.tip-start {
  color: gray;
}
/* .tip-start:before,
.tip-start:after {
  content: "--";
} */
@media only screen and (min-width: 768px) and (max-width: 1023px) {
  .login-main {
    width: 50%;
    left: 25%;
    top: 20%;
  }
}
@media only screen and (max-width: 767px) {
  .login-main {
    width: 86%;
    left: 7%;
    top: 20%;
  }
}
@media only screen and (max-width: 370px) {
  .login-main {
    width: 94%;
    height: 340x;
    left: 3%;
    top: 20%;
  }
}
</style>


