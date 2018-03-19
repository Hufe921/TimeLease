<template>
  <div class="lease-publish-container">
    <form method="post" :action="url+'/api/Article/postArticle'" id="leaseForm">
    <input type="text" name="userId" style="display:none" :value="user.Id">
      <!-- 标题 -->
      <div class="lease-item">
          <div class="l-i-title">
              <img src="static/img/title.png" />
              <p>标题</p>
          </div>
          <!-- 主体内容 -->
          <div class="l-i-main">
            <input type="text" class="hufe-input" placeholder="请输入标题，不多余20字！" v-model="publish.title" name="title">
          </div>
      </div>
      <!-- 封面-->
      <div class="lease-item">
        <div class="l-i-title">
            <img src="static/img/cover.png" />
            <p>封面</p>
        </div>
        <!-- 主体内容 -->
        <div class="l-i-main">
            <div class="input-block">
                <input type="file" id="up-cover" style="display:none;" name="cover" @change="check"/>
                <input type="text" class="hufe-input" readonly placeholder="点击上传封面" :value="publish.cover"/>
            </div>
            <div class="i-block cover" @click="upCover">
                <i class="fa fa-photo"></i>
            </div> 
            <div class="clear"></div>
        </div>
      </div>
      <!-- 城市-->
      <div class="lease-item">
           <div class="l-i-title">
                <img src="static/img/city.png" />
                <p>关联城市</p>
            </div>
            <!-- 主体内容 -->
            <div class="l-i-main">
                <div class="input-block">
                    <input type="text" class="hufe-input" readonly placeholder="关联城市" :value="publish.city" name="city"/>
                </div>
                <div class="i-block l-city" @click="openCity">
                    <i class="fa fa-map-marker"></i>
                </div>
                <div class="clear"></div>
            </div>
      </div>
       <!--标签-->
      <div class="lease-item">
           <div class="l-i-title">
                <img src="static/img/tag.png" />
                <p>文章标签</p>
            </div>
            <!-- 主体内容 -->
            <div class="l-i-main l-i-tag">
              <input type="text" style="display:none;" name="tag" :value="publish.tag">
                <ul>
                    <li v-for="(item,index) in leaseTags" :key="item.ID" @click="chooseTag($event,item.ID)"><p>{{item.Name}}</p><i class="fa fa-plus"></i></li>
                </ul>
            </div>
      </div>
       <!--类型-->
      <div class="lease-item">
           <div class="l-i-title">
                <img src="static/img/type.png" />
                <p>文章类型</p>
            </div>
            <!-- 主体内容 -->
            <div class="l-i-main l-i-type">
              <input type="text" style="display:none;" name="type" :value="publish.type">
                <ul>
                    <li v-for="(item,index) in leaseTypes" :key="item.ID" @click="chooseType($event,item.ID)"><p>{{item.Name}}</p><i class="fa fa-plus"></i></li>
                </ul>
            </div>
      </div>
       <!--时间-->
      <div class="lease-item">
           <div class="l-i-title">
                <img src="static/img/time.png" />
                <p>日期说明</p>
            </div>
            <!-- 主体内容 -->
            <div class="l-i-main">
                <input type="text" class="hufe-input" placeholder="请输入此活动的有效日期说明！" name="time" v-model="publish.time">
            </div>
      </div>
      <!--简要描述-->
       <div class="lease-item">
           <div class="l-i-title">
                <img src="static/img/remark.png" />
                <p>简要描述</p>
            </div>
            <!-- 主体内容 -->
            <div class="l-i-main">
                <textarea  class="hufe-input" placeholder="简要描述将作为首页展示推荐给用户，重要请认真填写（字数在80-150字！）" name="remark" v-model="publish.remark"></textarea>
            </div>
      </div>
      <!--悬赏积分-->
       <div class="lease-item">
           <div class="l-i-title">
                <img src="static/img/bp.png" />
                <p>所需积分</p>
            </div>
            <!-- 主体内容 -->
            <div class="l-i-main">
                <input type="number" class="hufe-input"  name="bp" v-model="publish.bp" step="1">
            </div>
      </div>
       <!-- 富文本-->
      <div class="lease-item">
            <div class="l-i-title">
                <img src="static/img/muti.png" />
                <p>详细描述</p>
                <a :href="'http://120.76.145.211:8012/Home/Index?Id='+user.Id" target="blank">点击进入富文本编辑</a>
                <img src="static/img/refresh.png" class="refresh" @click="getDetailList"/>
            </div>
            <!-- 主体内容 -->
            <div class="l-i-main ue">
              <input type="text" style="display:none;" :value="publish.detail" name="detail">
              <template v-if="DetailList.length">
                <div class="i-article" v-for="item in DetailList" :key="item.ID">
                    <div class="i-a-data">
                        <img src="static/img/wait.png"/>
                        <p>{{item.CreatedOn}} 编写文章，点击右侧“预览“可查看！</p>
                    </div>
                    <a :href="'http://120.76.145.211:8012/Home/Detail?userId='+user.Id+'&articleId='+item.ID" target="blank">点击预览</a>
                    <img src="static/img/close.png" class="detail-img" @click="selectDetail($event,item.ID)"/>
                </div>
              </template>
              <template v-else>
                 <lease-null :isShow="true"></lease-null>
              </template>    
            </div>
      </div>
       <!-- 保存按钮-->
      <div class="lease-item submit-lease">
          <input type="submit"  id="submitBtn" style="display:none;">
          <input type="button" value="立刻发布" @click="checkSubmit"/> 
          <div class="clear"></div>
      </div>
    <lease-place @popCity="selectCity"></lease-place>
    </form>
  </div>
</template>

<script>
import axios from "axios";
import Place from "../home/place.vue";
import Null from "../../Tip/null.vue";
import router from "../../../router/index.js";
export default {
  mounted: function() {
    this.getTags();
    this.getTypes();
    this.getDetailList();
    var options = {
      success: function(data) {
        $(".jconfirm").remove();
        showDialog("发布成功！");
        router.push({ name: "history" });
      },
      error: function(data) {
        $(".jconfirm").remove();
        showDialog(eval("(" + data.responseText + ")").Reason);
      }
    };
    $("#leaseForm").ajaxForm(options);
  },
  data: function() {
    return {
      url: this.$store.state.url,
      leaseTags: [],
      leaseTypes: [],
      DetailList: [],
      bp: "",
      publish: {
        title: "",
        cover: "",
        city: "",
        tag: "",
        type: "",
        time: "",
        remark: "",
        bp: "",
        detail: ""
      }
    };
  },
  methods: {
    selectDetail(event, Id) {
      //选择文章详情
      var obj = event.currentTarget;
      $(".detail-img").each(function() {
        $(this).attr("src", "static/img/close.png");
      });
      $(obj).attr("src", "static/img/open.png");
      this.publish.detail = Id;
    },
    selectCity(data) {
      this.publish.city = data;
      this.closeCity();
    },
    openCity() {
      //打开城市选择组件
      $(".place-container").animate({ right: "0%" });
    },
    closeCity() {
      //关闭
      $(".place-container").animate({ right: "-80%" });
    },
    upCover() {
      $("#up-cover").click();
    },
    check() {
      var self = this;
      var fixname = $("#up-cover")
        .val()
        .substring(
          $("#up-cover")
            .val()
            .lastIndexOf("."),
          $("#up-cover").val().length
        );
      var isRight = false;
      var fix = [".png", ".jpg", ".jpeg", ".gif"];
      for (var i = 0; i < fix.length; i++) {
        if (fix[i].indexOf(fixname) > -1) {
          isRight = true;
          break;
        }
      }
      if (isRight) {
        self.publish.cover = $("#up-cover").val();
      } else {
        $("#up-cover").val("");
        showDialog("请输入正确格式！");
      }
    },
    getDetailList() {
      //获取用户文章细节
      var self = this;
      const address = `${self.url}/api/Article/getDetailList?userId=${self.user
        .Id}`;
      axios
        .get(address)
        .then(res => {
          self.DetailList = res.data;
        })
        .catch(err => {
          if (err.response.status) {
            showDialog(err.response.data.Reason);
          }
        });
    },
    getTags() {
      //加载标签
      var self = this;
      const address = `${self.url}/api/Article/getTags`;
      axios
        .get(address)
        .then(res => {
          self.leaseTags = res.data;
        })
        .catch(err => {
          if (err.response.status) {
            showDialog(err.response.data.Reason);
          }
        });
    },
    getTypes() {
      //加载类型
      var self = this;
      const address = `${self.url}/api/Article/getTypes`;
      axios
        .get(address)
        .then(res => {
          self.leaseTypes = res.data;
        })
        .catch(err => {
          if (err.response.status) {
            showDialog(err.response.data.Reason);
          }
        });
    },
    chooseTag(event, tag) {
      $(".l-i-tag li").removeClass("tag-curr");
      var obj = event.currentTarget;
      $(obj).addClass("tag-curr");
      this.publish.tag = tag;
    },
    chooseType(event, type) {
      $(".l-i-type li").removeClass("type-curr");
      var obj = event.currentTarget;
      $(obj).addClass("type-curr");
      this.publish.type = type;
    },
    checkSubmit() {
      var self = this.publish;
      var _self = this;
      if (self.title == "" || self.title.length > 20) {
        showDialog("请输入标题且不可超过20个字！");
      } else if (self.cover == "") {
        showDialog("请上传图片！");
      } else if (self.city == "") {
        showDialog("请选择城市！");
      } else if (self.tag == "") {
        showDialog("请选择标签！");
      } else if (self.type == "") {
        showDialog("请选择项目类型！");
      } else if (self.time == "") {
        showDialog("请输入日期！");
      } else if (
        self.remark == "" ||
        self.remark.length < 80 ||
        self.remark.length > 150
      ) {
        showDialog("请输入简要描述且次数在80到150字！");
      } else if (self.bp == "") {
        showDialog("请输入悬赏积分！");
      } else if (self.detail == "") {
        showDialog("请选择详细描述，如果没有请点击上方富文本进行编辑！");
      } else {
        loadingDialog();
        $("#submitBtn").click();
      }
    }
  },
  computed: {
    user() {
      return this.$store.state.user;
    }
  },
  components: {
    "lease-null": Null,
    "lease-place": Place
  }
};
</script>

<style>
.l-city {
  cursor: pointer;
  background-color: #76c2af;
}
.cover {
  cursor: pointer;
  background-color: #e0995e;
}
.article {
  background-color: #1a9d6c;
}
.refresh {
  width: 40px !important;
  height: 40px !important;
  float: right !important;
  margin-right: 2%;
  cursor: pointer;
}
.lease-publish-container {
  width: 100%;
  margin: 25px auto;
}
.lease-item {
  width: 100%;
  margin-top: 30px;
}
.l-i-title {
  width: 100%;
  height: 55px;
}
.l-i-title img {
  width: 45px;
  height: 45px;
  float: left;
}
.l-i-title p {
  float: left;
  margin-left: 3px;
  line-height: 45px;
}
.l-i-main {
  width: 95%;
  margin: 0px auto;
}
.i-block {
  width: 110px;
  height: 38px;
  float: left;
  text-align: center;
  border-top-right-radius: 3px;
  border-bottom-right-radius: 3px;
}
.i-block i {
  color: #fff;
  line-height: 38px;
  font-size: 18px;
}
.input-block {
  width: calc(100% - 110px);
  height: 38px;
  float: left;
}
.input-block input {
  border-top-right-radius: 0px;
  border-bottom-right-radius: 0px;
}
.tag-curr {
  background-color: #76c2af !important;
}
.type-curr {
  background-color: #d4cc89 !important;
}
.l-i-main li {
  height: 30px;
  padding: 0px 10px;
  display: inline-block;
  border-radius: 15px;
  margin: 5px 5px;
  cursor: pointer;
  background-color: #d2d2d2;
}
.l-i-main li p {
  line-height: 30px;
  font-size: 13px;
  color: #fff;
  float: left;
}
.l-i-main li i {
  font-size: 13px;
  color: #fff;
  float: right;
  line-height: 30px;
  margin-left: 10px;
}
.l-i-main textarea {
  width: 100%;
  height: 100px;
  padding: 20px;
}
.submit-lease {
  width: 95%;
  margin: 10px auto 20px;
}
.submit-lease input {
  width: 150px;
  height: 40px;
  border: 0px;
  outline: none;
  border-radius: 3px;
  color: #fff;
  float: right;
  background-color: #f01414;
}
.l-i-title a {
  line-height: 45px;
  float: left;
  margin-left: 5px;
}
.i-article {
  width: 100%;
  padding: 5px;
  float: left;
  font-size: 13px;
  border-radius: 3px;
  margin: 10px auto;
  border: 1px solid #dcdcdc;
}
.i-a-data img {
  width: 35px;
  height: 35px;
  float: left;
  border-radius: 50%;
}
.i-a-data p {
  float: left;
  line-height: 35px;
  margin-left: 2%;
}
.i-article a,
.i-article > img {
  float: right;
  margin-right: 1%;
  line-height: 40px;
}
.i-article > img {
  width: 30px;
  height: 30px;
  margin-top: 6px;
  cursor: pointer;
}
</style>


