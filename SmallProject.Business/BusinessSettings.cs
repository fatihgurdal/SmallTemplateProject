using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmallProject.Context;
using SmallProject.Entity;

namespace SmallProject.Business
{
    public class BusinessSettings
    {
        private Repository<Settings> db { get; }

        public BusinessSettings()
        {
            db = new Repository<Settings>();
        }

        public Settings GetSettings(int id)
        {
            return db.First(x => x.Id == id);
        }
    }
}
