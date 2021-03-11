using EasterRaces.Models.Cars.Contracts;
using EasterRaces.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasterRaces.Repositories.Entities
{
    public class CarRepository : IRepository<ICar>
    {
        private List<ICar> cars;

        public CarRepository()
        {
            cars = new List<ICar>();
        }

        public List<ICar> Cars => cars;
        public void Add(ICar model)
        {
            this.cars.Add(model);
        }

        public IReadOnlyCollection<ICar> GetAll()
        {
            return this.cars;
        }

        public ICar GetByName(string name)
        {
            return this.cars.FirstOrDefault(x => x.Model == name);
        }

        public bool Remove(ICar model)
        {
            return this.cars.Remove(model);
        }
    }
}
