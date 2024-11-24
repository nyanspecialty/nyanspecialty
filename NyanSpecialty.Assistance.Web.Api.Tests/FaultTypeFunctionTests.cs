
using Azure;
using Castle.Core.Logging;
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
    public class FaultTypeFunctionTests
    {
        private FaultTypeFunction _faultTypeFunction;
        private Mock<IFaultTypeDataManager> _mockFaultTypeDataManager;
        private Mock<ILogger<FaultTypeFunction>> _mockLogger;


        [SetUp]
        public void Setup()
        {
            _mockFaultTypeDataManager = new Mock<IFaultTypeDataManager>();
            _mockLogger = new Mock<ILogger<FaultTypeFunction>>();
            _faultTypeFunction = new FaultTypeFunction(_mockLogger.Object, _mockFaultTypeDataManager.Object);

        }
        [Test]
        public async Task GetFaultTypes_ShouldReturnOkObjectResult_WithData()
        {
            //Arrange
            var model = new List<FaultType>
            {
               new FaultType {FaultTypeId = 1, Name = "Test_Name", Description = "Des" },
               new FaultType {FaultTypeId=2, Name="Name1", Description="Des1" }
            };
            _mockFaultTypeDataManager.Setup(x => x.GetFaultTypesAsync()).ReturnsAsync(model);
            var mockHttp= new Mock<HttpRequest>();
            //Act
            var response= await _faultTypeFunction.GetFaultTypes(mockHttp.Object);

            //Assert
            Assert.That(response, Is.InstanceOf<OkObjectResult>());
            var okResult = response as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);
            Assert.That(okResult.Value, Is.EqualTo(model));
        }
        [Test]
        public async Task GetFaultTypes_ShouldReturnInternalServerError_WhenExceptionThrown()
        {
            // Arrange
            _mockFaultTypeDataManager.Setup(x => x.GetFaultTypesAsync()).ThrowsAsync(new Exception("Database error"));
            var mockHttpRequest = new Mock<HttpRequest>();

            // Act
            var response = await _faultTypeFunction.GetFaultTypes(mockHttpRequest.Object);

            // Assert
            Assert.That(response, Is.InstanceOf<StatusCodeResult>());
            var statusCodeResult = response as StatusCodeResult;
            Assert.That(statusCodeResult, Is.Not.Null);
            Assert.That(statusCodeResult.StatusCode, Is.EqualTo(StatusCodes.Status500InternalServerError));
        }
        [Test]
        public async Task GetFaultTypes_ReturnsOkResult_WithEmptyData()
        {
            // Arrange
            var expectedData = new List<FaultType>(); // Empty list
            _mockFaultTypeDataManager
                .Setup(m => m.GetFaultTypesAsync())
                .ReturnsAsync(expectedData); // Mock returns an empty list

            var mockHttp = new Mock<HttpRequest>(); 

            // Act
            var response = await _faultTypeFunction.GetFaultTypes(mockHttp.Object);

            // Assert
            Assert.That(response, Is.InstanceOf<OkObjectResult>()); 
            var okResult = response as OkObjectResult;
            Assert.That(okResult, Is.Not.Null); // Ensure the result is not null
            Assert.That(okResult.StatusCode, Is.EqualTo(StatusCodes.Status200OK)); 
            Assert.That(okResult.Value, Is.EqualTo(expectedData)); // Ensure the value is the expected empty list
        }
        [Test]
        public async Task GetFaultTypeByID_ShouldReturnOkObjectResult_WithValidId()
        {
            // Arrange
            var faultType = new FaultType { FaultTypeId = 1, Name = "John Doe" , Description="Des"};
            long FaultTypeId = 1;

            _mockFaultTypeDataManager.Setup(x => x.GetFaultTypeByID(FaultTypeId)).ReturnsAsync(faultType);
            var mockHttpRequest = new Mock<HttpRequest>();

            // Act
            var response = await _faultTypeFunction.GetFaultTypeById(mockHttpRequest.Object, FaultTypeId);

            // Assert
            Assert.That(response, Is.InstanceOf<OkObjectResult>());
            var okResult = response as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);
            Assert.That(okResult.Value, Is.EqualTo(faultType));
        }

        [Test]
        public async Task GetFaultTypeByID_ShouldReturnInternalServerError_WhenExceptionThrown()
        {
            // Arrange
            long GetFaultTypeByID = 1;
            _mockFaultTypeDataManager.Setup(x => x.GetFaultTypeByID(GetFaultTypeByID)).ThrowsAsync(new Exception("Database error"));
            var mockHttpRequest = new Mock<HttpRequest>();

            // Act
            var response = await _faultTypeFunction.GetFaultTypeById(mockHttpRequest.Object, GetFaultTypeByID);

            // Assert
            Assert.That(response, Is.InstanceOf<StatusCodeResult>());
            var statusCodeResult = response as StatusCodeResult;
            Assert.That(statusCodeResult, Is.Not.Null);
            Assert.That(statusCodeResult.StatusCode, Is.EqualTo(StatusCodes.Status500InternalServerError));
        }
        [Test]
        public async Task SaveFaultType_ShouldReturnBadRequest_WhenBodyIsNull()
        {
            // Arrange
            var mockHttpRequest = new Mock<HttpRequest>();
            mockHttpRequest.Setup(req => req.Body).Returns((Stream)null);

            // Act
            var response = await _faultTypeFunction.SaveFaultType(mockHttpRequest.Object);

            // Assert
            Assert.That(response, Is.InstanceOf<BadRequestObjectResult>());
            var badRequestResult = response as BadRequestObjectResult;
            Assert.That(badRequestResult, Is.Not.Null);
            Assert.That(badRequestResult.Value, Is.EqualTo("valid faulttype details NOT provided"));
        }
        [Test]
        public async Task SaveFaultType_ShouldReturnBadRequest_WhenBodyIsEmpty()
        {
            // Arrange
            var mockHttpRequest = new Mock<HttpRequest>();
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(string.Empty));
            mockHttpRequest.Setup(req => req.Body).Returns(stream);

            // Act
            var response = await _faultTypeFunction.SaveFaultType(mockHttpRequest.Object);

            // Assert
            Assert.That(response, Is.InstanceOf<BadRequestObjectResult>());
            var badRequestResult = response as BadRequestObjectResult;
            Assert.That(badRequestResult, Is.Not.Null);
            Assert.That(badRequestResult.Value, Is.EqualTo("valid faulttype details NOT provided"));
        }
        [Test]
        public async Task SaveFaultType_ShouldReturnBadRequest_WhenDeserializationFails()
        {
            // Arrange
            var invalidJson = "{ invalid json }"; // Invalid JSON
            var mockHttpRequest = new Mock<HttpRequest>();
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(invalidJson));
            mockHttpRequest.Setup(req => req.Body).Returns(stream);

            // Act
            var response = await _faultTypeFunction.SaveFaultType(mockHttpRequest.Object);

            // Assert
            Assert.That(response, Is.InstanceOf<BadRequestObjectResult>());
            var badRequestResult = response as BadRequestObjectResult;
            Assert.That(badRequestResult, Is.Not.Null);
            Assert.That(badRequestResult.Value, Is.EqualTo("valid fault details NOT provided"));
        }
        [Test]
        public async Task SaveFaultTypes_ShouldReturnInternalServerError_WhenExceptionThrown()
        {
            // Arrange
            var faultType = new FaultType { FaultTypeId = 1, Name = "John Doe", Description="Des" };
            var requestBody = JsonConvert.SerializeObject(faultType);
            var mockHttpRequest = new Mock<HttpRequest>();
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(requestBody));
            mockHttpRequest.Setup(req => req.Body).Returns(stream);

            _mockFaultTypeDataManager.Setup(x => x.InsertOrUpdateFaultTypeAsync(faultType)).ThrowsAsync(new Exception("Database error"));

            // Act
            var response = await _faultTypeFunction.SaveFaultType(mockHttpRequest.Object);

            // Assert
            Assert.That(response, Is.InstanceOf<StatusCodeResult>());
            var statusCodeResult = response as StatusCodeResult;
            Assert.That(statusCodeResult, Is.Not.Null);
            Assert.That(statusCodeResult.StatusCode, Is.EqualTo(StatusCodes.Status500InternalServerError));
        }
    }
}
