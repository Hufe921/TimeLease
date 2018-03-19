<template>
    <div class="all-container">
      <tl-head></tl-head>
      <div class="content">
        <router-view></router-view>
      </div> 
      <tl-foot></tl-foot>
      <tl-login></tl-login>
		</div>
</template>

<script>
import Head from "./components/head/head";
import Foot from "./components/foot/foot";
import Login from "./components/login/login";
export default {
  components: {
    "tl-head": Head,
    "tl-foot": Foot,
    "tl-login": Login
  }
};
</script>
