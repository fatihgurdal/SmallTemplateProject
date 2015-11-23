using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SmallProject.Context
{
    public class Repository<T> where T : class
    {
        IObjectContextAdapter _context;
        IObjectSet<T> _objectSet;
 
        public Repository()
        {

            _context = new MyContext();
            _objectSet = _context.ObjectContext.CreateObjectSet<T>();
            

        }
        public IQueryable<T> AsQueryable()
        {
            return _objectSet;
        }
        /// <summary>
        ///İlk Veriyi Getirir 
        /// </summary>
        /// <param name="where">istediğiniz verinin sorgusunu linq formatında yazınız</param>
        /// <returns>Geriye Tek nesne gönderir.</returns>
        public T First(Expression<Func<T, bool>> where)
        {

            return _objectSet.First(where);
        }
        /// <summary>
        /// İstenilen sorgudan veriye sahip kaç adet satır bulunmaktadır.
        /// </summary>
        /// <param name="where"></param>
        /// <returns>Var olan satır sayısı</returns>
        public int Count(Expression<Func<T, bool>> where)
        {

            return _objectSet.Count(where);
        }
        /// <summary>
        /// Tabloda arama yapar.
        /// </summary>
        /// <param name="where">istediğiniz verinin sorgusunu linq formatında yazınız</param>
        /// <returns>Gönderdiğiniz tipte IEnumerable lsitesi döndürür</returns>
        public IEnumerable<T> Find(Expression<Func<T, bool>> where)
        {
            return _objectSet.Where(where);
        }
        /// <summary>
        /// Tablodan veri silmek için kullanılır.
        /// </summary>
        /// <param name="entity">Silinecek nesnenin entity halini veriiz</param>
        /// <returns>Etkilenen satır sayısını döndürür</returns>
        public int Delete(T entity)
        {
            _objectSet.DeleteObject(entity);
            return ((MyContext)_context).SaveChanges();
        }
        /// <summary>
        /// Tabloya Veri Ekler
        /// </summary>
        /// <param name="entity">Ekleecek verinin entity halini veriniz.</param>
        /// <returns>Etkilenen satır sayısını döndürür</returns>
        public int Add(T entity)
        {
            _objectSet.AddObject(entity);
            //return _context.ObjectContext.SaveChanges();
            return ((MyContext)_context).SaveChanges();

        }
        public void Attach(T entity)
        {
            _objectSet.Attach(entity);
            _context.ObjectContext.SaveChanges();
        }
        /// <summary>
        /// Tüm verileri listele
        /// </summary>
        /// <returns>Gönderdiğiniz entity tipinde list döndürür.</returns>
        public List<T> List()
        {
            List<T> liste = _objectSet.ToList();
            return liste;
        }
        /// <summary>
        /// Ekleme, silme ve güncelleme işlemlerini database'e işler
        /// </summary>
        /// <returns>Etkilenen satır sayısını geri döndürür</returns>
        public int UpdateSaveChanges()
        {
            return ((MyContext)_context).SaveChanges();
        }
        /// <summary>
        /// Tabloda istenen verinin varlığını kontrol eder.
        /// </summary>
        /// <param name="where">istediğiniz verinin sorgusunu linq formatında yazınız</param>
        /// <returns>True / False geri dönüş yapar</returns>
        public bool ExistData(Expression<Func<T, bool>> where)
        {
            return _objectSet.Any(where);
        }
        /// <summary>
        /// Tüm verileri listeler
        /// </summary>
        /// <typeparam name="F"></typeparam>
        /// <param name="where">Sıralam sorgusu yazınız</param>
        /// <returns>Gönderdiğiniz tipte liste döndürür</returns>
        public List<T> OrderList<F>(Expression<Func<T, F>> where)
        {
            return _objectSet.OrderBy(where).ToList();
        }
        /// <summary>
        /// Custom Listeleme
        /// </summary>
        /// <param name="where">İstenen verilerin linq sorgusu</param>
        /// <returns>Gönderdiğiniz tipte liste döndürür</returns>
        public List<T> QueryList(Expression<Func<T, bool>> where)
        {
            return _objectSet.Where(where).ToList();
        }

    }
}
