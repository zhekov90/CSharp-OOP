namespace _3._Raiding.Factory
{
    using Models;
    using System;

    public class FactoryHeroes
    {
        public static BaseHero CreateHero(string heroType, string heroName)
        {
            BaseHero baseHero;

            if (heroType == "Druid")
            {
                baseHero = new Druid(heroName);
            }
            else if (heroType == "Paladin")
            {
                baseHero = new Paladin(heroName);
            }
            else if (heroType == "Rogue")
            {
                baseHero = new Rogue(heroName);
            }
            else if (heroType == "Warrior")
            {
                baseHero = new Warrior(heroName);
            }
            else
            {
                throw new ArgumentException("Invalid hero!");
            }

            return baseHero;
        }

    }
}
