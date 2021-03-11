using System;
using System.Collections.Generic;
using System.Text;

namespace FoodShortage.Interfaces
{
   public  interface IBuyer:INameable
    {
        void BuyFood();
        int Food { get; }
    }
}
