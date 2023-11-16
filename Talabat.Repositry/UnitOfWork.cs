using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregation;
using Talabat.Core.Repositry;
using Talabat.Repositry.Data;

namespace Talabat.Repositry
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext _Context;
        private Hashtable _repositries;
        public UnitOfWork(StoreContext context)
        {
            _Context = context;
            _repositries = new Hashtable();
        }
        public IGenericRepositry<Tentity> Repositry<Tentity>() where Tentity : BaseEntity
        {
            var type= typeof(Tentity).Name;
            if(! _repositries.ContainsKey(type) )
            {
                var repositry= new GenericRepositry<Tentity>(_Context); 
                _repositries.Add(type, repositry);  
            }
            return _repositries[type] as IGenericRepositry<Tentity>;
        }

       
        public async Task<int> Complete()
            => await _Context.SaveChangesAsync();   

        public async ValueTask DisposeAsync()
        => await _Context.DisposeAsync();   

      
    }
}
