using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using TestNinja.Mocking;
namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    public class VideoServiceTests
    {
        #region Public Members
        [SetUp]
        public void Setup()
        {
            //_fakeFileReader=A.Fake<IFileReader>();

            //Mock Object, Only for Deal with External Dependencies
            _mockFileReader = new Mock<IFileReader>();

            _videoRepository = new Mock<IVideoRepository>();

            //UNIT TEST TAKES FAKE FILE READER
            //Object such implements IFileReader
            _videoService = new VideoService(_mockFileReader.Object, _videoRepository.Object);
        }

        [Test]
        [Author("Ledesm@")]
        public void ReadVideoTitle_EmptyFile_ReturnErrorMessage()
        {
            //=>Goes to              //Returns, Throws<>
            _mockFileReader.Setup(fr => fr.Read("video.txt")).Returns("");

            //A.Dependency Via Method Parameter
            //var result = service.ReadVideoTitle(new FakeFileReader());

            //B.Dependency Via Property
            var result = _videoService.ReadVideoTitle();

            //C.Dependency Via Constructor
            Assert.That(result, Does.Contain("error").IgnoreCase);
        }

        [Test]
        [Author("Ledesm@")]
        [Description("Testing GetUnprocessedVideosAsCSV For All Videos Processed")]
        public void GetUnprocessedVideosAsCsv_AllVideosAreProcessed_ReturnEmptyString()
        {
            //Arrange
            _videoRepository.Setup(r => r.GetUnprocessedVideos()).Returns(new List<Video>());

            //Act
            var result = _videoService.GetUnprocessedVideosAsCsv();

            //Assert
            Assert.That(result, Is.EqualTo(string.Empty));
        }

        [Test]
        [Author("Ledesm@")]
        [Description("Testing GetUnprocessedVideosAsCSV For All Videos Processed")]
        public void GetUnprocessedVideosAsCsv_AtLeastOneVideosWasNotProcessed_ReturnUnprocessedVideosIds()
        {
            //Arrange
            _videoRepository.Setup(r => r.GetUnprocessedVideos()).Returns(new List<Video>
            {
                 new Video()
                ,new Video()
            });

            //Act
            var result = _videoService.GetUnprocessedVideosAsCsv();

            //Assert
            Assert.That(result.Length, Is.GreaterThan(byte.MinValue));
        }
        #endregion

        #region Private Members
        private VideoService _videoService;
        private Mock<IFileReader> _mockFileReader;
        private Mock<IVideoRepository> _videoRepository;
        #endregion
    }
}