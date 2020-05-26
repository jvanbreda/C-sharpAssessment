using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;
using Microsoft.Extensions.Configuration;

using ConsoleApp;
using WebApp.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApp.Controllers
{
    public class AssessmentController : Controller
    {
        private readonly ApiService _apiService;
        private readonly IConfiguration Configuration;

        private readonly string _apiKey;
        private IList<ViewModel> viewModels = new List<ViewModel>();

        public AssessmentController(ApiService apiService, IConfiguration configuration)
        {
            _apiService = apiService;
            Configuration = configuration;

            _apiKey = Configuration["ApiKey"];
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            var orderList = GetOrders();

            var merchantProductNos = orderList.Select(x => x.GetMerchantProductNos());

            var merchantProductNoList = new List<string>();

            foreach (var list in merchantProductNos)
            {
                foreach (var item in list)
                {
                    merchantProductNoList.Add(item);
                }
            }

            var productList = GetProducts(merchantProductNoList);

            return View(viewModels.OrderByDescending(x => x.QuantitySold).Take(5).ToList());
        }

        private IList<OrderModel> GetOrders()
        {
            var result = _apiService.GetOrdersWithStatus(new string[] { "IN_PROGRESS" }, _apiKey);

            var orderList = new List<OrderModel>();

            foreach(var order in result["Content"])
            {
                var orderModel = new OrderModel();
                orderModel.Id = int.Parse(order["Id"].ToString());
                orderModel.Lines = new List<OrderLine>();

                foreach(var orderLine in order["Lines"])
                {
                    var orderLineModel = new OrderLine();
                    orderLineModel.Gtin = orderLine["Gtin"].ToString();
                    orderLineModel.MerchantProductNo = orderLine["MerchantProductNo"].ToString();
                    orderLineModel.Quantity = int.Parse(orderLine["Quantity"].ToString());
                    orderModel.Lines.Add(orderLineModel);

                    if(!viewModels.Any(x => x.MerchantProductNo == orderLineModel.MerchantProductNo))
                    {
                        viewModels.Add(new ViewModel()
                        {
                            Ean = orderLineModel.Gtin,
                            MerchantProductNo = orderLineModel.MerchantProductNo,
                            QuantitySold = orderLineModel.Quantity
                        });
                    }
                    else
                    {
                        viewModels.Where(x => x.MerchantProductNo == orderLineModel.MerchantProductNo)
                            .First().QuantitySold += orderLineModel.Quantity;
                    }
                }

                orderList.Add(orderModel);
            }

            return orderList;
        }

        private IList<ProductModel> GetProducts(List<string> merchantProductNumbers)
        {
            var result = _apiService.GetProductWithMerchantProductNo(merchantProductNumbers.ToArray(), _apiKey);

            var productList = new List<ProductModel>();

            foreach(var product in result["Content"])
            {
                var productModel = new ProductModel();
                productModel.Name = product["Name"].ToString();
                productModel.Ean = product["Ean"].ToString();
                productModel.MerchantProductNo = product["MerchantProductNo"].ToString();

                productList.Add(productModel);

                viewModels.Where(x => x.MerchantProductNo == productModel.MerchantProductNo).First().ProductName = productModel.Name;
            }

            return productList;
        }
    }
}
