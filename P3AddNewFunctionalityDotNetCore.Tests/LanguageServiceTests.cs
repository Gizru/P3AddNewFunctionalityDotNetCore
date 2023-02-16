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

namespace LanguageServiceTests
{
    public class SetCulture
    {

        [Fact]
        public void CheckDefault()

        {
            Mock<ILanguageService> mockLanguageService = new Mock<ILanguageService>();

            LanguageService languageService = new LanguageService();

            var result = languageService.SetCulture("fiuvgduhai");

            Assert.Equal("en", result);
        }

        [Fact]
        public void CheckFrench()

        {
            Mock<ILanguageService> mockLanguageService = new Mock<ILanguageService>();

            LanguageService languageService = new LanguageService();

            var result = languageService.SetCulture("French");

            Assert.Equal("fr", result);
        }

    }
}
