using System;
using System.Collections.Generic;
using System.Linq;
using OnlineShop.Common;
using OnlineShop.Common.Constants;
using OnlineShop.Models.Products.Components;
using OnlineShop.Models.Products.Computers;
using OnlineShop.Models.Products.Peripherals;

namespace OnlineShop.Core
{
    public class Controller : IController
    {
        private readonly List<Computer> _computers = new List<Computer>();
        private readonly List<Peripheral> _peripherals = new List<Peripheral>();
        private readonly List<Component> _components = new List<Component>();


        public string AddComputer(string computerType, int id, string manufacturer, string model, decimal price)
        {
            if (GetComputerById(id) != null)
            {
                throw new ArgumentException(ExceptionMessages.ExistingComputerId);
            }

            Computer computer = computerType switch
            {
                "Laptop" => new Laptop(id, manufacturer, model, price),
                "DesktopComputer" => new DesktopComputer(id, manufacturer, model, price),
                _ => throw new ArgumentException(ExceptionMessages.InvalidComputerType)
            };

            this._computers.Add(computer);

            return string.Format(SuccessMessages.AddedComputer, id);
        }

        public string AddPeripheral(int computerId, int id, string peripheralType, string manufacturer, string model, decimal price,
            double overallPerformance, string connectionType)
        {
            Validation.ComputerExists(computerId, _computers);

            if (this._peripherals.Any(p => p.Id == id))
            {
                throw new ArgumentException(ExceptionMessages.ExistingPeripheralId);
            }

            Peripheral peripheral = peripheralType switch
            {
                "Headset" => new Headset(id, manufacturer, model, price, overallPerformance, connectionType),
                "Keyboard" => new Keyboard(id, manufacturer, model, price, overallPerformance, connectionType),
                "Monitor" => new Monitor(id, manufacturer, model, price, overallPerformance, connectionType),
                "Mouse" => new Mouse(id, manufacturer, model, price, overallPerformance, connectionType),
                _ => throw new ArgumentException(ExceptionMessages.InvalidPeripheralType)
            };

            var comp = GetComputerById(computerId);

            this._peripherals.Add(peripheral);
            comp.AddPeripheral(peripheral);

            return string.Format(SuccessMessages.AddedPeripheral, peripheralType, id, computerId);
        }

        public string RemovePeripheral(string peripheralType, int computerId)
        {
            var comp = GetComputerById(computerId);

            if (comp == null)
            {
                throw new ArgumentException(ExceptionMessages.NotExistingComputerId);
            }

            var peripheral = comp.RemovePeripheral(peripheralType);

            return string.Format(SuccessMessages.RemovedPeripheral, peripheralType, peripheral.Id);
        }

        public string AddComponent(int computerId, int id, string componentType, string manufacturer, string model, decimal price,
            double overallPerformance, int generation)
        {
            Validation.ComputerExists(computerId, this._computers);

            if (this._components.Any(c => c.Id == id))
            {
                throw new ArgumentException(ExceptionMessages.ExistingComponentId);
            }

            Component component = componentType switch
            {
                "CentralProcessingUnit" => new CentralProcessingUnit(id, manufacturer, model, price, overallPerformance,
                    generation),
                "Motherboard" => new Motherboard(id, manufacturer, model, price, overallPerformance, generation),
                "PowerSupply" => new PowerSupply(id, manufacturer, model, price, overallPerformance, generation),
                "SolidStateDrive" => new SolidStateDrive(id, manufacturer, model, price, overallPerformance,
                    generation),
                "VideoCard" => new VideoCard(id, manufacturer, model, price, overallPerformance, generation),
                "RandomAccessMemory" => new RandomAccessMemory(id, manufacturer, model, price, overallPerformance,
                    generation),
                _ => throw new ArgumentException(ExceptionMessages.InvalidComponentType)
            };

            var comp = GetComputerById(computerId);
            comp.AddComponent(component);
            this._components.Add(component);

            return string.Format(SuccessMessages.AddedComponent, componentType, id, computerId);
        }

        public string RemoveComponent(string componentType, int computerId)
        {
            var comp = GetComputerById(computerId);

            if (comp == null)
            {
                throw new ArgumentException(ExceptionMessages.NotExistingComputerId);
            }

            var component = comp.RemoveComponent(componentType);
            return string.Format(SuccessMessages.RemovedComponent, componentType, component.Id);
        }

        public string BuyComputer(int id)
        {
            var comp = GetComputerById(id);

            if (comp == null)
            {
                throw new ArgumentException(ExceptionMessages.NotExistingComputerId);
            }

            this._computers.Remove(comp);

            return comp.ToString();
        }

        public string BuyBest(decimal budget)
        {
            var bestPc = this._computers
                .Where(c => c.Price <= budget)
                .OrderByDescending(c => c.OverallPerformance)
                .FirstOrDefault();

            if (bestPc == null)
            {
                var msg = string.Format(ExceptionMessages.CanNotBuyComputer, budget);

                throw new ArgumentException(msg);
            }

            this._computers.Remove(bestPc);

            return bestPc.ToString();
        }

        public string GetComputerData(int id)
        {
           var pc = this._computers.FirstOrDefault(c => c.Id == id);

           if (pc == null)
           {
               throw new ArgumentException(ExceptionMessages.NotExistingComputerId);
           }

           return pc.ToString();
        }

        private Computer GetComputerById(int id)
        {
            return this._computers.FirstOrDefault(c => c.Id == id);
        }
    }
}
