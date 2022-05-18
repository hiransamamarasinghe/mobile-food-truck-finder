using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodTruckFinder.Domain.Exceptions
{
    public class BadRequestExceptions : Exception
    {
        public BadRequestExceptions(string code) : base(code)
        {

        }
    }
}
