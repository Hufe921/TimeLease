<template>
  <div class="lease-container">
    <lease-head></lease-head>
    <lease-main></lease-main>
  </div>
</template>

<script>
import Head from "./lease/lease-head.vue";
import Main from "./lease/lease-main.vue";
export default {
  components: {
    "lease-head": Head,
    "lease-main": Main
  }
};
</script>

<style>
.lease-container {
  width: 100%;
}
</style>


