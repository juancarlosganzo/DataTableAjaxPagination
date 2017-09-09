using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DataTableAjaxPagination.Models
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext()  
            :base("AppConnection")
        {
          }
        public virtual DbSet<Item> Items { get; set; }
    }
}