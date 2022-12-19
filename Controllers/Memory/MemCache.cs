using LW1.Controllers.Memory.Interfaces;
using LW1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using LW1.Extensions;

namespace LW1.Controllers.Memory
{
    public class MemCache : IStorage<Car>
    {
        private readonly object _sync = new object();
        private readonly List<Car> _memCache = new List<Car>();
        public List<Car> All => _memCache.ToList();

        public Car this[Guid id]
        {
            get
            {
                lock (_sync)
                {
                    if (!Has(id))
                    {
                        throw new IncorrectCarException($"No Car with id {id}");
                    }

                    return _memCache.Single(x => x.Id == id);
                }
            }
            set
            {
                if (id == Guid.Empty)
                {
                    throw new IncorrectCarException("Cannot request Car with an empty id");
                }

                lock (_sync)
                {
                    if (Has(id))
                    {
                        RemoveAt(id);
                    }

                    value.Id = id;
                    _memCache.Add(value);
                }
            }
        }

        public void Add(Car value)
        {
            if (value.Id != Guid.Empty)
            {
                throw new IncorrectCarException($"Cannot add value with predefined id {value.Id}");
            }

            value.Id = Guid.NewGuid();
            this[value.Id] = value;
        }

        public void RemoveAt(Guid id)
        {
            lock (_sync)
            {
                _memCache.RemoveAll(x => x.Id == id);
            }
        }

        public bool Has(Guid id)
        {
            return _memCache.Any(x => x.Id == id);
        }
    }
}
