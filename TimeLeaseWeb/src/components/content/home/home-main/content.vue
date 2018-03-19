<template>
  <div class="main-content-container">
      <!-- 内容聚合区 -->
      <div class="content-left">
          <!-- 检索区 -->
          <div class="content-search">
              <ul>
                  <li class="search-cur">聚合信息</li>
                  <li @click="intoType('find')">时间有关</li>
                  <li @click="intoType('help')">公益行动</li>
              </ul>
          </div>
          <!-- 内容展示区 -->
          <div class="content-show">
            <template v-if="content.length!=0">
              <content-item v-for="(item,index) in content" :key="index" :content="item"></content-item>
            </template>
            <template v-else>
              <content-null :isShow="isShow"></content-null>
            </template>
            <content-load :isLoader="isLoader"></content-load>  
            <load-more :tip="message" @click.native="getMore"></load-more>
          </div>
      </div>
      <!-- 系统通知及实时任务 -->
      <div class="content-right">
        <content-activity></content-activity>
      </div>
      <div class="clear"></div>
  </div>
</template>

<script>
import router from "../../../../router/index.js";
import ContentItem from "./content-item.vue";
import Activity from "./activity.vue";
import More from "../../more.vue";
import HomeLoader from "../../../Tip/loading.vue";
import HomeNull from "../../../Tip/null.vue";
import axios from "axios";
export default {
  mounted: function() {
    this.getArticle();
  },
  data: function() {
    return {
      url: this.$store.state.url,
      currentPage: 1,
      content: [],
      message: "加载更多",
      isAllow: true,
      isLoader: true,
      isShow: false
    };
  },
  methods: {
    getArticle() {
      var self = this;
      self.isShow = false;
      const address = `${self.url}/api/Article/${self.currentPage}/articles`;
      axios
        .get(address)
        .then(res => {
          self.isLoader = false;
          self.content = res.data;
          if (res.data.length < 10) {
            self.isAllow = false;
            self.message = "加载完成";
          }
          if (self.currentPage == 1 && res.data.length == 0) {
            self.isShow = true;
          }
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
        const address = `${self.url}/api/Article/${self.currentPage}/articles`;
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
    intoType(name) {
      router.push({ name: name });
    }
  },
  components: {
    "content-item": ContentItem,
    "content-activity": Activity,
    "load-more": More,
    "content-load": HomeLoader,
    "content-null": HomeNull
  }
};
</script>

<style>
.main-content-container {
  width: 90%;
  margin: 10px auto;
}
.content-left {
  width: 65%;
  float: left;
  padding-right: 10px;
  border-right: 1px solid #dcdcdc;
}
.content-search {
  width: 100%;
  height: 40px;
  border-bottom: 1px solid #dcdcdc;
}
.content-search li {
  width: 20%;
  height: 100%;
  float: left;
  cursor: pointer;
  font-size: 15px;
  margin-left: 5px;
  line-height: 38px;
  text-align: center;
}
.search-cur {
  color: #1e9e69 !important;
  border-bottom: 2px solid #57b382;
}
.content-show {
  width: 100%;
}
.content-right {
  width: 33%;
  float: right;
}
/* 以下是小屏幕适配 */
@media only screen and (min-width: 768px) and (max-width: 1140px) {
  .main-content-container {
    width: 97%;
  }
}
@media only screen and (max-width: 767px) {
  .main-content-container {
    width: 99%;
  }
  .content-right {
    display: none;
  }
  .content-left {
    width: 100%;
    float: none;
    padding-right: 0px;
    border-right: 0px solid #dcdcdc;
  }
}
</style>


