<template>
  <div class="home-container">
      <home-banner></home-banner>
      <home-main></home-main> 
  </div>
</template>

<script>
import Banner from "./home/banner.vue";
import HomeMain from "./home/home-main.vue";
export default {
  components: {
    "home-banner": Banner,
    "home-main": HomeMain
  }
};
</script>

<style>
.home-container {
  width: 100%;
}
.home-swiper {
  width: 77%;
  margin: 20px auto;
}
.home-swiper .swiper-container {
  border-radius: 5px;
}
@media only screen and (max-width: 767px) {
  .home-swiper {
    width: 98%;
  }
}
</style>


