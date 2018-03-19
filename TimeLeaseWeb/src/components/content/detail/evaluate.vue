<template>
  <div class="evaluate-container">
      <div class="evaluate-item">
          <h4>用户留言：</h4>
          <ul>
              <template v-if="evaluate.length">
                <li v-for="(item,index) in evaluate" :key="index">
                    <div class="e-i-img">
                        <img :src="url+item.Icon"/>
                    </div>
                    <div class="e-i-message">
                        <div class="e-i-basic">
                            <p>{{item.User}}</p>
                            <p>{{item.CreatedOn}}</p>
                            <div class="clear"></div>
                        </div>
                        <div class="e-i-evaluate">
                            <p>{{item.Content}}</p>
                        </div>
                    </div>
                    <div class="clear"></div>
                </li>
              </template>
              <template v-else>
                    <evaluate-null :isShow="true"></evaluate-null>
              </template>
            
          </ul>
      </div>
      <!-- 发表评论 -->
      <div class="evaluate-send">
          <h4>发表评论</h4>
          <textarea name="" id="" cols="30" rows="5" placeholder="输入你的评论" v-model="content"></textarea>
          <input type="button" value="发表" @click="publishComment">
      </div>
  </div>
</template>

<script>
import axios from "axios";
import TipNull from "../../Tip/null.vue";
export default {
  mounted: function() {
    this.getComment();
  },
  data: function() {
    return {
      url: this.$store.state.url,
      evaluate: [],
      content: ""
    };
  },
  methods: {
    getComment() {
      var self = this;
      var articleId = this.$route.query.Id;
      const address = `${self.url}/api/Article/${articleId}/getComment`;
      axios
        .get(address)
        .then(res => {
          self.evaluate = res.data;
        })
        .catch(err => {
          if (err.response.data.Reason) {
            showDialog(err.response.data.Reason);
          }
        });
    },
    publishComment() {
      var self = this;
      const address = `${self.url}/api/Article/publishComment`;
      axios
        .post(address, {
          Content: self.content,
          ArticleId: this.$route.query.Id,
          UserId: self.user.Id
        })
        .then(res => {
          self.content = "";
          this.getComment();
        })
        .catch(err => {
          if (err.response.data.Reason) {
            showDialog(err.response.data.Reason);
          }
        });
    }
  },
  computed: {
    user() {
      return this.$store.state.user;
    }
  },
  components: {
    "evaluate-null": TipNull
  }
};
</script>

<style>
.evaluate-container {
  width: 100%;
}
.evaluate-item,
.evaluate-send {
  width: 100%;
}
.evaluate-item li {
  width: 95%;
  max-width: 10px auto;
  border-radius: 3px;
  padding: 10px;
  box-shadow: 0px 4px 8px 0px rgba(7, 17, 27, 0.1);
}
.e-i-img {
  width: 40px;
  float: left;
}
.e-i-img img {
  width: 40px;
  height: 40px;
  border-radius: 50%;
}
.e-i-message {
  width: calc(100% - 50px);
  margin-left: 5px;
  float: right;
}
.e-i-basic p {
  color: gray;
  font-size: 13px;
}
.e-i-basic p:nth-child(1) {
  float: left;
}
.e-i-basic p:nth-child(2) {
  float: right;
}
.e-i-evaluate p {
  font-size: 13px;
  letter-spacing: 1px;
  line-height: 20px;
}
.evaluate-send textarea {
  width: 95%;
  margin: 0px auto;
  padding: 10px;
  outline: none;
  border-radius: 3px;
  border: 1px solid #dcdcdc;
}
.evaluate-send input {
  width: 150px;
  height: 40px;
  border: 0px;
  outline: none;
  border-radius: 3px;
  color: #fff;
  background-color: #f01414;
}
</style>


