using FoodShortage.Interfaces;

namespace FoodShortage.Models
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