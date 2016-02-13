using System.Collections.Generic;
using System.Linq;
using Core;
using Core.Models;
using DataAccess;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Should;
using UnitTests.Extensions;

namespace UnitTests
{
    public class MunicipalMappingTests : TestBase
    {
        [Test, Category("Unit")]
        public void Should_Map_Worksheet_From_Dictionary_Into_Class_List()
        {
            var sampleInputDictionary = GetSampleInputDictionary();
            IProcessor processor = new Processor();
            var result = processor.MapToCashBondForfitureInput(sampleInputDictionary);
            result.Count().ShouldEqual(3);
        }

        [Test, Category("Unit")]
        public void Should_Map_The_Standard_Municipal_Columns_Into_ForfitureInput()
        {
            var sampleInputDictionary = GetStandardMunicipalInputsDictionary();
            IProcessor processor = new Processor();
            var result = processor.MapToCashBondForfitureInput(sampleInputDictionary).ToList();
            result.Count().ShouldEqual(3);
            var input = result.First();
            input.ShouldNotBeNull();
            input.ShouldNotHaveEmptyProperties();
        }

        [Test, Category("Unit")]
        public void Should_Map_From_Input_Class_List_Into_Output_Class_List()
        {
            List<CashBondForfitureInput> input = new List<CashBondForfitureInput>();
            Fixture.AddManyTo(input, 5);
            IProcessor processor = new Processor();
            var result = processor.MapToCashBondForfitureOutput(input);
            result.Count().ShouldEqual(5);
        }

        [Test, Category("Unit")]
        public void Should_Correctly_DeDuplicate_The_File()
        {
            GetTestInputList();
            IProcessor processor = new Processor();
            var result = processor.MapToCashBondForfitureOutput(SampleInputList);
            result.Count().ShouldEqual(3);
            result.First().Citations.Count.ShouldEqual(1);
        }

        private Dictionary<int, IEnumerable<RowItem>> GetStandardMunicipalInputsDictionary()
        {
            return new Dictionary<int, IEnumerable<RowItem>>
            {
                [0] = GetStandardMunicipalInputRowItemsForDictionary(),
                [1] = GetStandardMunicipalInputRowItemsForDictionary(),
                [2] = GetStandardMunicipalInputRowItemsForDictionary()
            };
        }

        private IEnumerable<RowItem> GetStandardMunicipalInputRowItemsForDictionary()
        {
            string[] cols = { "Offense Date", "Citationýnumber", "Name", "Address", "City, St Zip", "Offense", "Juvenile", "Disp Oper", "Bf Status Date" };
            
            return cols.Select(col => Fixture.Build<RowItem>()
                .With(x => x.ColumnName, col)
                .With(x=> x.Value, Fixture.Create<string>())
                .Create());
        }
    }
}