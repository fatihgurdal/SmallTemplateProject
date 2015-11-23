using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmallProject.Context;
using SmallProject.Entity;

namespace SmallProject.Business
{
    public class BusinessErrorLog
    {
        private Repository<ErrorLog> db { get; }

        public BusinessErrorLog()
        {
            db = new Repository<ErrorLog>();
        }

        public int AddErrorLog(ErrorLog log)
        {
            return db.Add(log);
        }
        public int DeleteErrorLog(int id)
        {
            return ExistErrorLog(id) ? db.Delete(GetErrorLog(id)) : 0;
        }

        public ErrorLog GetErrorLog(int id)
        {
            return ExistErrorLog(id) ? db.First(x => x.Id == id) : new ErrorLog() { Id = 0 };
        }

        public bool ExistErrorLog(int id)
        {
            return db.QueryList(x => x.Id == id).Any();
        }
    }
}
