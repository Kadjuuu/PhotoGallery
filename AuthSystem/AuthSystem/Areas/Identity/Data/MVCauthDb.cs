using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthSystem.Areas.Identity.Data
{
    public class MVCauthDb : DbContext
    {
        public MVCauthDb(DbContextOptions<MVCauthDb> options) : base(options)
        {

        }

        public DbSet<AspNetUsers> aspNetUsers { get; set; }

    }
}
