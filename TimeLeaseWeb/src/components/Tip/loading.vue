<template>
  <div class="loader-container" v-if="isLoader">
      	<div class="loading">
            <span></span>
            <span></span>
            <span></span>
            <span></span>
            <span></span>
        </div>
  </div>
</template>

<script>
export default {
  props: {
    isLoader: Boolean
  }
};
</script>

<style>
.loader-container {
  width: 80px;
  margin: 80px auto;
}
.loading {
  width: 80px;
  height: 40px;
  margin: 0px auto;
}
.loading span {
  display: inline-block;
  width: 8px;
  height: 100%;
  border-radius: 4px;
  background: lightgreen;
  animation: load 1.04s ease infinite;
  -webkit-animation: load 1.04s ease infinite;
}
@keyframes load {
  0%,
  100% {
    height: 40px;
    background: lightgreen;
  }
  50% {
    height: 60px;
    margin-top: -20px;
    background: lightblue;
  }
}
.loading span:nth-child(2) {
  animation-delay: 0.13s;
  -webkit-animation-delay: 0.13s;
}
.loading span:nth-child(3) {
  animation-delay: 0.26s;
  -webkit-animation-delay: 0.26s;
}
.loading span:nth-child(4) {
  animation-delay: 0.39s;
  -webkit-animation-delay: 0.39s;
}
.loading span:nth-child(5) {
  animation-delay: 0.52s;
  -webkit-animation-delay: 0.52s;
}
</style>


