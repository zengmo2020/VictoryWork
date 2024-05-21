using System;
using System.Collections.Generic;
using System.Text;

namespace TalentOldDBHelper
{
    public class userinfor
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class ZhuanJiaBirth
    {
        public int Id { get; set; }
        public string Phone { get; set; }
        public string PhoneBase64 { get; set; }
        public string Sex { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
