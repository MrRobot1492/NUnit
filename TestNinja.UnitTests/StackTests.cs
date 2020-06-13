using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    public class StackTests
    {

        #region Public Methods

        [SetUp]
        public void SetUp()
        {
            //1.Arrange
            userStack = new Stack<User>();
            user = new User();
        }

        #region Push
        [Test]
        public void Push_ArgumentIsNull_ThrowArgumentNullException()
        {
            //1.Arrange
            user = null;

            //2.Act && 3.Assert
            Assert.That(() => userStack.Push(user), Throws.ArgumentNullException);
        }

        [Test]
        public void Push_ValidArgument_AddingElementToStack()
        {
            //1.Arrange
            var stackCounter = userStack.Count;

            //2.Act
            userStack.Push(user);

            //3.Assert
            Assert.That(userStack.Count, Is.EqualTo(stackCounter + 1));
        }

        [Test]
        public void Count_EmptyStack_ReturnZero()
        {
            Assert.That(userStack.Count,Is.EqualTo(byte.MinValue));
        }
        #endregion

        #region Pop
        [Test]
        public void Pop_EmptyStack_ThrowInvalidOperationException()
        {
            ////2.Act && 3.Assert
            Assert.That(() => userStack.Pop(), Throws.InvalidOperationException);
        }

        [Test]
        public void Pop_StackWithElements_ReturnObjectOnTheTop()
        {
            //1.Arrange
            userStack.Push(new User());
            userStack.Push(new User());
            userStack.Push(user);

            //2.Act
            var result = userStack.Pop();

            //3.Assert
            Assert.That(result, Is.EqualTo(user));
        }

        [Test]
        public void Pop_StackWithElements_RemoveObjectOnTheTop()
        {
            //1.Arrange
            userStack.Push(new User());
            userStack.Push(new User());
            userStack.Push(new User());
            var stackCounter = userStack.Count;

            //2.Act
            var result = userStack.Pop();

            //3.Assert
            Assert.That(userStack.Count, Is.EqualTo(stackCounter - 1));
        }
        #endregion

        #region Peek
        public void Peek_EmptyStack_ThrowInvalidOperationException()
        {
            //2.Act && 3.Assert
            Assert.That(() => userStack.Peek(), Throws.InvalidOperationException);
        }
        public void Peek_StackWithElements_ReturnLastStackElement()
        {
            //1.Arrange
            userStack.Push(new User());
            userStack.Push(new User());
            userStack.Push(user);

            //2.Act
            var result = userStack.Peek();

            //3.Assert
            Assert.That(result, Is.EqualTo(user));
        }

        public void Peek_StackWithElements_DoesNotRemoveTheObjectAtTheTopOfTheStack()
        {
            //1.Arrange
            userStack.Push(new User());
            userStack.Push(new User());
            userStack.Push(user);
            var stackCounter = userStack.Count;

            //2.Act
            var result = userStack.Peek();

            //3.Assert
            Assert.That(userStack.Count, Is.EqualTo(stackCounter));
        }
        #endregion

        #endregion
        #region Members
        private Stack<User> userStack;
        private User user;
        #endregion
    }
}