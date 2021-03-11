﻿using AquaShop.Models.Decorations.Contracts;
using AquaShop.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace AquaShop.Repositories
{
    public class DecorationRepository : IRepository<IDecoration>
    {
        private readonly List<IDecoration> models;
        public DecorationRepository()
        {
            this.models = new List<IDecoration>();
        }

        public IReadOnlyCollection<IDecoration> Models => this.models;

        public void Add(IDecoration model)
        {
            this.models.Add(model);
        }
        public bool Remove(IDecoration model) => this.models.Remove(model);

        public IDecoration FindByType(string type)
        {
            var model = this.models.FirstOrDefault(m => m.GetType().Name == type);
            return model;
        }

    }

}

