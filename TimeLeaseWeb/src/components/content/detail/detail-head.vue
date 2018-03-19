<template>
    <div class="d-h-container" style="background-image: url(static/img/home.png);">
        <!-- 导航 -->
        <div class="d-h-nav">
          <template v-if="detail.Type='爱心公益'">
            <p>爱心公益/{{detail.Tag}}</p>
          </template>
          <template v-else>
            <p>出租时间/{{detail.Tag}}</p>
          </template>  
        </div>
        <!-- 标题 -->
        <div class="d-h-title">
            <p>{{detail.Title}}</p>
        </div>
        <!--该文章数据及帮助按钮 -->
        <div class="d-h-data">
            <div class="d-h-d-main">
                <!-- 该文章信息 -->
                <div class="d-h-d-left">
                    <ul>
                        <li>
                            <img src="static/img/line.png" />
                            <p>{{detail.User}}</p>
                        </li>
                        <li>
                            <img src="static/img/nav.png" />
                            <p>{{detail.City}}</p>
                        </li>
                        <li>
                            <img src="static/img/see.png" />
                            <p>{{detail.Look}}</p>
                        </li>
                        <li>
                            <img src="static/img/up.png" @click="praise(detail.ID)" class="praise"/>
                            <p>{{detail.Praise}}</p>
                        </li>
                    </ul>
                </div>
                <!-- 价钱及购买按钮 -->
                <div class="d-h-d-right">
                    <!-- 价格 -->
                    <div class="d-h-d-price">
                        <p>所需积分：{{detail.BonusPoints}}</p>
                    </div>
                    <!-- 购买按钮 -->
                    <div class="d-hd-button">
                        <input type="button" value="立即联系" @click="applyShow"/>
                    </div>
                </div>

            </div>
        </div>
    </div>
</template>

<script>
import axios from "axios";
export default {
  mounted: function() {
    this.getDetail();
    this.getBp();
  },
  data: function() {
    return {
      url: this.$store.state.url,
      bp: 0,
      articleBp: 0,
      detail: {}
    };
  },
  methods: {
    getDetail() {
      var self = this;
      const address = `${self.url}/api/Article/${self.user.Id}/${self.$route
        .query.Id}/getDetailById`;
      axios
        .get(address)
        .then(res => {
          self.detail = res.data;
          self.articleBp = res.data.BonusPoints;
        })
        .catch(err => {
          if (err.response.data.Reason) {
            showDialog(err.response.data.Reason);
          }
        });
    },
    praise(Id) {
      var self = this;
      const address = `${self.url}/api/Article/${self.user
        .Id}/${Id}/articlePraise`;
      axios
        .post(address)
        .then(res => {
          self.getDetail();
        })
        .catch(err => {
          if (err.response.data.Reason) {
            showDialog(err.response.data.Reason);
          }
        });
    },
    getBp() {
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
            self.bp = 0;
          }
        });
    },
    applyShow() {
      var self = this;
      if (self.bp < self.articleBp) {
        showDialog("所需积分不足！请充值");
      } else {
        self.$emit("ApplyShowByHead", true);
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
.hong {
  color: #d65656 !important;
}
.huang {
  color: #d99b08 !important;
}
.lan {
  color: #00aeee !important;
}
.praise {
  cursor: pointer !important;
}
.d-h-container {
  width: 100%;
  height: 350px;
  overflow: hidden;
  background-size: cover;
  background-repeat: no-repeat;
  background-position: center center;
}
.d-h-nav {
  width: 90%;
  height: 30px;
  margin: 50px auto;
}
.d-h-nav p {
  color: rgba(255, 255, 255, 0.8);
  line-height: 30px;
}
.d-h-title {
  width: 90%;
  margin: 50px auto;
}
.d-h-title p {
  font-size: 48px;
  line-height: 48px;
  color: #fff;
  letter-spacing: 2px;
  font-weight: 300;
  text-align: center;
  text-shadow: 2px 3px 0 rgba(0, 0, 0, 0.15);
}
.d-h-data {
  width: 90%;
  height: 100px;
  margin: 0px auto;
  background-color: #fff;
  border-radius: 12px;
  border: 1px solid transparent;
  box-shadow: 0px 12px 24px rgba(7, 17, 27, 0.1);
}
.d-h-d-main {
  width: 98%;
  height: 80px;
  margin: 10px auto 0;
}
.d-h-d-left {
  width: 70%;
  height: 80px;
  float: left;
}
.d-h-d-left li {
  width: 20%;
  height: 80px;
  float: left;
  text-align: center;
  border-right: 1px solid #dcdcdc;
}
.d-h-d-left li img {
  width: 40px;
  height: 40px;
  margin-top: 10px;
}
.d-h-d-left li p {
  color: #555;
  font-size: 13px;
}
.d-h-d-right {
  width: 30%;
  height: 80px;
  float: right;
}
.d-h-d-right > div {
  float: left;
}
.d-h-d-price p {
  font-size: 24px;
  color: #f01414;
  line-height: 80px;
}
.d-hd-button {
  margin-top: 20px;
  margin-left: 5%;
}
.d-hd-button input {
  width: 150px;
  height: 40px;
  border: 0px;
  outline: none;
  border-radius: 3px;
  color: #fff;
  background-color: #f01414;
}
@media only screen and (max-width: 1023px) {
  .d-h-data {
    width: 99%;
  }
  .d-h-d-left {
    width: 60%;
  }
  .d-h-d-right {
    width: 40%;
  }
  .d-h-title p {
    font-size: 38px;
  }
}
@media only screen and (max-width: 765px) {
  .d-h-data {
    margin-top: -30px;
  }
  .d-h-d-left {
    width: 60%;
    display: none;
  }
  .d-h-d-right {
    width: 90%;
  }
  .d-hd-button input {
    width: 145px;
  }
}
@media only screen and (max-width: 370px) {
  .d-h-d-right {
    width: 100%;
  }
  .d-hd-button input {
    width: 100px;
  }
}
</style>


