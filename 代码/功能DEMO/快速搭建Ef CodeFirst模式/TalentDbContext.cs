using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace TalentOldDBHelper
{
    public class TalentDbContext : DbContext
    {
        public DbSet<userinfor> userinfor { get; set; }
        public DbSet<ZhuanJiaBirth> ZhuanJiaBirth { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=123.206.186.100\\MSSQLSERVER2014,1433; Database=TalentService0102;user = sa; password=Sunyah123;");
        }
    }
}
