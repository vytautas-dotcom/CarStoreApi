using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarStoreApi.Models
{
    public class SeedData
    {
        public readonly Dictionary<Guid, Store> stores;
        readonly Factory factory = new();
        public SeedData()
        {
            stores = new Dictionary<Guid, Store>();
            foreach (var store in factory.GenerateStoreList())
            {
                AddStore(store);
            }
        }

        public void AddStore(Store store)
        {
            stores[store.Id] = store;
        }
    }
}
