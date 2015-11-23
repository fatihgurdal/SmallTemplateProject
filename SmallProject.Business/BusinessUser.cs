using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmallProject.Context;
using SmallProject.Entity;

namespace SmallProject.Business
{
    public class BusinessUser
    {
        private Repository<User> db { get; }

        public BusinessUser()
        {
            db = new Repository<User>();
        }

        public User GetUser(int id)
        {
            return db.First(x => x.Id == id);
        }
        public User GetUser(string username,string password)
        {
            password = Sm.MD5Encryption(password);
            return db.First(x => x.UserName== username && x.Password==password);
        }
        public User GetAdmin()
        {
            return db.List().First();
        }
        public User GetUser(User user)
        {
            return GetUser(user.Id);
        }
        public bool ExistUser(int id)
        {
            return db.ExistData(x => x.Id == id);
        }
        public bool ExistUser(string UserName,string Pasword)
        {
            Pasword = Sm.MD5Encryption(Pasword);
            return db.ExistData(x => x.UserName==UserName && x.Password==Pasword);
        }
        public IEnumerable<User> ListAllUsers()
        {
            return db.List();
        }
        public IEnumerable<User> ListUsers()
        {
            return db.QueryList(x => x.IsDeleted == false);
        }

        public bool AddUser(User user)
        {
            user.Password = Sm.MD5Encryption(user.Password);
            return (db.Add(user) > 0);
        }
    }
}
