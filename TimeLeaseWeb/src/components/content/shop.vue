<template>
    <div class="shop-container">
        <shop-head></shop-head>
    </div>
</template>


<script>
import Head from "./shop/shop-head.vue";
export default {
  components: {
    "shop-head": Head
  }
};
</script>

<style>
.shop-container {
  width: 100%;
}
</style>

