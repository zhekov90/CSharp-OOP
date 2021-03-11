namespace _3._Raiding.Models
{
    using Contracts;

    public abstract class BaseHero : ICastAbilitable
    {
        protected BaseHero(string name, int power)
        {
            this.Name = name;
            this.Power = power;
        }

        public string Name { get; private set; }

        public int Power { get; private set; }

        public virtual string CastAbility()
        {
            return $"{ this.GetType().Name} - {this.Name} healed for { this.Power}";
        }
    }
}
