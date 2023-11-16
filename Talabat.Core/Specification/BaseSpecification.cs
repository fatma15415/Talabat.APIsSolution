using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specification
{
    public class BaseSpecification<T> : ISpecification<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>> Creiteria { get; set; } //Where
        public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>(); // Intialize
        public Expression<Func<T, object>> OrderBy { get; set; }
        public Expression<Func<T, object>> OrderByDescdening { get ; set; }
        public int Skip { get ; set; }
        public int Take { get; set; }
        public bool IsPagenationEnable { get; set ; }

        //List of expression >> that lbdmda expression which inside include
        public BaseSpecification()  //Get all
        {
                    
        }
        public BaseSpecification(Expression<Func<T,bool>> Creiteria) //(P=>P.Id = ID) //for get product by id
        {
            this.Creiteria = Creiteria; 
                
        }
        public void AddOrderBy(Expression<Func<T, object>> OrderBy)
        {
            this.OrderBy = OrderBy; 
        }
        public void AddOrderByDescebding(Expression<Func<T, object>> OrderByDescdening)
        {
            this.OrderByDescdening = OrderByDescdening;
        }
        public void ApplayPagenation(int skip, int take)
        {
            IsPagenationEnable = true;  
            Skip = skip;   
            Take = take;   

        }

    }
}
