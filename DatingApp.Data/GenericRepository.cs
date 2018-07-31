using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace DatingApp.Data
{
    
    public  class GenericRepo<T> : IGenericRepo<T> where T : class 

    {
        #region Properties
        private readonly DbContext _context;
        private readonly DbSet<T> _dbSet;
        #endregion
        #region ctors
        public GenericRepo(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        #endregion
        #region Interface method
        public IEnumerable<T> Get(Func<T, bool> func = null)
        {
            if (func != null)
            {
                return _context.Set<T>().Where(func).ToList();
            }

            return _dbSet.ToList();
        }
        public virtual int Add(T obj)
        {
            _dbSet.Add(obj);
            return Save();
        }
        public virtual int Remove(T obj)
        {
            _dbSet.Remove(obj);
            return Save();
        }
        public virtual int Update()
        {
            _context.Entry(typeof(T)).State = EntityState.Modified;
            return Save();
        }

        private  int Save()
        {
            return _context.SaveChanges();
        }

        #endregion
    }
}
