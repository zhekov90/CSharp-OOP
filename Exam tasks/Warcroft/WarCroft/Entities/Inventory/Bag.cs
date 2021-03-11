using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WarCroft.Constants;
using WarCroft.Entities.Items;

namespace WarCroft.Entities.Inventory
{
    public abstract class Bag : IBag
    {
        private readonly List<Item> items;
        private int capacity;

        protected Bag(int capacity)
        {
            this.Capacity = capacity;
        }
        public int Capacity { get; set; }
        public int Load => this.Items.Sum(x => x.Weight);

        public IReadOnlyCollection<Item> Items => this.items.AsReadOnly();

        public void AddItem(Item item)
        {
            if (this.Load+item.Weight>this.capacity)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.ExceedMaximumBagCapacity));
            }
            this.AddItem(item);
        }

        public Item GetItem(string name)
        {
            if (this.items.Count==0)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.EmptyBag));
            }
           if (!this.Items.Any(x=>x.GetType().Name==name))
            {
                throw new ArgumentException(string.Format(ExceptionMessages.ItemNotFoundInBag, name));
            }
            Item item = this.Items.First(x => x.GetType().Name == name);
            this.items.Remove(item);
            return item;
        }
    }
}
