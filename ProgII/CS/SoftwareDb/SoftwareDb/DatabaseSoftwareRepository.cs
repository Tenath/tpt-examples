using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareDb
{
    public class DatabaseSoftwareRepository : ISoftwareRepository
    {
        private SoftwareDbContext dbContext = new SoftwareDbContext();

        public void Add(Software sw)
        {
            dbContext.Soft.Add(sw);
            dbContext.SaveChanges();
        }

        public IEnumerable<Software> GetList()
        {
            return dbContext.Soft;
        }

        public void Remove(Software sw)
        {
            dbContext.Soft.Remove(sw);
            dbContext.SaveChanges();
        }

        public void RemoveAt(int index)
        {
            Software sw = dbContext.Soft.Skip(index).Take(1).FirstOrDefault();
            if(sw != null)
                dbContext.Soft.Remove(sw);
            dbContext.SaveChanges();
        }

        public void SaveChanges()
        {
            dbContext.SaveChanges();
        }
    }
}
