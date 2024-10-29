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
    public class PolicyCategoryFunctionTests
    {
        private PolicyCategoryFunction _policyCategoryFunctions;
        private Mock<IPolicyCategoryDataManager> _mockPolicyCategoryDataManager;
        private Mock<ILogger<PolicyCategoryFunction>> _mockLogger;

        [SetUp]
        public void Setup()
        {
            _mockPolicyCategoryDataManager = new Mock<IPolicyCategoryDataManager>();
            _mockLogger = new Mock<ILogger<PolicyCategoryFunction>>();
            _policyCategoryFunctions = new PolicyCategoryFunction(_mockLogger.Object, _mockPolicyCategoryDataManager.Object);
        }

        [Test]
        public async Task GetPolicyCategories_ShouldReturnOkObjectResult_WithData()
        {
            // Arrange
            var policyCategories = new List<PolicyCategory>
        {
            new PolicyCategory { PolicyCategoryId = 1, Name = "Category 1" },
            new PolicyCategory { PolicyCategoryId = 2, Name = "Category 2" }
        };

            _mockPolicyCategoryDataManager.Setup(x => x.GetAllPolicyCategoryAsync()).ReturnsAsync(policyCategories);
            var mockHttpRequest = new Mock<HttpRequest>();

            // Act
            var response = await _policyCategoryFunctions.GetPolicyCategories(mockHttpRequest.Object);

            // Assert
            Assert.That(response, Is.InstanceOf<OkObjectResult>());
            var okResult = response as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);
            Assert.That(okResult.Value, Is.EqualTo(policyCategories));
        }

        [Test]
        public async Task GetPolicyCategories_ShouldReturnInternalServerError_WhenExceptionThrown()
        {
            // Arrange
            _mockPolicyCategoryDataManager.Setup(x => x.GetAllPolicyCategoryAsync()).ThrowsAsync(new Exception("Database error"));
            var mockHttpRequest = new Mock<HttpRequest>();

            // Act
            var response = await _policyCategoryFunctions.GetPolicyCategories(mockHttpRequest.Object);

            // Assert
            Assert.That(response, Is.InstanceOf<StatusCodeResult>());
            var statusCodeResult = response as StatusCodeResult;
            Assert.That(statusCodeResult, Is.Not.Null);
            Assert.That(statusCodeResult.StatusCode, Is.EqualTo(StatusCodes.Status500InternalServerError));
        }

        [Test]
        public async Task GetPolicyCategoryById_ShouldReturnOkObjectResult_WithValidId()
        {
            // Arrange
            var policyCategory = new PolicyCategory { PolicyCategoryId = 1, Name = "Category 1" };
            long policyCategoryId = 1;

            _mockPolicyCategoryDataManager.Setup(x => x.GetPolicyCategoryByIdAsync(policyCategoryId)).ReturnsAsync(policyCategory);
            var mockHttpRequest = new Mock<HttpRequest>();

            // Act
            var response = await _policyCategoryFunctions.GetPolicyCategoryById(mockHttpRequest.Object, policyCategoryId);

            // Assert
            Assert.That(response, Is.InstanceOf<OkObjectResult>());
            var okResult = response as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);
            Assert.That(okResult.Value, Is.EqualTo(policyCategory));
        }

        [Test]
        public async Task GetPolicyCategoryById_ShouldReturnInternalServerError_WhenExceptionThrown()
        {
            // Arrange
            long policyCategoryId = 1;
            _mockPolicyCategoryDataManager.Setup(x => x.GetPolicyCategoryByIdAsync(policyCategoryId)).ThrowsAsync(new Exception("Database error"));
            var mockHttpRequest = new Mock<HttpRequest>();

            // Act
            var response = await _policyCategoryFunctions.GetPolicyCategoryById(mockHttpRequest.Object, policyCategoryId);

            // Assert
            Assert.That(response, Is.InstanceOf<StatusCodeResult>());
            var statusCodeResult = response as StatusCodeResult;
            Assert.That(statusCodeResult, Is.Not.Null);
            Assert.That(statusCodeResult.StatusCode, Is.EqualTo(StatusCodes.Status500InternalServerError));
        }

        [Test]
        public async Task SavePolicyCategory_ShouldReturnOkObjectResult_WhenValidPolicyCategoryIsProvided()
        {
            // Arrange
            var policyCategory = new PolicyCategory { PolicyCategoryId = 1, Name = "Category 1" };
            var requestBody = JsonConvert.SerializeObject(policyCategory);
            var mockHttpRequest = new Mock<HttpRequest>();
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(requestBody));
            mockHttpRequest.Setup(req => req.Body).Returns(stream);

            _mockPolicyCategoryDataManager.Setup(x => x.InsertOrUpdatePolicyCategoryAsync(policyCategory)).ReturnsAsync(policyCategory);

            // Act
            var response = await _policyCategoryFunctions.SavePolicyCategory(mockHttpRequest.Object);

            // Assert
            Assert.That(response, Is.InstanceOf<OkObjectResult>());
            var okResult = response as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);
            Assert.That(okResult.Value, Is.EqualTo(policyCategory));
        }

        [Test]
        public async Task SavePolicyCategory_ShouldReturnBadRequest_WhenBodyIsNull()
        {
            // Arrange
            var mockHttpRequest = new Mock<HttpRequest>();
            mockHttpRequest.Setup(req => req.Body).Returns((Stream)null);

            // Act
            var response = await _policyCategoryFunctions.SavePolicyCategory(mockHttpRequest.Object);

            // Assert
            Assert.That(response, Is.InstanceOf<BadRequestObjectResult>());
            var badRequestResult = response as BadRequestObjectResult;
            Assert.That(badRequestResult, Is.Not.Null);
            Assert.That(badRequestResult.Value, Is.EqualTo("valid policy category object NOT provided"));
        }

        [Test]
        public async Task SavePolicyCategory_ShouldReturnBadRequest_WhenBodyIsEmpty()
        {
            // Arrange
            var mockHttpRequest = new Mock<HttpRequest>();
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(string.Empty));
            mockHttpRequest.Setup(req => req.Body).Returns(stream);

            // Act
            var response = await _policyCategoryFunctions.SavePolicyCategory(mockHttpRequest.Object);

            // Assert
            Assert.That(response, Is.InstanceOf<BadRequestObjectResult>());
            var badRequestResult = response as BadRequestObjectResult;
            Assert.That(badRequestResult, Is.Not.Null);
            Assert.That(badRequestResult.Value, Is.EqualTo("valid policy category object not provided"));
        }

        [Test]
        public async Task SavePolicyCategory_ShouldReturnBadRequest_WhenDeserializationFails()
        {
            // Arrange
            var invalidJson = "{ invalid json }"; // Invalid JSON
            var mockHttpRequest = new Mock<HttpRequest>();
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(invalidJson));
            mockHttpRequest.Setup(req => req.Body).Returns(stream);

            // Act
            var response = await _policyCategoryFunctions.SavePolicyCategory(mockHttpRequest.Object);

            // Assert
            Assert.That(response, Is.InstanceOf<BadRequestObjectResult>());
            var badRequestResult = response as BadRequestObjectResult;
            Assert.That(badRequestResult, Is.Not.Null);
            Assert.That(badRequestResult.Value, Is.EqualTo("valid policy category object not provided"));
        }

        [Test]
        public async Task SavePolicyCategory_ShouldReturnInternalServerError_WhenExceptionThrown()
        {
            // Arrange
            var policyCategory = new PolicyCategory { PolicyCategoryId = 1, Name = "Category 1" };
            var requestBody = JsonConvert.SerializeObject(policyCategory);
            var mockHttpRequest = new Mock<HttpRequest>();
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(requestBody));
            mockHttpRequest.Setup(req => req.Body).Returns(stream);

            _mockPolicyCategoryDataManager.Setup(x => x.InsertOrUpdatePolicyCategoryAsync(policyCategory)).ThrowsAsync(new Exception("Database error"));

            // Act
            var response = await _policyCategoryFunctions.SavePolicyCategory(mockHttpRequest.Object);

            // Assert
            Assert.That(response, Is.InstanceOf<StatusCodeResult>());
            var statusCodeResult = response as StatusCodeResult;
            Assert.That(statusCodeResult, Is.Not.Null);
            Assert.That(statusCodeResult.StatusCode, Is.EqualTo(StatusCodes.Status500InternalServerError));
        }
    }
}
