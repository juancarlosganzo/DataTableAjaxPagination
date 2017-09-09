namespace DataTableAjaxPagination.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DataTableAjaxPagination.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(DataTableAjaxPagination.Models.ApplicationDbContext context)
        {
            for (int c = 0; c < 10000; c++) {
                context.Items.AddOrUpdate(i => i.Description, new Models.Item {Description ="Item" +c.ToString() });
            }
        }
    }
}
