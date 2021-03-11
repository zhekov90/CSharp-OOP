using System.Linq;
using SantaWorkshop.Models.Dwarfs.Contracts;
using SantaWorkshop.Models.Presents.Contracts;
using SantaWorkshop.Models.Workshops.Contracts;

namespace SantaWorkshop.Models.Workshops
{
    public class Workshop : IWorkshop
    {
        private const int DwarfMinEnergy = 0;
        public void Craft(IPresent present, IDwarf dwarf)
        {
            while (dwarf.Energy > DwarfMinEnergy && dwarf.Instruments.Any())
            {
                var currentInstrument = dwarf.Instruments.First();

                while (!present.IsDone() && dwarf.Energy > DwarfMinEnergy
                                         && !currentInstrument.IsBroken())
                {
                    dwarf.Work();
                    present.GetCrafted();
                    currentInstrument.Use();
                }

                if (currentInstrument.IsBroken())
                {
                    dwarf.Instruments.Remove(currentInstrument);
                }

                if (present.IsDone())
                {
                    break;
                }
            }
        }
    }
}