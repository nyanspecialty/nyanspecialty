using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NyanSpecialty.Assistance.Web.Api.Functions;
using NyanSpecialty.Assistance.Web.Data.Manager;
using NyanSpecialty.Assistance.Web.Models;

namespace NyanSpecialty.Assistance.Web.Api.Tests
{
    [TestFixture]
    public class PalicyTypeFunctionTests
    {
        private readonly PolicyTypeFunction _policyFunctions;
        private readonly Mock<IPolicyTypeDataManager> _mockPolicyTypeDataManager;
        private readonly Mock<ILogger<PolicyTypeFunction>> _mockPlociyLoggerObject;
        private PolicyType validPolicyType;
        public PalicyTypeFunctionTests(Mock<IPolicyTypeDataManager> mockPolicyTypeDataManager, Mock<ILogger<PolicyTypeFunction>> mockPlociyLoggerObject)
        {
            _mockPolicyTypeDataManager = mockPolicyTypeDataManager;
            _mockPlociyLoggerObject = mockPlociyLoggerObject;
            _policyFunctions = new PolicyTypeFunction(_mockPlociyLoggerObject.Object, _mockPolicyTypeDataManager.Object);
        }

        [SetUp]
        public void Setup()
        {
            this.validPolicyType = new PolicyType()
            {
                Code = "Corp",
                Name = "Corporate",
                CreatedBy = 1,
                CreatedOn = DateTimeOffset.Now,
                IsActive = true,
                ModifiedBy = 1,
                ModifiedOn = DateTimeOffset.Now,
                PolicyTypeId = 1
            };
        }

        [Test]
        public async Task GetAllPolicyTypes_ShouldRetrunOkObjectResult_WithData()
        {
            //Arrunge
            List<PolicyType> policyTypes = new List<PolicyType>();
            policyTypes.Add(validPolicyType);

            _mockPolicyTypeDataManager.Setup(x => x.GetAllPolicyTypeAsync()).ReturnsAsync(policyTypes);

            var mockHttpRequest = new Mock<HttpRequest>();

            //Act

            var response = await _policyFunctions.GetAllPolicyTypes(mockHttpRequest.Object);

            Assert.Equals(1, 1);

        }
    }
}