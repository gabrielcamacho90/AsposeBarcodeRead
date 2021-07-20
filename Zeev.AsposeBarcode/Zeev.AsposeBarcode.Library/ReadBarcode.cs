using Aspose.BarCode.BarCodeRecognition;
using System;
using System.Drawing;
using System.IO;

namespace Zeev.AsposeBarcode.Library
{
    public class ReadBarcode
    {
        public static void Read()
        {
            Aspose.BarCode.License license = new Aspose.BarCode.License();
            license.SetLicense(@"D:\GIT-Estudo\Zeev.AsposeBarcode\Zeev.AsposeBarcode\Aspose.Total.lic");

            foreach (var item in Directory.GetFiles(@"D:\GIT-Estudo\DocsBarcode"))
            {
                try
                {
                    string fileName = Path.GetFileName(item);
                    //Bitmap bmpFile = new System.Drawing.Bitmap(item);

                    BaseDecodeType type = null;
                    BaseDecodeType.TryParse("CODE39", out type);

                    using (BarCodeReader reader = new BarCodeReader(item, DecodeType.Code39Standard))
                    {
                        Console.WriteLine();
                        Console.WriteLine("Arquivo: " + fileName);

                        foreach (BarCodeResult result in reader.ReadBarCodes())
                        {
                            Console.WriteLine("******************");
                            Console.WriteLine("Type: " + result.CodeType);
                            Console.WriteLine("CodeText: " + result.CodeText);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            Console.WriteLine("Finalizado");
            Console.ReadLine();
        }
    }
}
