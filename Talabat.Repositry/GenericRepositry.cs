using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Repositry;
using Talabat.Core.Specification;
using Talabat.Repositry.Data;

namespace Talabat.Repositry
{
    public class GenericRepositry<T> : IGenericRepositry<T> where T :BaseEntity
    {
        private readonly StoreContext _dbContext;

        public GenericRepositry(StoreContext dbcontext)
        {
            _dbContext = dbcontext;
        }

        #region Static queries
        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();


        }
        public async Task<T> GetByIDAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);


        }

        #endregion

        #region Dynamic Queries
        public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> spec)
        {
            return await ApplaySpecification(spec).ToListAsync();
        }



        public async Task<T> GetByIDlWithSpecAsync(ISpecification<T> spec)
        {
            return await ApplaySpecification(spec).FirstOrDefaultAsync();
        }



        #endregion
        public async Task<int> GetCountWithSpecAsync(ISpecification<T> spec)
        {
            return await ApplaySpecification(spec).CountAsync();  
        }

        private IQueryable<T> ApplaySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_dbContext.Set<T>(), spec);
        }

        public async Task Add(T entity)
        => await _dbContext.Set<T>().AddAsync(entity);

        public void Update(T entity)
        => _dbContext.Set<T>().Update(entity);

        public void Delete(T entity)
       => _dbContext.Set<T>().Remove(entity);   
    }
}
