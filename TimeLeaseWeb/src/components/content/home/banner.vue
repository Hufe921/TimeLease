<template>
  <div class="banner-container" style="background-image: url('static/img/m.png');">
      <div class="banner-main">
          <!-- 存放定位及系统通知 -->
          <div class="place-notify">
              <!-- 定位 -->
              <div class="palce-container">
                  <img src="static/img/loc.png"/>
                  <p>{{city}}</p>
                  <i class="fa fa-plus-square-o plus" @click="openCity"></i>
              </div>
              <!-- 通知 -->
              <div class="notify">
                <ul>
                    <li><a href="#"><p></p></a></li>
                </ul>
              </div>
              <div class="clear"></div>
          </div>

          <!--首页集成信息 -->
          <div class="banner-message">
              <!--该城市数据信息 -->
              <div class="data-message">
                <div class="b-m-data">
                    <div class="b-m-main">
                        <!-- 时间出租 -->
                        <div class="b-m-left">
                            <img src="static/img/hfind.png" />
                            <p>{{cityData.find}}</p>
                            <p>寻找时间</p>
                        </div>
                        <!-- 发布历史 -->
                        <div class="b-m-left">
                            <img src="static/img/htime.png" />
                            <p>{{cityData.lease}}</p>
                            <p>出租时间</p>
                        </div>

                        <div class="b-m-left">
                            <img src="static/img/hxin.png" />
                            <p>{{cityData.help}}</p>
                            <p>爱心公益</p>
                        </div>
                    </div>
                </div>
              </div>
          </div> 
          <div class="clear"></div>
      </div>
      <banner-place @popCity="selectCity"></banner-place>
  </div>
</template>

<script>
import Place from "./place.vue";
import axios from "axios";
export default {
  mounted: function() {
    this.getCityData();
  },
  data: function() {
    return {
      url: this.$store.state.url,
      city: this.user ? this.user.city : "合肥市",
      cityData: { find: 0, lease: 0, help: 0 }
    };
  },
  methods: {
    selectCity(data) {
      this.city = data;
      this.getCityData();
      this.closeCity();
    },
    closeCity() {
      $(".place-container").animate({ right: "-80%" });
    },
    openCity() {
      $(".place-container").animate({ right: "0%" });
    },
    getCityData() {
      var self = this;
      const address = `${self.url}/api/Article/getDataByCity?city=${self.city}`;
      axios.get(address).then(res => {
        self.cityData = res.data;
      });
    }
  },
  computed: {
    user() {
      return this.$store.state.user;
    }
  },
  components: {
    "banner-place": Place
  }
};
</script>

<style>
.banner-container {
  width: 100%;
  height: 450px;
  background-color: #fff;
  background-size: cover;
  background-repeat: no-repeat;
  background-position: center center;
}
.banner-main {
  width: 85%;
  margin: 0px auto;
  border: 1px solid transparent;
}
.place-notify {
  width: 90%;
  margin: 50px auto;
  padding-bottom: 5px;
  border: 1px solid transparent;
}
.palce-container {
  height: 50px;
  float: left;
}
.palce-container i {
  color: #333;
  font-size: 28px;
}
.palce-container img {
  float: left;
  width: 35px;
  height: 35px;
  margin-top: 22px;
  margin-right: 8px;
  margin-left: 10px;
}
.plus {
  cursor: pointer;
  margin-left: 10px;
  margin-top: 30px !important;
}
.palce-container p {
  float: left;
  color: #333;
  margin-right: 5x;
  font-size: 46px;
}
/* 以上是定位 */
.notify {
  float: right;
  height: 30px;
  overflow: hidden;
  margin-top: 25px;
}
.notify li {
  height: 30px;
}
.notify li img {
  width: 22px;
  height: 22px;
  float: left;
  margin-right: 5px;
  margin-top: 3px;
}
.notify li p {
  color: #333;
  float: left;
  line-height: 30px;
}
/* 以上是通知 */
.banner-message {
  width: 100%;
  margin: 210px auto;
}
.data-message {
  width: 100%;
}
.b-m-data {
  width: 100%;
  height: 110px;
  margin: 0px auto;
  background-color: #fff;
  border-radius: 12px;
  border: 1px solid #ececec;
  box-shadow: 0px 12px 24px rgba(7, 17, 27, 0.1);
}
.b-m-main {
  width: 98%;
  height: 80px;
  margin: 10px auto 0;
}
.b-m-main > div {
  width: 33%;
  height: 80px;
  cursor: pointer;
  text-align: center;
}
.b-m-main img {
  width: 50px;
  height: 50px;
  margin-bottom: 5px;
}
.b-m-left {
  float: left;
  border-right: 1px solid #dcdcdc;
}
.b-m-left:nth-child(3) {
  border: 0px;
}
.b-m-left p {
  font-size: 13px;
  line-height: 10px;
}
/* 以上是banner区的地区集成信息 */
@media only screen and (min-width: 1024px) and (max-width: 1365px) {
  .banner-main {
    width: 95%;
  }
  .banner-message,
  .place-notify {
    width: 95%;
  }
}
@media only screen and (min-width: 768px) and (max-width: 1023px) {
  .banner-main {
    width: 95%;
  }
  .banner-message,
  .place-notify {
    width: 100%;
  }
}
/* 以上是banner的集成信息 */
@media only screen and (max-width: 767px) {
  .place-notify {
    float: left;
  }
  .banner-main {
    width: 99%;
  }
  .banner-container {
    height: 350px;
  }
  .banner-message,
  .place-notify {
    width: 100%;
  }
  .banner-message {
    margin: 200px auto;
  }
  .data-message {
    width: 100%;
  }
  .banner-container {
    background-position: 33%;
  }
  .palce-container p {
    font-size: 40px;
    margin-top: 7px;
  }
}
/* 以上是小屏幕适配 */
</style>


