using System;
using Xunit;
using Business;
using Data.Entity;

namespace Test
{
    public class BusinessControllerTests
    {
        private readonly IBusinessController _businessController;
        public BusinessControllerTests()
        {
            _businessController = new BusinessController();    
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
    }
}
