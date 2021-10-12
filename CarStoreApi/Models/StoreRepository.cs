using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarStoreApi.Models
{
    public class StoreRepository : IStoreRepository
    {
        private readonly SeedData _data;
        public StoreRepository(SeedData data)
        {
            _data = data;
        }
        public Store this[Guid id] => _data.stores.ContainsKey(id) ? _data.stores[id] : null;

        public IEnumerable<Store> Stores => _data.stores.Values;

        public Store AddStore(Store store)
        {
            if (store.Id == Guid.Empty)
            {
                Guid key = Guid.NewGuid();
                store.Id = key;
                //store.CarList = null;
            }
            _data.stores[store.Id] = store;
            return store;
        }

        public void DeleteStore(Guid id) => _data.stores.Remove(id);

        public Store UpdateStore(Store store) => AddStore(store);
    }
}
