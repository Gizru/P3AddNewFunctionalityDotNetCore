using P3AddNewFunctionalityDotNetCore.Controllers;
using P3AddNewFunctionalityDotNetCore.Models;
using P3AddNewFunctionalityDotNetCore.Models.Services;
using P3AddNewFunctionalityDotNetCore.Models.ViewModels;
using System.Collections.Generic;
using Xunit;

namespace P3AddNewFunctionalityDotNetCore.Tests
{
    public class ProductServiceTests
    {
        /// <summary>
        /// Take this test method as a template to write your test method.
        /// A test method must check if a definite method does its job:
        /// returns an expected value from a particular set of parameters
        /// </summary>
        [Fact]
        public void CheckProduct()
        {
            Cart cart = new Cart();

            ProductController productController = new ProductController();
            ProductViewModel product = new ProductViewModel();
            List<string> errorMessages = productController.CheckProduct(product);

            List<string> expectedErrorMessages = new List<string> {
                "MissingName",
                "MissingQuantity",
                "QuantityNotGreaterThanZero",
                "QuantityNotAnInteger",
                "MissingPrice",
                "PriceNotGreaterThanZero",
                "PriceNotANumber"
            };

            Assert.NotEmpty(errorMessages);
            Assert.Contains("MissingName",errorMessages);
            Assert.Contains("MissingQuantity", errorMessages);
            Assert.Contains("MissingPrice", errorMessages);


            //we modify the condition to check other error messages
            product.Price = "not a number";
            product.Stock = "10.5";
            errorMessages = productController.CheckProduct(product);


            Assert.Contains("MissingName", errorMessages);
            Assert.Contains("QuantityNotAnInteger", errorMessages);
            Assert.Contains("PriceNotANumber", errorMessages);


            //we modify the condition to check other error messages
            product.Price = "-10.5";
            product.Stock = "-5";
            errorMessages = productController.CheckProduct(product);


            Assert.Contains("MissingName", errorMessages);
            Assert.Contains("PriceNotGreaterThanZero", errorMessages);
            Assert.Contains("QuantityNotGreaterThanZero", errorMessages);


            //checking that we receive no error with the right conditions
            product.Name = "Test";
            product.Price = "10.5";
            product.Stock = "5";
            errorMessages = productController.CheckProduct(product);

            Assert.Empty(errorMessages);
        }

        // TODO write test methods to ensure a correct coverage of all possibilities
    }
}