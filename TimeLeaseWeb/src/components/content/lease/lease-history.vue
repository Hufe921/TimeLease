<template>
  <div class="lease-history-container">
    <h3>发布历史：</h3>
    <template v-if="content.length">
      <lease-history v-for="(item,index) in content" :key="index" :content="item"></lease-history>
    </template>
    <template v-else>
      <lease-history-null :isShow="true"></lease-history-null>
    </template>
    <lease-history-load :isLoader="isLoader"></lease-history-load>
  </div>
</template>

<script>
import axios from "axios";
import History from "../home/home-main/content-item.vue";
import TipNull from "../../Tip/null.vue";
import TipLoad from "../../Tip/loading.vue";
export default {
  data: function() {
    return {
      url: this.$store.state.url,
      content: [],
      isLoader: true
    };
  },
  mounted: function() {
    this.getContent();
  },
  methods: {
    getContent() {
      var self = this;
      const address = `${self.url}/api/Article/${self.user.Id}/getUserHistory`;
      axios
        .get(address)
        .then(res => {
          self.isLoader = false;
          self.content = res.data;
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
    "lease-history": History,
    "lease-history-null": TipNull,
    "lease-history-load": TipLoad
  }
};
</script>

<style>
.lease-history-container {
  width: 100%;
}
</style>

