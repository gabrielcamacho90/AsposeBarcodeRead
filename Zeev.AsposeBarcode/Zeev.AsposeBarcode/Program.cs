using Microsoft.Extensions.Configuration;
using System.IO;
using Zeev.AsposeBarcode.Library;

namespace Zeev.AsposeBarcode
{
    class Program
    {
        static void Main(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appSettings.json").Build();

            ReadBarcode readBarcode = new ReadBarcode(configuration);
            readBarcode.ReadFilesByAspose();
        }
    }
}
