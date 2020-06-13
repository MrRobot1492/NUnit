using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace TestNinja.Mocking
{
    public class VideoService
    {
        //B.Dependency Injection Via Properties
        //public IFileReader FileReader { get; set; }

        //C.Dependency Injection Via Constructor
        private IFileReader _fileReader;
        private IVideoRepository _videoRepository;

        //B.Dependency Injection Via Properties
        //public VideoService()
        //{
        //    FileReader = new FileReader();
        //}

        //C.Dependency Injection Via Constructor
        public VideoService(IFileReader fileReader = null, IVideoRepository videoRepository = null)
        {
            //If the parameter is null instanciates a new FileReader
            //if not, it takes the parameter
            //TEST(FAKE)    //PROD (REAL IMPLEMENTATION)
            //If you use a Dependency Injection Framework you don't have to deal with this poor validation
            _fileReader = fileReader ?? new FileReader();
            _videoRepository = videoRepository ?? new VideoRepository();
        }

        //A.Dependency Injection Via Method Parameter
        //public string ReadVideoTitle(IFileReader fileReader)
        public string ReadVideoTitle()
        {
            //A.Dependency Injection Via Method Parameter
            //var str = fileReader.Read("video.txt");
            //B.Dependency Injection Via Properties
            var str = _fileReader.Read("video.txt");
            //C.Dependency Via Constructor
            var video = JsonConvert.DeserializeObject<Video>(str);
            if (video == null)
                return "Error parsing the video.";
            return video.Title;
        }

        /// <summary>
        /// Decoupled and Testeable Method
        /// Possible Scenarios
        /// 1.[] => "" (Empty list returns empty string)
        /// 2.[{},{},...] => "1,2,3" (List with at least one value returns a list of ids)
        /// </summary>
        /// <returns></returns>
        public string GetUnprocessedVideosAsCsv()
        {
            var videoIds = new List<Guid>();

            //3.Inject Interface into VideoService
            var videos = _videoRepository.GetUnprocessedVideos();
            foreach (var v in videos)
                videoIds.Add(v.Id);

            return String.Join(",", videoIds);
        }
    }

    public class Video
    {
        public Video(Guid id)
        {
            this.Id = id;
        }

        public Video()
        {
            this.Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        public string Title { get; set; }
        public bool IsProcessed { get; set; }
    }

    public class VideoContext : DbContext
    {
        public DbSet<Video> Videos { get; set; }
    }
}