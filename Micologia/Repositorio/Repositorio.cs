using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Data.Entity;

namespace Micologia.Repositorio
{
    public class GenericRepository<TEntity> where TEntity : class
    {
        protected Expression<Func<TEntity, bool>> whereClausula = null;

        /// <summary>
        /// The IObjectSet that represents the current entity.
        /// </summary>
        private DbSet<TEntity> _objectSet;
        protected Contexto contexto;

        protected GenericRepository()
        {
            contexto = new Contexto();
            _objectSet = contexto.Set<TEntity>();            
        }

        /// <summary>
        /// Gets all records as an IQueryable
        /// </summary>
        /// <returns>An IQueryable object containing the results of the query</returns>
        protected IQueryable<TEntity> GetQuery()
        {
            return _objectSet;
        }

        /// <summary>
        /// Gets all records as an IEnumberable
        /// </summary>
        /// <returns>An IEnumberable object containing the results of the query</returns>
        protected IList<TEntity> GetAll()
        {
            return GetQuery().ToList();
        }

        /// <summary>
        /// Finds a record with the specified criteria
        /// </summary>
        /// <param name="predicate">Criteria to match on</param>
        /// <returns>A collection containing the results of the query</returns>
        protected IList<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _objectSet.Where<TEntity>(predicate).ToList();
        }

        /// <summary>
        /// Gets a single record by the specified criteria (usually the unique identifier)
        /// </summary>
        /// <param name="predicate">Criteria to match on</param>
        /// <returns>A single record that matches the specified criteria</returns>
        protected TEntity Single(Expression<Func<TEntity, bool>> predicate)
        {
            return _objectSet.Single<TEntity>(predicate);
        }

        /// <summary>
        /// The first record matching the specified criteria
        /// </summary>
        /// <param name="predicate">Criteria to match on</param>
        /// <returns>A single record containing the first record matching the specified criteria</returns>
        protected TEntity First(Expression<Func<TEntity, bool>> predicate)
        {
            return _objectSet.First<TEntity>(predicate);
        }

        /// <summary>
        /// Deletes the specified entitiy
        /// </summary>
        /// <param name="entity">Entity to delete</param>
        /// <exception cref="ArgumentNullException"> if <paramref name="entity"/> is null</exception>
        protected void Delete(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            _objectSet.Remove(entity);
        }

        /// <summary>
        /// Adds the specified entity
        /// </summary>
        /// <param name="entity">Entity to add</param>
        /// <exception cref="ArgumentNullException"> if <paramref name="entity"/> is null</exception>
        protected void Add(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            _objectSet.Add(entity);
        }

        protected void Update(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            contexto.Entry(entity).State = EntityState.Modified;
        }

        /// <summary>
        /// Attaches the specified entity
        /// </summary>
        /// <param name="entity">Entity to attach</param>
        protected void Attach(TEntity entity)
        {
            _objectSet.Attach(entity);
        }

        /// <summary>
        /// Saves all context changes
        /// </summary>
        protected void SaveChanges()
        {
            contexto.SaveChanges();
        }

        /// <summary>
        /// Releases all resources used by the WarrantManagement.DataExtract.Dal.ReportDataBase
        /// </summary>
        protected void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases all resources used by the WarrantManagement.DataExtract.Dal.ReportDataBase
        /// </summary>
        /// <param name="disposing">A boolean value indicating whether or not to dispose managed resources</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (contexto != null)
                {
                    contexto.Dispose();
                    contexto = null;
                }
            }
        }

        /// <summary>
        /// Retorna o objeto filtrado pela sua PK
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected TEntity SelectByKey(object id)
        {
            return contexto.Set<TEntity>().Find(id);
        }

    }  
}
