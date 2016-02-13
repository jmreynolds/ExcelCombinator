using Core.EqualityImplementations;
using Core.Models;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Should;

namespace UnitTests
{
    public class EqualityTester : TestBase
    {

        [Test, Category("Unit")]
        public void Equality_Comparer_Should_Return_Correct_Results()
        {
            var comparer = new CashBondForfitureOutputCompare();
            GetEqualityTestCases();
            comparer.Equals(Cb1, Cb2).ShouldBeTrue();
            comparer.Equals(Cb2, Cb1).ShouldBeTrue();
            comparer.Equals(Cb1, Cb3).ShouldBeFalse();
            comparer.Equals(Cb2, Cb3).ShouldBeFalse();
        }

        [Test, Category("Unit")]
        public void Equality_Comparer_Should_Ignore_Extra_Spaces_Between_Words()
        {
            var singleSpace = Fixture.Build<CashBondForfitureOutput>()
                .WithAutoProperties()
                .With(x => x.Name, "Some Thing")
                .With(x => x.Address, "Address 1")
                .With(x => x.AddressLine2, "City, St Zip")
                .With(x => x.DateOfBirth, "Date Of Birth")
                .Create();
            var doubleName = Fixture.Build<CashBondForfitureOutput>()
                .WithAutoProperties()
                .With(x => x.Name, "Some   Thing")
                .With(x => x.Address, "Address 1")
                .With(x => x.AddressLine2, "City, St Zip")
                .With(x => x.DateOfBirth, "Date Of Birth")
                .Create();
            var doubleAddress = Fixture.Build<CashBondForfitureOutput>()
                .WithAutoProperties()
                .With(x => x.Name, "Some Thing")
                .With(x => x.Address, "Address   1")
                .With(x => x.AddressLine2, "City, St Zip")
                .With(x => x.DateOfBirth, "Date Of Birth")
                .Create();
            var doubleCity = Fixture.Build<CashBondForfitureOutput>()
                .WithAutoProperties()
                .With(x => x.Name, "Some Thing")
                .With(x => x.Address, "Address 1")
                .With(x => x.AddressLine2, "City, St    Zip")
                .With(x => x.DateOfBirth, "Date Of Birth")
                .Create();
            var doubleDOB = Fixture.Build<CashBondForfitureOutput>()
                .WithAutoProperties()
                .With(x => x.Name, "Some Thing")
                .With(x => x.Address, "Address 1")
                .With(x => x.AddressLine2, "City, St Zip")
                .With(x => x.DateOfBirth, "Date    Of     Birth")
                .Create();
            var comparer = new CashBondForfitureOutputCompare();
            comparer.Equals(singleSpace, doubleName).ShouldBeTrue("Not accounting for Name");
            comparer.Equals(singleSpace, doubleAddress).ShouldBeTrue("Not accounting for Address");
            comparer.Equals(singleSpace, doubleCity).ShouldBeTrue("Not accounting for City State Zip");
            comparer.Equals(singleSpace, doubleDOB).ShouldBeTrue("Not accounting for DOB");


        }

        [Test, Category("Unit")]
        public void Equality_Comparer_Should_Account_For_Nulls()
        {
            var singleSpace = Fixture.Build<CashBondForfitureOutput>()
                .WithAutoProperties()
                .With(x => x.Name, "Some Thing")
                .With(x => x.Address, "Address 1")
                .With(x => x.AddressLine2, "City, St Zip")
                .With(x => x.DateOfBirth, "Date Of Birth")
                .Create();
            var doubleName = Fixture.Build<CashBondForfitureOutput>()
                .WithAutoProperties()
                .With(x => x.Name, null)
                .With(x => x.Address, "Address 1")
                .With(x => x.AddressLine2, "City, St Zip")
                .With(x => x.DateOfBirth, "Date Of Birth")
                .Create();
            var comparer = new CashBondForfitureOutputCompare();
            Assert.That(()=>comparer.Equals(singleSpace, doubleName), Throws.Nothing);
            comparer.Equals(singleSpace, doubleName).ShouldBeFalse("Not accounting for null Name");
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
    }
}