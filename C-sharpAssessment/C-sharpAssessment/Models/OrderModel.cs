using System.Collections.Generic;

namespace WebApp.Models
{
    public class OrderModel
    {
        public int Id { get; set; }
        public IList<OrderLine> Lines { get; set; }

        public IEnumerable<string> GetMerchantProductNos()
        {
            foreach(var orderLine in Lines)
            {
                yield return orderLine.MerchantProductNo;
            }
        }
    }

    public class OrderLine
    {
        public string Gtin { get; set; }
        public string MerchantProductNo { get; set; }
        public int Quantity { get; set; }
    }
}
