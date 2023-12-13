using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace MyProject.Core.Utililty
{
    /// <summary>
    /// 使用DES加密解密用户密码的类(注：这个类来源于公司老框架Sunyah.Jungle.Utility.dll)
    /// </summary>
    public static class DESCryptor
    {
        /// <summary>
        /// DES加密字符串
        /// </summary>
        /// <param name="planeText">待加密的平文</param>
        /// <param name="key">DES密钥</param>
        /// <param name="iv">DES初始化向量</param>
        /// <returns>加密后的密文</returns>
        public static string EncryptString(string planeText, string key = "hrzhidi1", string iv = "zktbdeoy")
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            des.Key = ASCIIEncoding.ASCII.GetBytes(key);
            des.IV = ASCIIEncoding.ASCII.GetBytes(iv);

            ICryptoTransform ct = des.CreateEncryptor();
            using (MemoryStream ms = new MemoryStream())
            {
                var byt = Encoding.UTF8.GetBytes(planeText);
                using (CryptoStream cs = new CryptoStream(ms, ct, CryptoStreamMode.Write))
                {
                    cs.Write(byt, 0, byt.Length);
                    cs.FlushFinalBlock();
                    string cipherText = Convert.ToBase64String(ms.ToArray());
                    return cipherText;
                }
            }
        }

        /// <summary>
        /// DES解密字符串
        /// </summary>
        /// <param name="cipherText">密文</param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns>平文</returns>
        public static string DecryptString(string cipherText, string key = "hrzhidi1", string iv = "zktbdeoy")
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            des.Key = ASCIIEncoding.ASCII.GetBytes(key);
            des.IV = ASCIIEncoding.ASCII.GetBytes(iv);

            ICryptoTransform ct = des.CreateDecryptor();
            var byt = Convert.FromBase64String(cipherText);
            using (MemoryStream ms = new MemoryStream(byt))
            {
                using (CryptoStream cs = new CryptoStream(ms, ct, CryptoStreamMode.Read))
                {
                    using (StreamReader sr = new StreamReader(cs))
                    {
                        string planeText = sr.ReadToEnd();
                        return planeText;
                    }
                }
            }
        }

    }
}
