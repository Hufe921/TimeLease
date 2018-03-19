using Newtonsoft.Json;
using Rita.System;
using Rita.Utilities.Helper;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using TimeLease.Domain;
using TimeLease.Domain.Entities;
using TimeLease.Dto.Article;
using TimeLease.Dto.Tag;

namespace TimeLease.Api.Controllers
{
    [RoutePrefix("api/Article")]
    public class ArticleController : ApiController
    {
        private readonly TimeLeaseUnitOfWork unitOfWork = new TimeLeaseUnitOfWork(new TimeLeaseDbContext());

        /// <summary>
        /// 添加文章详情
        /// </summary>
        /// <param name="article"></param>
        /// <returns></returns>
        [HttpPost, Route("postDetail")]
        public async Task<HttpResponseMessage> PostDetail([FromBody]ArticleDetailForAdd article)
        {
            HttpResponseMessage message = null;
            try
            {
                var adRepo = unitOfWork.CreateRepository<ArticleDetail>();
                var userRepo = unitOfWork.CreateRepository<UserEntity>();
                var userData = userRepo.Get(c => c.IsActive == true && c.ID == article.UserId).FirstOrDefault();
                if (userData == null)
                {
                    return HttpResponceHelper.Defined(message, "文章添加失败，该用户不存在！");
                }
                adRepo.Add(article.CloneTo<ArticleDetailForAdd, ArticleDetail>());
                unitOfWork.Commit();
                message = new HttpResponseMessage(HttpStatusCode.OK);
                return await Task.FromResult(message);
            }
            catch (Exception ex)
            {
                LogHelper.LogError(ex);
                return HttpResponceHelper.AddError(message);
            }
        }

        /// <summary>
        /// 查看文章详情
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="articleId"></param>
        /// <returns></returns>
        [HttpGet,Route("{userId}/{articleId}/getDetail")]
        public async Task<HttpResponseMessage> GetDetail(int userId,int articleId) {
            HttpResponseMessage message = new HttpResponseMessage();
            try
            {
                var adRepo = unitOfWork.CreateRepository<ArticleDetail>();
                var articleData = adRepo.Get(c => c.IsActive == true && c.ID == articleId&&c.UserId==userId).FirstOrDefault();
                if (articleData == null)
                {
                    return HttpResponceHelper.Defined(message, "没有找到符合文章！");
                }
                message = new HttpResponseMessage(HttpStatusCode.OK);
                message.Content = new StringContent(JsonConvert.SerializeObject(articleData.Content));
                return await Task.FromResult(message);
            }
            catch (Exception ex)
            {
                LogHelper.LogError(ex);
                return HttpResponceHelper.GetError(message);
            }
        }

        /// <summary>
        /// 获取数据库全部标签
        /// </summary>
        /// <returns></returns>
        [HttpGet,Route("getTags")]
        public async Task<HttpResponseMessage> GetTags() {
            HttpResponseMessage message = null;
            try {
                var tagRepo = unitOfWork.CreateRepository<Tag>();
                var data = tagRepo.Get(c => c.IsActive).SelectTo<Tag, TagForShow>();
                message = new HttpResponseMessage(HttpStatusCode.OK);
                message.Content = new StringContent(JsonConvert.SerializeObject(data));
                return await Task.FromResult(message);
            }
            catch (Exception ex) {
                LogHelper.LogError(ex);
                return HttpResponceHelper.GetError(message);
            }
        }

        /// <summary>
        /// 获得文章类型
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("getTypes")]
        public async Task<HttpResponseMessage> GetTypes()
        {
            HttpResponseMessage message = null;
            try
            {
                var typeRepo = unitOfWork.CreateRepository<TypeEntity>();
                var data = typeRepo.Get(c => c.IsActive).SelectTo<TypeEntity, TagForShow>();
                message = new HttpResponseMessage(HttpStatusCode.OK);
                message.Content = new StringContent(JsonConvert.SerializeObject(data));
                return await Task.FromResult(message);
            }
            catch (Exception ex)
            {
                LogHelper.LogError(ex);
                return HttpResponceHelper.GetError(message);
            }
        }

        /// <summary>
        /// 为出租时间提供文章详情列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet,Route("getDetailList")]
        public async Task<HttpResponseMessage> GetDetailList(int userId) {
            HttpResponseMessage message = null;
            try {
                var adRepo = unitOfWork.CreateRepository<ArticleDetail>();
                var data = adRepo.Get(c => c.IsActive&&!c.IsUse && c.UserId == userId);
                var newData = new List<ArticleDetailList>();
                if (data.Any())
                { 
                    foreach (var item in data.ToList())
                    {
                        ArticleDetailList ad = new ArticleDetailList()
                        {
                            CreatedOn = item.CreatedOn.ToString("yyyy-MM-dd HH:mm:ss"),
                            ID = item.ID
                        };
                        newData.Add(ad);
                    }
                }
                message = new HttpResponseMessage(HttpStatusCode.OK);
                message.Content = new StringContent(JsonConvert.SerializeObject(newData));
                return await Task.FromResult(message);
            }
            catch (Exception ex) {
                LogHelper.LogError(ex);
                return HttpResponceHelper.GetError(message);
            }
        }

        /// <summary>
        /// 上传文章
        /// </summary>
        /// <returns></returns>
        [HttpPost,Route("postArticle")]
        public async Task<HttpResponseMessage> PostArticle() {
            HttpResponseMessage message = new HttpResponseMessage();
            try
            {
                var articleRepo = unitOfWork.CreateRepository<Article>();
                var articleDetailRepo = unitOfWork.CreateRepository<ArticleDetail>();
                ArticleAdd aa = new ArticleAdd();
                foreach (var item in HttpContext.Current.Request.Form.AllKeys) //文本信息
                {
                    var requestData = HttpContext.Current.Request.Form.GetValues(item)[0];
                    if (item == "title")
                    {
                        aa.Title = requestData;
                    }
                    else if (item == "city")
                    {
                        aa.City = requestData;
                    }
                    else if (item == "tag")
                    {
                        aa.TagId = int.Parse(requestData);
                    }
                    else if (item == "type")
                    {
                        aa.TypeId =int.Parse(requestData);
                    }
                    else if (item == "time")
                    {
                        aa.Time = requestData;
                    }
                    else if (item == "remark")
                    {
                        aa.Remark = requestData;
                    }
                    else if (item == "bp")
                    {
                        aa.BonusPoints = int.Parse(requestData);
                    }
                    else if (item=="detail")
                    {
                        aa.ArticleDetailId =int.Parse(requestData);
                    }
                    else if (item == "userId")
                    {
                        aa.UserId = int.Parse(requestData);
                    }
                }
                //保存图片
                HttpFileCollection files = HttpContext.Current.Request.Files;
                HttpPostedFile file = files["cover"];
                var root = HttpContext.Current.Server.MapPath("~/Upload/Cover/");
                if (!Directory.Exists(root))
                {
                    Directory.CreateDirectory(root);
                }
                var fileName = Path.GetFileName(file.FileName);
                var suffix = Path.GetExtension(fileName).Substring(1);
                var reName = string.Format("{0}.{1}", DateTime.Now.ToString("yyyyMMddhhmmss"), suffix);
                var path = string.Format("/Upload/Cover/{0}", reName);//入库路径
                var fullPath = Path.Combine(root, reName); //真实路径
                //取头像的缩略图
                using (var img = Image.FromStream(file.InputStream))
                {
                    file.InputStream.Position = 0;
                    //压缩尺寸大小
                    var thumbnailImage = img.GetThumbnailImage(210, 150, null, IntPtr.Zero);
                    //保存
                    thumbnailImage.Save(fullPath);
                }
                aa.Cover = path;
                articleRepo.Add(aa.CloneTo<ArticleAdd, Article>());
                //修改文章已使用状态
                int detailId = int.Parse(HttpContext.Current.Request.Form.GetValues("detail")[0]);
                var detail=articleDetailRepo.Get(c => c.ID == detailId).FirstOrDefault();
                detail.IsUse = true;
                articleDetailRepo.Update(detail);
                unitOfWork.Commit();
                //成功
                message = new HttpResponseMessage(HttpStatusCode.OK);
                return await Task.FromResult(message);
            }
            catch (Exception ex)
            {
                LogHelper.LogError(ex);
                return HttpResponceHelper.AddError(message);
            }
        }

        /// <summary>
        /// 获取用户发布历史
        /// </summary>
        /// <returns></returns>
        [HttpGet,Route("{userId}/getUserHistory")]
        public async Task<HttpResponseMessage> GetUserHistory(int userId,string type="") {
            HttpResponseMessage message = null;
            try {
                var articleRepo = unitOfWork.CreateRepository<Article>();
                var typeRepo = unitOfWork.CreateRepository<TypeEntity>();
                var userRepo = unitOfWork.CreateRepository<UserEntity>();
                var praiseRepo = unitOfWork.CreateRepository<ArticlePraise>();
                var lookRepo = unitOfWork.CreateRepository<ArticleLook>();
                //获取类别
                var typeEntity=typeRepo.Get(c => c.Name == type).FirstOrDefault();
                var typeId = typeEntity == null ? 0 : typeEntity.ID;
                //筛选
                Expression<Func<Article, bool>> filterExpresstion =
                x => (typeId==0||x.TypeId == typeId) &&
                     (x.UserId==userId) && x.IsActive;
                var data = articleRepo.Get(filter:filterExpresstion).Select(c => new
                {
                    ID = c.ID,
                    Cover = c.Cover,
                    Title = c.Title,
                    Remark = c.Remark,
                    CreatedOn = c.CreatedOn,
                    City = c.City,
                    BonusPoints = c.BonusPoints,
                    UserId = c.UserId,
                    TypeId = c.TypeId
                });
                var result = new List<ArticleShow>();
                foreach (var item in data.ToList())
                {
                    var article = new ArticleShow()
                    {
                        ID = item.ID,
                        Cover = item.Cover,
                        Title = item.Title,
                        Remark = item.Remark,
                        CreatedOn = item.CreatedOn.ToString("yyyy-MM-dd HH:mm:ss"),
                        City = item.City,
                        BonusPoints = item.BonusPoints,
                        User = userRepo.Get(c => c.ID == item.UserId).First().Name,
                        Type = typeRepo.Get(c => c.ID == item.TypeId).First().Name,
                        Look = lookRepo.Get(c => c.ArticleId == item.ID).Count(),
                        Praise = praiseRepo.Get(c => c.ArticleId == item.ID).Count()
                    };
                    result.Add(article);
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
        /// 通过文章id获得文章详情
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("{userId}/{articleId}/getDetailById")]
        public async Task<HttpResponseMessage> GetDeatil(int articleId,int userId)
        {
            HttpResponseMessage message = new HttpResponseMessage();
            try {
                var articleRepo = unitOfWork.CreateRepository<Article>();
                var articleDetailRepo = unitOfWork.CreateRepository<ArticleDetail>();
                var userRepo = unitOfWork.CreateRepository<UserEntity>();
                var praiseRepo = unitOfWork.CreateRepository<ArticlePraise>();
                var lookRepo = unitOfWork.CreateRepository<ArticleLook>();
                var typeRepo = unitOfWork.CreateRepository<TypeEntity>();
                var tagRepo = unitOfWork.CreateRepository<Tag>();
                //查看详情
                var data = articleRepo.Get(c => c.ID == articleId).Select(c => new
                {
                    Id = c.ID,
                    Title = c.Title,
                    City = c.City,
                    BonusPoints = c.BonusPoints,
                    Time = c.Time,
                    TypeId = c.TypeId,
                    UserId = c.UserId,
                    TagId=c.TagId,
                    CreatedOn = c.CreatedOn,
                    ArticleDetailId=c.ArticleDetailId
                }).FirstOrDefault();
                DetailShow ds = null;
                if (data != null)
                {
                    //留下足迹 
                    var isLook = lookRepo.Get(c => c.UserId == userId&&c.ArticleId==articleId);
                    if (!isLook.Any())
                    {
                        var look = new ArticleLookAdd()
                        {
                            ArticleId = articleId,
                            UserId = userId
                        };
                        lookRepo.Add(look.CloneTo<ArticleLookAdd, ArticleLook>());
                        unitOfWork.Commit();
                    }
                    ds = new DetailShow()
                    {
                        ID = data.Id,
                        BonusPoints = data.BonusPoints,
                        City = data.City,
                        CreatedOn = data.CreatedOn.ToString("yyyy-MM-dd HH:mm:ss"),
                        Time=data.Time,
                        Title=data.Title,
                        User=userRepo.Get(c=>c.ID==data.UserId).First().Name,
                        Look = lookRepo.Get(c => c.ArticleId == data.Id).Count(),
                        Praise = praiseRepo.Get(c => c.ArticleId == data.Id).Count(),
                        Type = typeRepo.Get(c => c.ID == data.TypeId).First().Name,
                        Tag= tagRepo.Get(c=>c.ID==data.TagId).First().Name,
                        Content = articleDetailRepo.Get(c=>c.ID==data.ArticleDetailId).First().Content
                    };
                }
                message = new HttpResponseMessage(HttpStatusCode.OK);
                message.Content = new StringContent(JsonConvert.SerializeObject(ds));
                return await Task.FromResult(message);
            }
            catch (Exception ex) {
                LogHelper.LogError(ex);
                return HttpResponceHelper.GetError(message);
            }
        }

        /// <summary>
        /// 用户给文章点赞
        /// </summary>
        /// <returns></returns>
        [HttpPost,Route("{userId}/{articleId}/articlePraise")]
        public async Task<HttpResponseMessage> ArticlePraise(int userId,int articleId) {
            HttpResponseMessage message = null;
            try {
                var apRepo = unitOfWork.CreateRepository<ArticlePraise>();
                var isPraise = apRepo.Get(c => c.UserId == userId && c.ArticleId == articleId);
                if (!isPraise.Any())
                {

                    ArticlePraiseAdd apa = new ArticlePraiseAdd()
                    {
                        ArticleId = articleId,
                        UserId = userId
                    };
                    apRepo.Add(apa.CloneTo<ArticlePraiseAdd, ArticlePraise>());
                    unitOfWork.Commit();
                    message = new HttpResponseMessage(HttpStatusCode.OK);
                    return await Task.FromResult(message);
                }
                else
                {
                    return HttpResponceHelper.Defined(message, "你已经为该活动点过赞！");
                }
            }
            catch (Exception ex) {
                LogHelper.LogError(ex);
                return HttpResponceHelper.AddError(message);
            }
        }

        /// <summary>
        /// 用于发表评论
        /// </summary>
        /// <returns></returns>
        [HttpPost,Route("publishComment")]
        public async Task<HttpResponseMessage> PostComment(ArticleCommentAdd res) {
            HttpResponseMessage message = null;
            try {
                var comRepo = unitOfWork.CreateRepository<Comment>();
                comRepo.Add(res.CloneTo<ArticleCommentAdd, Comment>());
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
        /// 根据文章获得评论
        /// </summary>
        /// <param name="articleId"></param>
        /// <returns></returns>
        [HttpGet,Route("{articleId}/getComment")]
        public async Task<HttpResponseMessage> GetComment(int articleId) {
            HttpResponseMessage message = null;
            try {
                var comRepo = unitOfWork.CreateRepository<Comment>();
                var userRepo = unitOfWork.CreateRepository<UserEntity>();
                var data = comRepo.Get(c => c.IsActive && c.ArticleId == articleId).Select(c => new
                {
                    UserId = c.UserId,
                    Content = c.Content,
                    CreatedOn = c.CreatedOn
                });
                var result = new List<CommentShow>();
                foreach (var item in data.ToList()) {
                    var cs = new CommentShow() {
                         CreatedOn=item.CreatedOn.ToString("yyyy-MM-dd HH:mm:ss"),
                         Content=item.Content,
                         Icon=userRepo.Get(c=>c.ID==item.UserId).First().Icon,
                         User= userRepo.Get(c => c.ID == item.UserId).First().Name,
                    };
                    result.Add(cs);
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
        /// 文章搜索/0默认按时间排序倒叙，1是升序
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet, Route("{currentPage}/articles")]
        public async Task<HttpResponseMessage> GetArticle(int currentPage,int type=0,int tag=0,int sort=0,string city="")
        {
            HttpResponseMessage message = null;
            try
            {
                var articleRepo = unitOfWork.CreateRepository<Article>();
                var typeRepo = unitOfWork.CreateRepository<TypeEntity>();
                var userRepo = unitOfWork.CreateRepository<UserEntity>();
                var praiseRepo = unitOfWork.CreateRepository<ArticlePraise>();
                var lookRepo = unitOfWork.CreateRepository<ArticleLook>();
                //筛选
                Expression<Func<Article, bool>> filterExpresstion =
                x => (type==0 || x.TypeId== type) &&
                     (tag==0 || x.TagId== tag)&&
                     (string.IsNullOrEmpty(city)||x.City==city)&&x.IsActive;
                var temData = articleRepo.Get(filter: filterExpresstion).OrderByDescending(x=>x.CreatedOn);  
                if (sort==1) {
                    temData= temData.OrderBy(x => x.CreatedOn);
                }
                 var data= temData.Skip((currentPage - 1) * 10).Take(10)
                    .Select(c => new
                {
                    ID = c.ID,
                    Cover = c.Cover,
                    Title = c.Title,
                    Remark = c.Remark,
                    CreatedOn = c.CreatedOn,
                    City = c.City,
                    BonusPoints = c.BonusPoints,
                    UserId = c.UserId,
                    TypeId = c.TypeId
                });
                var result = new List<ArticleShow>();
                foreach (var item in data.ToList())
                {
                    var article = new ArticleShow()
                    {
                        ID = item.ID,
                        Cover = item.Cover,
                        Title = item.Title,
                        Remark = item.Remark,
                        CreatedOn = item.CreatedOn.ToString("yyyy-MM-dd HH:mm:ss"),
                        City = item.City,
                        BonusPoints = item.BonusPoints,
                        User = userRepo.Get(c => c.ID == item.UserId).First().Name,
                        Type = typeRepo.Get(c => c.ID == item.TypeId).First().Name,
                        Look = lookRepo.Get(c => c.ArticleId == item.ID).Count(),
                        Praise = praiseRepo.Get(c => c.ArticleId == item.ID).Count()
                    };
                    result.Add(article);
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
        /// 首页根据城市名称获取数据
        /// </summary>
        /// <returns></returns>
        [HttpGet,Route("getDataByCity")]
        public async Task<HttpResponseMessage> GetDataByCity(string city="") {
            HttpResponseMessage message = new HttpResponseMessage();
            try {
                var userRepo = unitOfWork.CreateRepository<UserEntity>();
                var articleRepo = unitOfWork.CreateRepository<Article>();
                var typeRepo = unitOfWork.CreateRepository<TypeEntity>();
                var leaseId = typeRepo.Get(c => c.Name == "出租时间").First().ID;
                var helpId = typeRepo.Get(c => c.Name == "爱心公益").First().ID;
                int leaseNum = articleRepo.Get(c => c.TypeId == leaseId && c.IsActive && c.City == city).Count();
                int HelpNum = articleRepo.Get(c => c.TypeId == helpId && c.IsActive && c.City == city).Count();
                int findNum = userRepo.Get(c => c.City == city && c.IsActive).Count();
                var result = new
                {
                    find = findNum,
                    lease = leaseNum,
                    help = HelpNum,
                };
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
        /// 获取8条最新发布的文章
        /// </summary>
        /// <returns></returns>
        [HttpGet,Route("getNewArticle")]
        public async Task<HttpResponseMessage> GetNewArticle() {
            HttpResponseMessage message = new HttpResponseMessage();
            try {
                var articleRepo = unitOfWork.CreateRepository<Article>();
                var typeRepo = unitOfWork.CreateRepository<TypeEntity>();
                var userRepo = unitOfWork.CreateRepository<UserEntity>();
                var data = articleRepo.Get().OrderByDescending(c => c.CreatedOn).Take(8).Select(c => new {
                    ID = c.ID,
                    Title = c.Title,
                    TypeId = c.TypeId,
                    UserId=c.UserId,
                    CreatedOn=c.CreatedOn
                });
                var result = new List<LastArticle>();
                if (data.Any())
                {
                    foreach (var item in data.ToList())
                    {
                        var temp = new LastArticle() {
                            ID = item.ID,
                            Title = item.Title,
                            CreatedOn = item.CreatedOn.ToString("yyyy-MM-dd HH:mm:ss"),
                            Type=typeRepo.Get(c=>c.ID==item.TypeId).First().Name,
                            User= userRepo.Get(c=>c.ID==item.UserId).First().Name
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
        /// 获取热门活动
        /// </summary>
        /// <returns></returns>
        [HttpGet,Route("getHotArticle")]
        public async Task<HttpResponseMessage> GetHotArticle() {
            HttpResponseMessage message = new HttpResponseMessage();
            try
            {
                var articleRepo = unitOfWork.CreateRepository<Article>();
                var typeRepo = unitOfWork.CreateRepository<TypeEntity>();
                var userRepo = unitOfWork.CreateRepository<UserEntity>();
                var lookRepo = unitOfWork.CreateRepository<ArticleLook>();
                var praiseRepo = unitOfWork.CreateRepository<ArticlePraise>();
                var data = articleRepo.Get().OrderByDescending(c => c.CreatedOn).Take(10).Select(c => new {
                    ID = c.ID,
                    Title = c.Title,
                    TypeId = c.TypeId,
                    UserId = c.UserId,
                    Cover=c.Cover,
                    CreatedOn = c.CreatedOn
                });
                var result = new List<LastArticle>();
                if (data.Any())
                {
                    foreach (var item in data.ToList())
                    {
                        var temp = new LastArticle()
                        {
                            ID = item.ID,
                            Title = item.Title,
                            Cover=item.Cover,
                            CreatedOn = item.CreatedOn.ToString("yyyy-MM-dd HH:mm:ss"),
                            Type = typeRepo.Get(c => c.ID == item.TypeId).First().Name,
                            User = userRepo.Get(c => c.ID == item.UserId).First().Name,
                            Look=lookRepo.Get(c=>c.ArticleId==item.ID).Count(),
                            Praise=praiseRepo.Get(c=>c.ArticleId==item.ID).Count()
                        };
                        result.Add(temp);
                    }
                    result=result.OrderByDescending(c => c.Look).ThenByDescending(c => c.Praise).ToList();
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
