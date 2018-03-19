<template>	
	<div class="swiper-container">
        <div class="swiper-wrapper">
            <div class="swiper-slide" v-for="(item,index) in imgList" :key="index"><img :src="item.src"/></div>
        </div>
        <!--分页器 -->
        <div class="swiper-pagination  swiper-pagination-white"></div>
    </div>
</template>

<script>
export default {
  props: {
    imgList: Array
  },
  mounted: function() {
    new Swiper(".swiper-container", {
      direction: "horizontal",
      autoplay: 5000,
      speed: 800,
      loop: true,
      autoplayDisableOnInteraction: false,
      pagination: ".swiper-pagination",
      paginationClickable: true
    });
  }
};
</script>


<style>
.swiper-container {
  width: 100%;
  margin: 0px auto;
}
.swiper-wrapper {
  width: 100%;
}
.swiper-wrapper img {
  width: 100%;
  height: 100%;
  border-radius: 5px;
  margin: 10px auto;
}
@media only screen and (max-width: 1120px) {
  .swiper-container {
    margin-top: 0px;
  }
}
@media only screen and (max-width: 1024px) {
  .swiper-wrapper img {
    margin: 0px auto;
  }
  .swiper-container {
    width: 100%;
    margin: 0px auto;
  }
}
</style>