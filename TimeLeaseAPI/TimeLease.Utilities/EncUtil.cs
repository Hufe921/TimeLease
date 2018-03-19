using System.Security.Cryptography;
using System.Text;

namespace Rita.System.Encrypt
{
    /// <summary>
    /// 加密帮助类
    /// </summary>
    /// <remarks>
    /// -------------------------------------------
    /// Description :   创建加密帮助类
    /// -------------------------------------------
    /// </remarks>
    public class EncUtil
    {
        /// <summary>
        /// SHA1单向加密
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string Encry(string password)
        {
            var sha1 = SHA1.Create();
            var hashBytes = sha1.ComputeHash(Encoding.UTF8.GetBytes(password));
            var hashBuilder = new StringBuilder();
            foreach (var item in hashBytes)
            {
                hashBuilder.AppendFormat("{0:x2}", item);
            }
            return hashBuilder.ToString();
        }
    }
}
