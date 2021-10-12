using CarStoreApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarStoreApi.Controllers
{
    [Route("stores/{storeId}/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly ICarRepository _carRepository;

        public CarsController(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        //GET /stores/{storeId}/cars
        [HttpGet]
        public ActionResult<List<Car>> GetAllCarsByStoreId(Guid storeId)
        {
            if (_carRepository.Cars(storeId) == null)
            {
                return NotFound("The id is incorrect.");
            }
            return Ok(_carRepository.Cars(storeId));
        }

        //GET /stores/{storeId}/cars/{carId}
        [HttpGet("{carId}")]
        public ActionResult<Car> GetCarById(Guid storeId, Guid carId)
        {
            if (_carRepository.CarById(storeId, carId) == null) return NotFound("Incorrect id");

            return Ok(_carRepository.CarById(storeId, carId));
        }

        //POST /stores/{storeId}/cars
        [HttpPost]
        public ActionResult<Car> AddCarToStore([FromBody] Car car, Guid storeId)
        {
            if (_carRepository.AddCar(storeId, car) == null) return BadRequest("Bad Id");

            return Ok(_carRepository.AddCar(storeId, car));
        }

        //PUT /stores/{storeId}/cars
        [HttpPut]
        public ActionResult<Car> UpdateCar([FromBody] Car car, Guid storeId)
        {
            if (_carRepository.UpdateCar(storeId, car) == null) return BadRequest("The id isn't correct.");

            return Ok(_carRepository.UpdateCar(storeId, car));
        }

        //DELETE /stores/{storeId}/cars/{carId}
        [HttpDelete("{carId}")]
        public IActionResult DeleteCar(Guid storeId, Guid carId)
        {
            if (!_carRepository.DeleteCar(storeId, carId)) return BadRequest("the id is not correct");

            _carRepository.DeleteCar(storeId, carId);
            return Ok("The car was successfully deleted.");
        }
    }
}
