using System.Collections.Generic;
using Core.Models;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;

namespace UnitTests
{
    public class TestBase
    {
        protected CashBondForfitureOutput Cb2;
        protected CashBondForfitureOutput Cb3;
        protected CashBondForfitureOutput Cb1;

        protected List<CashBondForfitureInput> SampleInputList;

        protected Fixture Fixture { get; set; }

        [SetUp]
        public void Setup()
        {
            Fixture = new Fixture();
            Fixture.Customize(new AutoMoqCustomization());
        }

        //protected Dictionary<int, IEnumerable<RowItem>> GetSampleInputDictionary()
        //{
        //    var sampleFileInput =
        //        new Dictionary<int, IEnumerable<RowItem>>
        //        {
        //            [1] =
        //                new[]
        //                {
        //                    "8 / 25 / 2014", "18763910.1", "ALVAREZ, EUNICE", "11 / 19 / 1990", "3316 ROSA",
        //                    "EL PASO, TX 79905", "MVI / NO OR EXP STICKER / AUTO", "BF", "1 / 26 / 2016",
        //                    "1 / 26 / 2016", "2", "TD", "50", "0", "MUNI8080"
        //                },
        //            [2] =
        //                new[]
        //                {
        //                    "8 / 25 / 2014", "18763910.2", "ALVAREZ, EUNICE", "11 / 19 / 1990", "3316 ROSA",
        //                    "EL PASO, TX 79905", "DL / NO OR EXP OPER LIC", "BF", "1 / 26 / 2016", "1 / 26 / 2016", "2",
        //                    "TD", "50", "0", "MUNI80800"
        //                },
        //            [3] =
        //                new[]
        //                {
        //                    "8 / 25 / 2014", "18763910.3", "ALVAREZ, EUNICE", "11 / 19 / 1990", "3316 ROSA",
        //                    "EL PASO, TX 79905", "FTMFR - Fail to maintain fin resp", "BF",  "1 / 26 / 2016",
        //                    "1 / 26 / 2016", "2", "TD", "50", "0", "MUNI8080"
        //                }
        //        };
        //    return sampleFileInput;
        //}

        protected List<CashBondForfitureInput> GetBondForfitureInputsSample()
        {
            var result = new List<CashBondForfitureInput>
            {

                new CashBondForfitureInput
                {
                    OffenseDate = "1/1/2015",
                    CitationNumber = "1",
                    Name = "One",
                    DateOfBirth = "1/1/1981",
                    Address = "111",
                    AddressLine2 = "222",
                    Offense = "Offense 1",
                    Final = "1",
                    DispositionDate = "1/1/2015",
                    LastHearingDate = "1/1/2015",
                    Court = "1",
                    LastHearingCode = "1",
                    Juvenile = "0",
                    DispOper = "1"
                },
                new CashBondForfitureInput
                {
                    OffenseDate = "1/1/2015",
                    CitationNumber = "2",
                    Name = "One",
                    DateOfBirth = "1/1/1981",
                    Address = "111",
                    AddressLine2 = "222",
                    Offense = "Offense 2",
                    Final = "1",
                    DispositionDate = "1/1/2015",
                    LastHearingDate = "1/1/2015",
                    Court = "1",
                    LastHearingCode = "1",
                    Juvenile = "0",
                    DispOper = "1"
                },
                new CashBondForfitureInput
                {
                    OffenseDate = "1/1/2015",
                    CitationNumber = "3",
                    Name = "One",
                    DateOfBirth = "1/1/1981",
                    Address = "111",
                    AddressLine2 = "222",
                    Offense = "Offense 3",
                    Final = "1",
                    DispositionDate = "1/1/2015",
                    LastHearingDate = "1/1/2015",
                    Court = "1",
                    LastHearingCode = "1",
                    Juvenile = "0",
                    DispOper = "1"
                },
                new CashBondForfitureInput
                {
                    OffenseDate = "1/1/2015",
                    CitationNumber = "1",
                    Name = "Two",
                    DateOfBirth = "1/1/1981",
                    Address = "222",
                    AddressLine2 = "333",
                    Offense = "Offense 1",
                    Final = "1",
                    DispositionDate = "1/1/2015",
                    LastHearingDate = "1/1/2015",
                    Court = "",
                    LastHearingCode = "1",
                    Juvenile = "0",
                    DispOper = "1"
                },
                new CashBondForfitureInput
                {
                    OffenseDate = "1/1/2015",
                    CitationNumber = "2",
                    Name = "Two",
                    DateOfBirth = "1/1/1981",
                    Address = "222",
                    AddressLine2 = "333",
                    Offense = "Offense 2",
                    Final = "1",
                    DispositionDate = "1/1/2015",
                    LastHearingDate = "1/1/2015",
                    Court = "1",
                    LastHearingCode = "1",
                    Juvenile = "0",
                    DispOper = "1"
                },
                new CashBondForfitureInput
                {
                    OffenseDate = "1/1/2015",
                    CitationNumber = "1",
                    Name = "Three",
                    DateOfBirth = "1/1/1981",
                    Address = "333",
                    AddressLine2 = "444",
                    Offense = "Offense 1",
                    Final = "1",
                    DispositionDate = "1/1/2015",
                    LastHearingDate = "1/1/2015",
                    Court = "1",
                    LastHearingCode = "1",
                    Juvenile = "0",
                    DispOper = "1"

                }
            };
            return result;
            
        }

        protected void GetEqualityTestCases()
        {
            Cb1 = Fixture.Build<CashBondForfitureOutput>()
                .WithAutoProperties()
                .With(x => x.Name, "One")
                .With(x => x.Address, "111")
                .With(x => x.AddressLine2, "222")
                .With(x => x.DateOfBirth, "1/1/1980")
                .Create();
            Cb2 = Fixture.Build<CashBondForfitureOutput>()
                .WithAutoProperties()
                .With(x => x.Name, "One")
                .With(x => x.Address, "111")
                .With(x => x.AddressLine2, "222")
                .With(x => x.DateOfBirth, "1/1/1980")
                .Create();
            Cb3 = Fixture.Build<CashBondForfitureOutput>()
                .WithAutoProperties()
                .Create();
        }

        protected void GetTestInputList()
        {
            var header = Fixture.Build<CashBondForfitureInput>()
                .WithAutoProperties()
                .With(x => x.Name, "Name")
                .With(x => x.Address, "Address")
                .With(x => x.AddressLine2, "City, St Zip")
                .With(x => x.DateOfBirth, "Date Of Birth")
                .Create();
            var cb1 = Fixture.Build<CashBondForfitureInput>()
                .WithAutoProperties()
                .With(x => x.Name, "One")
                .With(x => x.Address, "111")
                .With(x => x.AddressLine2, "222")
                .With(x => x.DateOfBirth, "1/1/1980")
                .Create();
            var cb2 = Fixture.Build<CashBondForfitureInput>()
                .WithAutoProperties()
                .With(x => x.Name, "One")
                .With(x => x.Address, "111")
                .With(x => x.AddressLine2, "222")
                .With(x => x.DateOfBirth, "1/1/1980")
                .Create();
            var cb3 = Fixture.Build<CashBondForfitureInput>()
                .WithAutoProperties()
                .Create();
            SampleInputList = new List<CashBondForfitureInput> {header, cb1, cb2, cb3};
        }
    }
}