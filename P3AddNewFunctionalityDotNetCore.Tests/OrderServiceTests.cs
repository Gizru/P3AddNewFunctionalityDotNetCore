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
using StackExchange.Redis;
using System.Net;
using System.Xml.Linq;

namespace OrderServiceTests
{
    public class CheckProductModelErrors
    {

        [Fact]
        public void GetOrders()
        {
            Mock<ICart> mocCart = new Mock<ICart>();
            Mock<IProductService> mockProductService = new Mock<IProductService>();
            Mock<IOrderRepository> mockOrderRepository = new Mock<IOrderRepository>();
            Mock<P3Referential> P3ReferentialMock = new Mock<P3Referential>();

            List<P3AddNewFunctionalityDotNetCore.Models.Entities.Order> orders = new List<P3AddNewFunctionalityDotNetCore.Models.Entities.Order>()
            {
                new P3AddNewFunctionalityDotNetCore.Models.Entities.Order { Id = 1, OrderLine = new List<OrderLine>()
                {
                    new OrderLine
                    {
                        Id = 2,
                        Product =  new Product
                        {
                            Name = "Power plant",
                            Id = 1,
                            Price = 20.52,
                            Quantity = 10 }
                        }
                    },

                    Address = "52 dhiagia",

                    City = "NYC",

                    Country = "USA",

                    Zip = "10075",

                    Name = "Pulity"

                }
            };

            P3ReferentialMock.Setup(x => x.Order).ReturnsDbSet(orders);

            ProductRepository productRepository = new ProductRepository(P3ReferentialMock.Object);

            var orderService = new OrderService(mocCart.Object, mockOrderRepository.Object, mockProductService.Object);

            var result = orderService.GetOrders().Result;

            Assert.NotEmpty(result);
        }

        [Fact]
        public void SaveOrder()
        {
            Mock<ICart> mocCart = new Mock<ICart>();
            Mock<IProductService> mockProductService = new Mock<IProductService>();
            Mock<IOrderRepository> mockOrderRepository = new Mock<IOrderRepository>();
            Mock<P3Referential> P3ReferentialMock = new Mock<P3Referential>();

            List<P3AddNewFunctionalityDotNetCore.Models.Entities.Order> orders = new List<P3AddNewFunctionalityDotNetCore.Models.Entities.Order>()
            {
                new P3AddNewFunctionalityDotNetCore.Models.Entities.Order { Id = 1, OrderLine = new List<OrderLine>()
                {
                    new OrderLine
                    {
                        Id = 2,
                        Product =  new Product
                        {
                            Name = "Power plant",
                            Id = 1,
                            Price = 20.52,
                            Quantity = 10 }
                        }
                    }
                }
            };

            P3ReferentialMock.Setup(x => x.Order).ReturnsDbSet(orders);

            ProductRepository productRepository = new ProductRepository(P3ReferentialMock.Object);

            var orderService = new OrderService(mocCart.Object, mockOrderRepository.Object, mockProductService.Object);

            orderService.SaveOrder(

                new OrderViewModel()
                {
                    OrderId = 1,
                    Address = "52 dhiagia",
                    City = "NYC",
                    Country = "USA",
                    Zip = "10075",
                    Name = "Pulity"
                }
            );

            var result = orderService.GetOrders().Result;

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void GetOrder()
        {
            Mock<ICart> mocCart = new Mock<ICart>();
            Mock<IProductService> mockProductService = new Mock<IProductService>();
            Mock<IOrderRepository> mockOrderRepository = new Mock<IOrderRepository>();
            Mock<P3Referential> P3ReferentialMock = new Mock<P3Referential>();

            List<P3AddNewFunctionalityDotNetCore.Models.Entities.Order> orders = new List<P3AddNewFunctionalityDotNetCore.Models.Entities.Order>()
            {
                new P3AddNewFunctionalityDotNetCore.Models.Entities.Order { Id = 1, OrderLine = new List<OrderLine>()
                {
                    new OrderLine
                    {
                        Id = 2,
                        Product =  new Product
                        {
                            Name = "Power plant",
                            Id = 1,
                            Price = 20.52,
                            Quantity = 10 }
                        }
                    }
                }
            };

            P3ReferentialMock.Setup(x => x.Order).ReturnsDbSet(orders);

            ProductRepository productRepository = new ProductRepository(P3ReferentialMock.Object);

            var orderService = new OrderService(mocCart.Object, mockOrderRepository.Object, mockProductService.Object);

            orderService.SaveOrder(

                new OrderViewModel()
                {
                    OrderId = 1,
                    Address = "52 dhiagia",
                    City = "NYC",
                    Country = "USA",
                    Zip = "10075",
                    Name = "Pulity"
                }
            );

            var result = orderService.GetOrder(1).Result;

            Assert.Equal("Pulity", result.Name);
        }

    }
}
