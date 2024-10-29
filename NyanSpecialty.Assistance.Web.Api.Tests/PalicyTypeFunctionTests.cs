using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using NyanSpecialty.Assistance.Web.Api.Functions;
using NyanSpecialty.Assistance.Web.Data.Manager;
using NyanSpecialty.Assistance.Web.Models;
using System.Text;

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
            Assert.That(response, Is.InstanceOf<OkObjectResult>());
            var statusCodeResult = response as OkObjectResult;
            Assert.That(statusCodeResult, Is.Not.Null);
            Assert.That(statusCodeResult.StatusCode, Is.EqualTo(StatusCodes.Status200OK));
           
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
           
        }
        [Test]
        public async Task GetPolicyTypeById_ShouldReturnOkObjectResult_WithValidId()
        {
            // Arrange
            var policyType = new PolicyType { PolicyTypeId = 1, Code = "Corp", Name = "Corporate" };
            long policyTypeId = 1;

            _mockPolicyTypeDataManager.Setup(x => x.GetPolicyTypeByIdAsync(policyTypeId)).ReturnsAsync(policyType);
            var mockHttpRequest = new Mock<HttpRequest>();

            // Act
            var response = await _policyTypeFunctions.GetPolicyTypeById(mockHttpRequest.Object, policyTypeId);

            // Assert
            Assert.That(response, Is.InstanceOf<OkObjectResult>());
            var okResult = response as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);
            Assert.That(okResult.Value, Is.EqualTo(policyType));
        }

        [Test]
        public async Task GetPolicyTypeById_ShouldReturnNotFound_WhenPolicyTypeDoesNotExist()
        {
            // Arrange
            long policyTypeId = 999; // Assuming this ID does not exist
            _mockPolicyTypeDataManager.Setup(x => x.GetPolicyTypeByIdAsync(policyTypeId)).ReturnsAsync((PolicyType)null);
            var mockHttpRequest = new Mock<HttpRequest>();

            // Act
            var response = await _policyTypeFunctions.GetPolicyTypeById(mockHttpRequest.Object, policyTypeId);

            // Assert
            Assert.That(response, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public async Task GetPolicyTypeById_ShouldReturnInternalServerError_WhenExceptionThrown()
        {
            // Arrange
            long policyTypeId = 1;
            _mockPolicyTypeDataManager.Setup(x => x.GetPolicyTypeByIdAsync(policyTypeId)).ThrowsAsync(new Exception("Database error"));
            var mockHttpRequest = new Mock<HttpRequest>();

            // Act
            var response = await _policyTypeFunctions.GetPolicyTypeById(mockHttpRequest.Object, policyTypeId);

            // Assert
            Assert.That(response, Is.InstanceOf<StatusCodeResult>());
            var statusCodeResult = response as StatusCodeResult;
            Assert.That(statusCodeResult, Is.Not.Null);
            Assert.That(statusCodeResult.StatusCode, Is.EqualTo(StatusCodes.Status500InternalServerError));
        }

        [Test]
        public async Task GetPolicyTypeById_ShouldReturnInternalServerError_WhenDataManagerThrowsArgumentNullException()
        {
            // Arrange
            long policyTypeId = 1;
            _mockPolicyTypeDataManager.Setup(x => x.GetPolicyTypeByIdAsync(policyTypeId)).ThrowsAsync(new ArgumentNullException("parameter", "Parameter cannot be null"));
            var mockHttpRequest = new Mock<HttpRequest>();

            // Act
            var response = await _policyTypeFunctions.GetPolicyTypeById(mockHttpRequest.Object, policyTypeId);

            // Assert
            Assert.That(response, Is.InstanceOf<StatusCodeResult>());
            var statusCodeResult = response as StatusCodeResult;
            Assert.That(statusCodeResult, Is.Not.Null);
            Assert.That(statusCodeResult.StatusCode, Is.EqualTo(StatusCodes.Status500InternalServerError));
        }

        [Test]
        public async Task GetPolicyTypeById_ShouldReturnInternalServerError_WhenDataManagerThrowsArgumentException()
        {
            // Arrange
            long policyTypeId = 1;
            _mockPolicyTypeDataManager.Setup(x => x.GetPolicyTypeByIdAsync(policyTypeId)).ThrowsAsync(new ArgumentException("Invalid argument", "parameter"));
            var mockHttpRequest = new Mock<HttpRequest>();

            // Act
            var response = await _policyTypeFunctions.GetPolicyTypeById(mockHttpRequest.Object, policyTypeId);

            // Assert
            Assert.That(response, Is.InstanceOf<StatusCodeResult>());
            var statusCodeResult = response as StatusCodeResult;
            Assert.That(statusCodeResult, Is.Not.Null);
            Assert.That(statusCodeResult.StatusCode, Is.EqualTo(StatusCodes.Status500InternalServerError));
        }
        [Test]
        public async Task SavePolicyType_ShouldReturnOkObjectResult_WhenValidPolicyTypeIsProvided()
        {
            // Arrange
            var policyType = new PolicyType { PolicyTypeId = 1, Code = "Corp", Name = "Corporate" };
            var requestBody = JsonConvert.SerializeObject(policyType);
            var mockHttpRequest = new Mock<HttpRequest>();
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(requestBody));
            mockHttpRequest.Setup(req => req.Body).Returns(stream);

            _mockPolicyTypeDataManager.Setup(x => x.InsertOrUpdatePolicyTypeAsync(policyType)).ReturnsAsync(policyType);

            // Act
            var response = await _policyTypeFunctions.Run(mockHttpRequest.Object);

            // Assert
            Assert.That(response, Is.InstanceOf<OkObjectResult>());
            var okResult = response as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);
            Assert.That(okResult.Value, Is.EqualTo(policyType));
        }

        [Test]
        public async Task SavePolicyType_ShouldReturnBadRequest_WhenBodyIsNull()
        {
            // Arrange
            var mockHttpRequest = new Mock<HttpRequest>();
            mockHttpRequest.Setup(req => req.Body).Returns((Stream)null);

            // Act
            var response = await _policyTypeFunctions.Run(mockHttpRequest.Object);

            // Assert
            Assert.That(response, Is.InstanceOf<BadRequestObjectResult>());
            var badRequestResult = response as BadRequestObjectResult;
            Assert.That(badRequestResult, Is.Not.Null);
            Assert.That(badRequestResult.Value, Is.EqualTo("valid policytype object NOT provided"));
        }

        [Test]
        public async Task SavePolicyType_ShouldReturnBadRequest_WhenBodyIsEmpty()
        {
            // Arrange
            var mockHttpRequest = new Mock<HttpRequest>();
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(string.Empty));
            mockHttpRequest.Setup(req => req.Body).Returns(stream);

            // Act
            var response = await _policyTypeFunctions.Run(mockHttpRequest.Object);

            // Assert
            Assert.That(response, Is.InstanceOf<BadRequestObjectResult>());
            var badRequestResult = response as BadRequestObjectResult;
            Assert.That(badRequestResult, Is.Not.Null);
            Assert.That(badRequestResult.Value, Is.EqualTo("valid policytype object NOT provided"));
        }

        [Test]
        public async Task SavePolicyType_ShouldReturnBadRequest_WhenDeserializationFails()
        {
            // Arrange
            var invalidJson = "{ invalid json }"; // Invalid JSON
            var mockHttpRequest = new Mock<HttpRequest>();
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(invalidJson));
            mockHttpRequest.Setup(req => req.Body).Returns(stream);

            // Act
            var response = await _policyTypeFunctions.Run(mockHttpRequest.Object);

            // Assert
            Assert.That(response, Is.InstanceOf<BadRequestObjectResult>());
            var badRequestResult = response as BadRequestObjectResult;
            Assert.That(badRequestResult, Is.Not.Null);
            Assert.That(badRequestResult.Value, Is.EqualTo("valid policytype object NOT provided"));
        }

        [Test]
        public async Task SavePolicyType_ShouldReturnInternalServerError_WhenExceptionThrown()
        {
            // Arrange
            var policyType = new PolicyType { PolicyTypeId = 1, Code = "Corp", Name = "Corporate" };
            var requestBody = JsonConvert.SerializeObject(policyType);
            var mockHttpRequest = new Mock<HttpRequest>();
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(requestBody));
            mockHttpRequest.Setup(req => req.Body).Returns(stream);

            _mockPolicyTypeDataManager.Setup(x => x.InsertOrUpdatePolicyTypeAsync(policyType)).ThrowsAsync(new Exception("Database error"));

            // Act
            var response = await _policyTypeFunctions.Run(mockHttpRequest.Object);

            // Assert
            Assert.That(response, Is.InstanceOf<StatusCodeResult>());
            var statusCodeResult = response as StatusCodeResult;
            Assert.That(statusCodeResult, Is.Not.Null);
            Assert.That(statusCodeResult.StatusCode, Is.EqualTo(StatusCodes.Status500InternalServerError));
        }
    }
}