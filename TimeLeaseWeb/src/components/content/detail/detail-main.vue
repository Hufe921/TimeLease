<template>
  <div class="d-m-container">
      <!--详情 -->
      <div class="d-m-left">
        <p><article-data></article-data></p>
        <article-evaluate></article-evaluate>
      </div>
      <!--推荐区 -->
      <div class="d-m-right">
        <article-advice></article-advice>
      </div>
      <div class="clear"></div>
  </div>
</template>

<script>
import Article from "./article.vue";
import Advice from "../home/home-main/activity.vue";
import Evaluate from "./evaluate.vue";
export default {
  components: {
    "article-data": Article,
    "article-advice": Advice,
    "article-evaluate": Evaluate
  }
};
</script>

<style>
.d-m-container {
  width: 90%;
  margin: 20px auto;
}
.d-m-left {
  width: 68%;
  float: left;
  border-right: 1px solid #dcdcdc;
}
.d-m-left > p {
  width: 95%;
  overflow: hidden;
}
.d-m-left img:not(.no-more-img) {
  max-width: 100%;
  width: auto !important;
  height: auto !important;
}
.d-m-right {
  width: 30%;
  float: right;
}
@media only screen and (max-width: 1023px) {
  .d-m-left {
    width: 100%;
    float: none;
    overflow: hidden;
    border-right: 0px;
  }
  .d-m-left > p {
    width: 99%;
  }
  .d-m-right {
    display: none;
  }
}
</style>


