using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using NyanSpecialty.Assistance.Web.Api.Functions;
using NyanSpecialty.Assistance.Web.Data.Manager;
using NyanSpecialty.Assistance.Web.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NyanSpecialty.Assistance.Web.Api.Tests
{
    [TestFixture]
    public class PolicyTypeFunctionTests
    {
        private PolicyTypeFunction _policyTypeFunctions;
        private Mock<IPolicyTypeDataManager> _mockPolicyTypeDataManager;
        private Mock<ILogger<PolicyTypeFunction>> _mockLogger;

        [SetUp]
        public void Setup()
        {
            _mockPolicyTypeDataManager = new Mock<IPolicyTypeDataManager>();
            _mockLogger = new Mock<ILogger<PolicyTypeFunction>>();
            _policyTypeFunctions = new PolicyTypeFunction(_mockLogger.Object, _mockPolicyTypeDataManager.Object);
        }

        [Test]
        public async Task GetAllPolicyTypes_ShouldReturnOkObjectResult_WithData()
        {
            // Arrange
            var policyTypes = new List<PolicyType>
            {
                new PolicyType { PolicyTypeId = 1, Code = "Corp", Name = "Corporate" },
                new PolicyType { PolicyTypeId = 2, Code = "Pers", Name = "Personal" }
            };

            _mockPolicyTypeDataManager.Setup(x => x.GetAllPolicyTypeAsync()).ReturnsAsync(policyTypes);
            var mockHttpRequest = new Mock<HttpRequest>();

            // Act
            var response = await _policyTypeFunctions.GetAllPolicyTypes(mockHttpRequest.Object);

            // Assert
            Assert.That(response, Is.InstanceOf<OkObjectResult>());
            var okResult = response as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);
            Assert.That(okResult.Value, Is.EqualTo(policyTypes));
        }

        [Test]
        public async Task GetAllPolicyTypes_ShouldReturnInternalServerError_WhenExceptionThrown()
        {
            // Arrange
            _mockPolicyTypeDataManager.Setup(x => x.GetAllPolicyTypeAsync()).ThrowsAsync(new Exception("Database error"));
            var mockHttpRequest = new Mock<HttpRequest>();

            // Act
            var response = await _policyTypeFunctions.GetAllPolicyTypes(mockHttpRequest.Object);

            // Assert
            Assert.That(response, Is.InstanceOf<StatusCodeResult>());
            var statusCodeResult = response as StatusCodeResult;
            Assert.That(statusCodeResult, Is.Not.Null);
            Assert.That(statusCodeResult.StatusCode, Is.EqualTo(StatusCodes.Status500InternalServerError));
            _mockLogger.Verify(logger => logger.LogError(It.Is<string>(s => s.Contains("Error retrieving policy classes: Database error"))), Times.Once);
        }

        [Test]
        public async Task GetAllPolicyTypes_ShouldLogInformation_WhenInvoked()
        {
            // Arrange
            var mockHttpRequest = new Mock<HttpRequest>();

            // Act
            await _policyTypeFunctions.GetAllPolicyTypes(mockHttpRequest.Object);

            // Assert
            _mockLogger.Verify(logger => logger.LogInformation("PolicyTypeFunction.GetPolicyTypes Invoked."), Times.Once);
        }

        [Test]
        public async Task GetAllPolicyTypes_ShouldReturnOkObjectResult_WithEmptyList()
        {
            // Arrange
            var policyTypes = new List<PolicyType>(); // Empty list
            _mockPolicyTypeDataManager.Setup(x => x.GetAllPolicyTypeAsync()).ReturnsAsync(policyTypes);
            var mockHttpRequest = new Mock<HttpRequest>();

            // Act
            var response = await _policyTypeFunctions.GetAllPolicyTypes(mockHttpRequest.Object);

            // Assert
            Assert.That(response, Is.InstanceOf<OkObjectResult>());
            var okResult = response as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);
            Assert.That(okResult.Value, Is.EqualTo(policyTypes));
        }

        [Test]
        public async Task GetAllPolicyTypes_ShouldReturnInternalServerError_WhenDataManagerReturnsNull()
        {
            // Arrange
            _mockPolicyTypeDataManager.Setup(x => x.GetAllPolicyTypeAsync()).ReturnsAsync((List<PolicyType>)null);
            var mockHttpRequest = new Mock<HttpRequest>();

            // Act
            var response = await _policyTypeFunctions.GetAllPolicyTypes(mockHttpRequest.Object);

            // Assert
            Assert.That(response, Is.InstanceOf<StatusCodeResult>());
            var statusCodeResult = response as StatusCodeResult;
            Assert.That(statusCodeResult, Is.Not.Null);
            Assert.That(statusCodeResult.StatusCode, Is.EqualTo(StatusCodes.Status500InternalServerError));
            _mockLogger.Verify(logger => logger.LogError(It.Is<string>(s => s.Contains("Error retrieving policy classes: Object reference not set to an instance of an object"))), Times.Once);
        }

        [Test]
        public async Task GetAllPolicyTypes_ShouldReturnInternalServerError_WhenDataManagerThrowsArgumentNullException()
        {
            // Arrange
            _mockPolicyTypeDataManager.Setup(x => x.GetAllPolicyTypeAsync()).ThrowsAsync(new ArgumentNullException("parameter", "Parameter cannot be null"));
            var mockHttpRequest = new Mock<HttpRequest>();

            // Act
            var response = await _policyTypeFunctions.GetAllPolicyTypes(mockHttpRequest.Object);

            // Assert
            Assert.That(response, Is.InstanceOf<StatusCodeResult>());
            var statusCodeResult = response as StatusCodeResult;
            Assert.That(statusCodeResult, Is.Not.Null);
            Assert.That(statusCodeResult.StatusCode, Is.EqualTo(StatusCodes.Status500InternalServerError));
            _mockLogger.Verify(logger => logger.LogError(It.Is<string>(s => s.Contains("Error retrieving policy classes: Parameter cannot be null"))), Times.Once);
        }

        [Test]
        public async Task GetAllPolicyTypes_ShouldReturnInternalServerError_WhenDataManagerThrowsArgumentException()
        {
            // Arrange
            _mockPolicyTypeDataManager.Setup(x => x.GetAllPolicyTypeAsync()).ThrowsAsync(new ArgumentException("Invalid argument", "parameter"));
            var mockHttpRequest = new Mock<HttpRequest>();

            // Act
            var response = await _policyTypeFunctions.GetAllPolicyTypes(mockHttpRequest.Object);

            // Assert
            Assert.That(response, Is.InstanceOf<StatusCodeResult>());
            var statusCodeResult = response as StatusCodeResult;
            Assert.That(statusCodeResult, Is.Not.Null);
            Assert.That(statusCodeResult.StatusCode, Is.EqualTo(StatusCodes.Status500InternalServerError));
            _mockLogger.Verify(logger => logger.LogError(It.Is<string>(s => s.Contains("Error retrieving policy classes: Invalid argument"))), Times.Once);
        }
    }
}