using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using P3AddNewFunctionalityDotNetCore.Controllers;
using P3AddNewFunctionalityDotNetCore.Data;
using P3AddNewFunctionalityDotNetCore.Models;
using P3AddNewFunctionalityDotNetCore.Models.Repositories;
using P3AddNewFunctionalityDotNetCore.Models.Services;
using P3AddNewFunctionalityDotNetCore.Models.ViewModels;
using System;
using System.Collections.Generic;
using Xunit;

namespace P3AddNewFunctionalityDotNetCore.Tests
{
    public class ProductServiceTests
    {

        ProductController productController;

        List<string> expectedErrorMessages = new List<string> {
                "MissingName",
                "MissingQuantity",
                "QuantityNotGreaterThanZero",
                "QuantityNotAnInteger",
                "MissingPrice",
                "PriceNotGreaterThanZero",
                "PriceNotANumber"
            };

        public ProductServiceTests()
        {
            DbContextOptions<P3Referential> options = new DbContextOptions<P3Referential>();


            P3Referential context = new P3Referential(options);


            Cart cart = new Cart();
            ProductRepository productRepository = new ProductRepository(context);
            OrderRepository orderRepository = new OrderRepository(context);


            ProductService productService = new ProductService(cart, productRepository, orderRepository);
            LanguageService languageService = new LanguageService();


            productController = new ProductController(productService, languageService);
        }

        /// <summary>
        /// Take this test method as a template to write your test method.
        /// A test method must check if a definite method does its job:
        /// returns an expected value from a particular set of parameters
        /// </summary>
        [Fact]
        public void CheckProductName()
        {
            ProductViewModel product = new ProductViewModel();
            List<string> errorMessages = productController.CheckProduct(product);

            product.Price = "10.5";
            product.Stock = "5";
            errorMessages = productController.CheckProduct(product);

            Assert.Contains("MissingName", errorMessages);
        }

        [Fact]
        public void CheckProductPrice()
        {
            ProductViewModel product = new ProductViewModel();
            List<string> errorMessages = productController.CheckProduct(product);

            product.Name = "Test";
            product.Price = null;
            product.Stock = "5";
            errorMessages = productController.CheckProduct(product);


            Assert.Contains("MissingPrice", errorMessages);
        }

        [Fact]
        public void CheckProductPriceIsANumber()
        {
            ProductViewModel product = new ProductViewModel();
            List<string> errorMessages = productController.CheckProduct(product);

            product.Name = "Test";
            product.Price = "fhdv";
            product.Stock = "5";
            errorMessages = productController.CheckProduct(product);


            Assert.Contains("PriceNotANumber", errorMessages);
        }

        [Fact]
        public void CheckProductPriceIsGreaterThanZero()
        {
            ProductViewModel product = new ProductViewModel();
            List<string> errorMessages = productController.CheckProduct(product);

            product.Name = "Test";
            product.Price = "-12.5";
            product.Stock = "5";
            errorMessages = productController.CheckProduct(product);


            Assert.Contains("PriceNotGreaterThanZero", errorMessages);
        }

        [Fact]
        public void CheckQuantity()
        {
            ProductViewModel product = new ProductViewModel();
            List<string> errorMessages = productController.CheckProduct(product);

            product.Name = "Test";
            product.Price = "10.5";
            product.Stock = null;
            errorMessages = productController.CheckProduct(product);


            Assert.Contains("MissingQuantity", errorMessages);
        }

        [Fact]
        public void CheckQuantityIsAnInteger()
        {
            ProductViewModel product = new ProductViewModel();
            List<string> errorMessages = productController.CheckProduct(product);

            product.Name = "Test";
            product.Price = "10.5";
            product.Stock = "2.5";
            errorMessages = productController.CheckProduct(product);


            Assert.Contains("QuantityNotAnInteger", errorMessages);
        }

        [Fact]
        public void CheckQuantityIsGreaterThanZero()
        {
            ProductViewModel product = new ProductViewModel();
            List<string> errorMessages = productController.CheckProduct(product);

            product.Name = "Test";
            product.Price = "10.5";
            product.Stock = "-5";
            errorMessages = productController.CheckProduct(product);


            Assert.Contains("QuantityNotGreaterThanZero", errorMessages);
        }
    }
}