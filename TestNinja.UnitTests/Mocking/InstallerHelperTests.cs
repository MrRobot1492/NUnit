using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    public class InstallerHelperTests
    {
        [SetUp]
        public void Setup()
        {
            //1.Arrange
            _fileDownloader = new Mock<IFileDownloader>();
            _installerHelper = new InstallerHelper(_fileDownloader.Object);
        }

        [Test]
        [Author("@Ledesma")]
        public void DownloadInstaller_DownloadSucced_ReturnTrue()
        {
            //1.Arrange
            //Is not neccesary to Arrange in order this method does not return anything, 
            //As well is not neccesary to mock it up since not exception to throw is neccesary here
            //The method itself will return true, only on the false scenary is neccesary to force launching an exception

            //2.Act
            var result = _installerHelper.DownloadInstaller("customer", "installer");

            //3.Assert
            Assert.That(result, Is.True);
        }

        [Test]
        [Author("@Ledesma")]
        public void DownloadInstaller_DownloadFails_ReturnFalse()
        {
            //1.Arrange
            //It only works when those parameters were supplied on the Setup Mock are 
            //the same as the Helper
                                                       //supply fake strings
            _fileDownloader.Setup(fd => 
                fd.DownloadFile(It.IsAny<string>(), It.IsAny<string>()))
                .Throws<WebException>();

            //2.Act
            var result = _installerHelper.DownloadInstaller("customer", "installer");

            //3.Assert
            Assert.That(result, Is.False);
        }

        Mock<IFileDownloader> _fileDownloader;
        InstallerHelper _installerHelper;
    }
}