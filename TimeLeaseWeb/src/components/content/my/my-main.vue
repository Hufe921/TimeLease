<template>
  <div class="my-main-container">
    <!--餐单 -->
    <div class="my-main-menu">
      <!-- 餐单分隔图 -->
      <div class="my-remark" style="background-image: url(static/img/my-banner.png);">
          <div class="my-mask"></div>
          <div class="my-remark-left">
              <p>
                  个人主页
              </p>
          </div>
      </div>
      <!-- 餐单区 -->
      <div class="my-menu">
          <ul>
              <li class="my-current m-activity" data-menu="activity">待处理</li>
              <li data-menu="apply" class="m-apply">申请历史</li>
              <li data-menu="line" class="m-line">合作历史</li>
              <li data-menu="password" class="m-password">密码修改</li>
              <li data-menu="icon" class="m-icon">头像修改</li>
              <li data-menu="other" class="m-other">其他修改</li>
              <li data-menu="bp" class="m-bp">我的积分</li>
              <li data-menu="place" class="m-place">收货地址</li>
              <li data-menu="exchange" class="m-exchange">兑换历史</li>
          </ul>
          <div class="clear"></div>
      </div>
    </div>
    <!-- 基础替换区 -->
    <div class="my-main-content">
      <router-view></router-view>
    </div>

  </div>
</template>

<script>
import router from "../../../router/index.js";
export default {
  mounted: function() {
    var self = this;
    $(".my-menu li").click(function() {
      self.into(this);
    });
  },
  methods: {
    into(obj) {
      router.push({ name: $(obj).attr("data-menu") });
    }
  }
};
</script>

<style>
.my-main-container {
  width: 90%;
  margin: 20px auto;
  border: 1px solid transparent;
}
.my-tool {
  width: 100%;
  margin: 40px auto;
  border-radius: 5px;
  background-color: #fff;
  border: 1px solid transparent;
  box-shadow: 00 4px 20px rgba(0, 0, 0, 0.2);
}
.my-remark {
  width: 95%;
  height: 90px;
  position: relative;
  margin: 20px auto;
  border-radius: 6px;
}
.my-mask {
  z-index: 1;
  background-color: rgba(0, 0, 0, 0.5);
  border-radius: 6px;
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
}
.my-remark-left {
  width: 100%;
  height: 100%;
  float: left;
  z-index: 2;
  position: absolute;
}
.my-remark-left p {
  color: #fff;
  font-size: 30px;
  line-height: 90px;
  letter-spacing: 3px;
  text-align: center;
}
/* 以下是工具栏 */
.my-menu {
  width: 95%;
  margin: 15px auto;
}
.my-current {
  color: #fff !important;
  background: linear-gradient(to right, #ffae12 0, #f07d17 100%);
}
.my-menu li {
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
.my-main-content {
  width: 95%;
  margin: 40px auto;
}

/* 以上是替换区 */

@media only screen and (max-width: 768px) {
  .my-main-container {
    width: 100%;
  }
  .my-main-content {
    width: 100%;
  }
}
</style>


