using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmallProject.Context;
using SmallProject.Entity;

namespace SmallProject.Business
{
    public class BusinessUserAuthorities
    {
        private Repository<UserAuthorities> db { get; }
        public BusinessAuthority BAuthority { get; set; }      

        public BusinessUserAuthorities()
        {
            db = new Repository<UserAuthorities>();
            BAuthority=new BusinessAuthority();
        }

        public bool AuthorityControl(int UserId, string ActionName, string ControllerName)
        {
            try
            {
                var authorityId = BAuthority.GetAuthority(ActionName, ControllerName).Id;
                var authorityCount = db.Count(x => x.UserId == UserId && x.AuthorityId == authorityId);
                return authorityCount > 0;
            }
            catch (Exception)
            {

                return false;
            }
            
        }

        public IEnumerable<UserAuthorities> UserAuthoritieses(int userid)
        {
            return db.QueryList(x => x.UserId == userid && x.IsDeleted==false && x.Authority.IsMenu==true);
        }
    }
}
