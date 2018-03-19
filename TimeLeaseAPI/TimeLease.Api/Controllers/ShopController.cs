using Newtonsoft.Json;
using Rita.System;
using Rita.Utilities.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using TimeLease.Domain;
using TimeLease.Domain.Entities;
using TimeLease.Dto.Shop;
using TimeLease.Dto.ShopExchange;

namespace TimeLease.Api.Controllers
{
    /// <summary>
    /// 积分商城有关
    /// </summary>
    [RoutePrefix("api/Shop")]
    public class ShopController : ApiController
    {
        private readonly TimeLeaseUnitOfWork unitOfWork = new TimeLeaseUnitOfWork(new TimeLeaseDbContext());

        /// <summary>
        /// 获取商品类型
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("getShopType")]
        public async Task<HttpResponseMessage> GetShopType()
        {
            HttpResponseMessage message = new HttpResponseMessage();
            try
            {
                var typeRepo = unitOfWork.CreateRepository<ShopType>();
                var result = typeRepo.Get(c => c.IsActive).SelectTo<ShopType, ShopTypeShow>();
                message = new HttpResponseMessage(HttpStatusCode.OK);
                message.Content = new StringContent(JsonConvert.SerializeObject(result));
                return await Task.FromResult(message);
            }
            catch (Exception ex)
            {
                LogHelper.LogError(ex);
                return HttpResponceHelper.GetError(message);
            }
        }

        /// <summary>
        /// 获取积分商品
        /// </summary>
        /// <param name="currentPage"></param>
        /// <param name="typeId"></param>
        /// <returns></returns>
        [HttpGet, Route("{currentPage}/getShop")]
        public async Task<HttpResponseMessage> GetShop(int currentPage, int typeId = 0) {
            HttpResponseMessage message = new HttpResponseMessage();
            try {
                var shopRepo = unitOfWork.CreateRepository<Shop>();
                //筛选
                Expression<Func<Shop, bool>> filterExpresstion =
                x => (typeId == 0 || x.ShopTypeId == typeId) && x.IsActive;
                var result = shopRepo.Get(filter: filterExpresstion)
                    .OrderByDescending(c=>c.CreatedOn)
                    .Skip((currentPage - 1) * 8)
                    .Take(8).SelectTo<Shop, ShopShow>();
                message = new HttpResponseMessage(HttpStatusCode.OK);
                message.Content = new StringContent(JsonConvert.SerializeObject(result));
                return await Task.FromResult(message);
            }
            catch (Exception ex) {
                LogHelper.LogError(ex);
                return HttpResponceHelper.GetError(message);
            }
        }

        /// <summary>
        /// 添加积分兑换
        /// </summary>
        /// <param name="exchange"></param>
        /// <returns></returns>
        [HttpPost,Route("shopExchange")]
        public async Task<HttpResponseMessage> PostShopExchange([FromBody]ShopExchangeAdd exchange) {
            HttpResponseMessage message = new HttpResponseMessage();
            try {
                var bpRepo = unitOfWork.CreateRepository<BonusPoint>();
                var exchangeRepo = unitOfWork.CreateRepository<ShopExchange>();
                var shopRepo = unitOfWork.CreateRepository<Shop>();
                var userBp=bpRepo.Get(c => c.IsActive && c.UserId == exchange.UserId).FirstOrDefault();
                var shopBp = shopRepo.Get(c => c.IsActive&&c.ID==exchange.ShopId).FirstOrDefault();
                if (userBp==null||shopBp==null) {
                    return HttpResponceHelper.Defined(message, "兑换失败！积分不足或商品可能已经下架请重试！");
                }
                if (userBp.Quantity<shopBp.BonusPoint) {
                    return HttpResponceHelper.Defined(message, "兑换失败！你的积分不足以兑换该商品！");
                }
                //减少用户积分
                userBp.Quantity = userBp.Quantity - shopBp.BonusPoint;
                bpRepo.Update(userBp);
                //添加交易状态
                exchange.State = 0;
                exchangeRepo.Add(exchange.CloneTo<ShopExchangeAdd, ShopExchange>());
                unitOfWork.Commit();
                message = new HttpResponseMessage(HttpStatusCode.OK);
                return await Task.FromResult(message);
            }
            catch (Exception ex) {
                LogHelper.LogError(ex);
                return HttpResponceHelper.AddError(message);
            }
        }

        /// <summary>
        /// 获取我的积分兑换商品历史
        /// </summary>
        /// <returns></returns>
        [HttpGet,Route("{userId}/getMyShop")]
        public async Task<HttpResponseMessage> GetMyShop(int userId) {
            HttpResponseMessage message = new HttpResponseMessage();
            try {
                var shopRepo = unitOfWork.CreateRepository<Shop>();
                var exchangeRepo = unitOfWork.CreateRepository<ShopExchange>();
                var result = new List<ShopExchangeHistory>();
                var data = exchangeRepo.Get(c => c.UserId == userId && c.IsActive).
                    OrderByDescending(c=>c.CreatedOn).Select(c=>new {
                    ID=c.ID,
                    UserId=c.UserId,
                    ShopId=c.ShopId,
                    Address=c.Address,
                    Phone=c.Phone,
                    State=c.State,
                    CreatedOn=c.CreatedOn
                });
                if (data.Any()) {
                    foreach (var item in data.ToList()) {
                        var shop = shopRepo.Get(c => c.ID == item.ShopId).First();
                        var temp = new ShopExchangeHistory() {
                             ID=item.ID,
                             Phone=item.Phone,
                             Address=item.Address,
                             State=item.State,
                             CreatedOn=item.CreatedOn.ToString("yyyy-MM-dd HH:mm:ss"),
                             BonusPoint= shop.BonusPoint,
                             Cover=shop.Cover,
                             Name=shop.Name
                        };
                        result.Add(temp);
                    }
                }
                message = new HttpResponseMessage(HttpStatusCode.OK);
                message.Content = new StringContent(JsonConvert.SerializeObject(result));
                return await Task.FromResult(message);
            }
            catch (Exception ex) {
                LogHelper.LogError(ex);
                return HttpResponceHelper.GetError(message);
            }
        }

        /// <summary>
        /// 确认收货
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPut,Route("{Id}/putShopExchange")]
        public async Task<HttpResponseMessage> putShopExchange(int Id) {
            HttpResponseMessage message = new HttpResponseMessage();
            try {
                var exchangeRepo = unitOfWork.CreateRepository<ShopExchange>();
                var data = exchangeRepo.Get(c => c.IsActive && c.State == 0&&c.ID==Id).FirstOrDefault();
                if (data == null)
                {
                    return HttpResponceHelper.Defined(message, "修改失败！不存在或已经修改，请刷新页面！");
                }
                data.State = 1;
                exchangeRepo.Update(data);
                unitOfWork.Commit();
                message = new HttpResponseMessage(HttpStatusCode.OK);
                return await Task.FromResult(message);
            }
            catch (Exception ex) {
                LogHelper.LogError(ex);
                return HttpResponceHelper.UpdateError(message);
            }
        }

        /// <summary>
        /// 是否允许兑换
        /// </summary>
        /// <returns></returns>
        [HttpGet,Route("{shopId}/{userId}/isAllowExchange")]
        public async Task<HttpResponseMessage> IsAllowExchange(int shopId,int userId) {
            HttpResponseMessage message = new HttpResponseMessage();
            try {
                var bpRepo = unitOfWork.CreateRepository<BonusPoint>();
                var shopRepo = unitOfWork.CreateRepository<Shop>();
                var shopBp = shopRepo.Get(c => c.ID == shopId).FirstOrDefault().BonusPoint;
                var user = bpRepo.Get(c => c.UserId == userId).FirstOrDefault();
                var userBp = user == null ? 0 : user.Quantity;
                var result = false;
                if (userBp>=shopBp)
                {
                    result = true;
                }
                message = new HttpResponseMessage(HttpStatusCode.OK);
                message.Content = new StringContent(JsonConvert.SerializeObject(result));
                return await Task.FromResult(message);
            }
            catch (Exception ex) {
                LogHelper.LogError(ex);
                return HttpResponceHelper.GetError(message);
            }
        }


        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (unitOfWork != null)
            {
                unitOfWork.Dispose();
            }
        }
    }
}
