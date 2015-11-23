using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using SmallProject.Entity;
namespace SmallProject.Context
{
    public class MyContext:DbContext
    {
        public DbSet<Log> Logs { get; set; }
        public DbSet<Settings> Settings { get; set; }
        public DbSet<ErrorLog> ErrorLogs { get; set; }
        
        public DbSet<User> Users { get; set; }
        public DbSet<Authority> Authorities { get; set; }
        public DbSet<UserAuthorities> UserAuthoritieses { get; set; }
        public MyContext(): base("name=MyContext")
        {
            Database.SetInitializer<MyContext>(new MyInitializer());
        }


        public override int SaveChanges()
        {
            // State'i insert ya da update olan entity'ler elde edilir.
            List<DbEntityEntry> dbEntityEntries =
                this.ChangeTracker.Entries().Where(
                x =>
                    x.State == EntityState.Added ||
                    x.State == EntityState.Modified).ToList();

            dbEntityEntries.ForEach(dbEntityEntry =>
            {
                if (dbEntityEntry.Entity is ErrorLog)
                {
                    var el = dbEntityEntry.Entity as ErrorLog;
                    el.EnterDate=DateTime.Now;
                }
                if (dbEntityEntry.Entity is Log)
                {
                    var el = dbEntityEntry.Entity as Log;
                    el.EnterDate = DateTime.Now;
                }
                if (dbEntityEntry.Entity is EntityBase)
                {
                    EntityBase entityBase = dbEntityEntry.Entity as EntityBase;


                    if (HttpContext.Current.Session != null && HttpContext.Current.Session["LoginObject"] != null)
                    {
                        entityBase.LastModifiedUserName = "fatihgurdal";
                    }

                    switch (dbEntityEntry.State)
                    {
                        case EntityState.Added:
                            entityBase.CreatedDate = DateTime.Now;
                            entityBase.IsDeleted = false;
                            break;
                        case EntityState.Deleted:
                            dbEntityEntry.State = EntityState.Modified;
                            entityBase.ModifiedDate = DateTime.Now;
                            entityBase.IsDeleted = true;
                            break;
                        case EntityState.Detached:
                            break;
                        case EntityState.Modified:
                            entityBase.ModifiedDate = DateTime.Now;
                            entityBase.IsDeleted = false;
                            break;
                        case EntityState.Unchanged:
                            break;
                        default:
                            break;
                    }

                }
            });

            return base.SaveChanges();
        }
        
    }

    public class MyInitializer : CreateDatabaseIfNotExists<MyContext>
    {
        /// <summary>
        /// Varasyilan ver'ler girilmektedir.
        /// </summary>
        /// <param name="context"></param>
        protected override void Seed(MyContext context)
        {
            #region <-- Kullanıcılar -->
            //İlk kullanıcı her zaman ÜST Admindir
            var u = new User()
                {
                    CreatedDate = DateTime.Now.Date,
                    EMail = "f.gurdal@hotmail.com.tr",
                    FirstName = "Fatih",
                    IsDeleted = false,
                    LastName = "GÜRDAL",
                    Password = Sm.MD5Encryption("123456"),
                    UserName = "fatihgurdal",
                    LastModifiedUserName = "fatihgurdal",
                    AvatarFull = "1.png"
                };
                context.Users.Add(u);
                context.SaveChanges();
            var u2 = new User()
            {
                CreatedDate = DateTime.Now.Date,
                EMail = "ziyaretci@fatihgurdak.net",
                FirstName = "Misafir",
                IsDeleted = false,
                LastName = "Ziyaretçi",
                Password = Sm.MD5Encryption("123456"),
                UserName = "misafir",
                LastModifiedUserName = "fatihgurdal"
            };
            context.Users.Add(u2);
            context.SaveChanges();
            #endregion
            #region <-- Yetkiler -->
            //Örnek bir yerki
            var y1 = new Authority()
            {
                ActionName = "Index",
                ControllerName = "Management",
                FaIconCode = "fa fa-home",
                IsDeleted = false,
                IsLink = true,
                CreatedDate = DateTime.Now.Date,
                Name = "Yönetim Paneli",
                IsMenu = true
            };
            var y2 = new Authority()
            {
                ActionName = "Profile",
                ControllerName = "Management",
                FaIconCode = "fa fa-home",
                IsDeleted = false,
                IsLink = true,
                CreatedDate = DateTime.Now.Date,
                Name = "Profil",
                IsMenu = false
            };
            context.Authorities.Add(y1);
            context.Authorities.Add(y2);
            context.SaveChanges();
            var y3 = new Authority()
            {
                FaIconCode = "fa fa-lock",
                IsDeleted = false,
                IsLink = true,
                CreatedDate = DateTime.Now.Date,
                Name = "Oturum Yönetimi",
                IsMenu = true
            };
            context.Authorities.Add(y3);
            context.SaveChanges();
            var y4 = new Authority()
            {
                ActionName = "UserList",
                ControllerName = "Management",
                FaIconCode = "fa fa-users",
                IsDeleted = false,
                IsLink = true,
                CreatedDate = DateTime.Now.Date,
                Name = "Kullanıcı Listesi",
                SubAuthorityId = y3.Id,
                IsMenu = true
            };
            var y5 = new Authority()
            {
                ActionName = "CreateUser",
                ControllerName = "Management",
                FaIconCode = "fa fa-user",
                IsDeleted = false,
                IsLink = true,
                CreatedDate = DateTime.Now.Date,
                Name = "Yeni Kullanıcı",
                SubAuthorityId = y3.Id,
                IsMenu = true
            };
            var y6 = new Authority()
            {
                ActionName = "Authorities",
                ControllerName = "Management",
                FaIconCode = "fa fa-lock",
                IsDeleted = false,
                IsLink = true,
                CreatedDate = DateTime.Now.Date,
                Name = "Yetkiler",
                SubAuthorityId = y3.Id,
                IsMenu = true
            };
            
            
            context.Authorities.Add(y4);
            context.Authorities.Add(y5);
            context.Authorities.Add(y6);
            context.SaveChanges();
            #endregion
            #region <-- Kullanıcı Yetkileri -->
            //Örnek bir yetkiyi bir kullanıcıya atamak.
            var ky1 = new UserAuthorities()
            {
                LastModifiedUserName = u.UserName,
                AuthorityId = y1.Id,
                CreatedDate = DateTime.Now,
                IsDeleted = false,
                UserId = u.Id
            };
            context.UserAuthoritieses.Add(ky1);
            context.SaveChanges();
            var ky2 = new UserAuthorities()
            {
                LastModifiedUserName = u.UserName,
                AuthorityId = y2.Id,
                CreatedDate = DateTime.Now,
                IsDeleted = false,
                UserId = u.Id
            };
            var ky3 = new UserAuthorities()
            {
                LastModifiedUserName = u.UserName,
                AuthorityId = y3.Id,
                CreatedDate = DateTime.Now,
                IsDeleted = false,
                UserId = u.Id
            };
            var ky4 = new UserAuthorities()
            {
                LastModifiedUserName = u.UserName,
                AuthorityId = y4.Id,
                CreatedDate = DateTime.Now,
                IsDeleted = false,
                UserId = u.Id
            };
            var ky5 = new UserAuthorities()
            {
                LastModifiedUserName = u.UserName,
                AuthorityId = y5.Id,
                CreatedDate = DateTime.Now,
                IsDeleted = false,
                UserId = u.Id
            };
            var ky6 = new UserAuthorities()
            {
                LastModifiedUserName = u.UserName,
                AuthorityId = y6.Id,
                CreatedDate = DateTime.Now,
                IsDeleted = false,
                UserId = u.Id
            };
            
            context.UserAuthoritieses.Add(ky2);
            context.UserAuthoritieses.Add(ky3);
            context.UserAuthoritieses.Add(ky4);
            context.UserAuthoritieses.Add(ky5);
            context.UserAuthoritieses.Add(ky6);
            context.SaveChanges();
            #endregion
            #region <-- Site Ayarları -->

            var s1 = new Settings()
            {
                Id = 1,
                Text = "fatican92@gmail.com",
                Value = "245002245002",
                Description = "Mail adresi ve şifre bilgileri"
            };

            #endregion
        }
    }
}
