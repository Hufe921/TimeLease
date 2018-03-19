<template>
  <div class="find-content-container">
    <template v-if="content.length!=0">
      <content-item v-for="(item,index) in content" :key="index" :content="item"></content-item>
    </template>
    <template v-else>
      <content-null :isShow="isShow"></content-null>
    </template>
    <content-load :isLoader="isLoader"></content-load>   
    <content-more :tip="message" @click.native="getMore"></content-more>
  </div>
</template>

<script>
import ContentItem from "../home/home-main/content-item.vue";
import FindMore from "../../content/more.vue";
import FindNull from "../../Tip/null.vue";
import FindLoad from "../../Tip/loading.vue";
import axios from "axios";
export default {
  props: {
    sort: Number,
    tag: Number,
    city:String
  },
  mounted: function() {
    this.getArticle();
  },
  data: function() {
    return {
      url: this.$store.state.url,
      content: [],
      currentPage: 1,
      isLoader: true,
      isShow: false,
      message: "加载更多",
      isAllow: true
    };
  },
  watch: {
    sort: function() {
      this.clear();
      this.getArticle();
    },
    tag: function() {
      this.clear();
      this.getArticle();
    },
    city:function(){
      this.clear();
      this.getArticle();
    }
  },
  methods: {
    clear() {
      this.content = [];
      this.currentPage = 1;
      this.message = "加载更多";
      this.isLoader = true;
      this.isAllow = true;
    },
    getArticle() {
      var self = this;
      self.isShow = false;
      const address = `${self.url}/api/Article/${self.currentPage}/articles?city=${self.city}&tag=${self.tag}&sort=${self.sort}`;
      axios
        .get(address)
        .then(res => {
          self.isLoader = false;
          if (res.data.length < 10) {
            self.isAllow = false;
            self.message = "加载完成";
          }
          if (self.currentPage == 1 && res.data.length == 0) {
            self.isShow = true;
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
        const address = `${self.url}/api/Article/${self.currentPage}/articles?city=${self.city}&tag=${self.tag}&sort=${self.sort}`;
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
    }
  },
  components: {
    "content-item": ContentItem,
    "content-more": FindMore,
    "content-null": FindNull,
    "content-load": FindLoad
  }
};
</script>

<style>
.find-content-container {
  width: 90%;
  margin: 20px auto;
}
.content-item-container {
  background-color: #fff;
}
@media only screen and (max-width: 768px) {
  .find-content-container {
    width: 99%;
  }
}
</style>


