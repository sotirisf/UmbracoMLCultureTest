using System.Collections.Generic;
using System.Globalization;
using System.Web.Mvc;
using Umbraco.Web.Mvc;
using Umbraco.Web.PublishedModels;
using UmbracoMLCultureTest.Models;

namespace UmbracoMLCultureTest.Controllers
{
    public class ProductsController : SurfaceController
    {
        public ActionResult GetProductsOldWayNoCultureSet(int productRootId, string defaultCurrency)
        {
            return PartialView("ProductList", GetProductVm(productRootId, defaultCurrency));
        }

        public ActionResult GetProductsOldWayWithCulture(int productRootId, string defaultCurrency, string culture="")
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo(culture);
            System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture);

            return PartialView("ProductList", GetProductVm(productRootId, defaultCurrency));
        }

        private IEnumerable<ProductViewModel> GetProductVm(int productRootId, string defaultCurrency)
        {
            var productsRootPage = Umbraco.Content(productRootId);
            foreach (Product product in productsRootPage.Children)
            {
                var vm = new ProductViewModel();
                vm.Price = product.Price;
                vm.Name = product.ProductName;
                vm.PhotoUrl = product.Photos.Url;
                vm.Url = product.Url;
                vm.DefaultCurrency = defaultCurrency;

                yield return vm;
            }
        }
    }
}