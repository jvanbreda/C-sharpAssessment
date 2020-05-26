using System.Collections.Generic;
using System.Linq;

namespace WebApp.Models
{
    public class ViewModel
    {
        public string ProductName { get; set; }
        public string Ean { get; set; }
        public string MerchantProductNo { get; set; }
        public int QuantitySold { get; set; }
    }
}
