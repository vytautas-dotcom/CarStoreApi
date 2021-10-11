using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarStoreApi.Models
{
    public class Factory
    {
        List<string> stocks = new List<string>
        {
            "Rome",
            "Berlin",
            "Milan",
            "Moscow",
            "Paris"
        };
        List<string> cars = new List<string>
        {
            "Cadillac",
            "BMW",
            "Hyundai",
            "Reno",
            "Volvo",
            "Lada",
            "Porsche",
            "Infinity",
            "Suzuki",
            "Toyota",
            "Mercedes",
            "Ford",
            "Alfa_Romeo",
            "Nissan"
        };
        public List<Car> GenerateCarList(int totalCars)
        {
            Random rand = new Random();
            List<Car> allCars = new List<Car>();

            for (int i = 0; i < totalCars; i++)
            {
                var year = rand.Next(1998, 2018);
                var price = 1000 + rand.Next(0, (24950 * (year - 1997)));
                foreach (var item in allCars)
                {
                    if (item.DateRelease < year && item.Price > price)
                    {
                        price = 1000 + rand.Next(item.Price, (24950 * (year - 1997)));
                    }
                }
                var index = rand.Next(cars.Count);
                var instock = rand.Next(0, 2);

                var car = new Car
                {
                    Id = Guid.NewGuid(),
                    Name = cars[index],
                    DateRelease = year,
                    Price = price,
                    Remark = year >= 2016 ? "technical inspection available" : "technical inspection not available",
                    IsInStore = instock == 0 ? false : true
                };
                allCars.Add(car);
            }
            return allCars;
        }
        public IEnumerable<Store> GenerateStoreList()
        {
            Random rand = new Random();
            foreach (var city in stocks)
            {
                yield return new Store()
                {
                    Id = Guid.NewGuid(),
                    Description = "Some description",
                    City = city,
                    CarList = GenerateCarList(rand.Next(12, 51))
                };

            }
        }
    }
}
