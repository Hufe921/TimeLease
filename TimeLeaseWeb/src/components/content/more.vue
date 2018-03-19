<template>
  <div class="more-container">
      <p>{{tip}}</p>
  </div>
</template>

<script>
export default {
  props:{
    tip:String
  }
};
</script>

<style>
.more-container {
  width: 100%;
  cursor: pointer;
  margin: 15px auto;
  border-radius: 6px;
  background-color: #dcdcdc;
}
.more-container p {
  color: white;
  font-size: 17px;
  padding-top: 3px;
  padding-bottom: 3px;
  text-align: center;
}
</style>


