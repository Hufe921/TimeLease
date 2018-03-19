<template>
    <div class="m-h-container" style="background-image: url(static/img/d1.png);">
        <!-- 标题 -->
        <div class="m-h-title">
            <p>我的信息及设置</p>
        </div>
        <!--时间出租与历史 -->
        <div class="m-h-data">
            <div class="m-h-d-main">
                <!-- 用户头像 -->
                <div class="m-h-d-left user-poster">
                    <img :src="url+user.Icon" />
                    <p>{{user.Name}}</p>
                </div>
                <!-- 用户积分 -->
                <div class="m-h-d-left">
                    <img src="static/img/bp.png" />
                    <p>{{bp}}</p>
                    <p>我的积分</p>
                </div>
                <!-- 用户所在城市 -->
                <div class="m-h-d-left">
                    <img src="static/img/nav.png" />
                    <p>{{user.City}}</p>
                    <p>常在城市</p>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
import axios from "axios";
export default {
  mounted: function() {
    this.getBp(); //获取用户积分
  },
  data: function() {
    return {
      url: this.$store.state.url,
      bp: 0
    };
  },
  methods: {
    getBp() {//获取用户积分
      var self = this;
      axios
        .get(self.url + "/api/User/bp", {
          params: {
            userId: self.user.Id
          }
        })
        .then(res => {
          self.bp = res.data;
        })
        .catch(err => {
          if (err.response.data.Reason) {
            showDialog(err.response.data.Reason);
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
.hong {
  color: #d65656 !important;
}
.huang {
  color: #d99b08 !important;
}
.lan {
  color: #00aeee !important;
}
.m-h-container {
  width: 100%;
  height: 350px;
  overflow: hidden;
  background-size: cover;
  background-repeat: no-repeat;
  background-position: center center;
}
.m-h-title {
  width: 90%;
  margin: 80px auto 40px;
  border: 1px solid transparent;
}
.m-h-title p {
  font-size: 48px;
  line-height: 48px;
  color: #fff;
  letter-spacing: 2px;
  font-weight: 300;
  text-align: center;
  text-shadow: 2px 3px 0 rgba(0, 0, 0, 0.15);
}
.m-h-data {
  width: 90%;
  height: 100px;
  margin: 60px auto;
  background-color: #fff;
  border-radius: 12px;
  border: 1px solid transparent;
  box-shadow: 0px 12px 24px rgba(7, 17, 27, 0.1);
}
.m-h-d-main {
  width: 98%;
  height: 80px;
  margin: 10px auto 0;
}
.m-h-d-main > div {
  width: 33%;
  height: 80px;
  cursor: pointer;
  text-align: center;
}
.m-h-d-main img {
  width: 50px;
  height: 50px;
  margin-bottom: 5px;
}
.m-h-d-left {
  float: left;
  border-right: 1px solid #dcdcdc;
}
.user-poster p {
  font-size: 16px !important;
}
.user-poster img {
  width: 60px;
  height: 60px;
  border-radius: 50%;
}
.m-h-d-left p {
  font-size: 13px;
  line-height: 10px;
}
.m-h-d-left:nth-child(3) {
  border: 0px;
}
@media only screen and (max-width: 1023px) {
  .d-h-data {
    width: 99%;
  }
  .m-h-data {
    width: 98%;
  }
  .m-h-title p {
    font-size: 38px;
  }
}
</style>


