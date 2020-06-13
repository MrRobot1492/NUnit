using System.Collections.Generic;
using System.Linq;

namespace TestNinja.Mocking
{
    /// <summary>
    /// 2.Extracting Interface of Our VideoRepository in order to decouple the classes
    /// </summary>
    public interface IVideoRepository
    {
        IEnumerable<Video> GetUnprocessedVideos();
    }

    /// <summary>
    /// 1.Encapsulates the code that touch external resources
    /// Moving the responsability where it really belongs
    /// </summary>
    public class VideoRepository : IVideoRepository
    {
        public IEnumerable<Video> GetUnprocessedVideos()
        {
            using (var context = new VideoContext())
            {
                var videos =
                    (from video in context.Videos
                     where !video.IsProcessed
                     select video).ToList();
                return videos;
            }
        }
    }
}