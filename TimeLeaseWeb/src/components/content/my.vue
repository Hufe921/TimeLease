<template>
    <div class="my-container">
        <my-head></my-head>
        <my-main></my-main>
    </div>
</template>

<script>
import Head from "./my/my-head.vue";
import Main from "./my/my-main.vue";
export default {
  components: {
    "my-head": Head,
    "my-main": Main
  }
};
</script>

<style>
.my-container {
  width: 100%;
  margin: 0px auto;
  background-color: #f3f5f7;
  border: 1px solid transparent;
}
</style>


