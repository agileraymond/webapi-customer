using System;
using Xunit;
using Business;
using Data.Entity;
using Data;
using Moq;

namespace Test
{
    public class BusinessControllerTests
    {
        private readonly IBusinessController _businessController;
        private readonly Mock<IDataController> _mockDataController;
        
        public BusinessControllerTests()
        {
            _mockDataController = new Mock<IDataController>();
            _businessController = new BusinessController(_mockDataController.Object);    
        }

        [Fact]
        public void AddCustomer_ThrowsNullRefException_WhenCustomerIsNull()
        {
            var exception = Assert.Throws<NullReferenceException>(() => _businessController.AddCustomer(null));
        }

        [Fact]
        public void AddCustomer_ThrowsArgumentException_WhenFirstNameIsNull()
        {
            var customer = new Customer { FirstName = null, LastName = "ln" }; 
            var exception = Assert.Throws<ArgumentException>(() => _businessController.AddCustomer(customer));
            Assert.True(exception.Message.Contains("FirstName"));        
        }
        
        [Fact]
        public void AddCustomer_ThrowsArgumentException_WhenLastNameIsEmpty()
        {
            var customer = new Customer { FirstName = "fn", LastName = string.Empty }; 
            var exception = Assert.Throws<ArgumentException>(() => _businessController.AddCustomer(customer));
            Assert.True(exception.Message.Contains("LastName"));        
        }

        [Fact]
        public void AddCustomer_WorksAsExpected()
        {
            var customer = new Customer { FirstName = "fn", LastName = "ln" }; 
            _mockDataController.Setup(x => x.AddCustomer(customer)).Returns(1);    
            Assert.True(_businessController.AddCustomer(customer) == 1);
        }

        [Fact]
        public void UpdateCustomer_ThrowsNullRefException_WhenCustomerIsNull()
        {
            var exception = Assert.Throws<NullReferenceException>(() => _businessController.UpdateCustomer(null));
        }

        [Fact]
        public void AddAddress_ThrowsNullReferenceException_WhenAddressIsNull()
        {
            var exception = Assert.Throws<NullReferenceException>(() => _businessController.AddAddress(null));
        }

        [Fact]
        public void AddAddress_ThrowsArgumentException_WhenCustomerIdIsInvalid()
        {
            var address = new Address { CustomerId = 0 };
            var exception = Assert.Throws<ArgumentException>(() => _businessController.AddAddress(address));
        }
    }
}
