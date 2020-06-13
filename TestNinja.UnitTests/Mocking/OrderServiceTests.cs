using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    public class OrderServiceTests
    {
        [Test]
        public void PlaceOrder_WhenCalled_StoreTheOrder()
        {
            //Arrange
            var storage = new Mock<IStorage>();
            var orderService = new OrderService(storage.Object);
            var order = new Order();

            //Act
            var result= orderService.PlaceOrder(order);

            //Assert-->Verify a method is called with the right arguments
            //Expected invocation on the mock at least once: s => s.Store(Order)
            storage.Verify(s=>s.Store(order));
        }
    }
}
