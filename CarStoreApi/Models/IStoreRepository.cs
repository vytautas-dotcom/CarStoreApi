using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarStoreApi.Models
{
    public interface IStoreRepository
    {
        IEnumerable<Store> Stores { get; }
        Store this[Guid id] { get; }
        Store AddStore(Store store, bool modified = false);
        Store UpdateStore(Store store);
        void DeleteStore(Guid id);
    }
}
