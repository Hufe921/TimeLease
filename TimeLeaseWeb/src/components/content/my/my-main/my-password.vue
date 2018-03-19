<template>
  <div class="my-password-container">
    <div class="password-item">
        <div class="p-i-title">
            <img src="static/img/mold.png" />
            <p>旧密码</p>
        </div>
        <!-- 主体内容 -->
        <div class="p-i-main">
        <input type="password" class="hufe-input" placeholder="请输入原始密码！" v-model="putPassword.oldPassword">
        </div>
    </div>
    <div class="password-item">
        <div class="p-i-title">
            <img src="static/img/rpas.png" />
            <p>新密码</p>
        </div>
        <!-- 主体内容 -->
        <div class="p-i-main">
        <input type="password" class="hufe-input" placeholder="请输入至少6位" v-model="putPassword.newPassword">
        </div>
    </div>
    <div class="password-item">
        <div class="p-i-title">
            <img src="static/img/rpas.png" />
            <p>重复密码</p>
        </div>
        <!-- 主体内容 -->
        <div class="p-i-main">
        <input type="password" class="hufe-input" placeholder="请重复输入新密码" v-model="putPassword.RepeatPassword">
        </div>
    </div>
     <!-- 保存按钮-->
    <div class="password-item submit-password">
        <input type="button" value="保存" @click="updatepassword"> 
    </div>
  </div>
</template>

<script>
import axios from "axios";
import router from "../../../../router/index.js";
export default {
  data: function() {
    return {
      url: this.$store.state.url,
      putPassword: { oldPassword: "", newPassword: "", RepeatPassword: "" }
    };
  },
  methods: {
    updatepassword() {
      var self = this;
      if (self.putPassword.oldPassword == "") {
        showDialog("请输入旧密码！");
      } else if (self.putPassword.newPassword == "") {
        showDialog("请输入新密码！");
      } else if (
        self.putPassword.newPassword != self.putPassword.RepeatPassword
      ) {
        showDialog("两次密码不一致！");
      } else {
        const address = `${self.url}/api/User/${self.putPassword
          .oldPassword}/${self.putPassword.newPassword}/${self.user
          .Id}/putPassword`;
        axios
          .put(address)
          .then(res => {
            self.putPassword.oldPassword = "";
            delCookie("user");
            self.$store.commit("getUser");
            showDialog("修改成功，请重新登录！");
            router.push({ name: "home" });
          })
          .catch(err => {
            if (err.response.data.Reason) {
              showDialog(err.response.data.Reason);
            }
          });
      }
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
.my-password-container {
  width: 100%;
}
.password-item {
  width: 100%;
  margin-top: 30px;
}
.p-i-title {
  width: 100%;
  height: 55px;
}
.p-i-title img {
  width: 45px;
  height: 45px;
  float: left;
}
.p-i-title p {
  float: left;
  margin-left: 3px;
  line-height: 45px;
}
.p-i-main {
  width: 95%;
  margin: 0px auto;
}
.i-block {
  width: 110px;
  height: 38px;
  float: left;
  text-align: center;
  border-top-right-radius: 3px;
  border-bottom-right-radius: 3px;
}
.i-block i {
  color: #fff;
  line-height: 38px;
  font-size: 18px;
}
.input-block {
  width: calc(100% - 110px);
  height: 38px;
  float: left;
}
.input-block input {
  border-top-right-radius: 0px;
  border-bottom-right-radius: 0px;
}
.p-i-main li {
  height: 30px;
  padding: 0px 10px;
  display: inline-block;
  border-radius: 15px;
  margin: 5px 5px;
  cursor: pointer;
  background-color: #d2d2d2;
}
.p-i-main li p {
  line-height: 30px;
  font-size: 13px;
  color: #fff;
  float: left;
}
.p-i-main li i {
  font-size: 13px;
  color: #fff;
  float: right;
  line-height: 30px;
  margin-left: 10px;
}
.submit-password {
  width: 95%;
  height: 80px;
  margin: 20px auto;
}
.submit-password input {
  width: 150px;
  height: 40px;
  border: 0px;
  outline: none;
  border-radius: 3px;
  color: #fff;
  background-color: #5bc0de;
}
.p-i-title a {
  line-height: 45px;
  float: left;
  margin-left: 5px;
}
</style>


