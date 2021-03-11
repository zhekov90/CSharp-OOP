using System;
using System.Collections.Generic;
using System.Linq;
using OnlineShop.Common.Constants;
using OnlineShop.Models.Products;
using OnlineShop.Models.Products.Computers;

namespace OnlineShop.Common
{
    public static class Validation
    {

        public static void ComputerExists(int id, ICollection<Computer> collection)
        {
            if (collection.FirstOrDefault(c => c.Id == id) == null)
            {
                throw new ArgumentException(ExceptionMessages.NotExistingComputerId);
            }
        }

    }
}
