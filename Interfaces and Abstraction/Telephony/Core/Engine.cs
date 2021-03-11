using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Telephony.Contracts;
using Telephony.Exceptions;
using Telephony.Models;

namespace Telephony.Core
{
    class Engine : IEngine
    {

        private readonly IReader reader;
        private readonly IWriter writer;
        private StationaryPhone stationaryPhone;
        private Smartphone smartphone;

        private Engine()
        {
            this.stationaryPhone = new StationaryPhone();
            this.smartphone = new Smartphone();
        }
        public Engine(IReader reader, IWriter writer)
            :this()
        {
            this.reader = reader;
            this.writer = writer;
        }
        public void Run()
        {
            string[] phoneNumbers = reader.ReadLine()
                .Split(" ")
                .ToArray();
            string[] sites = reader.ReadLine()
                .Split(" ")
                .ToArray();

            CallNumbers(phoneNumbers);

            BrowseSites(sites);
        }

        private void BrowseSites(string[] sites)
        {
            foreach (var url in sites)
            {
                try
                {
                    writer.WriteLine(smartphone.Browse(url));
                }
                catch (InvalidUrlException iue)
                {
                    writer.WriteLine(iue.Message);
                }
            }
        }

        private void CallNumbers(string[] phoneNumbers)
        {
            foreach (var number in phoneNumbers)
            {
                try
                {
                    if (number.Length == 7)
                    {
                        writer.WriteLine(stationaryPhone.Call(number));
                    }
                    else if (number.Length == 10)
                    {
                        writer.WriteLine(smartphone.Call(number));
                    }
                    else
                    {
                        throw new InvalidNumberException();
                    }
                }
                catch (InvalidNumberException ine)
                {
                    writer.WriteLine(ine.Message);
                }
            }
        }
    }
}
