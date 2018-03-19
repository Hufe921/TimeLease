<template>
  <div class="shop-head-container">
        <!--轮播 -->
        <div class="shop-swiper-container">
            <shop-swiper :imgList="bannerList"></shop-swiper>
        </div>
        <!-- 积分说明 -->
        <div class="shop-description">
            <p>亲爱的用户：通过寻找时间，出租时间，爱心公益模块获得的积分可以在此商城，进行礼物兑换，具体个人积分请登录主页在我的积分模块查看，祝：愉快！</p>
        </div> 
        <div class="shop-tool">
            <div class="shop-remark" style="background-image: url(static/img/shop-banner.png);">
                <div class="shop-mask"></div>
                <div class="shop-remark-left">
                    <p>
                        积分兑换
                    </p>
                </div>
            </div>
            <div class="shop-menu">
                <ul>
                    <li class="shop-current">全部</li>
                    <li v-for="(item,index) in type" :key="index">{{item.Name}}</li>
                </ul>
                <div class="clear"></div>
            </div>
        </div>
        

        <!-- 商品展示 -->
        <div class="shop-main">
            <div class="shop-main-center">
              <template v-if="shop.length!=0">
                <ul>
                  <li v-for="(item,index) in shop" :key="index"  @mouseover="over($event)" @mouseout="out($event)">
                      <div class="shop-first">
                          <img :src="url+item.Cover" />
                          <p class="shop-price">{{item.BonusPoint}}积分</p>
                          <p class="shop-name">{{item.Name}}</p>
                      </div>
                      <div class="shop-second">
                          <p class="shop-remark">{{item.Remark}}</p>
                          <p class="shop-price-s">{{item.BonusPoint}}积分</p>
                          <input type="button" value="立即兑换" @click="postExchange(item.ID)"/>
                      </div>  
                  </li>
              </ul>
              <div class="clear"></div>
              <shop-more :tip="message" @click.native="getMore"></shop-more>
              </template>
              <shop-load :isLoader="isLoader"></shop-load>  
            </div>  
        </div>
      <shop-exchange :isShow="isExchange" :shopId="shopId" @closeExchange="closeExchange"></shop-exchange>
  </div>
</template>

<script>
import ShopMore from "../more.vue";
import Head from "../swiper.vue";
import axios from "axios";
import ShopLoad from "../../Tip/loading.vue";
import ShopExchange from "./shop-exchange.vue";
export default {
  mounted: function() {
    this.getType();
    this.getShop();
  },
  data: function() {
    return {
      bannerList: [{ src: "static/img/s1.png" }, { src: "static/img/s2.png" }],
      url: this.$store.state.url,
      type: [],
      shop: [],
      currentPage: 1,
      typeId: 0,
      shopId: 0,
      message: "加载更多",
      isLoader: true,
      isAllow: true,
      isExchange: false
    };
  },
  methods: {
    getType() {
      var self = this;
      const address = `${self.url}/api/Shop/getShopType`;
      axios.get(address).then(res => {
        self.type = res.data;
      });
    },
    getShop() {
      var self = this;
      const address = `${self.url}/api/Shop/${self.currentPage}/getShop?typeId=${self.typeId}`;
      axios.get(address).then(res => {
        self.isLoader = false;
        if (res.data.length < 8) {
          self.isAllow = false;
          self.message = "加载完成";
        }
        self.shop = res.data;
      });
    },
    getMore() {
      var self = this;
      if (self.isAllow) {
        self.currentPage++;
        self.message = "正在加载...";
        const address = `${self.url}/api/Shop/${self.currentPage}/getShop?typeId=${self.typeId}`;
        axios
          .get(address)
          .then(res => {
            if (res.data.length < 8) {
              self.message = "加载完成";
              self.isAllow = false;
            } else {
              self.message = "加载更多";
            }
            res.data.forEach(item => {
              self.shop.push(item);
            });
          })
          .catch(err => {
            if (err.response.data.Reason) {
              showDialog(err.response.data.Reason);
            }
          });
      }
    },
    postExchange(id) {
      this.shopId = id;
      var self = this;
      if (self.user) {
        loadingDialog();
        const address = `${self.url}/api/Shop/${id}/${self.user
          .Id}/isAllowExchange`;
        axios
          .get(address)
          .then(res => {
            $(".jconfirm").remove();
            if (res.data) {
              self.isExchange = true;
            } else {
              self.isExchange = false;
              showDialog("您的的积分不足以兑换该商品！");
            }
          })
          .catch(err => {
            self.isExchange = false;
            if (err.response.data.Reason) {
              showDialog(err.response.data.Reason);
            }
          });
      } else {
        showDialog("请登录！");
      }
    },
    closeExchange() {
      this.isExchange = false;
    },
    over(event) {
      var obj = event.currentTarget;
      $(obj)
        .find(".shop-first")
        .css("display", "none");
      $(obj)
        .find(".shop-second")
        .css("display", "block");
    },
    out(event) {
      var obj = event.currentTarget;
      $(obj)
        .find(".shop-first")
        .css("display", "block");
      $(obj)
        .find(".shop-second")
        .css("display", "none");
    }
  },
  computed: {
    user() {
      return this.$store.state.user;
    }
  },
  components: {
    "shop-swiper": Head,
    "shop-more": ShopMore,
    "shop-load": ShopLoad,
    "shop-exchange": ShopExchange
  }
};
</script>

<style>
.shop-head-container {
  width: 100%;
}
.shop-swiper-container {
  width: 75%;
  margin: 20px auto;
}
/* 以上是轮播 */
.shop-description {
  width: 75%;
  margin: 30px auto;
  color: gray;
  font-size: 13px;
}

/* 以上是积分商城说明 */
.shop-tool {
  width: 75%;
  margin: 20px auto;
  border-radius: 6px;
  background-color: #fff;
  border: 1px solid transparent;
  box-shadow: 00 4px 20px rgba(0, 0, 0, 0.2);
}
.shop-remark {
  width: 95%;
  height: 90px;
  position: relative;
  margin: 20px auto;
  border-radius: 6px;
}
.shop-mask {
  z-index: 1;
  background-color: rgba(0, 0, 0, 0.5);
  border-radius: 6px;
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
}
.shop-remark-left {
  width: 100%;
  height: 100%;
  float: left;
  z-index: 2;
  position: absolute;
}
.shop-remark-left p {
  color: #fff;
  font-size: 30px;
  line-height: 90px;
  letter-spacing: 3px;
  text-align: center;
}
/* 以下是工具栏 */
.shop-menu {
  width: 95%;
  margin: 15px auto;
}
.shop-current {
  color: #fff !important;
  background: linear-gradient(to right, #ffae12 0, #f07d17 100%);
}
.shop-menu li {
  height: 28px;
  float: left;
  color: gray;
  cursor: pointer;
  margin-left: 5px;
  line-height: 14px;
  padding: 7px 18px;
  text-align: center;
  letter-spacing: 1px;
  border-radius: 15px;
}
/* 以上是分隔图和工具栏 */
.shop-main {
  width: 100%;
  margin: 30px auto;
  background-color: #fff;
  border: 1px solid transparent;
}
.shop-main-center {
  width: 85%;
  max-width: 1300px;
  margin: 15px auto;
}
.shop-main li {
  width: 20%;
  height: 345px;
  float: left;
  cursor: pointer;
  margin: 10px 2.5%;
  border-radius: 5px;
  overflow: hidden;
  background-color: #fff;
  border: 1px solid transparent;
}
.shop-first {
  width: 100%;
  height: 100%;
}
.shop-first img {
  width: 100%;
  height: 265px;
}
.shop-name {
  color: #333;
  text-align: center;
}
.shop-price {
  color: #d4282d;
  font-size: 13px;
  text-align: center;
}
.shop-second {
  width: 100%;
  height: 100%;
  background-color: #f2f2f2;
  display: none;
  border: 1px solid transparent;
}
.shop-remark {
  color: gray;
  text-align: center;
  margin-top: 70px;
  letter-spacing: 2px;
}
.shop-second input {
  width: 150px;
  height: 48px;
  margin: 10px auto;
  outline: none;
  border: 0px;
  color: #fff;
  display: block;
  border-radius: 3px;
  background-color: #b4a078;
}
.shop-price-s {
  color: #d4282d;
  font-size: 16px;
  text-align: center;
}

@media only screen and (min-width: 1124px) and (max-width: 1439px) {
  .shop-main-center {
    width: 95%;
  }
}
@media only screen and (min-width: 768px) and (max-width: 1124px) {
  .shop-main-center {
    width: 85%;
  }
  .shop-main li {
    width: 45%;
    margin: 10px 2.5%;
  }
}
@media only screen and (max-width: 765px) {
  .shop-main-center {
    width: 80%;
  }
  .shop-main li {
    width: 90%;
    margin: 10px 5%;
  }
  .shop-swiper-container {
    width: 95%;
  }
  .shop-swiper-container {
    width: 100%;
    margin: 0px auto;
  }
  .shop-remark {
    margin-top: 20px;
  }
  .shop-description {
    width: 95%;
  }
  .shop-tool {
    width: 95%;
  }
}
</style>


