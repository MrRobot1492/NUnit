using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    public class CustomerControllerTests
    {
        [Test]
        public void GetCustomer_IdIsZero_ReturnNotFound()
        {
            var controller = new CustomerController();

            var result = controller.GetCustomer(0);

            //Not Found Object
            Assert.That(result, Is.TypeOf<NotFound>());

            //Not Found Object or one of its derivatives
            Assert.That(result, Is.InstanceOf<NotFound>());
        }

        [Test]
        public void GetCustomer_IdIsNotZero_ReturnOk()
        {
            var controller = new CustomerController();

            var result = controller.GetCustomer(10);

            //Not Found Object
            Assert.That(result, Is.TypeOf<Ok>());

            //Not Found Object or one of its derivatives
            Assert.That(result, Is.InstanceOf<Ok>());
        }
    }
}