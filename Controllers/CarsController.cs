using System;
using System.Collections.Generic;
using System.Linq;
using LW1.Models;
using Microsoft.AspNetCore.Mvc;

namespace LW1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private static readonly IList<Car> MemCache = new List<Car>();

        // GET api/<CarsController>
        [HttpGet]
        public ActionResult<IEnumerable<Car>> Get()
        {
            return MemCache.ToList();
        }

        // GET api/<CarsController>/{id}
        [HttpGet("{id}")]
        public ActionResult<Car> Get(int id)
        {
            if (id < 0 || id >= MemCache.Count)
            {
                throw new IndexOutOfRangeException("Index out of range");
            }

            return MemCache[id];
        }

        // POST api/<CarsController>
        [HttpPost]
        public void Post([FromBody] Car value)
        {
            MemCache.Add(value);
        }

        // PUT api/<CarsController>/{id}
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Car value)
        {
            if (id < 0 || id >= MemCache.Count)
            {
                throw new IndexOutOfRangeException("Index out of range");
            }

            MemCache[id] = value;
        }

        // DELETE api/<CarsController>/{id}
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            if (id < 0 || id >= MemCache.Count)
            {
                throw new IndexOutOfRangeException("Index out of range");
            }

            MemCache.RemoveAt(id);
        }
    }
}
