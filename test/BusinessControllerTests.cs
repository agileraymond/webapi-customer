using System;
using Xunit;
using Business;

namespace Test
{
    public class BusinessControllerTests
    {
        [Fact]
        public void AddCustomer()
        {
            var businessController = new BusinessController();
            var exception = Assert.Throws<NullReferenceException>(() => businessController.AddCustomer(null));
            Assert.Equal("Customer object is required", exception.Message);
        }
    }
}
