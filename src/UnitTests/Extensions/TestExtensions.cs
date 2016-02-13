using System.Linq;
using NUnit.Framework;
using Should;

namespace UnitTests.Extensions
{
    public static class TestExtensions
    {
        public static void ShouldNotHaveEmptyProperties(this object testObj)
        {
            testObj.GetType()
                ?.GetProperties()
                ?.ToList()
                ?.ForEach(x =>
                {
                    x.ShouldNotBeNull();
                    x.GetValue(testObj, null).ToString().ShouldNotBeEmpty();
                });
        }
    }
}