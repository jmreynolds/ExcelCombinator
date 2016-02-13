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
                    DispositionDate = "1/1/2015",
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
                    DispositionDate = "1/1/2015",
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
                    DispositionDate = "1/1/2015",
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
                    DispositionDate = "1/1/2015",
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
                    DispositionDate = "1/1/2015",
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
                    DispositionDate = "1/1/2015",
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

        protected Dictionary<int, IEnumerable<RowItem>> GetSampleInputDictionary() => new Dictionary<int, IEnumerable<RowItem>>
        {
            [0] = new List<RowItem>
            {
                new RowItem() {ColumnName = "Column1", Value = "Row1Value1"},
                new RowItem() {ColumnName = "Column2", Value = "Row1Value2"}
            },
            [1] = new List<RowItem>
            {
                new RowItem() {ColumnName = "Column1", Value = "Row2Value1"},
                new RowItem() {ColumnName = "Column2", Value = "Row2Value2"}
            },
            [2] = new List<RowItem>
            {
                new RowItem() {ColumnName = "Column1", Value = "Row3Value1"},
                new RowItem() {ColumnName = "Column2", Value = "Row3Value2"}
            }
        };
    }
}