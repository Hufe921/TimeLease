<template>
    <div class="detail-container">
        <detail-head @ApplyShowByHead="ApplyShow"></detail-head>
        <detail-main></detail-main>
        <detail-apply :isShow="isShow" @ApplyShowByMain="ApplyShow"></detail-apply>
    </div>
</template>

<script>
import Head from "./detail/detail-head.vue";
import Main from "./detail/detail-main.vue";
import DetailApply from "./detail/apply.vue";
export default {
  data: function() {
    return {
      isShow: false
    };
  },
  methods: {
    ApplyShow(data) {
      this.isShow = data;
    }
  },
  components: {
    "detail-head": Head,
    "detail-main": Main,
    "detail-apply": DetailApply
  }
};
</script>

<style>
.detail-container {
  width: 100%;
}
</style>


