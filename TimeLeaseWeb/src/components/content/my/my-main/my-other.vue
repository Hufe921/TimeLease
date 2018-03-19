<template>
  <div class="other-container">
      <!-- 手机号 -->
      <div class="other-item">
          <div class="r-i-title">
              <img src="static/img/rphone.png" />
              <p>手机号</p>
          </div>
          <!-- 主体内容 -->
          <div class="r-i-main">
            <input type="text" class="hufe-input" placeholder="将作为登录账号！" v-model="phone">
          </div>
      </div>
      <!-- 用户名 -->
      <div class="other-item">
          <div class="r-i-title">
              <img src="static/img/ruser.png" />
              <p>用户名</p>
          </div>
          <!-- 主体内容 -->
          <div class="r-i-main">
            <input type="text" class="hufe-input" placeholder="请输入姓名不多余4个字！" v-model="name">
          </div>
      </div>
      
      <!-- 城市-->
      <div class="other-item">
           <div class="r-i-title">
                <img src="static/img/city.png" />
                <p>关联城市</p>
            </div>
            <!-- 主体内容 -->
            <div class="r-i-main">
                <div class="input-block">
                    <input type="text" class="hufe-input" readonly placeholder="关联城市" v-model="city"/>
                </div>
                <div class="i-block l-city" @click="openCity">
                    <i class="fa fa-map-marker"></i>
                </div>
                <div class="clear"></div>
            </div>
      </div>

       <!-- 保存按钮-->
      <div class="other-item submit-other">
          <input type="button" value="保存" @click="updateCity"> 
      </div>
     <other-place  @popCity="selectCity"></other-place>
  </div>
</template>

<script>
import Place from "../../home/place.vue";
import axios from "axios";
import router from "../../../../router/index.js"
export default {
  data: function() {
    return {
      url: this.$store.state.url,
      phone: this.$store.state.user.Phone,
      name: this.$store.state.user.Name,
      city: this.$store.state.user.City
    };
  },
  methods: {
    selectCity(data) {
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
    updateCity() {
      var self = this;
      if (self.phone == "" || self.phone.length != 11) {
        showDialog("请输入正确的手机号，将作为用户账号！");
      } else if (self.name == "" || self.name.length > 4) {
        showDialog("请输入不多于四个字的用户名！");
      } else if (self.city == "") {
        showDialog("请选择你常在的城市！");
      } else {
        const address = `${self.url}/api/User/${self.user.Id}/${self.phone}/${self.name}/${self.city}/putBasic`;
        axios
          .put(address)
          .then(res => {
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
  },
  components: {
    "other-place": Place
  }
};
</script>

<style>
.l-city {
  cursor: pointer;
  background-color: #76c2af;
}
.cover {
  background-color: #e0995e;
}
.article {
  background-color: #1a9d6c;
}
.other-container {
  width: 100%;
  margin: 25px auto;
}
.other-item {
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
.submit-other {
  width: 95%;
  height: 80px;
  margin: 20px auto;
}
.submit-other input {
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
  .other-container {
    width: 95%;
  }
}
</style>


