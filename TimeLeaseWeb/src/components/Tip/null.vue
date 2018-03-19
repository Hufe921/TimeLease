<template>
  <div class="no-more" v-if="isShow">
        <img src="static/img/no-more.png" class="no-more-img"/>
        <h3>没有检索到信息！</h3>
  </div>
</template>

<script>
export default {
    props:{
        isShow:Boolean
    }
};
</script>

<style>
.no-more {
  width: 100%;
  text-align: center;
  margin-bottom: 30px;
}
.no-more img {
  width: 210px !important;
  height: 210px !important;
  margin-top: 30px !important;
}
</style>


