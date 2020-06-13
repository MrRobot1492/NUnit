using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    public class EmployeeControllerTests
    {
        private Mock<IEmployeeStorage> _storage;
        private EmployeeController _controller;

        [SetUp]
        public void SetUp()
        {
            //1.Arrange
            _storage = new Mock<IEmployeeStorage>();
            _controller = new EmployeeController(_storage.Object);
        }

        [Test]
        public void DeleteEmployee_EmployeeFound_EmployeeRemovedFromDB()
        {

            //2.Act
            _controller.DeleteEmployee(It.IsAny<int>());

            //3.Assert
            //We don't care about the result
            //We just test the interaction of this controller\
            //To store the object
            _storage.Verify(s=>s.DeleteEmployee(It.IsAny<int>()));
        }

        [Test]
        public void DeleteEmployee_EmployeeNotFound_EmployeeNotRemovedFromDB()
        {
            //1.Arrange
            _storage.Setup(s => s.DeleteEmployee(It.IsAny<int>())).Throws<Exception>();

            //2.Act
            _controller.DeleteEmployee(1);

            //3.Assert
            Assert.That(() => _controller.DeleteEmployee(It.IsAny<int>()),
                Throws.TypeOf<Exception>());
        }
    }
}