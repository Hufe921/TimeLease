<template>
    <div class="l-h-container" style="background-image: url(static/img/lease.png);">
        <!-- 导航 -->
        <div class="l-h-nav">
            <p>出租时间</p>
        </div>
        <!-- 标题 -->
        <div class="l-h-title">
            <p>出租时间与历史</p>
        </div>
        <!--时间出租与历史 -->
        <div class="l-h-data">
            <div class="l-h-d-main">
                <!-- 时间出租 -->
                <div class="l-h-d-left" @click="leaseInto('publish')">
                    <img src="static/img/edit.png" />
                    <p>出租时间</p>
                </div>
                <!-- 发布历史 -->
                <div class="l-h-d-right" @click="leaseInto('history')">
                    <img src="static/img/history.png" />
                    <p>发布历史</p>
                </div>

            </div>
        </div>
    </div>
</template>

<script>
import router from "../../../router/index.js";
export default {
  methods: {
    leaseInto(name) {
      router.push({ name: name });
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
.l-h-container {
  width: 100%;
  height: 350px;
  overflow: hidden;
  background-size: cover;
  background-repeat: no-repeat;
  background-position: center center;
}
.l-h-nav {
  width: 90%;
  height: 30px;
  margin: 50px auto;
}
.l-h-nav p {
  color: rgba(255, 255, 255, 0.8);
  line-height: 30px;
}
.l-h-title {
  width: 90%;
  margin: 50px auto;
}
.l-h-title p {
  font-size: 48px;
  line-height: 48px;
  color: #fff;
  letter-spacing: 2px;
  font-weight: 300;
  text-align: center;
  text-shadow: 2px 3px 0 rgba(0, 0, 0, 0.15);
}
.l-h-data {
  width: 90%;
  height: 100px;
  margin: 0px auto;
  background-color: #fff;
  border-radius: 12px;
  border: 1px solid transparent;
  box-shadow: 0px 12px 24px rgba(7, 17, 27, 0.1);
}
.l-h-d-main {
  width: 98%;
  height: 80px;
  margin: 10px auto 0;
}
.l-h-d-main > div {
  width: 50%;
  height: 80px;
  cursor: pointer;
  text-align: center;
}
.l-h-d-main img {
  width: 50px;
  height: 50px;
  margin-bottom: 5px;
}
.l-h-d-main p {
  color: gray;
}
.l-h-d-left {
  float: left;
  border-right: 1px solid #dcdcdc;
}
.l-h-d-right {
  float: right;
}
@media only screen and (max-width: 1023px) {
  .d-h-data {
    width: 99%;
  }
  .l-h-title p {
    font-size: 38px;
  }
}
</style>


