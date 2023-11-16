using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specification
{
    public class ProductSpecification:BaseSpecification<Product>
    {
        public ProductSpecification(ProductSpecParam productSpec) :                //GetAll
            base(P=>
            (string.IsNullOrEmpty(productSpec.Search) || P.Name.ToLower().Contains(productSpec.Search)) &&
            (!productSpec.BrandId.HasValue || P.ProductBrandId == productSpec.BrandId)  &&
            (!productSpec.TypeId.HasValue || P.ProductTypeId == productSpec.TypeId)
            ) 
            
        {
            Includes.Add(P => P.ProductBrand);

            Includes.Add(P => P.ProductType);
            if(!string.IsNullOrEmpty(productSpec.Sort))
            {
                switch (productSpec.Sort) 
                {
                    case "PriceAsc":
                        AddOrderBy(P=>P.Price);
                        break;
                    case "PriceDesc":
                        AddOrderByDescebding(P =>P.Price);
                        break;
                    default:
                        AddOrderBy(P=>P.Name);
                        break;   


                }
            }
            ApplayPagenation(productSpec.PageSize * (productSpec.PageIndex-1) , productSpec.PageSize);

        }
        public ProductSpecification(int id):base(P=>P.Id == id)//Getbyid
        {
            Includes.Add(P => P.ProductBrand);

            Includes.Add(P => P.ProductType);

        }



    }
}
