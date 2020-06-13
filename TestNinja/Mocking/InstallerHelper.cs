using System.Net;
namespace TestNinja.Mocking
{
    public class InstallerHelper
    {
        //Depency Injection
        public InstallerHelper(IFileDownloader fileDownloader)
        {
            _fileDownloader = fileDownloader;
        }

        public bool DownloadInstaller(string customerName, string installerName)
        {
            try
            {
                //3.Injecting Interface into InstallerHelper
                //Superior class is delegating this responsability to this
                //File Downloader
                _fileDownloader.DownloadFile(
                    string.Format("http://example.com/{0}/{1}",
                    customerName,
                    installerName),
                    _setupDestinationFile);
                return true;
            }
            catch (WebException)
            {
                return false;
            }
        }

        #region private members
        private string _setupDestinationFile;
        private readonly IFileDownloader _fileDownloader;
        #endregion
    }
}