using System.Net;
namespace TestNinja.Mocking
{
    /// <summary>
    /// 2.Extracting Interface of Our InstallerRepository in order to decouple the classes
    /// </summary>
    public interface IFileDownloader
    {
        //This is a low level class such cares only on 
        //download file, it only requires an url and a path
        void DownloadFile(string url, string path);
    }

    /// <summary>
    /// 1.Encapsulates the code that touch external resources
    /// Moving the responsability where it really belongs
    /// </summary>
    public class FileDownloader : IFileDownloader
    {
        public void DownloadFile(string url, string path)
        {
            var client = new WebClient();
            client.DownloadFile(url, path);
        }
    }
}