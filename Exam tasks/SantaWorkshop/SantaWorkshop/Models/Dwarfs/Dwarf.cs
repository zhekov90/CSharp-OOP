﻿using System;
using System.Collections.Generic;
using System.Linq;
using SantaWorkshop.Models.Dwarfs.Contracts;
using SantaWorkshop.Models.Instruments.Contracts;
using SantaWorkshop.Utilities.Messages;

namespace SantaWorkshop.Models.Dwarfs
{
    public abstract class Dwarf : IDwarf
    {
        private const int MinEnergy = 0;

        private string name;
        private int energy;
        private List<IInstrument> instruments;

        protected Dwarf(string name, int energy)
        {
            this.Name = name;
            this.Energy = energy;
            this.instruments = new List<IInstrument>();
        }

        public string Name
        {
            get => this.name;
            protected set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    var msg = ExceptionMessages.InvalidDwarfName;
                    throw new ArgumentException(msg);
                }

                this.name = value;
            }
        }

        public int Energy
        {
            get => this.energy;
            protected set
            {
                if (value < MinEnergy)
                {
                    value = MinEnergy;
                }

                this.energy = value;
            }
        }

        public List<IInstrument> Instruments => this.instruments;

        List<IInstrument> IDwarf.Instruments => (List<IInstrument>)this.Instruments;

        public abstract void Work();
        public void AddInstrument(IInstrument instrument)
        {
            this.Instruments.Add(instrument);
        }
    }
}
