using System.Collections.Generic;
using System.Linq;
using Core.Models;

namespace Core.EqualityImplementations
{
    public class CashBondForfitureOutputCompare : IEqualityComparer<CashBondForfitureOutput>
    {
        public bool Equals(CashBondForfitureOutput x, CashBondForfitureOutput y)
        {
            if (x == null && y == null) return true;
            if (x == null || y == null) return false;

            var xName = GetStringValue(x, nameof(x.Name));
            var yName = GetStringValue(y, nameof(y.Name));
            var xAddr = GetStringValue(x, nameof(x.Address));
            var yAddr = GetStringValue(y, nameof(y.Address));
            var xAddr2 = GetStringValue(x, nameof(x.AddressLine2));
            var yAddr2 = GetStringValue(y, nameof(y.AddressLine2));
            var xDob = GetStringValue(x, nameof(x.DateOfBirth));
            var yDob = GetStringValue(y, nameof(y.DateOfBirth));

            return xName == yName 
                && xAddr == yAddr 
                && xAddr2 == yAddr2 
                && xDob == yDob;
        }

        private static string GetStringValue(CashBondForfitureOutput x, string propName)
        {
            string propValue = x.GetType().GetProperty(propName).GetValue(x, null).ToString();
            var values = propValue.Split(' ').Where(s => !string.IsNullOrWhiteSpace(s));
            var value = string.Join(" ", values);
            return value;
        }

        
        

        public int GetHashCode(CashBondForfitureOutput obj)
        {
            var dob = obj.DateOfBirth.Trim().ToUpper().GetHashCode();
            var name = obj.Name.Trim().ToUpper().GetHashCode();
            var add1 = obj.Address.Trim().ToUpper().GetHashCode();
            var add2 = obj.AddressLine2.Trim().ToUpper().GetHashCode();
            return dob + name + add1 + add2;
        }
    }
}