<template>
  <div class="my-bp-container">
  <!-- 积分及前往商城按钮 -->
    <div class="my-bp-button">
        <h2>{{bp}}积分</h2>
        <input type="button" value="前往积分商城" @click="into">
    </div>
    <div class="my-bp-img">
        <img src="static/img/hill.png" />
    </div>
  </div>
</template>

<script>
import router from "../../../../router/index.js";
import axios from "axios";
export default {
  mounted: function() {
    this.getBp();
  },
  data: function() {
    return {
      url: this.$store.state.url,
      bp: 0
    };
  },
  methods: {
    into() {
      router.push({ name: "shop" });
    },
    getBp() {
      var self = this;
      axios
        .get(self.url + "/api/User/bp", {
          params: {
            userId: self.user.Id
          }
        })
        .then(res => {
          self.bp = res.data;
        })
        .catch(err => {
          if (err.response.data.Reason) {
            showDialog(err.response.data.Reason);
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
.my-bp-container {
  width: 100%;
}
.my-bp-button,
.my-bp-img {
  width: 70%;
  height: 200px;
  overflow: hidden;
  margin: 0px auto;
  text-align: center;
}
.my-bp-button input {
  width: 150px;
  height: 48px;
  margin: 30px auto;
  outline: none;
  border: 0px;
  color: #fff;
  display: block;
  border-radius: 3px;
  background-color: #b4a078;
}
@media only screen and (max-width: 768px) {
  .my-bp-button,
  .my-bp-img {
    width: 100%;
  }
}
</style>


