using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmallProject.Context;
using SmallProject.Entity;

namespace SmallProject.Business
{
    public class BusinessAuthority
    {
        private Repository<Authority> db { get; }
        public BusinessAuthority()
        {
                db=new Repository<Authority>();
        }

        public Authority GetAuthority(int id)
        {
            return db.First(x => x.Id == id);
        }
        public Authority GetAuthority(string action,string controller)
        {
            return db.First(x => x.ActionName == action && x.ControllerName==controller);
        }

        public IEnumerable<Authority> GetAuthority(IEnumerable<UserAuthorities> YetkiListesi)
        {
            var authorities = (
                from au in db.List()
                join uau in YetkiListesi on au.Id equals uau.AuthorityId

                where au.IsDeleted == false && au.SubAuthorityId == null
                select au
                );
            return authorities;
        }
    }
}
