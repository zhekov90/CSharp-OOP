using FoodShortage.Interfaces;

namespace FoodShortage.Models
{
    public class Pet : INameable, IBirthable
    {
        public Pet(string name, string birthdate)
        {
            this.Name = name;
            this.Birthdate = birthdate;
        }
        public string Birthdate { get; private set; }
        public string Name { get; set; }
    }
}
