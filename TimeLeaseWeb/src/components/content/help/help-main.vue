<template>
  <div class="help-main-container">
    <div class="help-banner">
      <help-swiper :imgList="bannerList"></help-swiper>
    </div>
    <div class="help-content">
      <div class="l-c-head">
        <div class="l-c-main">
          <!-- 爱心公益 -->
          <div class="l-c-left" @click="getArticle">
              <img src="static/img/love3.png" />
              <p>爱心公益</p>
          </div>
          <!-- 我的爱心 -->
          <div class="l-c-right" @click="getMyHeart">
              <img src="static/img/shalou.png" />
              <p>我的爱心</p>
          </div>
        </div>
      </div>

      <div class="l-c-article"> 
        <template v-if="content">
          <help-article v-for="(item,index) in content" :key="index" :content="item"></help-article>
        </template>
        <template v-else>
          <help-load :isLoader="true"></help-load>
        </template>   
        <help-more :tip="message" @click.native="getMore"></help-more>
      </div>

    </div>
  </div>
</template>

<script>
import Swiper from "../swiper.vue";
import ContentHelp from "../home/home-main/content-item.vue";
import LoadMore from "../../content/more.vue";
import HelpLoad from "../../Tip/loading.vue";
import axios from "axios";
export default {
  mounted: function() {
    this.getTypes();
  },
  data: function() {
    return {
      bannerList: [{ src: "static/img/b1.png" }, { src: "static/img/b2.png" }],
      typeId: 2,
      url: this.$store.state.url,
      message: "加载更多",
      content: [],
      currentPage: 1,
      isAllow: true
    };
  },
  methods: {
    clear() {
      this.message = "加载更多";
      this.currentPage == 1;
      this.isAllow = true;
      this.content = [];
    },
    getMyHeart() {
      var self = this;
      if (self.user) {
        self.clear();
        const address = `${self.url}/api/Article/${self.user
          .Id}/getUserHistory?type=爱心公益`;
        axios
          .get(address)
          .then(res => {
            if (res.data.length < 10) {
              self.isAllow = false;
              self.message = "加载完成";
            }
            self.content = res.data;
          })
          .catch(err => {
            if (err.response.data.Reason) {
              showDialog(err.response.data.Reason);
            }
          });
      } else {
        showDialog("请登录！");
      }
    },
    getArticle() {
      var self = this;
      self.clear();
      const address = `${self.url}/api/Article/${self.currentPage}/articles?type=${self.typeId}`;
      axios
        .get(address)
        .then(res => {
          if (res.data.length < 10) {
            self.isAllow = false;
            self.message = "加载完成";
          }
          self.content = res.data;
        })
        .catch(err => {
          if (err.response.data.Reason) {
            showDialog(err.response.data.Reason);
          }
        });
    },
    getMore() {
      var self = this;
      if (self.isAllow) {
        self.currentPage++;
        self.message = "正在加载...";
        const address = `${self.url}/api/Article/${self.currentPage}/articles?type=${self.typeId}`;
        axios
          .get(address)
          .then(res => {
            if (res.data.length < 10) {
              self.message = "加载完成";
              self.isAllow = false;
            } else {
              self.message = "加载更多";
            }
            res.data.forEach(item => {
              self.content.push(item);
            });
          })
          .catch(err => {
            if (err.response.data.Reason) {
              showDialog(err.response.data.Reason);
            }
          });
      }
    },
    getTypes() {
      //加载类型
      var self = this;
      const address = `${self.url}/api/Article/getTypes`;
      axios
        .get(address)
        .then(res => {
          res.data.forEach(item => {
            if (item.Name == "爱心公益") {
              self.typeId = item.ID;
              self.getArticle();
            }
          });
        })
        .catch(err => {
          if (err.response.status) {
            showDialog(err.response.data.Reason);
          }
        });
    }
  },
  computed: {
    user() {
      return this.$store.state.user;
    }
  },
  components: {
    "help-swiper": Swiper,
    "help-article": ContentHelp,
    "help-more": LoadMore,
    "help-load": HelpLoad
  }
};
</script>

<style>
.help-main-container {
  width: 100%;
}
.help-banner {
  width: 80%;
  margin: 0px auto;
}
.help-content {
  width: 90%;
  margin: 0px auto;
}

.l-c-head {
  width: 90%;
  height: 100px;
  margin: 20px auto;
  background-color: #fff;
  border-radius: 5px;
  border: 1px solid #ececec;
  box-shadow: 0 4px 8px 0 rgba(7, 17, 27, 0.05);
}
.l-c-main {
  width: 98%;
  height: 80px;
  margin: 10px auto 0;
}
.l-c-main > div {
  width: 50%;
  height: 80px;
  cursor: pointer;
  text-align: center;
}
.l-c-main img {
  width: 50px;
  height: 50px;
  margin-bottom: 5px;
}
.l-c-main p {
  color: gray;
}
.l-c-left {
  float: left;
  border-right: 1px solid #dcdcdc;
}
.l-c-right {
  float: right;
}

.l-c-article {
  width: 90%;
  margin: 50px auto;
}
@media only screen and (max-width: 768px) {
  .help-banner {
    width: 90%;
  }
  .help-content {
    width: 100%;
  }
  .l-c-head {
    width: 90%;
  }
  .l-c-article {
    width: 99%;
  }
}
</style>


