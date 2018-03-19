<template>
  <div class="find-container">
    <find-tool @popSort="changeSort" @popTag="changeTag" @popCity="changeCity"></find-tool>
    <find-content :sort="sort" :tag="tag" :city="city"></find-content>
  </div>
</template>

<script>
import Tool from "./find/tool.vue";
import Content from "./find/find-content.vue";
export default {
  data: function() {
    return {
      sort: 0,
      tag: 0,
      city: ""
    };
  },
  methods: {
    changeSort(data) {
      this.sort = data;
    },
    changeTag(data) {
      this.tag = data;
    },
    changeCity(data) {
      this.city = data;
    }
  },
  components: {
    "find-tool": Tool,
    "find-content": Content
  }
};
</script>

<style>
.find-container {
  width: 100%;
  background-color: #f3f5f7;
}
</style>


