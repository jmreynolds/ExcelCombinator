using System.Collections.Generic;
using System.Linq;
using Core.Extensions;
using Core.Models;

namespace Core.EqualityImplementations
{
    public class CashBondForfitureOutputCompare : IEqualityComparer<CashBondForfitureOutput>
    {
        public bool Equals(CashBondForfitureOutput x, CashBondForfitureOutput y)
        {
            if (x == null && y == null) return true;
            if (x == null || y == null) return false;

            var xName = x.GetStringValue(nameof(x.Name));
            var yName = y.GetStringValue(nameof(y.Name));
            var xAddr = x.GetStringValue(nameof(x.Address));
            var yAddr = y.GetStringValue(nameof(y.Address));
            var xAddr2 = x.GetStringValue(nameof(x.AddressLine2));
            var yAddr2 = y.GetStringValue(nameof(y.AddressLine2));
            var xDob = x.GetStringValue(nameof(x.DateOfBirth));
            var yDob = y.GetStringValue(nameof(y.DateOfBirth));

            return xName == yName 
                && xAddr == yAddr 
                && xAddr2 == yAddr2 
                && xDob == yDob;
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