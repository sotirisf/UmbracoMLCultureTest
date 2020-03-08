using System.Collections.Generic;
using System.Web.Mvc;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web.Mvc;
using Umbraco.Web.PublishedModels;
using UmbracoMLCultureTest.Models;

namespace UmbracoMLCultureTest.Controllers
{
    public class Products2Controller : SurfaceController
    {
        private readonly IVariationContextAccessor _variationContextAccessor;
        public Products2Controller(IVariationContextAccessor variationContextAccessor)
        {
            _variationContextAccessor = variationContextAccessor;
        }
        public ActionResult GetProductsNewWay(int productRootId, string defaultCurrency, string culture = "")
        {
            // This is how the culture is set for the context we are in
            _variationContextAccessor.VariationContext = new VariationContext(culture);

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