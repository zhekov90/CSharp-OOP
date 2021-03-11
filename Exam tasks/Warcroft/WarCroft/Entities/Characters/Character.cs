using System;

using WarCroft.Constants;
using WarCroft.Entities.Inventory;
using WarCroft.Entities.Items;

namespace WarCroft.Entities.Characters.Contracts
{
    public abstract class Character 
    {
        private string name;
        private float baseHealth;
        private float health;
        private float baseArmor;
        private float armor;
        private float abilityPoints;
        private Bag _bag;
        protected Character(string name, float health, float armor, float abilityPoints, Bag bag)
        {
            this.Name = name;
            this.Health = health;
            this.Armor = armor;
            this.AbilityPoints = abilityPoints;
            this._bag = bag;
        }
        public string Name
        {
            get { return name; }
           private set {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.CharacterNameInvalid));
                }
                this.name=value;
            }
        }
        

        public float BaseHealth
        {
            get { return baseHealth; }
            set { baseHealth = value; }
        }
        

        public float Health
        {
            get { return health; }
            set { health = value; }
        }
        
        public float BaseArmor
        {
            get { return baseArmor; }
            set { baseArmor = value; }
        }
       

        public float Armor
        {
            get { return armor; }
            set { armor = value; }
        }
        

        public float AbilityPoints
        {
            get { return abilityPoints; }
            set { abilityPoints = value; }
        }

        public Bag bag => this.bag;

        public bool IsAlive { get; set; } = true;

		protected void EnsureAlive()
		{
			if (!this.IsAlive)
			{
				throw new InvalidOperationException(ExceptionMessages.AffectedCharacterDead);
			}
		}

        public void TakeDamage(float hitPoints)
        {
            if (this.IsAlive)
            {
                if (this.armor>=hitPoints)
                {
                    this.armor -= hitPoints;
                }
                else if(this.armor<hitPoints)
                {
                    var dmgLeft = hitPoints - this.armor;
                    this.health -= dmgLeft;

                }
                if (this.health<=0)
                {
                    if (this.health <= 0)
                    {
                        this.IsAlive = false;
                        this.health = 0;
                    }
                }
            }
        }

        public void UseItem(Item item)
        {
            if (this.IsAlive)
            {
                item.AffectCharacter(this);
                
            }

        }

    }
}