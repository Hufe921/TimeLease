using Newtonsoft.Json;
using Rita.System;
using Rita.Utilities.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using TimeLease.Domain;
using TimeLease.Domain.Entities;
using TimeLease.Dto.Apply;
using TimeLease.Dto.BP;

namespace TimeLease.Api.Controllers
{
    /// <summary>
    /// 申请有关
    /// </summary>
    [RoutePrefix("api/Apply")]
    public class ApplyController : ApiController
    {
        private readonly TimeLeaseUnitOfWork unitOfWork = new TimeLeaseUnitOfWork(new TimeLeaseDbContext());

        /// <summary>
        /// 提交申请
        /// </summary>
        /// <returns></returns>
        [HttpPost,Route("submitApply")]
        public async Task<HttpResponseMessage> PostApply([FromBody] ApplyAdd apply) {
            HttpResponseMessage message = new HttpResponseMessage();
            try {
                var applyRepo = unitOfWork.CreateRepository<Apply>();
                var articleRepo = unitOfWork.CreateRepository<Article>();
                var bpRepo = unitOfWork.CreateRepository<BonusPoint>();
                var data = applyRepo.Get(c => c.IsActive && c.UserId == apply.UserId && c.ArticleId == apply.ArticleId&&c.State!=2);
                if (data.Any()) {
                    return HttpResponceHelper.Defined(message, "申请正在进行或对方已经拒绝申请！");
                }
                var articleUser = articleRepo.Get(c => c.ID == apply.ArticleId).First();
                //所需积分是否够支付
                var bpTemp=bpRepo.Get(c => c.UserId == apply.UserId).FirstOrDefault();
                var myBp = bpTemp == null ? 0 : bpTemp.Quantity;
                var articleBp = articleUser.BonusPoints;
                if (myBp< articleBp) {
                    return HttpResponceHelper.Defined(message, "你的积分不足，请充值！");
                }
                if (bpTemp==null) {
                    var ba = new BpAdd() {
                         Quantity=0,
                         UserId=apply.UserId
                    };
                    bpRepo.Add(ba.CloneTo<BpAdd,BonusPoint>());
                }
                else
                {
                    bpTemp.Quantity = bpTemp.Quantity - articleUser.BonusPoints;
                    bpRepo.Update(bpTemp);
                }
                apply.State = 0;
                apply.ArticleUserId = articleUser.UserId;
                applyRepo.Add(apply.CloneTo<ApplyAdd, Apply>());
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
        /// 获取申请审核
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet,Route("{userId}/applyReview")]
        public async Task<HttpResponseMessage> ApplyReview(int userId) {
            HttpResponseMessage message = new HttpResponseMessage();
            try {
                var applyRepo = unitOfWork.CreateRepository<Apply>();
                var articleRepo = unitOfWork.CreateRepository<Article>();
                var typeRepo = unitOfWork.CreateRepository<TypeEntity>();
                var userRepo = unitOfWork.CreateRepository<UserEntity>();
                //获取未审核的
                var data = applyRepo.Get(c => c.IsActive && c.State == 0&&c.ArticleUserId==userId).Select(c => new
                {
                    ID = c.ID,
                    Phone = c.Phone,
                    Remark = c.Remark,
                    UserId = c.UserId,
                    ArticleId = c.ArticleId,
                    CreatedOn = c.CreatedOn,
                    State = c.State
                });
                var result = new List<ApplyReview>();
                if (data.Any()) {
                    foreach (var item in data.ToList())
                    {
                        var article = articleRepo.Get(c => c.ID == item.ArticleId).First();
                        ApplyReview ap = new ApplyReview()
                        {
                            ID = item.ID,
                            Phone = item.Phone,
                            Remark = item.Remark,
                            CreatedOn = item.CreatedOn.ToString("yyyy-MM-dd HH:mm:ss"),
                            State = item.State,
                            ArticleTitle = article.Title,
                            Type = typeRepo.Get(c => c.ID == article.TypeId).First().Name,
                            User = userRepo.Get(c => c.ID == item.UserId).First().Name
                        };
                        result.Add(ap);
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
        /// 更改审核状态
        /// </summary>
        /// <returns></returns>
        [HttpPut, Route("{applyId}/{state}/putApplyState")]
        public async Task<HttpResponseMessage> PutApplyState(int applyId, int state)
        {
            HttpResponseMessage message = new HttpResponseMessage();
            try {
                var applyRepo = unitOfWork.CreateRepository<Apply>();
                var bpRepo = unitOfWork.CreateRepository<BonusPoint>();
                var articleRepo = unitOfWork.CreateRepository<Article>();
                //确定申请是否存在
                var data = applyRepo.Get(c => c.ID == applyId && c.IsActive).FirstOrDefault();
                if (data == null)
                {
                    return HttpResponceHelper.Defined(message, "修改失败，可能用户已经撤回申请！");
                }
                //获取文章积分
                var articleBp = articleRepo.Get(c => c.ID == data.ArticleId).First().BonusPoints;
                if (state == -1)
                {//拒绝，积分回滚
                    var bp=bpRepo.Get(c => c.UserId == data.UserId).First();
                    bp.Quantity = bp.Quantity + articleBp;
                    bpRepo.Update(bp);
                }
                data.State =state;
                applyRepo.Update(data);
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
        /// 获取我的申请
        /// </summary>
        /// <returns></returns>
        [HttpGet,Route("{userId}/getMyApply")]
        public async Task<HttpResponseMessage> GetMyApply(int userId) {
            HttpResponseMessage message = new HttpResponseMessage();
            try {
                var applyRepo = unitOfWork.CreateRepository<Apply>();
                var typeRepo = unitOfWork.CreateRepository<TypeEntity>();
                var userRepo = unitOfWork.CreateRepository<UserEntity>();
                var articleRepo = unitOfWork.CreateRepository<Article>();
                //查找所有我的申请
                var data = applyRepo.Get(c => c.IsActive && c.UserId == userId).Select(c => new {
                    ID = c.ID,
                    Phone = c.Phone,
                    Remark = c.Remark,
                    UserId = c.UserId,
                    ArticleId = c.ArticleId,
                    CreatedOn = c.CreatedOn,
                    State = c.State
                });
                var result = new List<ApplyReview>();
                if (data.Any())
                {
                    foreach (var item in data.ToList())
                    {
                        var article = articleRepo.Get(c => c.ID == item.ArticleId).First();
                        ApplyReview ap = new ApplyReview()
                        {
                            ID = item.ID,
                            Phone = item.Phone,
                            Remark = item.Remark,
                            CreatedOn = item.CreatedOn.ToString("yyyy-MM-dd HH:mm:ss"),
                            State = item.State,
                            ArticleTitle = article.Title,
                            Type = typeRepo.Get(c => c.ID == article.TypeId).First().Name,
                            User = userRepo.Get(c => c.ID == item.UserId).First().Name
                        };
                        result.Add(ap);
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
        /// 获取我的搭档信息
        /// </summary>
        /// <returns></returns>
        [HttpGet,Route("{userId}/getPartner")]
        public async Task<HttpResponseMessage> GetPartner(int userId) {
            HttpResponseMessage message = new HttpResponseMessage();
            try
            {
                var applyRepo = unitOfWork.CreateRepository<Apply>();
                var typeRepo = unitOfWork.CreateRepository<TypeEntity>();
                var userRepo = unitOfWork.CreateRepository<UserEntity>();
                var articleRepo = unitOfWork.CreateRepository<Article>();
                var result = new List<ApplyReview>();
                var data=articleRepo.Get(c => c.IsActive && c.UserId == userId);
                if (data.Any()) {
                    var articleIds = data.Select(c => c.ID).ToList();
                    //获取我发布文章，我选择通过的申请
                    var applyTemp=applyRepo.Get(c => c.IsActive && articleIds.Contains(c.ArticleId) && (c.State == 1||c.State==2));
                    if (applyTemp.Any()) {
                        var applyMsg=applyTemp.Select(c => new {
                            ID = c.ID,
                            Phone = c.Phone,
                            Remark = c.Remark,
                            UserId = c.UserId,
                            ArticleId = c.ArticleId,
                            CreatedOn = c.CreatedOn,
                            State = c.State
                        });
                        foreach (var item in applyMsg.ToList())
                        {
                            var article = articleRepo.Get(c => c.ID == item.ArticleId).First();
                            ApplyReview ap = new ApplyReview()
                            {
                                ID = item.ID,
                                Phone = item.Phone,
                                Remark = item.Remark,
                                CreatedOn = item.CreatedOn.ToString("yyyy-MM-dd HH:mm:ss"),
                                State = item.State,
                                ArticleTitle = article.Title,
                                Type = typeRepo.Get(c => c.ID == article.TypeId).First().Name,
                                User = userRepo.Get(c => c.ID == item.UserId).First().Name,
                                ArticleFinish=article.IsFinsh
                            };
                            result.Add(ap);
                        }
                    }
                }
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
        /// 修改文章完成状态
        /// </summary>
        /// <param name="applyId"></param>
        /// <returns></returns>
        [HttpPut,Route("{applyId}/putArticleState")]
        public async Task<HttpResponseMessage> PutArticleState(int applyId) {
            HttpResponseMessage message = new HttpResponseMessage();
            try {
                var applyRepo = unitOfWork.CreateRepository<Apply>();
                var articleRepo = unitOfWork.CreateRepository<Article>();
                var bpRepo = unitOfWork.CreateRepository<BonusPoint>();
                //获取待确认申请（待收货）
                var data=applyRepo.Get(c => c.ID == applyId&&c.State==1).FirstOrDefault();
                if (data==null) {
                    return HttpResponceHelper.Defined(message, "修改失败！可能已经修改，请刷新！");
                }
                //修改申请状态为2，此申请已经完成
                data.State = 2;
                applyRepo.Update(data);
                //积分转到发布者
                var article=articleRepo.Get(c => c.ID == data.ArticleId).FirstOrDefault();
                var bp=bpRepo.Get(c => c.UserId == data.ArticleUserId).FirstOrDefault();
                //如果用户存在积分则修改，否则添加
                if (bp != null)
                {
                    bp.Quantity = bp.Quantity + article.BonusPoints;
                    bpRepo.Update(bp);
                }
                else {
                    var bpAdd = new BpAdd() {
                          Quantity=article.BonusPoints,
                          UserId=data.ArticleUserId
                    };
                    bpRepo.Add(bpAdd.CloneTo<BpAdd, BonusPoint>());
                }
                unitOfWork.Commit();
                message = new HttpResponseMessage(HttpStatusCode.OK);
                return await Task.FromResult(message);
            }
            catch (Exception ex) {
                LogHelper.LogError(ex);
                return HttpResponceHelper.UpdateError(message);
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
