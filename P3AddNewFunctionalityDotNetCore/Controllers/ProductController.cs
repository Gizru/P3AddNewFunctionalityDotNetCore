using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using P3AddNewFunctionalityDotNetCore.Models.Services;
using P3AddNewFunctionalityDotNetCore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace P3AddNewFunctionalityDotNetCore.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ILanguageService _languageService;

        public ProductController(IProductService productService, ILanguageService languageService)
        {
            _productService = productService;
            _languageService = languageService;
        }


        //This contructor is used for TESTING ONLY
        //Do not use it otherwise
        public ProductController()
        {
        }

        public IActionResult Index()
        {
            IEnumerable<ProductViewModel> products = _productService.GetAllProductsViewModel();
            return View(products);
        }

        [Authorize]
        public IActionResult Admin()
        {
            return View(_productService.GetAllProductsViewModel().OrderByDescending(p => p.Id));
        }

        [Authorize]
        public ViewResult Create()
        {
            return View();
        }

        public List<string> CheckProduct(ProductViewModel product)
        {
            List<string> modelErrors = new List<string>();

            if(product.Name is null)
            {
                modelErrors.Add("MissingName");
            }

            try
            {
                int lStock = Int32.Parse(product.Stock);

                if (lStock <= 0)
                {
                    modelErrors.Add("QuantityNotGreaterThanZero");
                }
            }
            catch (ArgumentNullException error)
            {
                modelErrors.Add("MissingQuantity");
            }
            catch (FormatException error)
            {
                modelErrors.Add("QuantityNotAnInteger");
            }

            try
            {
                float lPrice = float.Parse(product.Price);

                if (lPrice <= 0)
                {
                    modelErrors.Add("PriceNotGreaterThanZero");
                }
            }
            catch (ArgumentNullException error)
            {
                modelErrors.Add("MissingPrice");
            }
            catch (FormatException error)
            {
                modelErrors.Add("PriceNotANumber");
            }

            return modelErrors;
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create(ProductViewModel product)
        {
            List<string> modelErrors = CheckProduct(product);
            // TODO validation controls
            // Implement a method inside the ProductService class that will return an error message for each
            // product property that is not conform to its business rules. The return type of the method 
            // must be of List<string>.

            foreach (string error in modelErrors)
            {
                ModelState.AddModelError("", error);
            }

            if (ModelState.IsValid)
            {
                _productService.SaveProduct(product);
                return RedirectToAction("Admin");
            }
            else
            {
                return View(product);
            }
        }

        [Authorize]
        [HttpPost]
        public IActionResult DeleteProduct(int id)
        {
            _productService.DeleteProduct(id);
            return RedirectToAction("Admin");
        }
    }
}