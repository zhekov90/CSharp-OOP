using System;
using System.Collections.Generic;
using System.Text;
using WarCroft.Constants;
using WarCroft.Entities.Inventory;
using WarCroft.Entities.Inventory.Contracts;

namespace WarCroft.Entities.Characters.Contracts
{
    public class Warrior : Character, IAttacker
    {

        public Warrior(string name)
            : base(name, 100, 50, 40, new Satchel())
        {

        }

        public void Attack(Character character)
        {
            
            if (this==character)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.CharacterAttacksSelf));
            }
            if (this.IsAlive && character.IsAlive)
            {
                character.TakeDamage(this.AbilityPoints);
            }
            else
            {
                    throw new ArgumentException(string.Format(ExceptionMessages.AttackFail, this.Name));
                
            }
        }
    }
}
