using back_end.Controllers;
using back_end.Models;
using back_end.Services;
using Castle.Core.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.BackEnd.Passwords
{
    public class PasswordControllerTests
    {
        [Fact]
        public void When_ValidPasswordIsPassedIn_Then_ControllerStatusIs__Ok()
        {
            // Arrange
            string validPassword = "MyPassw0rd!";
            var loggerMockService = new Mock<ILogger<PasswordController>>();
            var mockService = new Mock<IPasswordService>();
            mockService
                .Setup(service => service.ValidatePassword(validPassword))
                .Returns("");

            var controller = new PasswordController(loggerMockService.Object, mockService.Object);

            // Act
            var result = controller.SetPassword(new PasswordChangeRequest() { Password = validPassword });

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public void When_InValidPasswordIsPassedIn_Then_ControllerStatusIs__BadRequest()
        {
            // Arrange
            string password = "password";
            var loggerMockService = new Mock<ILogger<PasswordController>>();
            var mockService = new Mock<IPasswordService>();
            mockService
                .Setup(service => service.ValidatePassword(password))
                .Returns("Example validation message");

            var controller = new PasswordController(loggerMockService.Object, mockService.Object);

            // Act
            var result = controller.SetPassword(new PasswordChangeRequest() { Password = password });

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
