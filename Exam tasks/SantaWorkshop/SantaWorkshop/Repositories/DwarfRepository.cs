using SantaWorkshop.Models.Dwarfs.Contracts;
using SantaWorkshop.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SantaWorkshop.Repositories
{
    public class DwarfRepository : IRepository<IDwarf>
    {
        private readonly  List<IDwarf> models;

        public DwarfRepository()
        {
            this.models = new List<IDwarf>();
        }
        public IReadOnlyCollection<IDwarf> Models => this.models.ToList().AsReadOnly();

        public void Add(IDwarf model)
        {
            this.models.Add(model);
        }

        public IDwarf FindByName(string name)
        {
            IDwarf dwarfToFind = this.models.First(d => d.Name == name);
            return dwarfToFind;
        }

        public bool Remove(IDwarf model) => this.models.Remove(model);
        
    }
}
