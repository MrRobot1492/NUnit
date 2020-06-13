using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using TestNinja.Fundamentals;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    public class HouseKeeperServiceTest
    {
        private HousekeeperService _service;
        private Mock<IStatementGenerator> _statement;
        private Mock<IEmailSender> _email;
        private Mock<IXtraMessageBox> _xtraMessage;
        private Housekeeper _housekeeper;
        private string _filename;
        private DateTime _statementDate => new DateTime(2017, 1, 1);

        [SetUp]
        public void SetUp()
        {
            _housekeeper = new Housekeeper
            {
                Email = "a",
                FullName = "b",
                Oid = 1,
                StatementEmailBody = "c"
            };

            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.Setup(uow => uow.Query<Housekeeper>()).Returns(
                new List<Housekeeper>
                {
                    _housekeeper
                }.AsQueryable);

            _statement = new Mock<IStatementGenerator>();
            _statement
                .Setup(sg => sg.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, _statementDate))
                .Returns(() => _filename); //We want to return a Null Function, not Null String for this Mock
            //Also with this Lazy Evaluation, this evaluation will be evaluated later
            _email = new Mock<IEmailSender>();
            _xtraMessage = new Mock<IXtraMessageBox>();

            _service = new HousekeeperService(
                unitOfWork.Object,
                _statement.Object,
                _email.Object, _xtraMessage.Object);
        }

        [Test]
        [Author("Ledesm@")]
        [Description("Validating Statement Generation when HouseKeeper Has Email")]
        public void SendStatementEmails_WhenCalled_GenerateStatements()
        {
            _service.SendStatementEmails(_statementDate);

            _statement.Verify(sg => sg.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, _statementDate));
        }

        [Test]
        [Author("Ledesm@")]
        [Description("Validating no generation of Statements when HouseKeeper has a wrong Email")]
        [TestCase(null)]
        [TestCase(" ")]
        [TestCase("")]
        public void SendStatementEmails_WhenHouseKeepersEMailIsNotValid_ShouldNotGenerateStatements(string email)
        {
            _housekeeper.Email = email;
            _service.SendStatementEmails(_statementDate);
            _statement.Verify(sg =>
                    sg.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, _statementDate),
                Times.Never);
        }

        [Test]
        [Author("Ledesm@")]
        [Description("Validating Sending Email when HouseKeeper has a correct Email")]
        public void SendStatementEmails_WhenHouseKeepersEMailIsValid_ShouldEmailTheStatement()
        {
            _service.SendStatementEmails(_statementDate);

            VerifyEmailSent();
        }

        [Test]
        [Author("Ledesm@")]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void SendStatementEmails_StatementFileNameIsNotValid_ShouldNotEmailTheStatement(string email)
        {
            _filename = email;
            _service.SendStatementEmails(_statementDate);
            VerifyEmailNotSent();
        }

        [Test]
        [Author("Ledesm@")]
        public void SendStatementEmails_EmailSendingFails_DisplayAMessageBox(string email)
        {
            _email.Setup(e => e.EmailFile(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>()
            )).Throws<Exception>();
            _service.SendStatementEmails(_statementDate);

            VerifyMessageShown();
        }

        private void VerifyMessageShown()
        {
            _xtraMessage.Verify(mb => mb.Show(It.IsAny<string>(), It.IsAny<string>(), MessageBoxButtons.OK));
        }

        private void VerifyEmailSent()
        {
            _email.Verify(es => es.EmailFile(
                _housekeeper.Email,
                _housekeeper.StatementEmailBody,
                _filename,
                It.IsAny<string>()));
        }

        private void VerifyEmailNotSent()
        {
            _email.Verify(es => es.EmailFile(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>()),
                Times.Never);
        }

    }
}