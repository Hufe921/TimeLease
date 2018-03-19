<template>
  <div class="apply-detail-container" v-if="isShow">
       <!-- 遮罩 -->
      <div class="apply-detail-wrapper" @click="close"></div>
      <!-- 申请框 -->
      <div class="apply-detail-main">
         <div class="input-container">
            <h4>申请</h4>
        </div>
        <div class="input-container">
            <input type="text" class="input-control" placeholder="联系方式" v-model="apply.Phone">
        </div>
        <div class="input-container">
            <textarea class="input-control apply-remark" placeholder="请输入申请理由" v-model="apply.Remark"></textarea>
        </div>
        <div class="input-container">
            <input type="button" value="提交" class="apply-detail-input btn-info" @click="submitApply" :disabled="isStop">
        </div>
      </div>
  </div>
</template>

<script>
import axios from "axios";
import router from "../../../router/index.js";
export default {
  props: {
    isShow: Boolean
  },
  data: function() {
    return {
      url: this.$store.state.url,
      isStop: false,
      apply: {
        Phone: "",
        Remark: "",
        UserId: "",
        ArticleId: ""
      }
    };
  },
  methods: {
    close() {
      this.$emit("ApplyShowByMain", false);
    },
    submitApply() {
      var self = this;
      self.apply.UserId = self.user.Id;
      self.apply.ArticleId = self.$route.query.Id;
      self.isStop = true;
      if (self.apply.Phone == "" || self.apply.Remark == "") {
        showDialog("请填写所有信息！");
        self.isStop = false;
      } else if (self.apply.UserId == "") {
        showDialog("请登录！");
        router.push({ name: "home" });
      } else if (self.ArticleId == "") {
        showDialog("请选择文章后申请！");
        router.push({ name: "home" });
      } else {
        const address = `${self.url}/api/Apply/submitApply`;
        axios
          .post(address, self.apply)
          .then(res => {
            self.isStop = false;
            showDialog("申请成功！");
            setTimeout(function() {
              router.push({ name: "apply" });
            }, 1000);
          })
          .catch(err => {
            if (err.response.data.Reason) {
              self.isStop = false;
              showDialog(err.response.data.Reason);
            }
          });
      }
    }
  },
  computed: {
    user() {
      return this.$store.state.user;
    }
  }
};
</script>

<style>
.apply-detail-container {
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  z-index: 99999;
  position: fixed;
}
.apply-detail-wrapper {
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  z-index: 99999;
  position: fixed;
  opacity: 0.3;
  background-color: #444;
}
.apply-detail-main {
  width: 28%;
  height: 330px;
  left: 35%;
  top: 200px;
  position: fixed;
  z-index: 999999;
  border-radius: 5px;
  background-color: #fff;
  box-shadow: 0 2px 6px rgba(0, 0, 0, 0.2);
}
.apply-remark {
  height: 120px !important;
}
.input-container {
  width: 90%;
  margin: 18px auto;
  text-align: center;
}
.input-control {
  width: 100%;
  min-height: 40px;
  line-height: 40px;
  margin: 0px auto;
  outline: none;
  padding: 0px 16px 0px 14px;
  border: 1px solid #dcdcdc;
}
.apply-detail-input {
  width: 100%;
  height: 40px;
  border: 0px;
  outline: none;
  border-radius: 5px;
  margin: 5px auto;
}
@media only screen and (min-width: 768px) and (max-width: 1023px) {
  .apply-detail-main {
    width: 50%;
    left: 25%;
    top: 20%;
  }
}
@media only screen and (max-width: 767px) {
  .apply-detail-main {
    width: 86%;
    left: 7%;
    top: 20%;
  }
}
@media only screen and (max-width: 370px) {
  .apply-detail-main {
    width: 94%;
    height: 340x;
    left: 3%;
    top: 20%;
  }
}
</style>


