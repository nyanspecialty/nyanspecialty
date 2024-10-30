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
    public class CustomersFunctionsTests
    {
        private CustomersFunctions _customersFunctions;
        private Mock<ICustomersDataManager> _mockCustomersDataManager;
        private Mock<ILogger<CustomersFunctions>> _mockLogger;

        [SetUp]
        public void Setup()
        {
            _mockCustomersDataManager = new Mock<ICustomersDataManager>();
            _mockLogger = new Mock<ILogger<CustomersFunctions>>();
            _customersFunctions = new CustomersFunctions(_mockLogger.Object, _mockCustomersDataManager.Object);
        }

        [Test]
        public async Task GetCustomers_ShouldReturnOkObjectResult_WithData()
        {
            // Arrange
            var customers = new List<Customers>
        {
            new Customers { CustomerID = 1, Name = "John Doe" },
            new Customers { CustomerID = 2, Name = "Jane Smith" }
        };

            _mockCustomersDataManager.Setup(x => x.GetCustomersAsync()).ReturnsAsync(customers);
            var mockHttpRequest = new Mock<HttpRequest>();

            // Act
            var response = await _customersFunctions.GetCustomers(mockHttpRequest.Object);

            // Assert
            Assert.That(response, Is.InstanceOf<OkObjectResult>());
            var okResult = response as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);
            Assert.That(okResult.Value, Is.EqualTo(customers));
        }

        [Test]
        public async Task GetCustomers_ShouldReturnInternalServerError_WhenExceptionThrown()
        {
            // Arrange
            _mockCustomersDataManager.Setup(x => x.GetCustomersAsync()).ThrowsAsync(new Exception("Database error"));
            var mockHttpRequest = new Mock<HttpRequest>();

            // Act
            var response = await _customersFunctions.GetCustomers(mockHttpRequest.Object);

            // Assert
            Assert.That(response, Is.InstanceOf<StatusCodeResult>());
            var statusCodeResult = response as StatusCodeResult;
            Assert.That(statusCodeResult, Is.Not.Null);
            Assert.That(statusCodeResult.StatusCode, Is.EqualTo(StatusCodes.Status500InternalServerError));
        }

        [Test]
        public async Task GetCustomersById_ShouldReturnOkObjectResult_WithValidId()
        {
            // Arrange
            var customer = new Customers { CustomerID = 1, Name = "John Doe" };
            long customerId = 1;

            _mockCustomersDataManager.Setup(x => x.GetCustomersByIdAsync(customerId)).ReturnsAsync(customer);
            var mockHttpRequest = new Mock<HttpRequest>();

            // Act
            var response = await _customersFunctions.GetCustomersById(mockHttpRequest.Object, customerId);

            // Assert
            Assert.That(response, Is.InstanceOf<OkObjectResult>());
            var okResult = response as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);
            Assert.That(okResult.Value, Is.EqualTo(customer));
        }

        [Test]
        public async Task GetCustomersById_ShouldReturnInternalServerError_WhenExceptionThrown()
        {
            // Arrange
            long customerId = 1;
            _mockCustomersDataManager.Setup(x => x.GetCustomersByIdAsync(customerId)).ThrowsAsync(new Exception("Database error"));
            var mockHttpRequest = new Mock<HttpRequest>();

            // Act
            var response = await _customersFunctions.GetCustomersById(mockHttpRequest.Object, customerId);

            // Assert
            Assert.That(response, Is.InstanceOf<StatusCodeResult>());
            var statusCodeResult = response as StatusCodeResult;
            Assert.That(statusCodeResult, Is.Not.Null);
            Assert.That(statusCodeResult.StatusCode, Is.EqualTo(StatusCodes.Status500InternalServerError));
        }

        

        [Test]
        public async Task SaveCustomers_ShouldReturnBadRequest_WhenBodyIsNull()
        {
            // Arrange
            var mockHttpRequest = new Mock<HttpRequest>();
            mockHttpRequest.Setup(req => req.Body).Returns((Stream)null);

            // Act
            var response = await _customersFunctions.SaveCustomers(mockHttpRequest.Object);

            // Assert
            Assert.That(response, Is.InstanceOf<BadRequestObjectResult>());
            var badRequestResult = response as BadRequestObjectResult;
            Assert.That(badRequestResult, Is.Not.Null);
            Assert.That(badRequestResult.Value, Is.EqualTo("valid customer details NOT provided"));
        }

        [Test]
        public async Task SaveCustomers_ShouldReturnBadRequest_WhenBodyIsEmpty()
        {
            // Arrange
            var mockHttpRequest = new Mock<HttpRequest>();
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(string.Empty));
            mockHttpRequest.Setup(req => req.Body).Returns(stream);

            // Act
            var response = await _customersFunctions.SaveCustomers(mockHttpRequest.Object);

            // Assert
            Assert.That(response, Is.InstanceOf<BadRequestObjectResult>());
            var badRequestResult = response as BadRequestObjectResult;
            Assert.That(badRequestResult, Is.Not.Null);
            Assert.That(badRequestResult.Value, Is.EqualTo("valid customer details NOT provided"));
        }

        [Test]
        public async Task SaveCustomers_ShouldReturnBadRequest_WhenDeserializationFails()
        {
            // Arrange
            var invalidJson = "{ invalid json }"; // Invalid JSON
            var mockHttpRequest = new Mock<HttpRequest>();
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(invalidJson));
            mockHttpRequest.Setup(req => req.Body).Returns(stream);

            // Act
            var response = await _customersFunctions.SaveCustomers(mockHttpRequest.Object);

            // Assert
            Assert.That(response, Is.InstanceOf<BadRequestObjectResult>());
            var badRequestResult = response as BadRequestObjectResult;
            Assert.That(badRequestResult, Is.Not.Null);
            Assert.That(badRequestResult.Value, Is.EqualTo("valid customer details NOT provided"));
        }

        [Test]
        public async Task SaveCustomers_ShouldReturnInternalServerError_WhenExceptionThrown()
        {
            // Arrange
            var customer = new Customers { CustomerID = 1, Name = "John Doe" };
            var requestBody = JsonConvert.SerializeObject(customer);
            var mockHttpRequest = new Mock<HttpRequest>();
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(requestBody));
            mockHttpRequest.Setup(req => req.Body).Returns(stream);

            _mockCustomersDataManager.Setup(x => x.InsertOrUpdateCustomersAsync(customer)).ThrowsAsync(new Exception("Database error"));

            // Act
            var response = await _customersFunctions.SaveCustomers(mockHttpRequest.Object);

            // Assert
            Assert.That(response, Is.InstanceOf<StatusCodeResult>());
            var statusCodeResult = response as StatusCodeResult;
            Assert.That(statusCodeResult, Is.Not.Null);
            Assert.That(statusCodeResult.StatusCode, Is.EqualTo(StatusCodes.Status500InternalServerError));
        }
    }
}
