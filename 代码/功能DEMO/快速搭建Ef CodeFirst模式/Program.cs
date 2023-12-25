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

        public static string StringToBase64(string Str)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(Str);
            return Convert.ToBase64String(bytes);
        }
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
