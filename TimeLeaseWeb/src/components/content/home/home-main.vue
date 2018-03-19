<template>
  <div class="home-main-container">
    <menu-item></menu-item>
    <home-main-content></home-main-content>
  </div>
</template>

<script>
import MenuItem from "./home-main/menu-item.vue";
import Content from "./home-main/content.vue";
export default {
  components: {
    "menu-item": MenuItem,
    "home-main-content":Content
  }
};
</script>

<style>
.home-main-container {
  width: 85%;
  margin: 50px auto;
}
@media only screen and (min-width: 768px) and (max-width: 1140px) {
  .home-main-container {
    width: 97%;
  }
}
@media only screen and (max-width: 767px) {
  .home-main-container {
    width: 99%;
    margin: 0px auto;
  }
}
</style>


