using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Localization;
using P3AddNewFunctionalityDotNetCore.Controllers;
using P3AddNewFunctionalityDotNetCore.Data;
using P3AddNewFunctionalityDotNetCore.Models;
using P3AddNewFunctionalityDotNetCore.Models.Repositories;
using P3AddNewFunctionalityDotNetCore.Models.Services;
using P3AddNewFunctionalityDotNetCore.Models.ViewModels;
using P3AddNewFunctionalityDotNetCore.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Moq;
using Microsoft.ApplicationInsights.Extensibility.Implementation;
using Moq.EntityFrameworkCore;

namespace ProductServiceTests
{
    public class CheckProductModelErrors
    {

        [Fact]
        public void CheckProductName()

        {
            Mock<ICart> mocCart = new Mock<ICart>();

            Mock<IOrderService> mockOrder = new Mock<IOrderService>();
            Mock<IProductService> mockProduct = new Mock<IProductService>();

            Mock<IProductRepository> mockProductRepository = new Mock<IProductRepository>();
            Mock<IOrderRepository> mockOrderRepository = new Mock<IOrderRepository>();
            Mock<IStringLocalizer<ProductService>> mockLocalizer = new Mock<IStringLocalizer<ProductService>>();

            var errorName = new LocalizedString("MissingName", "Please enter a name");

            mockLocalizer.Setup(ml => ml["MissingName"]).Returns(errorName);

            var productService = new ProductService(mocCart.Object, mockProductRepository.Object,
                                                    mockOrderRepository.Object, mockLocalizer.Object);

            var product = new ProductViewModel()
            {
                Name = null,
                Price = "11",
                Description = "product",
                Details = "details",
                Stock = "3"

            };

            var result = productService.CheckProductModelErrors(product);

            Assert.Contains("Please enter a name", result);
        }

        [Fact]
        public void CheckProductPrice()
        {
            Mock<ICart> mocCart = new Mock<ICart>();

            Mock<IOrderService> mockOrder = new Mock<IOrderService>();
            Mock<IProductService> mockProduct = new Mock<IProductService>();

            Mock<IProductRepository> mockProductRepository = new Mock<IProductRepository>();
            Mock<IOrderRepository> mockOrderRepository = new Mock<IOrderRepository>();
            Mock<IStringLocalizer<ProductService>> mockLocalizer = new Mock<IStringLocalizer<ProductService>>();
            var errorName = new LocalizedString("MissingPrice", "Please enter a price");

            mockLocalizer.Setup(ml => ml["MissingPrice"]).Returns(errorName);

            var productService = new ProductService(mocCart.Object, mockProductRepository.Object,
                                                    mockOrderRepository.Object, mockLocalizer.Object);

            var product = new ProductViewModel()
            {
                Name = "productName",
                Price = null,
                Description = "product",
                Details = "details",
                Stock = "3"

            };


            var result = productService.CheckProductModelErrors(product);

            Assert.Contains("Please enter a price", result);
        }

        [Fact]
        public void CheckProductPriceIsANumber()
        {
            Mock<ICart> mocCart = new Mock<ICart>();

            Mock<IOrderService> mockOrder = new Mock<IOrderService>();
            Mock<IProductService> mockProduct = new Mock<IProductService>();

            Mock<IProductRepository> mockProductRepository = new Mock<IProductRepository>();
            Mock<IOrderRepository> mockOrderRepository = new Mock<IOrderRepository>();
            Mock<IStringLocalizer<ProductService>> mockLocalizer = new Mock<IStringLocalizer<ProductService>>();
            var errorName = new LocalizedString("PriceNotANumber", "Please enter a correct Price");

            mockLocalizer.Setup(ml => ml["PriceNotANumber"]).Returns(errorName);

            var productService = new ProductService(mocCart.Object, mockProductRepository.Object,
                                                    mockOrderRepository.Object, mockLocalizer.Object);

            var product = new ProductViewModel()
            {
                Name = "productName",
                Price = "fhdv",
                Description = "product",
                Details = "details",
                Stock = "3"

            };


            var result = productService.CheckProductModelErrors(product);

            Assert.Contains("Please enter a correct Price", result);
        }

        [Fact]
        public void CheckProductPriceIsGreaterThanZero()
        {
            Mock<ICart> mocCart = new Mock<ICart>();

            Mock<IOrderService> mockOrder = new Mock<IOrderService>();
            Mock<IProductService> mockProduct = new Mock<IProductService>();

            Mock<IProductRepository> mockProductRepository = new Mock<IProductRepository>();
            Mock<IOrderRepository> mockOrderRepository = new Mock<IOrderRepository>();
            Mock<IStringLocalizer<ProductService>> mockLocalizer = new Mock<IStringLocalizer<ProductService>>();
            var errorName = new LocalizedString("PriceNotGreaterThanZero", "Please enter a price greater than Zero");

            mockLocalizer.Setup(ml => ml["PriceNotGreaterThanZero"]).Returns(errorName);

            var productService = new ProductService(mocCart.Object, mockProductRepository.Object,
                                                    mockOrderRepository.Object, mockLocalizer.Object);

            var product = new ProductViewModel()
            {
                Name = "productName",
                Price = "-12.5",
                Description = "product",
                Details = "details",
                Stock = "3"

            };


            var result = productService.CheckProductModelErrors(product);

            Assert.Contains("Please enter a price greater than Zero", result);
        }

        [Fact]
        public void CheckQuantity()
        {
            Mock<ICart> mocCart = new Mock<ICart>();

            Mock<IOrderService> mockOrder = new Mock<IOrderService>();
            Mock<IProductService> mockProduct = new Mock<IProductService>();

            Mock<IProductRepository> mockProductRepository = new Mock<IProductRepository>();
            Mock<IOrderRepository> mockOrderRepository = new Mock<IOrderRepository>();
            Mock<IStringLocalizer<ProductService>> mockLocalizer = new Mock<IStringLocalizer<ProductService>>();
            var errorName = new LocalizedString("MissingQuantity", "Please enter a quantity");

            mockLocalizer.Setup(ml => ml["MissingQuantity"]).Returns(errorName);

            var productService = new ProductService(mocCart.Object, mockProductRepository.Object,
                                                    mockOrderRepository.Object, mockLocalizer.Object);

            var product = new ProductViewModel()
            {
                Name = "productName",
                Price = "12.5",
                Description = "product",
                Details = "details",
                Stock = null

            };


            var result = productService.CheckProductModelErrors(product);

            Assert.Contains("Please enter a quantity", result);
        }

        [Fact]
        public void CheckQuantityIsAnInteger()
        {
            Mock<ICart> mocCart = new Mock<ICart>();

            Mock<IOrderService> mockOrder = new Mock<IOrderService>();
            Mock<IProductService> mockProduct = new Mock<IProductService>();

            Mock<IProductRepository> mockProductRepository = new Mock<IProductRepository>();
            Mock<IOrderRepository> mockOrderRepository = new Mock<IOrderRepository>();
            Mock<IStringLocalizer<ProductService>> mockLocalizer = new Mock<IStringLocalizer<ProductService>>();
            var errorName = new LocalizedString("QuantityNotAnInteger", "Please make sure the quantity number is an integer");

            mockLocalizer.Setup(ml => ml["QuantityNotAnInteger"]).Returns(errorName);

            var productService = new ProductService(mocCart.Object, mockProductRepository.Object,
                                                    mockOrderRepository.Object, mockLocalizer.Object);

            var product = new ProductViewModel()
            {
                Name = "productName",
                Price = "12.5",
                Description = "product",
                Details = "details",
                Stock = "2.5"

            };


            var result = productService.CheckProductModelErrors(product);

            Assert.Contains("Please make sure the quantity number is an integer", result);
        }

        [Fact]
        public void CheckQuantityIsGreaterThanZero()
        {
            Mock<ICart> mocCart = new Mock<ICart>();

            Mock<IOrderService> mockOrder = new Mock<IOrderService>();
            Mock<IProductService> mockProduct = new Mock<IProductService>();

            Mock<IProductRepository> mockProductRepository = new Mock<IProductRepository>();
            Mock<IOrderRepository> mockOrderRepository = new Mock<IOrderRepository>();
            Mock<IStringLocalizer<ProductService>> mockLocalizer = new Mock<IStringLocalizer<ProductService>>();
            var errorName = new LocalizedString("QuantityNotGreaterThanZero", "Please enter a quantity greater than zero");

            mockLocalizer.Setup(ml => ml["QuantityNotGreaterThanZero"]).Returns(errorName);

            var productService = new ProductService(mocCart.Object, mockProductRepository.Object,
                                                    mockOrderRepository.Object, mockLocalizer.Object);

            var product = new ProductViewModel()
            {
                Name = "productName",
                Price = "12.5",
                Description = "product",
                Details = "details",
                Stock = "-5"

            };


            var result = productService.CheckProductModelErrors(product);

            Assert.Contains("Please enter a quantity greater than zero", result);
        }

        [Fact]
        public void CheckHappy()
        {
            Mock<ICart> mocCart = new Mock<ICart>();

            Mock<IOrderService> mockOrder = new Mock<IOrderService>();
            Mock<IProductService> mockProduct = new Mock<IProductService>();

            Mock<IProductRepository> mockProductRepository = new Mock<IProductRepository>();
            Mock<IOrderRepository> mockOrderRepository = new Mock<IOrderRepository>();
            Mock<IStringLocalizer<ProductService>> mockLocalizer = new Mock<IStringLocalizer<ProductService>>();
            var errorName = new LocalizedString("QuantityNotGreaterThanZero", "Please enter a quantity greater than zero");

            mockLocalizer.Setup(ml => ml["QuantityNotGreaterThanZero"]).Returns(errorName);

            var productService = new ProductService(mocCart.Object, mockProductRepository.Object,
                                                    mockOrderRepository.Object, mockLocalizer.Object);

            var product = new ProductViewModel()
            {
                Name = "productName",
                Price = "12.5",
                Description = "product",
                Details = "details",
                Stock = "5"

            };


            var result = productService.CheckProductModelErrors(product);

            Assert.Empty(result);
        }

    }

    public class GetAllProductsViewModel
    {

        [Fact]
        public void CheckProductListIsEmpty()
        {
            Mock<ICart> mocCart = new Mock<ICart>();

            Mock<IOrderService> mockOrder = new Mock<IOrderService>();
            Mock<IProductService> mockProduct = new Mock<IProductService>();

            Mock<IProductRepository> mockProductRepository = new Mock<IProductRepository>();
            Mock<IOrderRepository> mockOrderRepository = new Mock<IOrderRepository>();
            Mock<IStringLocalizer<ProductService>> mockLocalizer = new Mock<IStringLocalizer<ProductService>>();

            var productService = new ProductService(mocCart.Object, mockProductRepository.Object,
                                                    mockOrderRepository.Object, mockLocalizer.Object);

            List<ProductViewModel> result = productService.GetAllProductsViewModel();

            Assert.Empty(result);
        }

        [Fact]
        public void CheckProductListIsNotEmpty()
        {
            Mock<ICart> mocCart = new Mock<ICart>();

            Mock<IOrderService> mockOrder = new Mock<IOrderService>();
            Mock<IProductService> mockProduct = new Mock<IProductService>();
            Mock<IOrderRepository> mockOrderRepository = new Mock<IOrderRepository>();
            Mock<IStringLocalizer<ProductService>> mockLocalizer = new Mock<IStringLocalizer<ProductService>>();
            Mock<P3Referential> P3ReferentialMock = new Mock<P3Referential>();

            List<Product> products = new List<Product>() 
            { 
                new Product { Id = 1, Name = "PowerPlant", Price = 2350.50, Quantity = 10}
            };
            P3ReferentialMock.Setup(x => x.Product).ReturnsDbSet(products);

            ProductRepository productRepository = new ProductRepository(P3ReferentialMock.Object);

            var productService = new ProductService(mocCart.Object, productRepository,
                                                    mockOrderRepository.Object, mockLocalizer.Object);

            var product = new ProductViewModel()
            {
                Name = "productName",
                Price = "12.5",
                Description = "product",
                Details = "details",
                Stock = "5"

            };

            productService.SaveProduct(product);

            List<ProductViewModel> result = productService.GetAllProductsViewModel();

            Assert.NotEmpty(result);
        }

        [Fact]
        public void CheckProductListExpectedType()
        {
            Mock<ICart> mocCart = new Mock<ICart>();

            Mock<IOrderService> mockOrder = new Mock<IOrderService>();
            Mock<IProductService> mockProduct = new Mock<IProductService>();

            Mock<IProductRepository> mockProductRepository = new Mock<IProductRepository>();
            Mock<IOrderRepository> mockOrderRepository = new Mock<IOrderRepository>();
            Mock<IStringLocalizer<ProductService>> mockLocalizer = new Mock<IStringLocalizer<ProductService>>();

            var productService = new ProductService(mocCart.Object, mockProductRepository.Object,
                                                    mockOrderRepository.Object, mockLocalizer.Object);

            var product = new ProductViewModel()
            {
                Name = "productName",
                Price = "12.5",
                Description = "product",
                Details = "details",
                Stock = "5"

            };

            productService.SaveProduct(product);

            List<ProductViewModel> result = productService.GetAllProductsViewModel();

            Assert.IsType<ProductViewModel>(result[0]);
        }

        
    }

    public class GetProductViewModel
    {
        [Fact]
        public void CheckEndType()
        {
            Product product = new Product()
            {
                Id = 1,
                Price = 1,
                Quantity = 1,
                Name = "Name1",
            };

            var result = ProductService.GetProductViewModel(product);

            Assert.IsType<ProductViewModel>(result);
        }
    }
}