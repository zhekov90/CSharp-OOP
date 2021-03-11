using OnlineShop.Common.Constants;
using OnlineShop.Models.Products.Components;
using OnlineShop.Models.Products.Peripherals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnlineShop.Models.Products.Computers
{
    public abstract class Computer : Product, IComputer
    {
        private List<IComponent> components;
        private List<IPeripheral> peripherals;
        protected Computer(int id, string manufacturer, string model, decimal price, double overallPerformance) : base(id, manufacturer, model, price, overallPerformance)
        {
            this.components = new List<IComponent>();
            this.peripherals = new List<IPeripheral>();
        }

        public IReadOnlyCollection<IComponent> Components => this.components.AsReadOnly();

        public IReadOnlyCollection<IPeripheral> Peripherals => this.peripherals.AsReadOnly();

        public override double OverallPerformance
        {
            get
            {
                if (!components.Any())
                {
                    return base.OverallPerformance;
                }
                else
                {
                    return base.OverallPerformance + components.Average(c => c.OverallPerformance);
                }
                
            }
        }

        public override decimal Price
        {
            get
            {
                return base.Price + components.Sum(c=>c.Price) + peripherals.Sum(p=>p.Price);
            }
        }

        public void AddComponent(IComponent component)
        {
            if (components.Contains(component))
            {
                throw new ArgumentException(string.Format(ExceptionMessages.ExistingComponent, component.GetType().Name, this.GetType().Name, this.Id));
            }
            components.Add(component);
        }

        public void AddPeripheral(IPeripheral peripheral)
        {
            if (peripherals.Contains(peripheral))
            {
                throw new ArgumentException(string.Format(ExceptionMessages.ExistingPeripheral, peripheral.GetType().Name, this.GetType().Name, this.Id));
            }
            peripherals.Add(peripheral);
        }

        public IComponent RemoveComponent(string componentType)
        {
            if (!components.Any(c=>c.GetType().Name==componentType) || components.Count==0)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.NotExistingComponent, componentType, this.GetType().Name, this.Id));
            }
            IComponent componentToRemove = components.First(c => c.GetType().Name == componentType);
            components.Remove(componentToRemove);
            return componentToRemove;
        }

        public IPeripheral RemovePeripheral(string peripheralType)
        {
            if (!peripherals.Any(p => p.GetType().Name == peripheralType) || peripherals.Count == 0)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.NotExistingPeripheral, peripheralType, this.GetType().Name, this.Id));
            }
            IPeripheral peripheralToRemove = peripherals.First(p => p.GetType().Name == peripheralType);
            peripherals.Remove(peripheralToRemove);
            return peripheralToRemove;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(base.ToString());
            sb.AppendLine($" Components ({components.Count}):");
            foreach (var item in components)
            {
                sb.AppendLine($"  {item}");
            }
            sb.AppendLine($" Peripherals ({peripherals.Count}); Average Overall Performance ({peripherals.Average(p=>p.OverallPerformance)}):");
            foreach (var item in peripherals)
            {
                sb.AppendLine($"  {item}");
            }
            return sb.ToString().TrimEnd();
        }
    }
}
