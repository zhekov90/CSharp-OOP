using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WarCroft.Constants;
using WarCroft.Entities.Characters.Contracts;
using WarCroft.Entities.Inventory;
using WarCroft.Entities.Items;

namespace WarCroft.Core
{
    public class WarController
    {
        private  List<Character> characters;
        private  List<Priest> healers;
        private  List<Warrior> warriors;
        private  List<Bag> bags;
        private  List<Item> items;
        public WarController()
        {
            this.characters = new List<Character>();
            this.healers = new List<Priest>();
            this.warriors = new List<Warrior>();
            this.bags = new List<Bag>();
            this.items = new List<Item>();
        }

        public string JoinParty(string[] args)
        {
            var charType = args[0];
            Character character = null;
            Warrior warrior = null;
            Priest healer = null;
            if (charType != "Warrior" && charType != "Priest")
            {
                throw new ArgumentException(string.Format(ExceptionMessages.InvalidCharacterType,charType));
            }
            if (args[0] == "Warrior")
            {
                character = new Warrior(args[0]);
                warrior = new Warrior(args[0]);
                this.warriors.Add(warrior);
            }
            else if (args[0] =="Priest")
            {
                character = new Priest(args[0]);
                healer = new Priest(args[0]);
                this.healers.Add(healer);
            }
            this.characters.Add(character);
            return string.Format(SuccessMessages.JoinParty, character.Name);
        }

        public string AddItemToPool(string[] args)
        {
            var itemType = args[0];
            Item item = null;
            if (itemType != "HealthPotion" && itemType != "FirePotion")
            {
                throw new ArgumentException(string.Format(ExceptionMessages.InvalidItem, itemType));
            }
            if (args[0] == "HealthPotion")
            {
                item = new HealthPotion();
            }
            else if (args[0] == "FirePotion")
            {
                item = new FirePotion();
            }
            this.items.Add(item);
            return string.Format(SuccessMessages.AddItemToPool, item.GetType().Name);
        }

        public string PickUpItem(string[] args)
        {
            string name = args[0];

            
            if (!this.characters.Any(x => x.Name == name))
            {
                throw new ArgumentException(string.Format(ExceptionMessages.CharacterNotInParty, name));
            }
            if (this.items.Count == 0)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.ItemPoolEmpty));
            }
            var lastItemIndex = this.items.Count - 1;
            
            
             var  charToPickItem = this.characters.First(x => x.Name == name);
            
            charToPickItem.bag.AddItem(this.items[lastItemIndex]);
            return string.Format(SuccessMessages.PickUpItem, name, this.items[lastItemIndex]);
        }

        public string UseItem(string[] args)
        {
            string name = args[0];
            string item = args[1];
            if (!this.characters.Any(x => x.Name == name))
            {
                throw new ArgumentException(string.Format(ExceptionMessages.CharacterNotInParty, name));
            }
            var charToUseItem = this.characters.FirstOrDefault(x => x.Name == name);
            Item itemToBeUsed = null;
            if (name is nameof(HealthPotion))
            {
                itemToBeUsed = new HealthPotion();
            }
            else if (name is nameof(FirePotion))
            {
                itemToBeUsed = new FirePotion();
            }
            charToUseItem.UseItem(itemToBeUsed);
            return string.Format(SuccessMessages.UsedItem, name, itemToBeUsed.GetType().Name);
        }

        public string GetStats()
        {
            var sortedChars = this.characters.OrderByDescending(x => x.IsAlive).ThenByDescending(h => h.Health).ToList();
            StringBuilder sb = new StringBuilder();
            foreach (var character in sortedChars)
            {
                sb.Append($"{character.Name} - HP: {character.Health}/{character.BaseHealth}, AP: {character.Armor}/{character.BaseArmor}, Status: ");
                if (character.IsAlive)
                {
                    sb.AppendLine("Alive");
                }
                else
                {
                    sb.AppendLine("Dead");
                }
            }
            return sb.ToString().TrimEnd();
        }

        public string Attack(string[] args)
        {
            string attackerName = args[0];
            string receiverName = args[1];
            if (!this.characters.Any(x => x.Name == attackerName))
            {
                throw new ArgumentException(string.Format(ExceptionMessages.CharacterNotInParty, attackerName));
            }
            if (!this.characters.Any(x => x.Name == receiverName))
            {
                throw new ArgumentException(string.Format(ExceptionMessages.CharacterNotInParty, receiverName));
            }
            Warrior attacker = (Warrior)this.characters.FirstOrDefault(x => x.Name == attackerName);
            var receiver = this.characters.FirstOrDefault(x => x.Name == receiverName);
           
                attacker.Attack(receiver);

                StringBuilder sb = new StringBuilder();
                sb.AppendLine($"{attackerName} attacks {receiverName} for {attacker.AbilityPoints} hit points!");
                sb.AppendLine($"{receiverName} has {receiver.Health}/{receiver.BaseHealth} HP and {receiver.Armor}/{receiver.BaseArmor} AP left!");
                if (!receiver.IsAlive)
                {
                    sb.AppendLine($"{receiver.Name} is dead!");
                }
                return sb.ToString().TrimEnd();
            
        }

        public string Heal(string[] args)
        {
            string healerName = args[0];
            string healingReceiverName = args[1];
            if (!this.characters.Any(x => x.Name == healerName))
            {
                throw new ArgumentException(string.Format(ExceptionMessages.CharacterNotInParty, healerName));
            }
            if (!this.characters.Any(x => x.Name == healingReceiverName))
            {
                throw new ArgumentException(string.Format(ExceptionMessages.CharacterNotInParty, healingReceiverName));
            }

            Priest healer = (Priest)this.characters.FirstOrDefault(x => x.Name == healerName);
            var healingReceiver = this.characters.FirstOrDefault(x => x.Name == healingReceiverName);

            healer.Heal(healingReceiver);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{healer.Name} heals {healingReceiver.Name} for {healer.AbilityPoints}! {healingReceiver.Name} has {healingReceiver.Health} health now!");
            return sb.ToString().TrimEnd();
        }
    }
}
