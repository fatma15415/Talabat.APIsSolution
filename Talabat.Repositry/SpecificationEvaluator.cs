using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specification;

namespace Talabat.Repositry
{
    public static class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> GetQuery (IQueryable<TEntity> InputQuery,ISpecification<TEntity> Spec)
        {
            var Query = InputQuery;   //Context.product =>> getAll
            if (Spec.Creiteria is not null)     
                Query=Query.Where(Spec.Creiteria);  //P=>P.id=id  
                                                //Context.product.where(P=>P.id=id)  =>> GetbyID

            if(Spec.OrderBy is not null)
                Query=Query.OrderBy(Spec.OrderBy);
            //Context.product.OrderBy(P=>P.Name) 
            if (Spec.OrderByDescdening is not null)
                Query = Query.OrderByDescending(Spec.OrderByDescdening);
            //Context.product.OrderByDescending(P=>P.Price) 

            if (Spec.IsPagenationEnable)
                Query = Query.Skip(Spec.Skip).Take(Spec.Take);
          


            Query = Spec.Includes.Aggregate(Query, (Currentquery, IncludeExpression)
                                                     => Currentquery.Include(IncludeExpression));
            //SortAscByName
            //Context.product.OrderBy(P=>P.Name).Include(P=>P.ProductBrand).Include(P=>P.ProductType)
            //SortAscByPrice
            //Context.product.OrderByDescending(P=>P.Price).Include(P=>P.ProductBrand).Include(P=>P.ProductType)


            return Query;
        }

    }

}
    
      

