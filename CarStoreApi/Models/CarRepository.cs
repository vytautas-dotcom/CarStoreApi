using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarStoreApi.Models
{
    public class CarRepository : ICarRepository
    {
        private readonly SeedData _data;

        public CarRepository(SeedData data)
        {
            _data = data;
        }
        public Car CarById(Guid storeId, Guid carId)
        {
            if (!_data.stores.ContainsKey(storeId) ||
                 _data.stores[storeId].CarList.FirstOrDefault(car => car.Id == carId) == null) return null;

            return _data.stores[storeId].CarList.FirstOrDefault(car => car.Id == carId);
        }

        public IEnumerable<Car> Cars(Guid storeId)
        {
            if (!_data.stores.ContainsKey(storeId)) return null;

            return _data.stores[storeId].CarList;
        }

        public Car AddCar(Guid storeId, Car car)
        {
            if (!_data.stores.ContainsKey(storeId)) return null;

            Guid id = Guid.NewGuid();
            _data.stores[storeId].CarList.Add(new Car 
            {
                Id = id,
                Name = car.Name,
                DateRelease = car.DateRelease,
                Price = car.Price,
                Remark = car.Remark,
                IsInStore = car.IsInStore
            });
            return _data.stores[storeId].CarList.FirstOrDefault(car => car.Id == id);
        }

        public bool DeleteCar(Guid storeId, Guid carId)
        {
            Car carFromList = _data.stores[storeId].CarList.FirstOrDefault(car => car.Id == carId);

            if (!_data.stores.ContainsKey(storeId) || carFromList == null) return false;

            _data.stores[storeId].CarList.Remove(carFromList);
            return true;
        }

        public Car UpdateCar(Guid storeId, Car carB)
        {
            Car carFromList = _data.stores[storeId].CarList.FirstOrDefault(car => car.Id == carB.Id);

            if (!_data.stores.ContainsKey(storeId) || carFromList == null) return null;

            _data.stores[storeId].CarList.Remove(carFromList);
            _data.stores[storeId].CarList.Add(new Car
            {
                Id = carB.Id,
                Name = carB.Name,
                DateRelease = carB.DateRelease,
                Price = carB.Price,
                Remark = carB.Remark,
                IsInStore = carB.IsInStore
            });
            return _data.stores[storeId].CarList.FirstOrDefault(car => car.Id == carB.Id);
        }
    }
}
