using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregation;
using Talabat.Core.Repositry;

namespace Talabat.Core
{
    public interface IUnitOfWork:IAsyncDisposable
    {
      IGenericRepositry<Tentity> Repositry<Tentity>() where Tentity :BaseEntity;    
        Task<int> Complete();


    }
}
