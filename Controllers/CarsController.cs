using System;
using System.Collections.Generic;
using System.Linq;
using LW1.Controllers.Memory;
using LW1.Controllers.Memory.Interfaces;
using LW1.Models;
using Microsoft.AspNetCore.Mvc;

namespace LW1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly IStorage<Car> _memCache;

        public CarsController(IStorage<Car> memCache)
        {
            _memCache = memCache;
        }

        // GET api/<CarsController>
        [HttpGet]
        public ActionResult<IEnumerable<Car>> Get()
        {
            return Ok(_memCache.All);
        }

        // GET api/<CarsController>/{id}
        [HttpGet("{id}")]
        public ActionResult<Car> Get(Guid id)
        {
            if (!_memCache.Has(id))
            {
                return NotFound("No such");
            }

            return Ok(_memCache[id]);
        }

        // POST api/<CarsController>
        [HttpPost]
        public IActionResult Post([FromBody] Car value)
        {
            var validationResult = value.Validate();

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            _memCache.Add(value);

            return Ok($"{value.ToString()} has been added");
        }

        // PUT api/<CarsController>/{id}
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] Car value)
        {
            if (!_memCache.Has(id))
            {
                return NotFound("No such");
            }

            var validationResult = value.Validate();

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var previousValue = _memCache[id];
            _memCache[id] = value;

            return Ok($"{previousValue.ToString()} has been updated to {value.ToString()}");
        }

            // DELETE api/<CarsController>/{id}
            [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            if (!_memCache.Has(id))
            {
                return NotFound("No such");
            }

            var valueToRemove = _memCache[id];
            _memCache.RemoveAt(id);

            return Ok($"{valueToRemove.ToString()} has been removed");

        }
    }
}
