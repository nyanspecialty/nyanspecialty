using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using NyanSpecialty.Assistance.Web.Api.Functions;
using NyanSpecialty.Assistance.Web.Data.Manager;
using NyanSpecialty.Assistance.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NyanSpecialty.Assistance.Web.Api.Tests
{
    [TestFixture]
    public class VehicleClassFunctionsTests
    {
        private VehicleClassFunctions _vehicleClassFunctions;
        private Mock<IVehicleClassDataManager> _mockVehicleClassDataManager;
        private Mock<ILogger<VehicleClassFunctions>> _mockLogger;

        [SetUp]
        public void Setup()
        {
            _mockVehicleClassDataManager = new Mock<IVehicleClassDataManager>();
            _mockLogger = new Mock<ILogger<VehicleClassFunctions>>();
            _vehicleClassFunctions = new VehicleClassFunctions(_mockVehicleClassDataManager.Object, _mockLogger.Object);
        }

        [Test]
        public async Task GetVehicleClassById_ShouldReturnOkObjectResult_WithValidId()
        {
            // Arrange
            var vehicleClass = new VehicleClass { VehicleClassId = 1, Name = "Sedan" };
            long vehicleClassId = 1;

            _mockVehicleClassDataManager.Setup(x => x.GetVehicleClassAsync(vehicleClassId)).ReturnsAsync(vehicleClass);
            var mockHttpRequest = new Mock<HttpRequest>();

            // Act
            var response = await _vehicleClassFunctions.GetVehicleClassById(mockHttpRequest.Object, vehicleClassId);

            // Assert
            Assert.That(response, Is.InstanceOf<OkObjectResult>());
            var okResult = response as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);
            Assert.That(okResult.Value, Is.EqualTo(vehicleClass));
        }

        [Test]
        public async Task GetVehicleClassById_ShouldReturnInternalServerError_WhenExceptionThrown()
        {
            // Arrange
            long vehicleClassId = 1;
            _mockVehicleClassDataManager.Setup(x => x.GetVehicleClassAsync(vehicleClassId)).ThrowsAsync(new Exception("Database error"));
            var mockHttpRequest = new Mock<HttpRequest>();

            // Act
            var response = await _vehicleClassFunctions.GetVehicleClassById(mockHttpRequest.Object, vehicleClassId);

            // Assert
            Assert.That(response, Is.InstanceOf<StatusCodeResult>());
            var statusCodeResult = response as StatusCodeResult;
            Assert.That(statusCodeResult, Is.Not.Null);
            Assert.That(statusCodeResult.StatusCode, Is.EqualTo(StatusCodes.Status500InternalServerError));
        }

        [Test]
        public async Task GetVehicleClasses_ShouldReturnOkObjectResult_WithData()
        {
            // Arrange
            var vehicleClasses = new List<VehicleClass>
        {
            new VehicleClass { VehicleClassId = 1, Name = "Sedan" },
            new VehicleClass { VehicleClassId = 2, Name = "SUV" }
        };

            _mockVehicleClassDataManager.Setup(x => x.GetAllVehicleClassesAsync()).ReturnsAsync(vehicleClasses);
            var mockHttpRequest = new Mock<HttpRequest>();

            // Act
            var response = await _vehicleClassFunctions.GetVehicleClasses(mockHttpRequest.Object);

            // Assert
            Assert.That(response, Is.InstanceOf<OkObjectResult>());
            var okResult = response as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);
            Assert.That(okResult.Value, Is.EqualTo(vehicleClasses));
        }

        [Test]
        public async Task GetVehicleClasses_ShouldReturnInternalServerError_WhenExceptionThrown()
        {
            // Arrange
            _mockVehicleClassDataManager.Setup(x => x.GetAllVehicleClassesAsync()).ThrowsAsync(new Exception("Database error"));
            var mockHttpRequest = new Mock<HttpRequest>();

            // Act
            var response = await _vehicleClassFunctions.GetVehicleClasses(mockHttpRequest.Object);

            // Assert
            Assert.That(response, Is.InstanceOf<StatusCodeResult>());
            var statusCodeResult = response as StatusCodeResult;
            Assert.That(statusCodeResult, Is.Not.Null);
            Assert.That(statusCodeResult.StatusCode, Is.EqualTo(StatusCodes.Status500InternalServerError));
        }

        [Test]
        public async Task SaveVehicleClass_ShouldReturnOkObjectResult_WhenValidVehicleClassIsProvided()
        {
            // Arrange
            var vehicleClass = new VehicleClass { VehicleClassId = 1, Name = "Sedan" };
            var requestBody = JsonConvert.SerializeObject(vehicleClass);
            var mockHttpRequest = new Mock<HttpRequest>();
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(requestBody));
            mockHttpRequest.Setup(req => req.Body).Returns(stream);

            _mockVehicleClassDataManager.Setup(x => x.InsertOrUpdateVehicleClassAsync(vehicleClass)).ReturnsAsync(vehicleClass);

            // Act
            var response = await _vehicleClassFunctions.SaveVehicleClass(mockHttpRequest.Object);

            // Assert
            Assert.That(response, Is.InstanceOf<OkObjectResult>());
            var okResult = response as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);
            Assert.That(okResult.Value, Is.EqualTo(vehicleClass));
        }

        [Test]
        public async Task SaveVehicleClass_ShouldReturnBadRequest_WhenBodyIsNull()
        {
            // Arrange
            var mockHttpRequest = new Mock<HttpRequest>();
            mockHttpRequest.Setup(req => req.Body).Returns((Stream)null);

            // Act
            var response = await _vehicleClassFunctions.SaveVehicleClass(mockHttpRequest.Object);

            // Assert
            Assert.That(response, Is.InstanceOf<BadRequestObjectResult>());
            var badRequestResult = response as BadRequestObjectResult;
            Assert.That(badRequestResult, Is.Not.Null);
            Assert.That(badRequestResult.Value, Is.EqualTo("valid vehicleclass object NOT provided"));
        }

        [Test]
        public async Task SaveVehicleClass_ShouldReturnBadRequest_WhenBodyIsEmpty()
        {
            // Arrange
            var mockHttpRequest = new Mock<HttpRequest>();
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(string.Empty));
            mockHttpRequest.Setup(req => req.Body).Returns(stream);

            // Act
            var response = await _vehicleClassFunctions.SaveVehicleClass(mockHttpRequest.Object);

            // Assert
            Assert.That(response, Is.InstanceOf<BadRequestObjectResult>());
            var badRequestResult = response as BadRequestObjectResult;
            Assert.That(badRequestResult, Is.Not.Null);
            Assert.That(badRequestResult.Value, Is.EqualTo("valid vehicleclass object not provided"));
        }

        [Test]
        public async Task SaveVehicleClass_ShouldReturnBadRequest_WhenDeserializationFails()
        {
            // Arrange
            var invalidJson = "{ invalid json }"; // Invalid JSON
            var mockHttpRequest = new Mock<HttpRequest>();
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(invalidJson));
            mockHttpRequest.Setup(req => req.Body).Returns(stream);

            // Act
            var response = await _vehicleClassFunctions.SaveVehicleClass(mockHttpRequest.Object);

            // Assert
            Assert.That(response, Is.InstanceOf<BadRequestObjectResult>());
            var badRequestResult = response as BadRequestObjectResult;
            Assert.That(badRequestResult, Is.Not.Null);
            Assert.That(badRequestResult.Value, Is.EqualTo("valid vehicleclass object not provided"));
        }

        [Test]
        public async Task SaveVehicleClass_ShouldReturnInternalServerError_WhenExceptionThrown()
        {
            // Arrange
            var vehicleClass = new VehicleClass { VehicleClassId = 1, Name = "Sedan" };
            var requestBody = JsonConvert.SerializeObject(vehicleClass);
            var mockHttpRequest = new Mock<HttpRequest>();
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(requestBody));
            mockHttpRequest.Setup(req => req.Body).Returns(stream);

            _mockVehicleClassDataManager.Setup(x => x.InsertOrUpdateVehicleClassAsync(vehicleClass)).ThrowsAsync(new Exception("Database error"));

            // Act
            var response = await _vehicleClassFunctions.SaveVehicleClass(mockHttpRequest.Object);

            // Assert
            Assert.That(response, Is.InstanceOf<StatusCodeResult>());
            var statusCodeResult = response as StatusCodeResult;
            Assert.That(statusCodeResult, Is.Not.Null);
            Assert.That(statusCodeResult.StatusCode, Is.EqualTo(StatusCodes.Status500InternalServerError));
        }
    }
}
