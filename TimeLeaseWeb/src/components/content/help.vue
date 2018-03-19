<template>
  <div class="help-container">
    <help-head></help-head>
    <help-main></help-main>
  </div>
</template>

<script>
import Head from "./help/help-head.vue";
import Main from "./help/help-main.vue";
export default {
  components: {
    "help-head": Head,
    "help-main": Main
  }
};
</script>

<style>
.help-container {
  width: 100%;
}
</style>


