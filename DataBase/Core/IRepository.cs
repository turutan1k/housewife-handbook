using Server.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataBase.Services
{
    public interface IRepository<T> where T :class
    {
        void Delete(T TToDelete);
        void Delete(object id);
        IEnumerable<T> Get(
            Expression<Func<T, bool>> filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        string includeProperties = "");
        T GetByID(object id);
        void Insert(T entity);
        void Update(T entityToUpdate);
        IEnumerable<CourseModel> OrderBy(Func<object, object> p);
    }
}
