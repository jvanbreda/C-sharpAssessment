using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Utilities
{
    public class ListMethods
    {
        public static IList<ViewModel> GetTop5Products(IList<ViewModel> input)
        {
            if (input != null)
                return input.OrderByDescending(x => x.QuantitySold).Take(5).ToList();
            else
                return new List<ViewModel>();
        }
    }
}
