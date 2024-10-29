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
    public class InsurancePolicyFunctionsTests
    {
        private InsurancePolicyFunctions _insurancePolicyFunctions;
        private Mock<IInsurancePolicyDataManager> _mockInsurancePolicyDataManager;
        private Mock<ILogger<InsurancePolicyFunctions>> _mockLogger;

        [SetUp]
        public void Setup()
        {
            _mockInsurancePolicyDataManager = new Mock<IInsurancePolicyDataManager>();
            _mockLogger = new Mock<ILogger<InsurancePolicyFunctions>>();
            _insurancePolicyFunctions = new InsurancePolicyFunctions(_mockInsurancePolicyDataManager.Object, _mockLogger.Object);
        }

        [Test]
        public async Task GetInsurancePolicies_ShouldReturnOkObjectResult_WithData()
        {
            // Arrange
            var insurancePolicies = new List<InsurancePolicy>
        {
            new InsurancePolicy { InsurancePolicyId = 1, Education = "Policy 1" },
            new InsurancePolicy { InsurancePolicyId = 2, Education = "Policy 2" }
        };

            _mockInsurancePolicyDataManager.Setup(x => x.GetAllInsurancePoliciesAsync()).ReturnsAsync(insurancePolicies);
            var mockHttpRequest = new Mock<HttpRequest>();

            // Act
            var response = await _insurancePolicyFunctions.GetInsurancePolicies(mockHttpRequest.Object);

            // Assert
            Assert.That(response, Is.InstanceOf<OkObjectResult>());
            var okResult = response as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);
            Assert.That(okResult.Value, Is.EqualTo(insurancePolicies));
        }

        [Test]
        public async Task GetInsurancePolicies_ShouldReturnInternalServerError_WhenExceptionThrown()
        {
            // Arrange
            _mockInsurancePolicyDataManager.Setup(x => x.GetAllInsurancePoliciesAsync()).ThrowsAsync(new Exception("Database error"));
            var mockHttpRequest = new Mock<HttpRequest>();

            // Act
            var response = await _insurancePolicyFunctions.GetInsurancePolicies(mockHttpRequest.Object);

            // Assert
            Assert.That(response, Is.InstanceOf<StatusCodeResult>());
            var statusCodeResult = response as StatusCodeResult;
            Assert.That(statusCodeResult, Is.Not.Null);
            Assert.That(statusCodeResult.StatusCode, Is.EqualTo(StatusCodes.Status500InternalServerError));
        }

        [Test]
        public async Task GetInsurancePolicyById_ShouldReturnOkObjectResult_WithValidId()
        {
            // Arrange
            var insurancePolicy = new InsurancePolicy { InsurancePolicyId = 1, CustomerEmail = "Policy 1" };
            long insurancePolicyId = 1;

            _mockInsurancePolicyDataManager.Setup(x => x.GetInsurancePolicyByIdAsync(insurancePolicyId)).ReturnsAsync(insurancePolicy);
            var mockHttpRequest = new Mock<HttpRequest>();

            // Act
            var response = await _insurancePolicyFunctions.GetInsurancePolicies(mockHttpRequest.Object, insurancePolicyId);

            // Assert
            Assert.That(response, Is.InstanceOf<OkObjectResult>());
            var okResult = response as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);
            Assert.That(okResult.Value, Is.EqualTo(insurancePolicy));
        }

        
        [Test]
        public async Task SaveInsurancePolicy_ShouldReturnOkObjectResult_WhenValidPolicyIsProvided()
        {
            // Arrange
            var insurancePolicy = new InsurancePolicy { InsurancePolicyId = 1, CustomerEmail = "Policy 1" };
            var requestBody = JsonConvert.SerializeObject(insurancePolicy);
            var mockHttpRequest = new Mock<HttpRequest>();
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(requestBody));
            mockHttpRequest.Setup(req => req.Body).Returns(stream);

            _mockInsurancePolicyDataManager.Setup(x => x.InsertOrUpdateInsurancePolicyAsync(insurancePolicy)).Returns(Task.CompletedTask);

            // Act
            var response = await _insurancePolicyFunctions.SaveInsurancePolicy(mockHttpRequest.Object);

            // Assert
            Assert.That(response, Is.InstanceOf<OkObjectResult>());
            var okResult = response as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);
            Assert.That(okResult.Value, Is.EqualTo(true));
        }

        [Test]
        public async Task SaveInsurancePolicy_ShouldReturnBadRequest_WhenBodyIsNull()
        {
            // Arrange
            var mockHttpRequest = new Mock<HttpRequest>();
            mockHttpRequest.Setup(req => req.Body).Returns((Stream)null);

            // Act
            var response = await _insurancePolicyFunctions.SaveInsurancePolicy(mockHttpRequest.Object);

            // Assert
            Assert.That(response, Is.InstanceOf<BadRequestObjectResult>());
            var badRequestResult = response as BadRequestObjectResult;
            Assert.That(badRequestResult, Is.Not.Null);
            Assert.That(badRequestResult.Value, Is.EqualTo("Valid insurance policy object NOT provided"));
        }

        [Test]
        public async Task SaveInsurancePolicy_ShouldReturnBadRequest_WhenBodyIsEmpty()
        {
            // Arrange
            var mockHttpRequest = new Mock<HttpRequest>();
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(string.Empty));
            mockHttpRequest.Setup(req => req.Body).Returns(stream);

            // Act
            var response = await _insurancePolicyFunctions.SaveInsurancePolicy(mockHttpRequest.Object);

            // Assert
            Assert.That(response, Is.InstanceOf<BadRequestObjectResult>());
            var badRequestResult = response as BadRequestObjectResult;
            Assert.That(badRequestResult, Is.Not.Null);
            Assert.That(badRequestResult.Value, Is.EqualTo("Valid insurance policy object NOT provided"));
        }

        [Test]
        public async Task SaveInsurancePolicy_ShouldReturnBadRequest_WhenDeserializationFails()
        {
            // Arrange
            var invalidJson = "{ invalid json }"; // Invalid JSON
            var mockHttpRequest = new Mock<HttpRequest>();
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(invalidJson));
            mockHttpRequest.Setup(req => req.Body).Returns(stream);

            // Act
            var response = await _insurancePolicyFunctions.SaveInsurancePolicy(mockHttpRequest.Object);

            // Assert
            Assert.That(response, Is.InstanceOf<BadRequestObjectResult>());
            var badRequestResult = response as BadRequestObjectResult;
            Assert.That(badRequestResult, Is.Not.Null);
            Assert.That(badRequestResult.Value, Is.EqualTo("Valid insurance policy object NOT provided"));
        }

        [Test]
        public async Task SaveInsurancePolicy_ShouldReturnInternalServerError_WhenExceptionThrown()
        {
            // Arrange
            var insurancePolicy = new InsurancePolicy { InsurancePolicyId = 1, EmploymentStatus = "Policy 1" };
            var requestBody = JsonConvert.SerializeObject(insurancePolicy);
            var mockHttpRequest = new Mock<HttpRequest>();
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(requestBody));
            mockHttpRequest.Setup(req => req.Body).Returns(stream);

            _mockInsurancePolicyDataManager.Setup(x => x.InsertOrUpdateInsurancePolicyAsync(insurancePolicy)).ThrowsAsync(new Exception("Database error"));

            // Act
            var response = await _insurancePolicyFunctions.SaveInsurancePolicy(mockHttpRequest.Object);

            // Assert
            Assert.That(response, Is.InstanceOf<StatusCodeResult>());
            var statusCodeResult = response as StatusCodeResult;
            Assert.That(statusCodeResult, Is.Not.Null);
            Assert.That(statusCodeResult.StatusCode, Is.EqualTo(StatusCodes.Status500InternalServerError));
        }

        [Test]
        public async Task UploadInsurancePolicies_ShouldReturnOkObjectResult_WhenValidPoliciesAreProvided()
        {
            // Arrange
            var insurancePolicies = new List<InsurancePolicy>
        {
            new InsurancePolicy { InsurancePolicyId = 1, EmploymentStatus = "Policy 1" },
            new InsurancePolicy { InsurancePolicyId = 2, EmploymentStatus = "Policy 2" }
        };
            var requestBody = JsonConvert.SerializeObject(insurancePolicies);
            var mockHttpRequest = new Mock<HttpRequest>();
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(requestBody));
            mockHttpRequest.Setup(req => req.Body).Returns(stream);

            _mockInsurancePolicyDataManager.Setup(x => x.InsertOrUpdateInsurancePolicyAsync(insurancePolicies)).Returns(Task.CompletedTask);

            // Act
            var response = await _insurancePolicyFunctions.UploadInsurancePolicies(mockHttpRequest.Object);

            // Assert
            Assert.That(response, Is.InstanceOf<OkObjectResult>());
            var okResult = response as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);
            Assert.That(okResult.Value, Is.EqualTo(true));
        }

        [Test]
        public async Task UploadInsurancePolicies_ShouldReturnBadRequest_WhenBodyIsNull()
        {
            // Arrange
            var mockHttpRequest = new Mock<HttpRequest>();
            mockHttpRequest.Setup(req => req.Body).Returns((Stream)null);

            // Act
            var response = await _insurancePolicyFunctions.UploadInsurancePolicies(mockHttpRequest.Object);

            // Assert
            Assert.That(response, Is.InstanceOf<BadRequestObjectResult>());
            var badRequestResult = response as BadRequestObjectResult;
            Assert.That(badRequestResult, Is.Not.Null);
            Assert.That(badRequestResult.Value, Is.EqualTo("Valid insurance policy object NOT provided"));
        }

        [Test]
        public async Task UploadInsurancePolicies_ShouldReturnBadRequest_WhenBodyIsEmpty()
        {
            // Arrange
            var mockHttpRequest = new Mock<HttpRequest>();
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(string.Empty));
            mockHttpRequest.Setup(req => req.Body).Returns(stream);

            // Act
            var response = await _insurancePolicyFunctions.UploadInsurancePolicies(mockHttpRequest.Object);

            // Assert
            Assert.That(response, Is.InstanceOf<BadRequestObjectResult>());
            var badRequestResult = response as BadRequestObjectResult;
            Assert.That(badRequestResult, Is.Not.Null);
            Assert.That(badRequestResult.Value, Is.EqualTo("Valid insurance policy object NOT provided"));
        }

        [Test]
        public async Task UploadInsurancePolicies_ShouldReturnBadRequest_WhenDeserializationFails()
        {
            // Arrange
            var invalidJson = "{ invalid json }"; // Invalid JSON
            var mockHttpRequest = new Mock<HttpRequest>();
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(invalidJson));
            mockHttpRequest.Setup(req => req.Body).Returns(stream);

            // Act
            var response = await _insurancePolicyFunctions.UploadInsurancePolicies(mockHttpRequest.Object);

            // Assert
            Assert.That(response, Is.InstanceOf<BadRequestObjectResult>());
            var badRequestResult = response as BadRequestObjectResult;
            Assert.That(badRequestResult, Is.Not.Null);
            Assert.That(badRequestResult.Value, Is.EqualTo("Valid insurance policy object NOT provided"));
        }

        [Test]
        public async Task UploadInsurancePolicies_ShouldReturnInternalServerError_WhenExceptionThrown()
        {
            // Arrange
            var insurancePolicies = new List<InsurancePolicy>
        {
            new InsurancePolicy {InsurancePolicyId = 1, EmploymentStatus = "Policy 1"},
            new InsurancePolicy {InsurancePolicyId = 2, EmploymentStatus = "Policy 2"}
        };
            var requestBody = JsonConvert.SerializeObject(insurancePolicies);
            var mockHttpRequest = new Mock<HttpRequest>();
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(requestBody));
            mockHttpRequest.Setup(req => req.Body).Returns(stream);

            _mockInsurancePolicyDataManager.Setup(x => x.InsertOrUpdateInsurancePolicyAsync(insurancePolicies)).ThrowsAsync(new Exception("Database error"));

            // Act
            var response = await _insurancePolicyFunctions.UploadInsurancePolicies(mockHttpRequest.Object);

            // Assert
            Assert.That(response, Is.InstanceOf<StatusCodeResult>());
            var statusCodeResult = response as StatusCodeResult;
            Assert.That(statusCodeResult, Is.Not.Null);
            Assert.That(statusCodeResult.StatusCode, Is.EqualTo(StatusCodes.Status500InternalServerError));
        }
    }
}
