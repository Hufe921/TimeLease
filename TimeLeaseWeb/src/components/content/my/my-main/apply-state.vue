<template>
  <div class="apply-state-container">
      <!-- 存放活动标题和申请时间 -->
      <div class="apply-title-time">
        <div class="a-t-type">
          <template v-if="content.Type=='爱心公益'">
            <p><i class="fa fa-heart hong"></i>{{content.ArticleTitle}}</p>
          </template>
          <template v-else>
            <p><i class="fa fa-cny huang"></i>{{content.ArticleTitle}}</p>
          </template>
        </div>
        <div class="a-t-time">
            <p>{{content.CreatedOn}}</p>
        </div>
      </div>
      <!-- 存放活动申请者信息 -->
      <div class="apply-user">
        <!-- 用户名 -->
        <div class="a-u-user">
            <p>{{content.User}}：</p>
        </div>
        <!-- 简要描述 -->
        <div class="a-u-remark">
            <p>{{content.Remark}}联系方式：{{content.Phone}}</p> 
        </div>
      </div>
      <!-- 存放状态或审核按钮 -->
      <div class="apply-menu">
        <template v-if="isActivity">
          <ul>
            <li @click="changeState(content.ID,1)"><img src="static/img/agree.png" /></li>
            <li @click="changeState(content.ID,-1)"><img src="static/img/rejuce.png" /></li>
          </ul>
        </template>
        <template v-else-if="isApply">
          <template v-if="content.State==0">
            <p class="apply-state">状态：未审核</p>
          </template>
          <template v-else-if="content.State==-1">
            <p class="apply-state">状态：已拒绝</p>
          </template>
          <template v-else-if="content.State==2">
            <p class="apply-state">状态：已结束</p>
          </template>
          <template v-else>
            <li @click="changeFinish(content.ID)" class="apply-line"><img src="static/img/success.png" /><p>确认验收</p></li>
          </template>
        </template>
        <template v-else-if="isLine">
          <template v-if="content.State==1">
            <p class="apply-state">状态：等待验收</p>
          </template>
          <template v-else>
            <p class="apply-state">状态：已结束</p>
          </template>
        </template>
          <div class="clear"></div>
      </div>
  </div>
</template>

<script>
import axios from "axios";
export default {
  props: {
    content: {},
    isActivity: Boolean,
    isApply: Boolean,
    isLine: Boolean
  },
  data: function() {
    return {
      url: this.$store.state.url
    };
  },
  methods: {
    changeState(id, state) {
      var self = this;
      const address = `${self.url}/api/Apply/${id}/${state}/putApplyState`;
      $.confirm({
        title: "通知!",
        content: "你确定修改此状态？",
        buttons: {
          取消: function() {},
          确定: {
            btnClass: "btn-blue",
            action: function() {
              loadingDialog();
              axios
                .put(address)
                .then(res => {
                  $(".jconfirm").remove();
                  showDialog("修改成功！");
                  self.$emit("refresh");
                })
                .catch(err => {
                  if (err.response.data.Reason) {
                    $(".jconfirm").remove();
                    showDialog(err.response.data.Reason);
                  }
                });
            }
          }
        }
      });
    },
    changeFinish(id) {
      var self = this;
      const address = `${self.url}/api/Apply/${id}/putArticleState`;
      $.confirm({
        title: "通知!",
        content: "确定对方已经完成，并将所需积分将转给对方？",
        buttons: {
          取消: function() {},
          确定: {
            btnClass: "btn-blue",
            action: function() {
              loadingDialog();
              axios
                .put(address)
                .then(res => {
                  $(".jconfirm").remove();
                  showDialog("修改成功！");
                  self.$emit("refresh");
                })
                .catch(err => {
                  if (err.response.status) {
                    $(".jconfirm").remove();
                    showDialog(err.response.data.Reason);
                  }
                });
            }
          }
        }
      });
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
.hong {
  color: #d65656 !important;
}
.huang {
  color: #d99b08 !important;
}
.lan {
  color: #00aeee !important;
}
.apply-state {
  text-align: right;
}
.apply-line p,
.apply-line img {
  float: left;
  line-height: 40px;
}
.apply-state-container {
  width: 100%;
  cursor: pointer;
  border-radius: 3px;
  margin: 15px auto 0px;
  background-color: #fff;
  border: 1px solid #ececec;
  box-shadow: 0px 4px 8px 0px rgba(7, 17, 27, 0.1);
}
.apply-state-container > div {
  width: 98%;
  margin: 0px auto;
}
.apply-title-time {
  height: 40px;
}
.a-t-type {
  float: left;
  padding-top: 10px;
}
.a-t-type i {
  margin-right: 5px;
}
.a-t-time {
  float: right;
  padding-top: 10px;
}
.a-t-type p,
.a-t-time p {
  font-size: 13px;
}
.a-u-user {
  width: 100%;
  height: 30px;
}
.a-u-user p {
  color: #333;
  font-size: 18px;
  font-weight: 500;
}
.a-u-remark {
  width: 100%;
  margin-top: 5px;
}
.a-u-remark p {
  color: #666;
  letter-spacing: 1px;
}
.apply-menu li {
  float: right;
  margin-right: 1%;
}
.apply-menu li img {
  width: 40px;
  height: 40px;
  margin-bottom: 3px;
}
</style>


