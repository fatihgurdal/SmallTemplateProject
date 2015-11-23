using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmallProject.Context;
using SmallProject.Entity;

namespace SmallProject.Business
{
    public class BusinessLog
    {
        private Repository<Log> db { get; }

        public BusinessLog()
        {
            db = new Repository<Log>();
        }

        public int AddLog(Log log)
        {
            return db.Add(log);
        }
        public int DeleteLog(int id)
        {
            return ExistLog(id) ? db.Delete(GetLog(id)) : 0;
        }

        public Log GetLog(int id)
        {
            return ExistLog(id) ? db.First(x => x.Id == id) : new Log() {Id = 0};
        }

        public bool ExistLog(int id)
        {
            return db.QueryList(x=>x.Id==id).Any();
        }
    }
}
