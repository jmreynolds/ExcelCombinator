using System.Collections.Generic;
using Core.Models;
using Infrastructure.Modules;
using Ninject;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;

namespace IntegrationTests
{
    public class TestBase
    {
        protected string TestPath { get; } = @"C:\Development\InfoCraft\ExcelCombinator\TestFiles\";
        protected Fixture Fixture { get; set; }
        public List<CashBondForfitureOutput> SampleOutputList { get; set; } = new List<CashBondForfitureOutput>();
        protected StandardKernel Kernel { get; private set; }
        protected void Bootstrap()
        {
            Kernel = new StandardKernel(new NetOfficeModule());
        }

        protected void GenerateSampleOutputList()
        {
            Fixture = new Fixture();
            Fixture.Customize(new AutoMoqCustomization());

            var list1 = new List<Citation>();
            Fixture.AddManyTo(list1,3);
            var item1 = Fixture.Build<CashBondForfitureOutput>()
                .WithAutoProperties()
                .With(x => x.DynamicItems,
                    new List<DynamicItem>
                    {
                        new DynamicItem {ColumnName = "Name", Value = Fixture.Create<string>(), ShouldRemoveDupes = true},
                        new DynamicItem {ColumnName = "Address", Value = Fixture.Create<string>(), ShouldRemoveDupes = true},
                        new DynamicItem {ColumnName = "AddressLine2", Value = Fixture.Create<string>(), ShouldRemoveDupes = true},
                        new DynamicItem {ColumnName = "DateOfBirth", Value = Fixture.Create<string>(), ShouldRemoveDupes = true},
                        new DynamicItem {ColumnName = "DispositionDate", Value = Fixture.Create<string>(), ShouldRemoveDupes = false},
                        new DynamicItem {ColumnName = "Citations", Value = list1, ShouldRemoveDupes = false}
                    })
                .Create();

            var list2 = new List<Citation>();
            Fixture.AddManyTo(list2, 1);
            var item2 = Fixture.Build<CashBondForfitureOutput>()
                .WithAutoProperties()
                .With(x => x.DynamicItems,
                    new List<DynamicItem>
                    {
                        new DynamicItem {ColumnName = "Name", Value = Fixture.Create<string>(), ShouldRemoveDupes = true},
                        new DynamicItem {ColumnName = "Address", Value = Fixture.Create<string>(), ShouldRemoveDupes = true},
                        new DynamicItem {ColumnName = "AddressLine2", Value = Fixture.Create<string>(), ShouldRemoveDupes = true},
                        new DynamicItem {ColumnName = "DateOfBirth", Value = Fixture.Create<string>(), ShouldRemoveDupes = true},
                        new DynamicItem {ColumnName = "DispositionDate", Value = Fixture.Create<string>(), ShouldRemoveDupes = false},
                        new DynamicItem {ColumnName = "Citations", Value = list2, ShouldRemoveDupes = false}
                    })
                .Create();
            var list3 = new List<Citation>();
            Fixture.AddManyTo(list3, 5);
            var item3 = Fixture.Build<CashBondForfitureOutput>()
                .WithAutoProperties()
                .With(x => x.DynamicItems,
                    new List<DynamicItem>
                    {
                        new DynamicItem {ColumnName = "Name", Value = Fixture.Create<string>(), ShouldRemoveDupes = true},
                        new DynamicItem {ColumnName = "Address", Value = Fixture.Create<string>(), ShouldRemoveDupes = true},
                        new DynamicItem {ColumnName = "AddressLine2", Value = Fixture.Create<string>(), ShouldRemoveDupes = true},
                        new DynamicItem {ColumnName = "DateOfBirth", Value = Fixture.Create<string>(), ShouldRemoveDupes = true},
                        new DynamicItem {ColumnName = "DispositionDate", Value = Fixture.Create<string>(), ShouldRemoveDupes = false},
                        new DynamicItem {ColumnName = "Citations", Value = list3, ShouldRemoveDupes = false}
                    })
                .Create();


            SampleOutputList.Add(item1);
            SampleOutputList.Add(item2);
            SampleOutputList.Add(item3);
        }
    }
}