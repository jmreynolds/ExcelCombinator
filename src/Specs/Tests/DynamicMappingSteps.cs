using System.Collections.Generic;
using System.Linq;
using Core.Models;
using Ploeh.AutoFixture;
using Should;
using TechTalk.SpecFlow;

namespace Specs.Tests
{
    [Binding]
    public class DynamicMappingSteps : TestBase
    {
        private Dictionary<int, IEnumerable<RowItem>> _sampleInputDictionary;
        private IEnumerable<CashBondForfitureInput> _inputResult;
        private IEnumerable<CashBondForfitureInput> _inputList;
        private IEnumerable<CashBondForfitureOutput> _outputResult;
        private string[] _inclusionFields;
        private string[] _dedupeFields;
        private string[] _aggregateFields;
        private string[] _inputColumns;


        [Given]
        public void Given_A_sample_generic_input_worksheet_with_P0_rows(int p0) => 
            _sampleInputDictionary = GetSampleInputDictionary();

        [Given]
        public void Given_a_sample_input_worksheet_with_the_municipal_format_with_P0_rows(int p0) => 
            _sampleInputDictionary = GetStandardMunicipalInputsDictionary();

        [When]
        public void When_I_map_them_to_a_POCO_list() => 
            _inputResult = Processor.MapToCashBondForfitureInput(_sampleInputDictionary);

        [Then]
        public void Then_the_input_result_should_have_P0_rows(int p0) => 
            _inputResult.Count().ShouldEqual(3);


        [Given]
        public void Given_a_sample_list_mapped_from_the_municipal_format() => 
            _inputList = new List<CashBondForfitureInput>();

        [Given]
        public void Given_the_input_columns_are(Table table)
        {
            _inputColumns = new string[table.RowCount];
            for (int i = 0; i < table.RowCount; i++)
            {
                _inputColumns[i] = table.Rows[i]["Column"];
            }

        }

        [Given]
        public void Given_the_following_fields_are_marked_for_inclusion(Table table)
        {
            _inclusionFields = new string[table.RowCount];
            for (int i = 0; i < table.RowCount; i++)
            {
                _inclusionFields[i] = table.Rows[i]["Column"];
            }

        }

        [Given]
        public void Given_the_following_fields_are_marked_as_de_dupe_fields(Table table)
        {
            _dedupeFields = new string[table.RowCount];
            for (int i = 0; i < table.RowCount; i++)
            {
                _dedupeFields[i] = table.Rows[i]["Column"];
            }
        }

        [Given]
        public void Given_the_following_fields_are_marked_as_aggregate_fields(Table table)
        {
            _aggregateFields = new string[table.RowCount];
            for (int i = 0; i < table.RowCount; i++)
            {
                _aggregateFields[i] = table.Rows[i]["Column"];
            }
        }


        [Given]
        public void Given_the_list_has_P0_rows_with_P1_duplicate_records(int rowcount, int duplicateCount)
        {
            _inputList = CreateInputList(_inputColumns, 
                _inclusionFields, 
                _dedupeFields, 
                _aggregateFields, 
                rowcount, 
                duplicateCount);
        }

        [When]
        public void When_I_process_the_list_for_output() => 
            _outputResult = Processor.MapToCashBondForfitureOutput(_inputList);

        [Then]
        public void Then_the_result_should_be_an_output_in_the_municipal_format()
        {
            _outputResult.ToList().ShouldBeType(typeof(List<CashBondForfitureOutput>));
        }

        [Then]
        public void Then_the_output_result_should_have_P0_rows(int p0)
        {
            _outputResult.Count().ShouldEqual(p0);
        }


        [Then]
        public void Then_the_column_names_should_be(Table table)
        {
            var outputColumns = _outputResult.SelectMany(bonds =>
                bonds.DynamicItems, (output, item) => item.ColumnName).Distinct().ToList();
                //bonds.DynamicItems.Select(item => item.ColumnName)).ToList();
            var expectedColumns = table.Rows.Select(row => row["Column"]).ToList();
            outputColumns.Count.ShouldEqual(table.RowCount);
            outputColumns.ForEach(column=>expectedColumns.ShouldContain(column));
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
                .With(x => x.Value, Fixture.Create<string>())
                .Create());
        }

    }
}
