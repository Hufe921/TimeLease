<template>
  <div class="help-head-container">
    <div class="head-ball head-ball-b1">
        <i></i>
    </div>
    <div class="head-ball head-ball-b2">
        <i></i>
    </div>
    <div class="head-ball head-ball-b3">
        <i></i>
    </div>
    <div class="head-ball head-ball-b4">
        <i></i>
    </div>
    <div class="head-ball head-ball-b5">
       <i></i>
    </div>
    <div class="head-ball head-ball-b6">
       <i></i>
    </div>
    <div class="head-ball head-ball-b7">
       <i></i>
    </div>


    <div class="help-title">
        <img src="static/img/help1.png" />
        <p class="slogn1">爱 无处不在</p>
        <p class="slogn2">让我们将爱与公益继续扩散...</p>
    </div>
  </div>
</template>

<script>
export default {
  mounted:function(){
    $(".help-head-container").animate({
      height:"650px",
    },1000);
  }
};
</script>

<style>
.help-head-container {
  width: 100%;
  height: 0px;
  position: relative;
  overflow: hidden;
}
.head-ball {
  position: absolute;
  transition: 2s cubic-bezier(0.47, 0.01, 0.29, 1.01);
}
.head-ball i {
  display: block;
  width: 100%;
  height: 100%;
  border-radius: 100%;
}
.head-ball-b1 {
  width: 41px;
  height: 41px;
  top: 65%;
  left: 10%;
}
.head-ball-b1 i {
  background-image: radial-gradient(30% 30%, #fff212, #ffcc7c 100%);
  box-shadow: inset -5px -5px 8px 0 rgba(243, 255, 116, 0.49),
    inset -5px -5px 20px 0 rgba(118, 38, 38, 0.14);
  animation: ballFrame 2s ease-in-out infinite alternate;
  animation-duration: 1.8s;
}
.head-ball-b2 {
  width: 76px;
  height: 76px;
  top: 20%;
  left: 20%;
}
.head-ball-b2 i {
  background-color: #ff3b30;
  box-shadow: inset -5px -5px 8px 0 rgba(243, 255, 116, 0.49),
    inset -5px -5px 20px 0 rgba(118, 38, 38, 0.14);
  box-shadow: 0 0 black;
  animation: ballFrame 2s ease-in-out infinite alternate;
}
.head-ball-b3 {
  width: 93px;
  height: 93px;
  top: 25%;
  right: -10px;
}
.head-ball-b3 i {
  background-image: radial-gradient(30% 30%, #30c9ff, #7095ff 100%);
  box-shadow: inset -3px -5px 10px 0 rgba(255, 255, 255, 0.12),
    inset -2px -2px 5px 0 rgba(255, 255, 255, 0.31),
    inset -5px -5px 10px 0 rgba(182, 223, 255, 0.59),
    inset -10px -10px 18px 0 rgba(27, 41, 111, 0.14);
  animation: ballFrame 2s ease-in-out infinite alternate;
  animation-duration: 2.2s;
}
.head-ball-b4 {
  width: 176px;
  height: 176px;
  top: 25%;
  left: -3%;
}
.head-ball-b4 i {
  background-image: radial-gradient(30% 30%, #20d4ff, #5db2ff 100%);
  box-shadow: inset -3px -5px 10px 0 rgba(255, 255, 255, 0.12),
    inset -2px -2px 5px 0 rgba(255, 255, 255, 0.31),
    inset -5px -5px 10px 0 #75bff8,
    inset -10px -10px 18px 0 rgba(74, 27, 111, 0.14);
  animation: ballFrame 2s ease-in-out infinite alternate;
  animation-duration: 1.8s;
}
.head-ball-b5 {
  width: 155px;
  height: 155px;
  top: -15%;
  left: 30%;
}
.head-ball-b5 i {
  background-image: radial-gradient(30% 30%, #30c9ff, #5a85ff 100%);
  box-shadow: inset -3px -5px 10px 0 rgba(255, 255, 255, 0.12),
    inset -2px -2px 5px 0 rgba(255, 255, 255, 0.31),
    inset -5px -5px 10px 0 rgba(182, 223, 255, 0.59),
    inset -10px -10px 18px 0 rgba(27, 41, 111, 0.14);
  animation: ballFrame 2s ease-in-out infinite alternate;
  animation-duration: 1.2s;
}

.head-ball-b6 {
  width: 195px;
  height: 195px;
  top: -25%;
  left: 60%;
}
.head-ball-b6 i {
  background-image: radial-gradient(30% 30%, #eb52e6, #df1b2a 100%);
  box-shadow: inset -3px -5px 10px 0 rgba(255, 255, 255, 0.12),
    inset -2px -2px 5px 0 rgba(255, 255, 255, 0.31),
    inset -5px -5px 10px 0 rgba(213, 232, 250, 0.38),
    inset -10px -10px 18px 0 rgba(27, 41, 111, 0.14);
  animation: ballFrame 2s ease-in-out infinite alternate;
  animation-duration: 2.3s;
}
.head-ball-b7 {
  width: 474px;
  height: 474px;
  top: -60%;
  left: 35%;
}
.head-ball-b7 i {
  background-image: radial-gradient(30% 30%, #fff212, #efb150 100%);
  box-shadow: inset -5px -5px 8px 0 rgba(243, 255, 116, 0.49),
    inset -5px -5px 20px 0 rgba(118, 38, 38, 0.14);
  animation: ballFrame 2s ease-in-out infinite alternate;
  animation-duration: 3.2s;
}

@keyframes ballFrame {
  0% {
    transform: translateY(0);
  }
  100% {
    transform: translateY(20px);
  }
}

.help-title {
  text-align: center;
}
.help-title img {
  width: 200px;
  height: 200px;
  margin-top: 12%;
}
.slogn1 {
  line-height: 67px;
  text-align: center;
  font-size: 48px;
  color: #444;
  font-weight: 300;
}
.slogn2 {
  line-height: 33px;
  font-size: 24px;
  color: #444;
  font-weight: 300;
}
@media only screen and (min-width: 768px) and (max-width: 1024px) {
  .help-title img {
    width: 200px;
    height: 200px;
    margin-top: 25%;
  }
}
@media only screen and (max-width: 768px) {
  .help-title img {
    width: 200px;
    height: 200px;
    margin-top: 50%;
  }
  .head-ball-b1 {
    width: 30px;
    height: 30px;
    top: 65%;
    left: 2%;
  }
  .head-ball-b2 {
    width: 60px;
    height: 60px;
    top: 15%;
    left: 15%;
  }
  .head-ball-b3 {
    width: 60px;
    height: 60px;
    top: 25%;
    right: -10px;
  }
  .head-ball-b4 {
    width: 90px;
    height: 90px;
    top: 25%;
    left: -10%;
  }
  .head-ball-b7 {
    width: 400px;
    height: 400px;
  }
}
</style>


