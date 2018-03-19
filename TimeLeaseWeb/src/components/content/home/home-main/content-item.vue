<template>
    <div class="content-item-container" @click="intoDetail(content.ID)">
        <!-- 类型及时间 -->
        <div class="c-i-type-time">
            <div class="i-type">
              <template v-if="content.Type=='爱心公益'">
                <p><i class="fa fa-heart hong"></i>{{content.Type}}</p>
              </template>
              <template v-else>
                <p><i class="fa fa-cny huang"></i>{{content.Type}}</p>
              </template>    
            </div>
            <div class="i-time">
                <p>{{content.CreatedOn}}</p>
            </div>
        </div>
        <!-- 封面及信息 -->
        <div class="c-i-cover-message">
            <div class="i-cover">
                <img :src="url+content.Cover">
            </div>
            <div class="i-message">
                <!-- title -->
                <div class="i-m-title">
                    <p>{{content.Title}}</p>
                </div>
                <!-- 简要描述 -->
                <div class="i-m-remark">
                    <p>{{content.Remark}}</p> 
                </div>
                <!-- 遮盖层 -->
                <div class="p-wrap"></div>
                <!-- 基础信息数据 -->
                <div class="i-m-data">
                    <!-- 发布者信息 -->
                    <div class="i-m-d-left">
                        <ul>
                            <li><i class="fa fa-map-marker"></i>{{content.City}}，</li>
                            <li><i class="fa fa-user"></i>{{content.User}}，</li>
                            <li><i class="fa fa-eye"></i>{{content.Look}}</li>
                        </ul>
                    </div>
                    <!-- 顶数 -->
                    <div class="i-m-d-right">
                        <p><i class="fa fa-thumbs-o-up huang"></i>{{content.Praise}}</p>
                    </div>
                </div>
            </div>
            <div class="clear"></div>
        </div>
    </div>
</template>

<script>
import router from "../../../../router/index.js";
export default {
  props: {
    content: {}
  },
  data: function() {
    return {
      url: this.$store.state.url
    };
  },
  methods: {
    intoDetail(Id) {
      router.push({ name: "detail", query: { Id: Id } });
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
.content-item-container {
  width: 100%;
  height: 215px;
  cursor: pointer;
  border-radius: 3px;
  margin: 15px auto 0px;
  background-color: #fff;
  border: 1px solid #ececec;
  box-shadow: 0px 4px 8px 0px rgba(7, 17, 27, 0.1);
}
.content-item-container:hover {
  animation: bottomFrame 0.5s ease forwards;
}
@keyframes bottomFrame {
  0% {
    transform: translateY(0px);
  }
  100% {
    transform: translateY(-3px);
    box-shadow: 0 1px 3px rgba(0, 0, 0, 0.1);
  }
}

.content-item-container > div {
  width: 98%;
  margin: 0px auto;
}
.c-i-type-time {
  height: 40px;
}
.i-type {
  float: left;
  padding-top: 10px;
}
.i-type i {
  margin-right: 5px;
}
.i-time {
  float: right;
  padding-top: 10px;
}
.i-type p,
.i-time p {
  font-size: 13px;
}
/* 以上是title */
.c-i-cover-message {
  height: 160px;
  padding-top: 5px;
}
.i-cover {
  width: 210px;
  height: 150px;
  float: left;
}
.i-cover img {
  width: 100%;
  height: 100%;
}
.i-message {
  width: calc(100% - 220px);
  height: 150px;
  float: right;
}
.i-m-title {
  width: 100%;
  height: 30px;
}
.i-m-title p {
  color: #333;
  font-size: 18px;
  overflow: hidden;
  font-weight: 500;
  white-space: nowrap;
  text-overflow: ellipsis;
}
.i-m-remark {
  width: 100%;
  height: 80px;
  overflow: hidden;
  margin-top: 5px;
}
.i-m-remark p {
  color: #666;
  letter-spacing: 1px;
}
.p-wrap {
  width: 100%;
  height: 10px;
  float: left;
  position: relative;
  top: -10px;
  background: linear-gradient(rgba(255, 255, 255, 0.8), rgba(255, 255, 255, 1));
}
.i-m-data {
  width: 100%;
  height: 30px;
  margin-top: 5px;
}
.i-m-d-left {
  width: 70%;
  height: 30px;
  float: left;
}
.i-m-d-left li {
  float: left;
  font-size: 12px;
  margin-left: 3px;
}
.i-m-d-left li i {
  color: gray;
  margin-right: 3px;
}
.i-m-d-right p {
  font-size: 13px;
}
.i-m-d-right p i {
  color: gray;
  margin-right: 3px;
}
.i-m-d-right {
  float: right;
}
@media only screen and (max-width: 767px) {
  .content-item-container {
    height: 160px;
  }
  .i-cover {
    width: 140px;
    height: 100px;
  }
  .i-message {
    width: calc(100% - 143px);
    height: 100px;
  }
  .i-m-title p {
    font-size: 16px;
  }
  .i-m-remark p {
    font-size: 13px;
  }
  .i-m-title {
    height: 20px;
  }
  .i-m-remark {
    height: 50px;
  }
  .i-m-data {
    height: 20px;
  }
  .i-m-d-left {
    width: 70%;
    height: 20px;
  }
  .i-m-d-left li {
    font-size: 11px;
  }
  .i-m-d-right p {
    font-size: 12px;
  }
}
@media only screen and (max-width: 370px) {
  .i-m-d-right {
    display: none;
  }
  .i-m-d-left {
    width: 100%;
  }
}
</style>


