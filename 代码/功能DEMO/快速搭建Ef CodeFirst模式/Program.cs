using System;
using System.Linq;
using System.Text;

namespace TalentOldDBHelper
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new TalentDbContext())
            {
                var dataList = db.ZhuanJiaBirth.ToList();
                Console.WriteLine("总数：" + dataList.Count());

                var i = 0;
                foreach (var data in dataList)
                {
                    data.PhoneBase64 = StringToBase64(data.Phone);
                    i = i++;
                    Console.WriteLine(i);
                }
                db.SaveChanges();
            }
            Console.ReadLine();



            using (var db = new TalentDbContext())
            {
                var data = db.userinfor.ToList();
                Console.WriteLine("总数：" + data.Count());

                var i = 0;
                foreach (var user in data)
                {
                    user.Name = Base64ToString(user.Name);
                    i = i++;
                    Console.WriteLine(i);
                }
                db.SaveChanges();
            }

            Console.ReadLine();
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="Str"></param>
        /// <returns></returns>
        public static string StringToBase64(string Str)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(Str);
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="Str"></param>
        /// <returns></returns>
        public static string Base64ToString(string Str)
        {
            try
            {
                byte[] bytes = Convert.FromBase64String(Str);
                return Encoding.UTF8.GetString(bytes);
            }
            catch (Exception)
            {
                return "";
            }
        }
    }
}
