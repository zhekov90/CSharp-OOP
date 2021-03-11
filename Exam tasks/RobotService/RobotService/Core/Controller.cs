using System;
using System.Collections.Generic;
using System.Linq;
using RobotService.Core.Contracts;
using RobotService.Models.Garages;
using RobotService.Models.Garages.Contracts;
using RobotService.Models.Procedures;
using RobotService.Models.Procedures.Contracts;
using RobotService.Models.Robots;
using RobotService.Models.Robots.Contracts;
using RobotService.Utilities.Messages;

namespace RobotService.Core
{
    public class Controller : IController
    {
        private readonly IGarage _garage;
        private readonly ICollection<IProcedure> _allProcedures;

        private readonly Chip _chip;
        private readonly TechCheck _techCheck;
        private readonly Work _work;
        private readonly Rest _rest;
        private readonly Charge _charge;
        private readonly Polish _polish;

        private IRobot _currentRobot;

        private string _message;
        public Controller()
        {
            this._polish = new Polish();
            this._chip = new Chip();
            this._techCheck = new TechCheck();
            this._work = new Work();
            this._charge = new Charge();
            this._rest = new Rest();

            this._allProcedures = new List<IProcedure>();
            this._garage = new Garage();
        }
        public string Manufacture
            (string robotType, string name, int energy, int happiness, int procedureTime)
        {
            this._currentRobot = robotType switch
            {
                nameof(HouseholdRobot) => (IRobot)new HouseholdRobot(name, energy, happiness, procedureTime),
                nameof(PetRobot) => new PetRobot(name, energy, happiness, procedureTime),
                nameof(WalkerRobot) => new WalkerRobot(name, energy, happiness, procedureTime),
                _ => throw new ArgumentException(string.Format(ExceptionMessages.InvalidRobotType, robotType))
            };

            this._garage.Manufacture(this._currentRobot);

            this._message = string.Format(OutputMessages.RobotManufactured, _currentRobot.Name);
            return this._message;
        }

        public string Chip(string robotName, int procedureTime)
        {
            this._currentRobot = this.FindRobot(robotName);

            this._message = string.Format(OutputMessages.ChipProcedure, robotName);

            this.AddProcedureToRecords(this._chip, this._currentRobot);

            this._chip.DoService(this._currentRobot, procedureTime);
            return this._message;
        }

        public string TechCheck(string robotName, int procedureTime)
        {
            this._currentRobot = this.FindRobot(robotName);

            this._message = string.Format(OutputMessages.TechCheckProcedure, robotName);

            this._techCheck.DoService(this._currentRobot, procedureTime);

            this.AddProcedureToRecords(this._techCheck, this._currentRobot);

            return this._message;
        }

        public string Rest(string robotName, int procedureTime)
        {
            this._currentRobot = this.FindRobot(robotName);

            this._message = string.Format(OutputMessages.RestProcedure, robotName);

            this._rest.DoService(this._currentRobot, procedureTime);

            this.AddProcedureToRecords(this._rest, this._currentRobot);

            return this._message;
        }

        public string Work(string robotName, int procedureTime)
        {
            this._currentRobot = this.FindRobot(robotName);

            this._message = string.Format(OutputMessages.WorkProcedure, robotName, procedureTime);

            this._work.DoService(this._currentRobot, procedureTime);

            this.AddProcedureToRecords(this._work, this._currentRobot);

            return this._message;
        }

        public string Charge(string robotName, int procedureTime)
        {
            this._currentRobot = this.FindRobot(robotName);

            this._message = string.Format(OutputMessages.ChargeProcedure, robotName);

            this._charge.DoService(this._currentRobot, procedureTime);

            this.AddProcedureToRecords(this._charge, this._currentRobot);

            return this._message;
        }

        public string Polish(string robotName, int procedureTime)
        {
            this._currentRobot = this.FindRobot(robotName);

            this._message = string.Format(OutputMessages.PolishProcedure, robotName);

            this._polish.DoService(this._currentRobot, procedureTime);

            this.AddProcedureToRecords(this._polish, this._currentRobot);

            return this._message;
        }

        public string Sell(string robotName, string ownerName)
        {
            this._currentRobot = this.FindRobot(robotName);

            this._message = string.Format
                (this._currentRobot.IsChipped ? OutputMessages.SellChippedRobot
                : OutputMessages.SellNotChippedRobot, ownerName);

            this._garage.Sell(robotName, ownerName);

            return this._message;
        }

        public string History(string procedureType)
        {
            var procedure = this._allProcedures.First
                (p => p.GetType().Name == procedureType);

            return procedure.History();
        }

        private IRobot FindRobot(string robotName)
        {
            var robot = this._garage.Robots.FirstOrDefault
                (r => r.Value.Name == robotName).Value;

            if (robot == null)
            {
                var exceptionMessage = string.Format(ExceptionMessages.InexistingRobot, robotName);
                throw new ArgumentException(exceptionMessage);
            }

            return robot;

        }

        private void AddProcedureToRecords(Procedure procedure, IRobot currentRobot)
        {
            this._allProcedures.Add(procedure);

            if (procedure.Robots.All(r => r != currentRobot))
            {
                procedure.Robots.Add(currentRobot);
            }
        }
    }
}