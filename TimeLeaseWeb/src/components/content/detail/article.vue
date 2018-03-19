<template>
    <div class="aticels">
        <template v-if="detail.Time">
            <h4>日期：{{detail.Time}}</h4>
            <p v-html="detail.Content"></p>
        </template>
        <template v-else>
           <article-load :isLoader="true"></article-load>
        </template>       
    </div>
</template>

<script>
import axios from "axios";
import TipLoad from "../../Tip/loading.vue";
export default {
  mounted: function() {
    this.getDetail();
  },
  data: function() {
    return {
      url: this.$store.state.url,
      detail: {}
    };
  },
  methods: {
    getDetail() {
      var self = this;
      const address = `${self.url}/api/Article/${self.user.Id}/${self.$route
        .query.Id}/getDetailById`;
      axios
        .get(address)
        .then(res => {
          self.detail = res.data;
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
    "article-load": TipLoad
  }
};
</script>

