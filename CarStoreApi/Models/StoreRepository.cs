using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarStoreApi.Models
{
    public class StoreRepository : IStoreRepository
    {
        private readonly SeedData _data;
        private readonly ICarRepository _carRepository;
        public StoreRepository(SeedData data, ICarRepository carRepository)
        {
            _data = data;
            _carRepository = carRepository;
        }
        public Store this[Guid id] => _data.stores.ContainsKey(id) ? _data.stores[id] : null;

        public IEnumerable<Store> Stores => _data.stores.Values;

        public Store AddStore(Store store, bool modified = false)
        {
            List<Car> emptyList = new List<Car>();
            List<Car> carList = store.CarList;
            if (store.Id == Guid.Empty)
            {
                Guid key = Guid.NewGuid();
                store.Id = key;
                store.CarList = emptyList;
            }
            _data.stores[store.Id] = store;

            if (!modified)
            {
                if (carList.Count != 0)
                {
                    foreach (var car in carList)
                    {
                        _carRepository.AddCar(store.Id, car);
                    }
                }
            }
            
            return store;
        }

        public void DeleteStore(Guid id) => _data.stores.Remove(id);

        public Store UpdateStore(Store store)
        {
            List<Car> carListFromData = new List<Car>();
            carListFromData = _data.stores[store.Id].CarList;
            if (store.CarList.Count == 0)
            {
                Store updatedStore0 = new Store
                {
                    Id = store.Id,
                    Description = store.Description,
                    City = store.City,
                    CarList = carListFromData
                };
                return AddStore(updatedStore0, true);
            }
            else
            {
                foreach (var car in store.CarList)
                {
                    _carRepository.AddCar(store.Id, car);
                }
                List<Car> newCarListFromData = this[store.Id].CarList;
                Store updatedStore = new Store
                {
                    Id = store.Id,
                    Description = store.Description,
                    City = store.City,
                    CarList = newCarListFromData
                };
                return AddStore(updatedStore, true);
            }
        }
    }
}
