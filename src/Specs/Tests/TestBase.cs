using System.Collections.Generic;
using System.Linq;
using Core;
using Core.Models;
using DataAccess;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;
using TechTalk.SpecFlow;

namespace Specs.Tests
{
    public class TestBase
    {
        protected CashBondForfitureOutput Cb2;
        protected CashBondForfitureOutput Cb3;
        protected CashBondForfitureOutput Cb1;

        protected List<CashBondForfitureInput> SampleInputList;
        protected IProcessMunicipalItems Processor;

        protected Fixture Fixture { get; set; }

        [BeforeScenario()]
        public void Setup()
        {
            Fixture = new Fixture();
            Fixture.Customize(new AutoMoqCustomization());

            Processor = new ProcessMunicipalItems();
        }

        protected void GetEqualityTestCases()
        {
            Cb1 = BuildForfitureOutput()
                .Create();
            Cb2 = BuildForfitureOutput()
                .Create();
            Cb3 = Fixture.Build<CashBondForfitureOutput>()
                .WithAutoProperties()
                .Create();
        }

        protected void GetTestInputList()
        {
            var header = BuildForfitureInput()
                .Create();
            var cb1 = BuildForfitureInput()
                .Create();
            var cb2 = BuildForfitureInput()
                .Create();
            var cb3 = BuildForfitureInput()
                .Create();
            SampleInputList = new List<CashBondForfitureInput> { header, cb1, cb2, cb3 };
        }

        protected Dictionary<int, IEnumerable<RowItem>> GetSampleInputDictionary()
        {
            return new Dictionary<int, IEnumerable<RowItem>>
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

        protected List<CashBondForfitureInput> CreateInputList(string[] inputColumnNames, string[] inclusionFields, string[] dedupeFields, string[] aggregateFields, int numberOfRows, int duplicateCount)
        {
            List<CashBondForfitureInput> result = new List<CashBondForfitureInput>();
            int dupeCounter = 0;
            CashBondForfitureInput cashBondForfitureInput = null;
            for (int i = 0; i < numberOfRows; i++)
            {
                if (i > 0
                    && dupeCounter < duplicateCount)
                {
                    result.Add(cashBondForfitureInput);
                    dupeCounter++;
                }
                else
                {
                    List<DynamicItem> items = new List<DynamicItem>();
                    for (int j = 0; j < inputColumnNames.Length; j++)
                    {
                        var column = j;
                        var item = Fixture.Build<DynamicItem>()
                                          .With(x => x.ColumnName, inputColumnNames[column])
                                          .With(x => x.Value, Fixture.Create<string>())
                                          .With(x => x.ShouldAggregate,
                                              aggregateFields.Contains(inputColumnNames[column]))
                                          .With(x => x.ShouldIncludeInOutput,
                                              inclusionFields.Contains(inputColumnNames[column]))
                                          .With(x => x.ShouldRemoveDupes,
                                              dedupeFields.Contains(inputColumnNames[column]))
                                          .Create();
                        items.Add(item);
                    }
                    cashBondForfitureInput = Fixture.Build<CashBondForfitureInput>()
                                                    .WithAutoProperties()
                                                    .With(x => x.DynamicItems, items)
                                                    .Create();
                    result.Add(cashBondForfitureInput);
                }
            }
            return result;
        }

        private Ploeh.AutoFixture.Dsl.IPostprocessComposer<CashBondForfitureOutput> BuildForfitureOutput()
        {
            return Fixture.Build<CashBondForfitureOutput>()
                .WithAutoProperties()
                .With(x => x.DynamicItems,
                    new List<DynamicItem>
                    {
                        new DynamicItem {ColumnName = "Name", Value = "Name 1", ShouldRemoveDupes = true}
                    })
                .With(x => x.DynamicItems,
                    new List<DynamicItem>
                    {
                        new DynamicItem {ColumnName = "Address", Value = "Address 1", ShouldRemoveDupes = true}
                    })
                .With(x => x.DynamicItems,
                    new List<DynamicItem>
                    {
                        new DynamicItem {ColumnName = "Address2", Value = "City, St Zip", ShouldRemoveDupes = true}
                    })
                .With(x => x.DynamicItems,
                    new List<DynamicItem>
                    {
                        new DynamicItem {ColumnName = "DateOfBirth", Value = "Date Of Birth", ShouldRemoveDupes = true}
                    });
        }
        private Ploeh.AutoFixture.Dsl.IPostprocessComposer<CashBondForfitureInput> BuildForfitureInput()
        {
            return Fixture.Build<CashBondForfitureInput>()
                .WithAutoProperties()
                .With(x => x.DynamicItems,
                    new List<DynamicItem>
                    {
                        new DynamicItem {ColumnName = "Name", Value = Fixture.Create<string>(), ShouldRemoveDupes = true}
                    })
                .With(x => x.DynamicItems,
                    new List<DynamicItem>
                    {
                        new DynamicItem {ColumnName = "Address", Value = Fixture.Create<string>(), ShouldRemoveDupes = true}
                    })
                .With(x => x.DynamicItems,
                    new List<DynamicItem>
                    {
                        new DynamicItem {ColumnName = "Address2", Value = Fixture.Create<string>(), ShouldRemoveDupes = true}
                    })
                .With(x => x.DynamicItems,
                    new List<DynamicItem>
                    {
                        new DynamicItem {ColumnName = "DateOfBirth", Value = Fixture.Create<string>(), ShouldRemoveDupes = true}
                    });
        }

    }
}