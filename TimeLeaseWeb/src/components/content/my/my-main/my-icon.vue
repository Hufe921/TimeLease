<template>
  <div class="my-icon-container">
    <form method="post" :action="url+'/api/User/putIcon'" id="putIconForm">
      <div class="icon-item">
          <div class="i-i-title">
              <img src="static/img/rhead.png" />
              <p>旧头像</p>
          </div>
          <!-- 主体内容 -->
          <div class="i-i-main">
              <img :src="url+user.Icon" />
          </div>
      </div>
      <!-- 上传头像-->
      <div class="icon-item">
      <div class="i-i-title">
          <img src="static/img/ruser.png" />
          <p>新头像</p>
      </div>
      <!-- 主体内容 -->
      <div class="i-i-main">
          <div class="input-block">
              <input type="text" name="Id" :value="user.Id" style="display:none"/>
              <input type="file" style="display:none" class="update-icon" @change="check" name="Cover"/>
              <input type="text" class="hufe-input" readonly placeholder="点击上传头像" :value="tip"/>
          </div>
          <div class="i-block cover" @click="upIcon">
              <i class="fa fa-photo"></i>
          </div> 
          <div class="clear"></div>
      </div>
      </div>
    
      <!-- 保存按钮-->
      <div class="icon-item submit-icon">
          <input type="submit" style="display:none" id="putIcon">
          <input type="button" value="保存" @click="updataIcon"> 
      </div>
    </form>  
  </div>
</template>

<script>
export default {
  mounted: function() {
    var options = {
      success: function(data) {
        $(".jconfirm").remove();
        showDialog("修改成功，下次登录生效！");
      },
      error: function(data) {
        $(".jconfirm").remove();
        showDialog(eval("(" + data.responseText + ")").Reason);
      }
    };
    $("#putIconForm").ajaxForm(options);
  },
  data: function() {
    return {
      url: this.$store.state.url,
      tip: ""
    };
  },
  methods: {
    updataIcon() {
      var self = this;
      if (self.tip == "") {
        showDialog("请上传新头像！");
      } else {
        loadingDialog();
        $("#putIcon").click();
      }
    },
    upIcon() {
      $(".update-icon").click();
    },
    check() {
      var self = this;
      var fixname = $(".update-icon")
        .val()
        .substring(
          $(".update-icon")
            .val()
            .lastIndexOf("."),
          $(".update-icon").val().length
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
        self.tip = $(".update-icon").val();
      } else {
        $(".update-icon").val("");
        showDialog("请输入正确格式！");
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
.cover {
  cursor: pointer;
  background-color: #e0995e;
}
.my-icon-container {
  width: 100%;
}
.i-i-main img {
  width: 150px;
  height: 150px;
  border-radius: 50%;
}
.icon-item {
  width: 100%;
  margin-top: 30px;
}
.i-i-title {
  width: 100%;
  height: 55px;
}
.i-i-title img {
  width: 45px;
  height: 45px;
  float: left;
}
.i-i-title p {
  float: left;
  margin-left: 3px;
  line-height: 45px;
}
.i-i-main {
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
.i-i-main li {
  height: 30px;
  padding: 0px 10px;
  display: inline-block;
  border-radius: 15px;
  margin: 5px 5px;
  cursor: pointer;
  background-color: #d2d2d2;
}
.i-i-main li p {
  line-height: 30px;
  font-size: 13px;
  color: #fff;
  float: left;
}
.i-i-main li i {
  font-size: 13px;
  color: #fff;
  float: right;
  line-height: 30px;
  margin-left: 10px;
}
.submit-icon {
  width: 95%;
  height: 80px;
  margin: 20px auto;
}
.submit-icon input {
  width: 150px;
  height: 40px;
  border: 0px;
  outline: none;
  border-radius: 3px;
  color: #fff;
  background-color: #5bc0de;
}
.i-i-title a {
  line-height: 45px;
  float: left;
  margin-left: 5px;
}
</style>


