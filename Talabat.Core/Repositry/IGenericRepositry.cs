using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specification;

namespace Talabat.Core.Repositry
{
    public interface IGenericRepositry<T> where T :BaseEntity
    {
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T> GetByIDAsync(int id);  

        Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> spec);   //Function which work as Specification pattern
        Task<T> GetByIDlWithSpecAsync(ISpecification<T> spec);
        Task<int> GetCountWithSpecAsync(ISpecification<T> spec);    
        Task Add(T entity);
        void Update(T entity);
        void Delete(T entity);


    }
}
