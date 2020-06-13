namespace TestNinja.Mocking
{
    public class Program
    {
        public static void Main()
        {
            //PRODUCTION IT TAKES REAL FILE READER IMPLEMENTATION
            var service = new VideoService();

            var title=service.ReadVideoTitle();
        }
    }
}
