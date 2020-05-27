using System.Collections.Generic;
using WebApp.Models;
using WebApp.Utilities;
using Xunit;
using System.Linq;

namespace XUnitTest
{
    public class TestUtilities
    {
        public class TestNullList
        {
            private readonly IList<ViewModel> ViewModels;

            public TestNullList() {}

            [Fact]
            public void TestList_NullList_Returns0Items()
            {
                var orderedList = ListMethods.GetTop5Products(ViewModels);

                Assert.Equal(0, orderedList.Count);
            }

            [Fact]
            public void TestList_NullList_TestWholeList()
            {
                var orderedList = ListMethods.GetTop5Products(ViewModels);

                Assert.Equal(new int[] { }, orderedList.Select(x => x.QuantitySold).ToArray());
            }
        }

        public class TestEmptyList
        {
            private readonly IList<ViewModel> ViewModels;

            public TestEmptyList()
            {
                ViewModels = new List<ViewModel>();
            }

            [Fact]
            public void TestList_EmptyList_Returns0Items()
            {
                var orderedList = ListMethods.GetTop5Products(ViewModels);

                Assert.Equal(0, orderedList.Count);
            }

            [Fact]
            public void TestList_EmptyList_TestWholeList()
            {
                var orderedList = ListMethods.GetTop5Products(ViewModels);

                Assert.Equal(new int[] { }, orderedList.Select(x => x.QuantitySold).ToArray());
            }
        }

        public class TestListWithMoreThan5Items
        {
            private readonly IList<ViewModel> ViewModels;

            public TestListWithMoreThan5Items()
            {
                ViewModels = new List<ViewModel>()
                {
                    new ViewModel(){ ProductName="Product1", Ean="4823643643", MerchantProductNo="1", QuantitySold= 10 },
                    new ViewModel(){ ProductName="Product2", Ean="4823643644", MerchantProductNo="2", QuantitySold= 100 },
                    new ViewModel(){ ProductName="Product3", Ean="4823643645", MerchantProductNo="3", QuantitySold= 25 },
                    new ViewModel(){ ProductName="Product4", Ean="4823643646", MerchantProductNo="4", QuantitySold= 69 },
                    new ViewModel(){ ProductName="Product5", Ean="4823643647", MerchantProductNo="5", QuantitySold= 70 },
                    new ViewModel(){ ProductName="Product6", Ean="4823643648", MerchantProductNo="6", QuantitySold= 36 },
                };
            }

            [Fact]
            public void TestList_MoreThan5Items_Returns5Items()
            {
                var orderedList = ListMethods.GetTop5Products(ViewModels);

                Assert.Equal(5, orderedList.Count);
            }

            [Fact]
            public void TestList_MoreThan5Items_FirstItemIs100()
            {
                var orderedList = ListMethods.GetTop5Products(ViewModels);

                Assert.Equal(100, orderedList.First().QuantitySold);
            }

            [Fact]
            public void TestList_MoreThan5Items_LastItemIs25()
            {
                var orderedList = ListMethods.GetTop5Products(ViewModels);

                Assert.Equal(25, orderedList.Last().QuantitySold);
            }

            [Fact]
            public void TestList_MoreThan5Items_TestWholeList()
            {
                var orderedList = ListMethods.GetTop5Products(ViewModels);

                Assert.Equal(new int[] { 100, 70, 69, 36, 25 }, orderedList.Select(x => x.QuantitySold).ToArray());
            }
        }

        public class TestListWithLessThan5Items
        {
            private readonly IList<ViewModel> ViewModels;

            public TestListWithLessThan5Items()
            {
                ViewModels = new List<ViewModel>()
                {
                    new ViewModel(){ ProductName="Product1", Ean="4823643643", MerchantProductNo="1", QuantitySold= 10 },
                    new ViewModel(){ ProductName="Product2", Ean="4823643644", MerchantProductNo="2", QuantitySold= 100 },
                    new ViewModel(){ ProductName="Product3", Ean="4823643645", MerchantProductNo="3", QuantitySold= 25 },
                    new ViewModel(){ ProductName="Product4", Ean="4823643646", MerchantProductNo="4", QuantitySold= 69 }
                };
            }

            [Fact]
            public void TestList_LessThan5Items_ReturnsSameNumOfItemsAsOriginalList()
            {
                var orderedList = ListMethods.GetTop5Products(ViewModels);

                Assert.Equal(4, orderedList.Count);
            }

            [Fact]
            public void TestList_LessThan5Items_FirstItemIs100()
            {
                var orderedList = ListMethods.GetTop5Products(ViewModels);

                Assert.Equal(100, orderedList.First().QuantitySold);
            }

            [Fact]
            public void TestList_LessThan5Items_LastItemIs10()
            {
                var orderedList = ListMethods.GetTop5Products(ViewModels);

                Assert.Equal(10, orderedList.Last().QuantitySold);
            }

            [Fact]
            public void TestList_LessThan5Items_TestWholeList()
            {
                var orderedList = ListMethods.GetTop5Products(ViewModels);

                Assert.Equal(new int[] { 100, 69, 25, 10 }, orderedList.Select(x => x.QuantitySold).ToArray());
            }
        }
    }
}
