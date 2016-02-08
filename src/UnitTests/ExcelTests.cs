using System.Collections.Generic;
using System.Linq;
using Core;
using Core.Models;
using DataAccess;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Should;

namespace UnitTests
{
    [TestFixture]
    public class ExcelTests : TestBase
    {
        //[Test, Category("Unit"), Ignore("Need to Fix this")]
        //public void Should_Map_Worksheet_From_Dictionary_Into_Class_List()
        //{
        //    var sampleInputDictionary = GetSampleInputDictionary();
        //    IProcessor processor = new Processor();
        //    var result = processor.MapToCashBondForfitureInput(sampleInputDictionary);
        //    result.Count().ShouldEqual(3);
        //}

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
            result.Count().ShouldEqual(2);
            result.First().Citations.Count.ShouldEqual(2);
        }

        [Test, Category("Unit")]
        public void Hash_Code_Should_Return_Correct_Results()
        {
            var comparer = new CashBondForfitureOutputCompare();
            GetEqualityTestCases();
            var hc1 = comparer.GetHashCode(Cb1);
            var hc2 = comparer.GetHashCode(Cb2);
            var hc3 = comparer.GetHashCode(Cb3);
            hc1.ShouldEqual(hc2);
            hc2.ShouldEqual(hc1);
            hc1.ShouldNotEqual(hc3);
            hc2.ShouldNotEqual(hc3);
        }

        [Test, Category("Unit")]
        public void Equality_Tester_Should_Return_Correct_Results()
        {
            var comparer = new CashBondForfitureOutputCompare();
            GetEqualityTestCases();
            comparer.Equals(Cb1, Cb2).ShouldBeTrue();
            comparer.Equals(Cb2, Cb1).ShouldBeTrue();
            comparer.Equals(Cb1, Cb3).ShouldBeFalse();
            comparer.Equals(Cb2, Cb3).ShouldBeFalse();
        }
    }
}