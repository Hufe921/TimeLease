<template>
    <div class="exchange-container">
        <div class="my-exchange-container" v-for="(item,index) in shop" :key="index">
            <!-- 商品图片 -->
            <div class="my-exchange-left">
                <img :src="url+item.Cover" />
            </div>
            <div class="my-exchange-right">
                <p>{{item.CreatedOn}}</p>
                <p>{{item.Name}}</p>
                <p>积分：{{item.BonusPoint}}</p>
                <p>{{item.Phone}}</p>
                <p :title="item.Address">{{item.Address}}</p>
                 <div class="my-exchange-state">
                     <template v-if="item.State==0">
                        <div class="update-state" @click="putShopExchange(item.ID)">确认收货</div>
                     </template>
                     <template v-else>
                        <div class="update-state state-success">已完成</div>
                     </template>
                </div>
            </div>
            <div class="clear"></div>
        </div>
        <div class="clear"></div>
        <exchange-load :isLoader="isLoader"></exchange-load>
        <exchange-null :isShow="isShow"></exchange-null>
    </div>
</template>

<script>
import axios from "axios";
import ExchangeNull from "../../../Tip/null.vue";
import ExchangeLoad from "../../../Tip/loading.vue";
export default {
  data: function() {
    return {
      url: this.$store.state.url,
      shop: [],
      isLoader: true,
      isShow: false
    };
  },
  mounted: function() {
    this.getShop();
  },
  methods: {
    clear() {
      this.shop = [];
      this.isShow = false;
      this.isLoader = true;
    },
    getShop() {
      var self = this;
      const address = `${self.url}/api/Shop/${self.user.Id}/getMyShop`;
      axios.get(address).then(res => {
        self.isLoader = false;
        if (!res.data.length) {
          self.isShow = true;
        }
        self.shop = res.data;
      });
    },
    putShopExchange(id) {
      var self = this;
      const address = `${self.url}/api/Shop/${id}/putShopExchange`;
      $.confirm({
        title: "通知!",
        content: "确认收货？",
        buttons: {
          取消: function() {},
          确定: {
            btnClass: "btn-blue",
            action: function() {
              loadingDialog();
              axios
                .put(address)
                .then(res => {
                  $(".jconfirm").remove();
                  showDialog("收货成功！");
                  self.getShop();
                })
                .catch(err => {
                  if (err.response.status) {
                    $(".jconfirm").remove();
                    showDialog(err.response.data.Reason);
                  }
                });
            }
          }
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
    "exchange-null": ExchangeNull,
    "exchange-load": ExchangeLoad
  }
};
</script>

<style>
.exchange-container {
  width: 100%;
}
.my-exchange-container {
  width: 45%;
  height: 215px;
  cursor: pointer;
  float: left;
  border-radius: 3px;
  margin: 15px 0px 0px 2.5%;
  background-color: #fff;
  border: 1px solid #ececec;
  box-shadow: 0px 4px 8px 0px rgba(7, 17, 27, 0.1);
}
.my-exchange-left {
  width: 215px;
  height: 215px;
  float: left;
}
.my-exchange-left img {
  width: 100%;
  height: 100%;
}
.my-exchange-right {
  width: calc(100% - 220px);
  height: 100%;
  margin-left: 5px;
  float: right;
}
.my-exchange-right p {
  width: 100%;
  text-align: center;
  overflow: hidden;
  white-space: nowrap;
  text-overflow: ellipsis;
}
.my-exchange-right p:nth-child(1) {
  line-height: 35px;
}
.update-state {
  width: 100px;
  height: 30px;
  color: #fff;
  cursor: pointer;
  margin: 0px auto;
  line-height: 15px;
  padding: 7px 18px;
  text-align: center;
  letter-spacing: 1px;
  border-radius: 18px;
  background: linear-gradient(to right, #ffae12 0, #f07d17 100%);
}
.state-success {
  background: linear-gradient(to right, #91ea86 0, #50c57a 100%) !important;
}
@media only screen and (max-width: 768px) {
  .my-exchange-container {
    width: 99%;
    height: 215px;
    float: none;
    margin: 15px auto;
  }
  .my-exchange-right p {
    width: 95%;
  }
}
</style>


