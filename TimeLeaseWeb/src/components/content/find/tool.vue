<template>
	 <div class="toolbar-container">
        <div class="toolbar-main">
            <div class="find-title">
                <span>检索栏</span>
            </div>
            <!--搜索项-->
            <div class="search-tool">
                <div class="find-category type-container">
                    <span>时间</span>
                    <div class="tag">
                        <ul>
                            <li v-on:click="selectTime($event,0)" class="on">最新</li>
                            <li v-on:click="selectTime($event,1)">最旧</li>
                            <li><input type="text" class="hufe-input choose-city" readonly placeholder="点击选择城市" v-model="city" @click="openCity"/></li>
                        </ul>

                    </div>
                    <div class="clear"></div>
                </div>
                <div class="find-tag type-container">
                    <span>标签</span>
                    <div class="tag">
                        <ul>
                            <li @click="selectTag($event,0)" class="on">全部</li>
                            <li @click="selectTag($event,item.ID)" v-for="(item,index) in tags" :key="index">{{item.Name}}</li>
                        </ul>
                    </div>
                    <div class="clear"></div>
                </div>
            </div>
        </div>
        <tool-place @popCity="selectCity"></tool-place>
    </div>
</template>

<script>
import axios from "axios";
import City from "../home/place.vue";
export default {
  data: function() {
    return {
      tags: [],
      city: "",
      url: this.$store.state.url
    };
  },
  mounted: function() {
    this.getTags();
  },
  methods: {
    openCity() {
      //打开城市选择组件
      $(".place-container").animate({ right: "0%" });
    },
    closeCity() {
      //关闭
      $(".place-container").animate({ right: "-80%" });
    },
    selectTime: function(event, sort) {
      //选择类别
      var self = this;
      var obj = event.currentTarget;
      $(obj)
        .parent("ul")
        .find("li")
        .removeClass("on");
      $(obj).addClass("on");
      //向父组件传递数据
      this.$emit("popSort", sort);
    },
    selectTag: function(event, tag) {
      //选择标签
      var self = this;
      var obj = event.currentTarget;
      $(obj)
        .parent("ul")
        .find("li")
        .removeClass("on");
      $(obj).addClass("on");
      //向父组件传递数据
      this.$emit("popTag", tag);
    },
    selectCity(data) {
      //子组件传来的城市数据
      this.city = data;
      this.closeCity();
      this.$emit("popCity", data);
    },
    getTags() {
      //加载标签
      var self = this;
      const address = `${self.url}/api/Article/getTags`;
      axios
        .get(address)
        .then(res => {
          self.tags = res.data;
        })
        .catch(err => {
          if (err.response.status) {
            showDialog(err.response.data.Reason);
          }
        });
    }
  },
  components: {
    "tool-place": City
  }
};
</script>

<style>
.find-title {
  width: 95%;
  height: 40px;
  padding-top: 5px;
  margin: 0px auto;
}
.find-title span {
  float: left;
  display: block;
  line-height: 40px;
  margin-right: 10px;
  font-size: 28px;
  font-weight: 600;
}
.toolbar-container {
  width: 100%;
  margin: 20px auto;
  background-color: #fff;
}
.toolbar-main {
  width: 95%;
  margin: 0px auto;
}
.search-tool {
  width: 95%;
  margin: 0px auto;
}
.type-container {
  width: 95%;
  margin: 5px auto;
  border-bottom: 1px solid #edf1f2;
}
.type-container span {
  width: 8%;
  height: 40px;
  float: left;
  display: block;
  line-height: 40px;
  font-weight: 700;
  font-size: 14px;
  color: #07111b;
}
.type-container .tag {
  width: 90%;
  margin: 10px 0px;
  float: left;
}
.type-container li {
  float: left;
  cursor: pointer;
  margin-left: 2%;
  font-size: 15px;
  line-height: 40px;
  padding-left: 8px;
  padding-right: 8px;
}
.on {
  color: white;
  background-color: #000;
}
@media only screen and (max-width: 1024px) {
  .toolbar-main {
    width: 100%;
  }
  .toolbar-container {
    margin: 10px auto;
  }
  .type-container span {
    width: 10%;
    height: 40px;
    line-height: 20px;
    font-weight: 600;
    font-size: 13px;
  }
  .type-container .tag {
    width: 89%;
  }
  .type-container li {
    font-size: 14px;
    line-height: 35px;
    padding-left: 5px;
    padding-right: 5px;
  }
}
@media only screen and (max-width:370px) {
  .choose-city {
    width: 130px !important;
  }
}
</style>