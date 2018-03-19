<template>
  <div class="my-place-container">
     <!-- 旧货地址 -->
      <div class="my-place-item">
          <div class="p-i-title">
              <img src="static/img/rloc.png" />
              <p>旧收货地址</p>
          </div>
          <!-- 主体内容 -->
          <div class="p-i-main">
            <input type="text" class="hufe-input" placeholder="暂时没有找到" :value="place">
          </div>
      </div>
      <!-- 收货地址 -->
      <div class="my-place-item">
          <div class="p-i-title">
              <img src="static/img/pnew.png" />
              <p>新收货地址</p>
          </div>
          <!-- 主体内容 -->
          <div class="p-i-main">
            <input type="text" class="hufe-input" placeholder="请输入收货地址！" v-model="newPlace">
          </div>
      </div>

       <!-- 保存按钮-->
      <div class="my-place-item submit-my-place">
          <input type="button" value="保存" @click="submitPlace"> 
      </div>

  </div>
</template>

<script>
import axios from "axios";
export default {
  mounted: function() {
    this.getAddress();
  },
  data: function() {
    return {
      url: this.$store.state.url,
      place: "",
      newPlace: ""
    };
  },
  methods: {
    getAddress() {
      var self = this;
      const address = `${self.url}/api/User/${self.user.Id}/getAddress`;
      axios
        .get(address)
        .then(res => {
          self.place = res.data;
        })
        .catch(err => {
          if (err.response.data.Reason) {
            showDialog(err.response.data.Reason);
          }
        });
    },
    submitPlace() {
      var self = this;
      if (self.newPlace == "") {
        showDialog("请输入收货地址！");
      } else {
        const address = `${self.url}/api/User/${self.user.Id}/putAddress`;
        axios
          .put(address, {
            address: self.newPlace
          })
          .then(res => {
            showDialog("修改成功");
            self.getAddress();
          })
          .catch(err => {
            if (err.response.data.Reason) {
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
.l-city {
  background-color: #76c2af;
}
.cover {
  background-color: #e0995e;
}
.article {
  background-color: #1a9d6c;
}
.my-place-container {
  width: 100%;
  margin: 25px auto;
}
.my-place-item {
  width: 100%;
  margin-top: 30px;
}
.p-i-title {
  width: 100%;
  height: 55px;
}
.p-i-title img {
  width: 45px;
  height: 45px;
  float: left;
}
.p-i-title p {
  float: left;
  margin-left: 3px;
  line-height: 45px;
}
.p-i-main {
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
.p-i-main li {
  height: 30px;
  padding: 0px 10px;
  display: inline-block;
  border-radius: 15px;
  margin: 5px 5px;
  cursor: pointer;
  background-color: #d2d2d2;
}
.p-i-main li p {
  line-height: 30px;
  font-size: 13px;
  color: #fff;
  float: left;
}
.p-i-main li i {
  font-size: 13px;
  color: #fff;
  float: right;
  line-height: 30px;
  margin-left: 10px;
}
.submit-my-place {
  width: 95%;
  height: 80px;
  margin: 20px auto;
}
.submit-my-place input {
  width: 150px;
  height: 40px;
  border: 0px;
  outline: none;
  border-radius: 3px;
  color: #fff;
  background-color: #5bc0de;
}
.p-i-title a {
  line-height: 45px;
  float: left;
  margin-left: 5px;
}

@media only screen and (max-width: 768px) {
  .my-place-container {
    width: 95%;
  }
}
</style>


