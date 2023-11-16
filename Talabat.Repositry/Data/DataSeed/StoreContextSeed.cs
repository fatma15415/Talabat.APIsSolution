using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregation;

namespace Talabat.Repositry.Data.DataSeed
{
    public static class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext dbcontext)
        {
            if(!dbcontext.productBrands.Any()) 
            {
                var Branddata = File.ReadAllText("../Talabat.Repositry/Data/DataSeed/brands.json");
                var Brands = JsonSerializer.Deserialize<List<ProductBrand>>(Branddata);
                if (Brands?.Count > 0)
                {
                    foreach (var Brand in Brands)
                        await dbcontext.productBrands.AddAsync(Brand);

                    await dbcontext.SaveChangesAsync();
                }
            }

            if (!dbcontext.productTypes.Any())
            {
                var typedata = File.ReadAllText("../Talabat.Repositry/Data/DataSeed/types.json");
                var types = JsonSerializer.Deserialize<List<ProductType>>(typedata);
                if (types?.Count > 0)
                {
                    foreach (var type in types)
                        await dbcontext.productTypes.AddAsync(type);

                    await dbcontext.SaveChangesAsync();
                }
            }
            if (!dbcontext.products.Any())
            {
                var productdata = File.ReadAllText("../Talabat.Repositry/Data/DataSeed/products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(productdata);
                if (products?.Count > 0)
                {
                    foreach (var product in products)
                        await dbcontext.products.AddAsync(product);

                    await dbcontext.SaveChangesAsync();
                }
            }

            if (!dbcontext.deliveryMethods.Any())
            {
                var Deliverydata = File.ReadAllText("../Talabat.Repositry/Data/DataSeed/delivery.json");
                var deliverymethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(Deliverydata);
                if (deliverymethods?.Count > 0)
                {
                    foreach (var method in deliverymethods)
                        await dbcontext.deliveryMethods.AddAsync(method);

                    await dbcontext.SaveChangesAsync();
                }
            }




        }

    }
}
