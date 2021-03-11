using System;
using System.Collections.Generic;
using System.Text;
using WarCroft.Constants;
using WarCroft.Entities.Inventory;
using WarCroft.Entities.Inventory.Contracts;

namespace WarCroft.Entities.Characters.Contracts
{
    public class Priest : Character, IHealer
    {
        public Priest(string name) : base(name, 50, 25, 40, new Backpack())
        {
        }

        public void Heal(Character character)
        {

            if (this.IsAlive && character.IsAlive)
            {
                character.Health += this.AbilityPoints;
            }
            else
            {
                throw new ArgumentException(string.Format(ExceptionMessages.HealerCannotHeal, this.Name));
            }
        }
    }
}
