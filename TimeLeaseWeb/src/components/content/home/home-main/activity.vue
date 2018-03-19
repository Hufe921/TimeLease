<template>
    <div class="activity-container">
        <!-- 最新活动 -->
        <div class="last-activity">
            <!-- title -->
            <div class="title-container">
                <p>最新信息</p>
            </div>
            <!-- 主体内容 -->
            <div class="last-main">
                <ul>
                    <li v-for="(item,index) in newArticle" :key="index">
                      <p>
                        <template v-if="item.Type=='爱心公益'">
                          <i class="fa fa-heart hong fa-fw"></i>
                        </template>
                        <template v-else>
                          <i class="fa fa-cny huang fa-fw"></i>
                        </template>
                        {{item.User}}发布“{{item.Title}}” {{item.CreatedOn}}
                        </p>
                      </li>
                </ul>
            </div>
        </div>
        <!-- 推荐活动 -->
        <div class="advice-activity">
            <!-- title -->
            <div class="title-container">
                <p>推荐活动</p>
            </div>
            <!-- 推荐活动内容 -->
            <div class="advice-main">
                <ul>
                    <li v-for="(item,index) in hotArticle" :key="index">
                        <div class="advice-img">
                            <img :src="url+item.Cover">
                        </div>
                        <div class="a-remark">
                            <p>{{item.Title}}，来自{{item.User}}发表于{{item.CreatedOn}},被看过{{item.Look}}次，被赞赏过{{item.Praise}}次。</p>
                        </div>
                    </li>
                </ul>

            </div>
        </div>
        <!-- 网站活动 -->
        <div class="time-lease-activity">
             <!-- title -->
            <div class="title-container">
                <p>网站活动</p>
            </div>
            <div class="time-main">
                <p>租赁本身就是所有权和使用权的一种信贷关系，随着计算机技术的不断发展，计算机以及自动处理技术已经彻底融入我们的生活并且在各个领域发挥着重要的作用。今天我们使用计算机实现一种新鲜的租赁和管理方式，面向学生以及社会人士，在丰富人们忙碌之余的生活之外，系统管理也具有手动管理无法比拟的优点。实际的物品可以出租，自己的闲暇时间自然也是可以出租并赚取一定报酬。一个平台提供真实有效的活动信息，有时间租赁、物品捐赠、组织陪伴留守儿童、空巢老人等志愿者活动。社会人士可报名参加。</p>
            </div>
        </div>
    </div>
</template>

<script>
import axios from "axios";
export default {
  mounted: function() {
    this.getLastArticle();
    this.getHotArticle();
  },
  data: function() {
    return {
      url: this.$store.state.url,
      newArticle: [],
      hotArticle: []
    };
  },
  methods: {
    getLastArticle() {
      var self = this;
      const address = `${self.url}/api/Article/getNewArticle`;
      axios.get(address).then(res => {
        self.newArticle = res.data;
      });
    },
    getHotArticle() {
      var self = this;
      const address = `${self.url}/api/Article/getHotArticle`;
      axios.get(address).then(res => {
        self.hotArticle = res.data;
      });
    }
  }
};
</script>

<style>
.activity-container {
  width: 100%;
}
.title-container {
  width: 100%;
  height: 50px;
  position: relative;
  background-color: #fafafa;
}
.title-container p {
  line-height: 50px;
  text-indent: 15px;
  color: #888;
  border: #e7e7e7 solid 1px;
}
.title-container:after,
.title-container:before {
  position: absolute;
  top: 10px;
  left: -8px;
  right: 100%;
  width: 0;
  height: 0;
  display: block;
  content: " ";
  border-color: transparent;
  border-right-color: #dcdcdc;
  border-style: solid solid outset;
  border-width: 8px 8px 8px 0;
}
.title-container:after {
  border-right-color: #fafafa;
  margin-left: 1px;
  z-index: 2;
}
.activity-container > div {
  width: 100%;
  margin-top: 10px;
  min-height: 300px;
}
/* 以上是活动区相同部分 */
.last-main {
  width: 100%;
}
.last-main li {
  width: 90%;
  height: 30px;
  margin: 5px auto;
  border-bottom: 1px dotted #dcdcdc;
}
.last-main li p {
  width: 100%;
  height: 100%;
  font-size: 13px;
  overflow: hidden;
  white-space: nowrap;
  text-overflow: ellipsis;
}
.last-main li i {
  font-size: 13px;
  margin-right: 5px;
}
/* 以上是最新信息 */
.advice-main {
  width: 100%;
}
.advice-main li {
  width: 90%;
  height: 80px;
  margin: 5px auto;
  border-bottom: 1px dotted #dcdcdc;
}
.advice-img {
  width: 105px;
  height: 75px;
  float: left;
}
.advice-img img {
  width: 100%;
  height: 100%;
}
.a-remark {
  width: calc(100% - 105px);
  height: 100%;
  float: right;
  overflow: hidden;
}
.a-remark p {
  width: 95%;
  margin-left: 2%;
  font-size: 13px;
  text-indent: 1em;
}
/* 以上是推荐活动 */
.time-main {
  width: 100%;
}
.time-main p {
  font-size: 13px;
  color: gray;
  margin-top: 10px;
  text-indent: 1em;
  letter-spacing: 1px;
}
</style>


