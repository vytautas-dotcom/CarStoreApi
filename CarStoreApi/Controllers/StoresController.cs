using CarStoreApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarStoreApi.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class StoresController : ControllerBase
    {
        private readonly IStoreRepository _storeRepository;

        public StoresController(IStoreRepository storeRepository)
        {
            _storeRepository = storeRepository;
        }
        //GET /stores
        public IEnumerable<Store> GetAllStores() => _storeRepository.Stores;

        //GET /stores/{id}
        [HttpGet("{id}")]
        public ActionResult<Store> GetStoreById(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("The id is incorrect.");
            }

            return Ok(_storeRepository[id]);
        }

        //POST /stores
        [HttpPost]
        public IActionResult AddNewStore([FromBody] Store store)
        {
            return Ok(_storeRepository.AddStore(new Store
            {
                City = store.City,
                Description = store.Description,
                CarList = store.CarList
            }));
        }

        //PUT /stores
        [HttpPut]
        public Store UpdateStore([FromBody] Store store) => _storeRepository.UpdateStore(store);

        //PATCH /stores/{id}
        [HttpPatch("{id}")]
        public StatusCodeResult UpdateStorePatch([FromBody] JsonPatchDocument<Store> store, Guid id)
        {
            var storeToUpdate = (Store)((OkObjectResult)GetStoreById(id).Result).Value;

            if (storeToUpdate == null) return NotFound();

            store.ApplyTo(storeToUpdate);
            return Ok();
        }

        //DELETE /stores/{id}
        [HttpDelete("{id}")]
        public void DeleteStore(Guid id) => _storeRepository.DeleteStore(id);
    }
}
