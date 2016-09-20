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
            SampleInputList = new List<CashBondForfitureInput> {header, cb1, cb2, cb3};
        }

        protected Dictionary<int, IEnumerable<RowItem>> GetSampleInputDictionary() => new Dictionary<int, IEnumerable<RowItem>>
        {
            [0] = new List<RowItem>
            {
                new RowItem() {ColumnName = "Name", Value = "Row1Value1"},
                new RowItem() {ColumnName = "Address", Value = "Row1Value2"}
            },
            [1] = new List<RowItem>
            {
                new RowItem() {ColumnName = "Name", Value = "Row2Value1"},
                new RowItem() {ColumnName = "Address", Value = "Row2Value2"}
            },
            [2] = new List<RowItem>
            {
                new RowItem() {ColumnName = "Name", Value = "Row3Value1"},
                new RowItem() {ColumnName = "Address", Value = "Row3Value2"}
            }
        };

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