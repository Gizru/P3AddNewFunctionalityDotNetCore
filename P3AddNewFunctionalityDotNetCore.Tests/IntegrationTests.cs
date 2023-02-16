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

namespace IntegrationTests

{

    // this class to be used to make a connection to in memory databse without making it every time 
    public class ConnectionClass : IDisposable

    {
        public P3Referential CreateInMemoryDatabase()
        {
            // setup the connection options 
            var option = new DbContextOptionsBuilder<P3Referential>().UseInMemoryDatabase(databaseName: "P3Referential-2f561d3b-493f-46fd-83c9-6e2643e7bd0a").Options;
            // pass the option to the base connection class 
            var context = new P3Referential(option);

            // validate the context 
            if (context != null)
            {
                // delete old database and create a new one 
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }

            return context;
        }
        protected virtual void Dispose(bool disposing) {
           
        } 

        // mandaotroy to close the connection between the controller and database mandatory by Idisposal Interface 
        public void Dispose() { Dispose(true); }

    }
    public class IntegrationTests
    {

        [Fact]
        public void GetAllProductsInMemoryDatabase()
        {

            // object for the connection class 
            var connection = new ConnectionClass();


            // using in scope call to force destroy the scope after complete 
            using (var  context = connection.CreateInMemoryDatabase())
            {
                var product = new Product
                {
                    Name = "Test",
                    Price = 10,
                    Description = "product",
                    Details = "first product",
                    Quantity = 1,
                };
                var product2 = new Product
                {
                    Name = "Test2",
                    Price = 10,
                    Description = "product2",
                    Details = "second product",
                    Quantity = 5,
                };

                var productRepo= new ProductRepository(context);
                productRepo.SaveProduct(product);
                productRepo.SaveProduct(product2);

                var results =  productRepo.GetAllProducts();

                Assert.NotEmpty(results.ToList());  
            }
        }

    }
}
