using CounterStrike.Models.Guns.Contracts;
using CounterStrike.Models.Players.Contracts;
using CounterStrike.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace CounterStrike.Models.Players
{
    public abstract class Player : Contracts.IPlayer
    {
        private string username;
        private int health;
        private int armor;
        private Guns.Contracts.IGun gun;

        public Player(string username, int health, int armor, Guns.Contracts.IGun gun)
        {
            this.Username = username;
            this.Health = health;
            this.Armor = armor;
            this.Gun = gun;
        }

        public string Username
        {
            get { return this.username; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidPlayerName);
                }
                this.username = value;
            }
        }
        public int Health
        {
            get { return this.health; }
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidPlayerHealth);
                }
                this.health = value;
            }
        }
        public int Armor
        {
            get { return this.armor; }
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidPlayerArmor);
                }
                this.armor = value;
            }
        }

        public Guns.Contracts.IGun Gun
        {
            get { return this.gun; }
            private set
            {
                if (value == null)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidGun);
                }
                this.gun = value;
            }
        }

        public bool IsAlive => this.health > 0;

        public void TakeDamage(int points)
        {
            if (armor - points >= 0)
            {
                armor -= points;
                return;
            }
            else if (armor > 0)
            {
                points -= armor;
                armor = 0;
            }

            Health -= points;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{this.GetType().Name}: {username}");
            sb.AppendLine($"--Health: {health}");
            sb.AppendLine($"--Armor: {armor}");
            sb.AppendLine($"--Gun: {gun.Name}");

            return sb.ToString().TrimEnd();
        }
    }
}
