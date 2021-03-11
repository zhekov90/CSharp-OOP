using System;
using System.Collections.Generic;
using System.Text;

namespace Birthday
{
    public class Robot : INameable,IIdentifiable
    {
        public Robot(string model, string id)
        {
            this.Name = model;
            this.Id = id;
        }

        public string Id { get; private set; }

        public string Name { get; private set; }
    }
}