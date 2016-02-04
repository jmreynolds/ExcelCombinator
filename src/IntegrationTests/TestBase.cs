using System.Collections.Generic;
using Core.Models;
using Infrastructure.Modules;
using Ninject;

namespace IntegrationTests
{
    public class TestBase
    {
        public List<CashBondForfitureOutput> SampleOutputList { get; set; } = new List<CashBondForfitureOutput>();
        protected StandardKernel Kernel { get; private set; }
        protected void Bootstrap()
        {
            Kernel = new StandardKernel(new NetOfficeModule());
        }

        protected void GenerateSampleOutputList()
        {
            var item1 = new CashBondForfitureOutput()
            {
                Name = "One",
                Address = "111",
                AddressLine2 = "222",
                DispositionDate = "1/1/1900",
                DateOfBirth = "1/1/1980",
                Citations = new List<Citation>
                {
                    new Citation {CitationNumber = "1", Offense = "Offense 1"},
                    new Citation {CitationNumber = "2", Offense = "Offense 2"},
                    new Citation {CitationNumber = "3", Offense = "Offense 3"},
                }
            };
            var item2 = new CashBondForfitureOutput()
            {
                Name = "Two",
                Address = "222",
                AddressLine2 = "333",
                DispositionDate = "2/2/1900",
                DateOfBirth = "2/2/1980",
                Citations = new List<Citation>
                {
                    new Citation {CitationNumber = "1", Offense = "Offense 1"},
                }
            };
            var item3 = new CashBondForfitureOutput()
            {
                Name = "Three",
                Address = "333",
                AddressLine2 = "444",
                DispositionDate = "3/3/1900",
                DateOfBirth = "3/3/1980",
                Citations = new List<Citation>
                {
                    new Citation {CitationNumber = "1", Offense = "Offense 1"},
                    new Citation {CitationNumber = "2", Offense = "Offense 2"},
                    new Citation {CitationNumber = "3", Offense = "Offense 3"},
                    new Citation {CitationNumber = "4", Offense = "Offense 4"},
                    new Citation {CitationNumber = "5", Offense = "Offense 5"},
                }
            };
            SampleOutputList.Add(item1);
            SampleOutputList.Add(item2);
            SampleOutputList.Add(item3);
        }
    }
}