<template>
  <div class="register-container">
    <form :action="url+'/api/User'" method="post" id="registerForm">
       <!-- 手机号 -->
      <div class="register-item">
          <div class="r-i-title">
              <img src="static/img/rphone.png" />
              <p>手机号</p>
          </div>
          <!-- 主体内容 -->
          <div class="r-i-main">
            <input type="text" class="hufe-input" placeholder="将作为登录账号！" name="Phone" v-model="phone">
          </div>
      </div>
      <!-- 用户名 -->
      <div class="register-item">
          <div class="r-i-title">
              <img src="static/img/ruser.png" />
              <p>用户名</p>
          </div>
          <!-- 主体内容 -->
          <div class="r-i-main">
            <input type="text" class="hufe-input" placeholder="请输入姓名不多余4个字！" name="Name" v-model="user">
          </div>
      </div>
       <!-- 密码 -->
      <div class="register-item">
          <div class="r-i-title">
              <img src="static/img/rpas.png" />
              <p>密码</p>
          </div>
          <!-- 主体内容 -->
          <div class="r-i-main">
            <input type="text" class="hufe-input" placeholder="请输入至少6位" name="Password" v-model="password">
          </div>
      </div>
      <!-- 头像-->
      <div class="register-item">
        <div class="r-i-title">
            <img src="static/img/rhead.png" />
            <p>封面</p>
        </div>
        <!-- 主体内容 -->
        <div class="r-i-main">
            <div class="input-block">
              <input type="file" value="" name="Cover" style="display:none" class="userIcon" @change="coverUpload">
              <input type="text" class="hufe-input" readonly placeholder="点击上传封面" name="Cover" v-model="cover"/>
            </div>
            <div class="i-block cover pointer" @click="selectIcon">
                <i class="fa fa-photo"></i>
            </div> 
            <div class="clear"></div>
        </div>
      </div>
      <!-- 城市-->
      <div class="register-item">
           <div class="r-i-title">
                <img src="static/img/city.png" />
                <p>关联城市</p>
            </div>
            <!-- 主体内容 -->
            <div class="r-i-main">
                <div class="input-block">
                    <input type="text" class="hufe-input" readonly placeholder="关联城市" name="City" v-model="city"/>
                </div>
                <div class="i-block l-city pointer" @click="openCity">
                    <i class="fa fa-map-marker"></i>
                </div>
                <div class="clear"></div>
            </div>
      </div>

       <!-- 保存按钮-->
      <div class="register-item submit-register">
          <input type="submit" style="display:none" id="submitRegister">
          <input type="button" value="注册" @click="check"> 
      </div>
    </form>
    <register-place @popCity="selectCity"></register-place>
  </div>
</template>

<script>
import Place from "../content/home/place.vue";
export default {
  mounted: function() {
    var self = this;
    var options = {
      success: function(data) {
        $(".jconfirm").remove();
        showDialog("注册成功，请登录！");
        self.clear();
        setTimeout(function() {
          self.$store.commit("changeLogin", true);
        }, 1000);
      },
      error: function(data) {
        $(".jconfirm").remove();
        showDialog(eval("(" + data.responseText + ")").Reason);
      }
    };
    $("#registerForm").ajaxForm(options);
  },
  data: function() {
    return {
      url:this.$store.state.url,
      phone: "",
      user: "",
      password: "",
      cover: "",
      city: ""
    };
  },
  components: {
    "register-place": Place
  },
  methods: {
    clear() {
      this.phone = "";
      this.user = "";
      this.password = "";
      this.cover = "";
      this.city = "";
    },
    selectCity(data) {
      //子组件传来的城市数据
      this.city = data;
      this.closeCity();
    },
    openCity() {
      //打开城市选择组件
      $(".place-container").animate({ right: "0%" });
    },
    closeCity() {
      //关闭
      $(".place-container").animate({ right: "-80%" });
    },
    selectIcon() {
      //触发选择城市
      $(".userIcon").click();
    },
    coverUpload() {
      var self = this;
      var fixname = $(".userIcon")
        .val()
        .substring(
          $(".userIcon")
            .val()
            .lastIndexOf("."),
          $(".userIcon").val().length
        );
      var isRight = false;
      var fix = [".png", ".jpg", ".jpeg", ".gif"];
      for (var i = 0; i < fix.length; i++) {
        if (fix[i].indexOf(fixname) > -1) {
          isRight = true;
          break;
        }
      }
      if (isRight) {
        self.cover = $(".userIcon").val();
      } else {
        $(".userIcon").val("");
        showDialog("请输入正确格式！");
      }
    },
    check() {
      //检查表单数据
      var self = this;
      if (self.phone == "" || self.phone.length != 11) {
        showDialog("请输入正确的手机号，将作为用户账号！");
      } else if (self.user == "" || self.user.length > 4) {
        showDialog("请输入不多于四个字的用户名！");
      } else if (self.password == "" || self.password.length < 6) {
        showDialog("请输入密码至少六位！");
      } else if (self.cover == "") {
        showDialog("请上传头像，1:1比例");
      } else if (self.city == "") {
        showDialog("请选择你常在的城市！");
      } else {
        loadingDialog();
        $("#submitRegister").click();
      }
    }
  }
};
</script>

<style>
.l-city {
  background-color: #76c2af;
}
.cover {
  background-color: #e0995e;
}
.article {
  background-color: #1a9d6c;
}
.pointer {
  cursor: pointer !important;
}
.register-container {
  width: 70%;
  margin: 25px auto;
}
.register-item {
  width: 100%;
  margin-top: 30px;
}
.r-i-title {
  width: 100%;
  height: 55px;
}
.r-i-title img {
  width: 45px;
  height: 45px;
  float: left;
}
.r-i-title p {
  float: left;
  margin-left: 3px;
  line-height: 45px;
}
.r-i-main {
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
.r-i-main li {
  height: 30px;
  padding: 0px 10px;
  display: inline-block;
  border-radius: 15px;
  margin: 5px 5px;
  cursor: pointer;
  background-color: #d2d2d2;
}
.r-i-main li p {
  line-height: 30px;
  font-size: 13px;
  color: #fff;
  float: left;
}
.r-i-main li i {
  font-size: 13px;
  color: #fff;
  float: right;
  line-height: 30px;
  margin-left: 10px;
}
.submit-register {
  width: 95%;
  height: 80px;
  margin: 20px auto;
}
.submit-register input {
  width: 150px;
  height: 40px;
  border: 0px;
  outline: none;
  border-radius: 3px;
  color: #fff;
  background-color: #5bc0de;
}
.r-i-title a {
  line-height: 45px;
  float: left;
  margin-left: 5px;
}

@media only screen and (max-width: 768px) {
  .register-container {
    width: 95%;
  }
}
</style>


