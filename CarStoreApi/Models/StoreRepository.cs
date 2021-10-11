using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarStoreApi.Models
{
    public class StoreRepository : IStoreRepository
    {
        private readonly Dictionary<Guid, Store> stores;
        readonly Factory factory = new();
        public StoreRepository()
        {
            stores = new Dictionary<Guid, Store>();
            foreach (var store in factory.GenerateStoreList())
            {
                AddStore(store);
            }
        }
        public Store this[Guid id] => stores.ContainsKey(id) ? stores[id] : null;

        public IEnumerable<Store> Stores => stores.Values;

        public Store AddStore(Store store)
        {
            if (store.Id == Guid.Empty)
            {
                Guid key = Guid.NewGuid();
                store.Id = key;
                //store.CarList = null;
            }
            stores[store.Id] = store;
            return store;
        }

        public void DeleteStore(Guid id) => stores.Remove(id);

        public Store UpdateStore(Store store) => AddStore(store);
    }
}
