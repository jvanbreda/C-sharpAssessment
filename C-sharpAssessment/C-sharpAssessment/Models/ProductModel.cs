using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class ProductModel
    {
        public string Name { get; set; }
        public string Ean { get; set; }
        public string MerchantProductNo { get; set; }
        public int Quantity { get; set; }
    }
}
