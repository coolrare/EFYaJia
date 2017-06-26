using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFYaJia.Models
{
    public partial class ContosoUniversityEntities : DbContext
    {
        public override int SaveChanges()
        {
            var entries = this.ChangeTracker.Entries();

            foreach (var entity in entries)
            {
                if (entity.Entity is Course)
                {

                }
                if (entity.State == EntityState.Modified)
                {
                    entity.CurrentValues.SetValues(new { ModifiedOn = DateTime.Now });
                }
            }

            return base.SaveChanges();
        }
    }
}
