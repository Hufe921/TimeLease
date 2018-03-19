<template>
  <div class="place-container">
    <!-- title及切换按钮 -->
    <div class="place-title">
      <div class="place-button" @click="closeCity">
        <i class="fa fa-sign-in"></i>
      </div>
      <p>选择城市</p>
    </div>

    <!-- 城市选择 -->
    <div class="select-container">
      <city-select @popCity="selectCity"></city-select>
    </div>
    
  </div>
</template>

<script>
import City from "./city.vue";
export default {
  methods:{
    closeCity(){
      $(".place-container").animate({right:"-80%"});
    },
    selectCity(data){
      this.$emit("popCity",data);
    }
  },
  components: {
    "city-select": City
  }
};
</script>


<style>
.place-container {
  width: 60%;
  height: 100%;
  right: -80%;
  top: 0px;
  z-index: 999;
  position: fixed;
  background-color: #fff;
  box-shadow: 2px 2px 10px #222;
}
.place-title {
  width: 100%;
  height: 80px;
  background-color: #57b382;
}
.place-button {
  width: 100px;
  height: 100%;
  float: left;
  cursor: pointer;
  text-align: center;
  background-color: #4a996f;
}
.place-button i {
  color: #fff;
  font-size: 30px;
  line-height: 80px;
}
.place-title p {
  float: left;
  color: #fff;
  font-size: 20px;
  margin-left: 5%;
  letter-spacing: 2px;
  line-height: 80px;
}
.select-container {
  width: 100%;
  height: calc(100% - 100px);
  overflow: auto;
}
@media only screen and (max-width: 767px) {
  .place-container {
    width: 80%;
  }
}
</style>



