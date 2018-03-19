<template>
    <div class="my-activity-container">
      <template v-if="review.length!=0">
        <my-activity v-for="(item,index) in review" :content="item" :key="index" :isLine="true" @refresh="refresh"></my-activity>
      </template>
      <template v-else>
        <activity-null :isShow="isShow"></activity-null>
      </template>
      <activity-load :isLoader="isLoader"></activity-load>
    </div>
</template>

<script>
import MyAcitvity from "./apply-state.vue";
import axios from "axios";
import ActivityNull from "../../../Tip/null.vue";
import ActivityLoad from "../../../Tip/loading.vue";
export default {
  mounted: function() {
    this.getActivity();
  },
  data: function() {
    return {
      url: this.$store.state.url,
      isShow: false,
      isLoader: true,
      review: []
    };
  },
  methods: {
    clear() {
      this.review = [];
      this.isShow = false;
      this.isLoader = true;
    },
    getActivity() {
      var self = this;
      self.clear();
      const address = `${self.url}/api/Apply/${self.user.Id}/getPartner`;
      axios
        .get(address)
        .then(res => {
          if (res.data.length == 0) {
            self.isShow = true;
          }
          self.isLoader = false;
          self.review = res.data;
        })
        .catch(err => {
          if (err.response.status) {
            showDialog(err.response.data.Reason);
          }
        });
    },
    refresh() {
      this.getActivity();
    }
  },
  components: {
    "my-activity": MyAcitvity,
    "activity-null": ActivityNull,
    "activity-load": ActivityLoad
  },
  computed: {
    user() {
      return this.$store.state.user;
    }
  }
};
</script>

<style>
.my-activity-container {
  width: 100%;
}
</style>

