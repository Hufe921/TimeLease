<template>
  <!-- 内容区菜单 -->
  <div class="menu-item-container">
      <div class="menu-item-main">
            <!-- 菜单 -->
            <div class="menu-item-left">
                <ul>
                    <li data-menu="publish">
                        <i class="fa fa-edit"></i>
                        <p>发布任务</p>
                    </li>
                    <li data-menu="help">
                        <i class="fa fa-heart"></i>
                        <p>公益行动</p>
                    </li>
                    <li data-menu="shop">
                        <i class="fa fa-shopping-cart"></i>
                        <p>积分商城</p>
                    </li>
                    <li data-menu="bp">
                        <i class="fa fa-cny"></i>
                        <p>我的钱包</p>
                    </li>
                </ul>
            </div>
            <!-- 我 -->
            <div class="menu-item-right">
              <img src="static/img/set.jpg" @click="into($event.currentTarget)" data-menu="my"/>
              <i class="fa fa-user" @click="into($event.currentTarget)" data-menu="my"></i>
            </div> 
      </div>
  </div>
</template>

<script>
import router from "../../../../router/index.js";
export default {
  mounted: function() {
    var self = this;
    $(".menu-item-left li").click(function() {
      self.into(this);
    });
  },
  methods: {
    into(obj) {
      var name = $(obj).attr("data-menu");
      router.push({ name: name });
    }
  }
};
</script>

<style>
.lv {
  color: #729c9b !important;
}
.hong {
  color: #d65656 !important;
}
.huang {
  color: #d99b08 !important;
}
.lan {
  color: #5fc5ea !important;
}
.menu-item-container {
  width: 90%;
  height: 110px;
  margin: 20px auto;
  background-color: #fff;
  border: 1px solid #e7e7e7;
  box-shadow: 0 1px 3px rgba(0, 0, 0, 0.1);
}
.menu-item-main {
  width: 100%;
  height: 90px;
  margin: 10px auto 0px;
}
.menu-item-left {
  width: 80%;
  float: left;
}
.menu-item-left li {
  width: 20%;
  float: left;
  cursor: pointer;
  text-align: center;
  border-right: 1px solid #dcdcdc;
}
.menu-item-left li i {
  font-size: 32px;
  color: #adadad;
  line-height: 60px;
}
.menu-item-left li p {
  color: #555;
}
.menu-item-right {
  width: 10%;
  float: right;
  cursor: pointer;
  text-align: center;
}
.menu-item-right img {
  width: 100%;
  height: 85px;
  max-width: 117px;
}
.menu-item-right i {
  display: none;
  cursor: pointer;
  font-size: 32px;
  color: #adadad;
  line-height: 90px;
}
@media only screen and (max-width: 1140px) {
  .menu-item-right i {
    display: inline-block;
  }
  .menu-item-right img {
    display: none;
  }
}
@media only screen and (min-width: 768px) and (max-width: 1140px) {
  .menu-item-container {
    width: 97%;
  }
}
@media only screen and (max-width: 767px) {
  .menu-item-container {
    width: 99%;
  }
  .menu-item-left {
    width: 85%;
  }
  .menu-item-left li {
    width: 24%;
  }
  .menu-item-left li i {
    font-size: 28px;
  }
  .menu-item-right {
    width: 15%;
  }
}
</style>


