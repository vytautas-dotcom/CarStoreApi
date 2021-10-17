using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarStoreApi.Models
{
    public interface ICarRepository
    {
        IEnumerable<Car> Cars(Guid storeId);
        Car CarById(Guid storeId, Guid carId);
        Guid AddCar(Guid storeId, Car car);
        Car UpdateCar(Guid storeId, Car car);
        bool DeleteCar(Guid storeId, Guid carId);
    }
}
