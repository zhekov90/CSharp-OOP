using SantaWorkshop.Models.Presents.Contracts;
using SantaWorkshop.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SantaWorkshop.Repositories
{
    public class PresentRepository : IRepository<IPresent>
    {
        private readonly List<IPresent> models;
        public PresentRepository()
        {
            this.models = new List<IPresent>();
        }
        public IReadOnlyCollection<IPresent> Models => this.models.ToList().AsReadOnly();

        public void Add(IPresent model)
        {
            this.models.Add(model);
        }

        public IPresent FindByName(string name)
        {
            IPresent presentToFind = this.models.First(p => p.Name == name);
            return presentToFind;
        }

        public bool Remove(IPresent model) => this.models.Remove(model);

    }
}
