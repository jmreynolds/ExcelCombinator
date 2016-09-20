using System.Linq;
using Core.EqualityImplementations;
using Core.Models;
using Should;
using TechTalk.SpecFlow;

namespace Specs.Tests
{
    [Binding]
    public class EqualitySteps
    {

        private DynamicOutput _output1;
        private DynamicOutput _output2;
        private readonly DynamicOutputCompare _comparer = new DynamicOutputCompare();
        private bool _compareResult;

        [Given]
        public void Given_I_have_P0_Dynamic_Outputs(int p0)
        {
            _output1 = new DynamicOutput();
            _output2 = new DynamicOutput();
        }
        
        [Given]
        public void Given_the_first_has_the_following_columns_and_values(Table table)
        {
            table.Rows
                .ToList()
                .ForEach(row =>
                         {
                             _output1.DynamicItems
                             .Add(new DynamicItem()
                             {
                                 ColumnName = row["Column"],
                                 Value = row["Value"],
                                 ShouldRemoveDupes = row["ShouldRemoveDupes"] == "true",
                                 ShouldAggregate = row["ShouldAggregate"] == "true",
                                 ShouldIncludeInOutput = row["ShouldIncludeInOutput"]=="true"
                             });
                         }
                );
        }
        
        [Given]
        public void Given_the_second_has_the_following_columns_and_values(Table table)
        {
            table.Rows
                .ToList()
                .ForEach(row =>
                {
                    _output2.DynamicItems
                    .Add(new DynamicItem()
                    {
                        ColumnName = row["Column"],
                        Value = row["Value"],
                        ShouldRemoveDupes = row["ShouldRemoveDupes"] == "true",
                        ShouldAggregate = row["ShouldAggregate"] == "true",
                        ShouldIncludeInOutput = row["ShouldIncludeInOutput"] == "true"
                    });
                }
                );
        }

        [When]
        public void When_I_compare_the_two_items()
        {
            _compareResult = _comparer.Equals(_output1, _output2);
        }
        
        [Then]
        public void Then_result_of_equality_should_be_P0(bool p0)
        {
            _compareResult.ShouldEqual(p0);
        }
    }
}
