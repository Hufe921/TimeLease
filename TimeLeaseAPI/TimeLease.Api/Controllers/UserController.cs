using Newtonsoft.Json;
using Rita.System;
using Rita.Utilities.Helper;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using TimeLease.Domain;
using TimeLease.Domain.Entities;
using TimeLease.Dto.User;

namespace TimeLease.Api.Controllers
{
    [RoutePrefix("api/User")]
    public class UserController : ApiController
    {
        private readonly TimeLeaseUnitOfWork unitOfWork = new TimeLeaseUnitOfWork(new TimeLeaseDbContext());

        /// <summary>
        /// 基本登录
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("login")]
        public async Task<HttpResponseMessage> Get(string phone,string password)
        {
            HttpResponseMessage message = null;
            var userRepo = unitOfWork.CreateRepository<UserEntity>();
            try {
                var data = userRepo.Get(c => c.Phone == phone && c.Password == password).FirstOrDefault();
                if (data != null)
                {
                    var result = new
                    {
                        Id=data.ID,
                        Name = data.Name,
                        Phone = data.Phone,
                        Icon = data.Icon,
                        City = data.City
                    };
                    message = new HttpResponseMessage(HttpStatusCode.OK);
                    message.Content = new StringContent(JsonConvert.SerializeObject(result));
                    return await Task.FromResult(message);
                }
                else {
                    return HttpResponceHelper.Defined(message, "请检查账号和密码！");
                }
            }
            catch (Exception ex) {
                LogHelper.LogError(ex);
                return HttpResponceHelper.GetError(message);
            }    
        }

        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> Post() {
            HttpResponseMessage message = new HttpResponseMessage();  
            try {
                var userRepo = unitOfWork.CreateRepository<UserEntity>();
                UserRegister ur = new UserRegister();
                var phone = HttpContext.Current.Request.Form.GetValues("Phone")[0];
                var isExit= userRepo.Get(c => c.IsActive == true && c.Phone == phone);
                if (isExit.Any()) {
                    return HttpResponceHelper.Defined(message, "该手机号已被注册！");
                }
                foreach (var item in HttpContext.Current.Request.Form.AllKeys) //文本信息
                {
                    var requestData= HttpContext.Current.Request.Form.GetValues(item)[0];
                    if (item == "Phone") {
                        ur.Phone = requestData;
                    } else if (item == "Name") {
                        ur.Name = requestData;
                    } else if (item == "Password") {
                        ur.Password = requestData;
                    } else if (item== "City") {
                        ur.City = requestData;
                    }
                }
                //保存图片
                HttpFileCollection files = HttpContext.Current.Request.Files;
                HttpPostedFile file = files["Cover"];
                var root = HttpContext.Current.Server.MapPath("~/Upload/UserIcon/");
                if (!Directory.Exists(root))
                {
                    Directory.CreateDirectory(root);
                }
                var fileName = Path.GetFileName(file.FileName);
                var suffix = Path.GetExtension(fileName).Substring(1);
                var reName = string.Format("{0}.{1}", DateTime.Now.ToString("yyyyMMddhhmmss"), suffix);
                var path = string.Format("/Upload/UserIcon/{0}", reName);//入库路径
                var fullPath = Path.Combine(root, reName); //真实路径
                //取头像的缩略图
                using (var img = Image.FromStream(file.InputStream))
                {
                    file.InputStream.Position = 0;
                    //压缩尺寸大小
                    var thumbnailImage = img.GetThumbnailImage(150, 150, null, IntPtr.Zero);
                    //保存
                    thumbnailImage.Save(fullPath);
                }
                ur.Icon = path;
                userRepo.Add(ur.CloneTo<UserRegister, UserEntity>());
                unitOfWork.Commit();
                //成功
                message = new HttpResponseMessage(HttpStatusCode.OK);
                return await Task.FromResult(message);
            }
            catch (Exception ex) {
                LogHelper.LogError(ex);
                return HttpResponceHelper.AddError(message);
            }
        }

        /// <summary>
        /// 用户修改密码
        /// </summary>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPut,Route("{oldPassword}/{newPassword}/{userId}/putPassword")]
        public async Task<HttpResponseMessage> PutPassword(string oldPassword,string newPassword,int userId) {
            HttpResponseMessage message = new HttpResponseMessage();
            var userRepo = unitOfWork.CreateRepository<UserEntity>();
            try {
                var data = userRepo.Get(c => c.IsActive == true && c.Password == oldPassword && c.ID == userId).FirstOrDefault();
                if (data == null)
                {
                    return HttpResponceHelper.Defined(message, "修改失败！请确认原密码是否正确！");
                }
                data.Password = newPassword;
                userRepo.Update(data);
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
        /// 用户修改头像
        /// </summary>
        /// <returns></returns>
        [HttpPost,Route("putIcon")]
        public async Task<HttpResponseMessage> PutIcon() {
            HttpResponseMessage message = new HttpResponseMessage();
            try
            {
                var userRepo = unitOfWork.CreateRepository<UserEntity>();
                int userId = int.Parse(HttpContext.Current.Request.Form.GetValues("Id")[0]);
                var userData = userRepo.Get(c => c.IsActive == true && c.ID == userId).FirstOrDefault();
                if (userData==null) {
                    return HttpResponceHelper.Defined(message, "修改失败！用户存在！");
                }
                //保存图片
                HttpFileCollection files = HttpContext.Current.Request.Files;
                HttpPostedFile file = files["Cover"];
                var root = HttpContext.Current.Server.MapPath("~/Upload/UserIcon/");
                var fileName = Path.GetFileName(file.FileName);
                var suffix = Path.GetExtension(fileName).Substring(1);
                var reName = string.Format("{0}.{1}", DateTime.Now.ToString("yyyyMMddhhmmss"), suffix);
                var path = string.Format("/Upload/UserIcon/{0}", reName);//入库路径
                var fullPath = Path.Combine(root, reName); //真实路径
                //取头像的缩略图
                using (var img = Image.FromStream(file.InputStream))
                {
                    file.InputStream.Position = 0;
                    //压缩尺寸大小
                    var thumbnailImage = img.GetThumbnailImage(150, 150, null, IntPtr.Zero);
                    //保存
                    thumbnailImage.Save(fullPath);
                }
                userData.Icon = path;//附加新地址
                userRepo.Update(userData);
                unitOfWork.Commit();
                //成功
                message = new HttpResponseMessage(HttpStatusCode.OK);
                return await Task.FromResult(message);
            }
            catch (Exception ex)
            {
                LogHelper.LogError(ex);
                return HttpResponceHelper.UpdateError(message);
            }
        }

        /// <summary>
        /// 修改用户基础数据
        /// </summary>
        /// <returns></returns>
        [HttpPut,Route("{userId}/{phone}/{name}/{city}/putBasic")]
        public async Task<HttpResponseMessage> PutBasic(int userId,string phone,string name,string city) {
            HttpResponseMessage message = new HttpResponseMessage();
            try {
                var userRepo = unitOfWork.CreateRepository<UserEntity>();
                var userData = userRepo.Get(c => c.IsActive == true && c.ID==userId).FirstOrDefault();
                if (userData == null)
                {
                    return HttpResponceHelper.Defined(message, "修改失败，该用户不存在！");
                }
                userData.Phone = phone;
                userData.Name = name;
                userData.City = city;
                userRepo.Update(userData);
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
        /// 获取用户地址
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        [HttpGet,Route("{userId}/getAddress")]
        public async Task<HttpResponseMessage> GetAddress(int userId)
        {
            HttpResponseMessage message = new HttpResponseMessage();
            try
            {
                var userRepo = unitOfWork.CreateRepository<UserEntity>();
                var userData = userRepo.Get(c => c.IsActive == true && c.ID == userId).FirstOrDefault();
                if (userData == null)
                {
                    return HttpResponceHelper.Defined(message, "该用户不存在！");
                }
                message = new HttpResponseMessage(HttpStatusCode.OK);
                message.Content = new StringContent(JsonConvert.SerializeObject(userData.Address));
                return await Task.FromResult(message);
            }
            catch (Exception ex)
            {
                LogHelper.LogError(ex);
                return HttpResponceHelper.GetError(message);
            }
        }

        /// <summary>
        /// 修改用户地址
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPut,Route("{userId}/putAddress")]
        public async Task<HttpResponseMessage> PutAddress(int userId,[FromBody]UserAddress address) {
            HttpResponseMessage message = new HttpResponseMessage();
            try {
                var userRepo = unitOfWork.CreateRepository<UserEntity>();
                var userData = userRepo.Get(c => c.IsActive == true && c.ID == userId).FirstOrDefault();
                if (userData == null)
                {
                    return HttpResponceHelper.Defined(message, "修改失败，该用户不存在！");
                }
                userData.Address = address.Address;
                userRepo.Update(userData);
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
        /// 通过userid获取积分
        /// </summary>
        /// <returns></returns>
        [HttpGet,Route("bp")]
        public async Task<HttpResponseMessage> GetBp(int userId) {
            HttpResponseMessage message = null;
            try {
                var bpRepo = unitOfWork.CreateRepository<BonusPoint>();
                var data = bpRepo.Get(c => c.IsActive == true && c.UserId == userId).FirstOrDefault();
                int quantity = data == null ? 0 : data.Quantity;
                message = new HttpResponseMessage(HttpStatusCode.OK);
                message.Content = new StringContent(JsonConvert.SerializeObject(quantity));
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
