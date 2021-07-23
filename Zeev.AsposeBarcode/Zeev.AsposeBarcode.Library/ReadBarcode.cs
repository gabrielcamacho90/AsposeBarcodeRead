using Aspose.BarCode.BarCodeRecognition;
using Aspose.OCR;
using Microsoft.Extensions.Configuration;
using SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Zeev.AsposeBarcode.Library.Entities;

namespace Zeev.AsposeBarcode.Library
{
    public class ReadBarcode
    {
        private XDocument BarcodeSettings;
        private List<SingleDecodeType> possibleFormats;
        private string Frames;
        private RulesCollection Rules;
        private readonly IConfiguration _configuration;
        private string pathLicenseAspose;
        private string pathFiles;
        private List<string> processedFiles;

        public ReadBarcode(IConfiguration configuration)
        {
            _configuration = configuration;
#if DEBUG
            pathLicenseAspose = Path.Combine(Directory.GetCurrentDirectory(), "Aspose.Total.lic");
            pathFiles = @"C:\git\Personal\AsposeBarcodeRead\Zeev.AsposeBarcode\Sample with Barcodes";
#else
            pathLicenseAspose = _configuration["Files:PathLicenseAspose"];
            pathFiles = _configuration["Files:PathDocs"];
#endif
            Aspose.BarCode.License license = new Aspose.BarCode.License();
            license.SetLicense(pathLicenseAspose);

            License licenseOcr = new Aspose.OCR.License();
            licenseOcr.SetLicense(pathLicenseAspose);
        }

        #region Implementação inicial
        public async Task ReadFilesByAspose()
        {
            var allFiles = Directory.GetFiles(pathFiles, "*.tif", SearchOption.AllDirectories);

            Console.WriteLine($"Total de arquivos coletados {allFiles.Length}");
            int fileCount = 1;
            foreach (var file in allFiles)
            {
                Console.WriteLine($"Processando arquivo numero {fileCount}");
                string barCode = string.Empty;
                string resultOcr = string.Empty;

                try
                {
                    string fileName = Path.GetFileName(file);
                    string pathTempImageOcr = Path.Combine(_configuration["Files:PathTemp"], $"{Path.GetRandomFileName()}.bmp");
                    string pathTempResultOcr = Path.Combine(_configuration["Files:PathTemp"], $"{Path.GetRandomFileName()}.txt");
                    List<string> typesBarCode = new List<string>();
                    List<string> resultBarCode = new List<string>();

                    Console.WriteLine($"Validando diretorio temporario");
                    // Valida se diretório existe
                    if (!Directory.Exists(Path.GetDirectoryName(pathTempImageOcr)))
                        Directory.CreateDirectory(Path.GetDirectoryName(pathTempImageOcr));

                    using (var bmpFile = new System.Drawing.Bitmap(file))
                    {
                        Console.WriteLine($"Aplicando BCR");
                        using (BarCodeReader reader = new BarCodeReader(
                            bmpFile,
                            DecodeType.Code39Extended,
                            DecodeType.Code39Standard,
                            DecodeType.Code128,
                            DecodeType.QR,
                            DecodeType.Standard2of5,
                            DecodeType.Interleaved2of5))
                        {
                            foreach (BarCodeResult result in reader.ReadBarCodes())
                            {
                                typesBarCode.Add(result.CodeType.TypeName);
                                resultBarCode.Add(result.CodeText);
                            }
                        }

                        bmpFile.Save(pathTempImageOcr, ImageFormat.Bmp);
                    }

                    // Recognize image
                    using (AsposeOcr api = new AsposeOcr())
                    {
                        resultOcr = api.RecognizeImage(pathTempImageOcr);

                        if (!string.IsNullOrEmpty(resultOcr))
                        {
                            using (StreamWriter sw = new StreamWriter(pathTempResultOcr))
                            {
                                sw.WriteLine(resultOcr);
                                sw.Close();
                            }
                        }
                    }

                    // Indexar ECM - Campos
                    var fieldsToIndex = GetIndexFields(String.Join(",", typesBarCode), String.Join(",", resultBarCode));

                    // Monta lista com arquivos
                    Byte[] bytesFile = File.ReadAllBytes(file);
                    string fileOriginal = Convert.ToBase64String(bytesFile);

                    var files = new Dictionary<string, string>();
                    files.Add(fileName, fileOriginal);

                    if (File.Exists(pathTempResultOcr))
                    {
                        Byte[] bytesFileOcr = File.ReadAllBytes(pathTempResultOcr);
                        string fileOcrBase64 = Convert.ToBase64String(bytesFileOcr);

                        files.Add(Path.GetFileName(pathTempResultOcr), fileOcrBase64);
                    }

                    // Indexa Arquivos
                    ZeevDocsIntegration.SendDocument(files, getPreFlowDoc(), getPosFlowDoc(), fieldsToIndex);

                    // Deleta arquivos criado em TEMP
                    File.Delete(pathTempImageOcr);
                    File.Delete(pathTempResultOcr);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                    var pathFlError = Path.Combine(_configuration["Files:PathTemp"], $"{Path.GetRandomFileName()}.log");
                    using (StreamWriter sw = new StreamWriter(pathFlError))
                    {
                        sw.WriteLine($"{ex.Message }\r\n {ex.StackTrace} \r\n");
                        sw.Close();
                    }
                }
                fileCount++;
            }

            Console.WriteLine("Finalizado");
            Console.ReadLine();
        }

        private Dictionary<string, string> GetIndexFields(string typesBarCode, string barCode)
        {
            return new Dictionary<string, string>()
            {
                {"IND_BARCODE", barCode},
                {"TIPOS_BARCODE", typesBarCode}
            };
        }

        private DocumentWorkflow getPreFlowDoc()
        {
            return new DocumentWorkflow
            {
                QueueId = 1,
                DoctypeId = 1,
                SituationId = 1,
                PendencyId = 1
            };
        }

        private DocumentWorkflow getPosFlowDoc()
        {
            return new DocumentWorkflow
            {
                QueueId = 3,
                SituationId = 2,
                PendencyId = 1
            };
        }
        #endregion
        #region Adaptação BarcodeManager
        public async Task ReadBarcodeManager()
        {
            Console.WriteLine("Inicio da leitura com processo BarcodeManager");

            string xmlSample = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "BarcodeSettings.xml"));
            SetupBarcode(xmlSample);

            var allFiles = Directory.GetFiles(pathFiles, "*.tif", SearchOption.AllDirectories).ToList();
            Console.WriteLine($"Total de arquivos coletados {allFiles.Count}");
            int fileCount = 1;

            processedFiles = new List<string>();
            while (allFiles.Count > 0)
            {
                Console.WriteLine("Processando os próximos 100");
                allFiles.Take(100).AsParallel()
                    .ToList().ForEach(file =>
                    {
                        try
                        {
                            string pathTempImageOcr = string.Empty;
                            string pathTempResultOcr = string.Empty;
#if (DEBUG)
                            pathTempImageOcr =Path.Combine( @"C:\git\Personal\AsposeBarcodeRead\Zeev.AsposeBarcode\temp\img", $"{Path.GetRandomFileName()}.bmp");
                            pathTempResultOcr = Path.Combine(@"C:\git\Personal\AsposeBarcodeRead\Zeev.AsposeBarcode\temp\ocr",$"{Path.GetRandomFileName()}.txt");
#else
                            pathTempImageOcr = Path.Combine(_configuration["Files:PathTemp"], $"{Path.GetRandomFileName()}.bmp");
                            pathTempResultOcr = Path.Combine(_configuration["Files:PathTemp"], $"{Path.GetRandomFileName()}.txt");
#endif
                            string resultOcr = string.Empty;
                            Console.WriteLine($"Processando arquivo {fileCount} do total");
                            string fileName = Path.GetFileName(file);                                                        
                            var typesBarCode = new List<string>();

                            Console.WriteLine($"Extraindo código de barras");
                            var resultBarCode = FindBarcode(file, out typesBarCode);

                            Console.WriteLine("Processando OCR");
                            // Recognize image
                            using (AsposeOcr api = new AsposeOcr())
                            {
                                using (var bitmap = new Bitmap(file))
                                {
                                    Console.WriteLine("Salvando versão em bitmap para extração");
                                    bitmap.Save(pathTempImageOcr, ImageFormat.Bmp);
                                }

                                resultOcr = api.RecognizeImage(pathTempImageOcr);
                                ClearRowFiles();

                                if (!string.IsNullOrEmpty(resultOcr))
                                {
                                    using (StreamWriter sw = new StreamWriter(pathTempResultOcr))
                                    {
                                        sw.WriteLine(resultOcr);
                                        sw.Close();
                                    }
                                }
                            }

                            // Indexar ECM - Campos
                            var fieldsToIndex = GetIndexFields(String.Join(",", typesBarCode), String.Join(",", resultBarCode));

                            // Monta lista com arquivos
                            Byte[] bytesFile = File.ReadAllBytes(file);
                            string fileOriginal = Convert.ToBase64String(bytesFile);

                            var files = new Dictionary<string, string>();
                            files.Add(fileName, fileOriginal);
                            
                            if (File.Exists(pathTempResultOcr))
                            {
                                Byte[] bytesFileOcr = File.ReadAllBytes(pathTempResultOcr);
                                string fileOcrBase64 = Convert.ToBase64String(bytesFileOcr);

                                files.Add(Path.GetFileName(pathTempResultOcr), fileOcrBase64);
                            }
                            Console.WriteLine($"Enviando arquivos para o ECM");
                            // Indexa Arquivos
                            
                            FileInfo fl = new FileInfo(file);
                            
                            fieldsToIndex.Add("IND_IDENTIFIER", fl.Directory.Name);
                            fl = null;
                            ZeevDocsIntegration.SendDocument(files, getPreFlowDoc(), getPosFlowDoc(), fieldsToIndex);

                            Console.WriteLine("Apagando temporarios");
                            // Deleta arquivos criado em TEMP
                            File.Delete(pathTempImageOcr);
                            File.Delete(pathTempResultOcr);
                            fileCount++;
                            processedFiles.Add(file);

                            Console.WriteLine($"Fim do processamento do arquivo {fileCount}");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);

                            var pathFlError = Path.Combine(_configuration["Files:PathTemp"], $"{Path.GetRandomFileName()}.log");
                            using (StreamWriter sw = new StreamWriter(pathFlError))
                            {
                                sw.WriteLine($"{ex.Message }\r\n {ex.StackTrace} \r\n");
                                sw.Close();
                            }
                        }
                    });

                processedFiles.ForEach(p =>
                {
                    allFiles.Remove(p);
                });

                processedFiles.Clear();
                Console.WriteLine($"Faltam {allFiles.Count} arquivos para processar");
            }
        }

        private void SetupBarcode(string xmlBarcodeTypes)
        {
            this.BarcodeSettings = XDocument.Parse(xmlBarcodeTypes);

            possibleFormats = new List<SingleDecodeType>();
            XElement barcodes = BarcodeSettings.Descendants("barcodes").FirstOrDefault();

            //Barcode types to be recognized
            foreach (XElement barcordeType in barcodes.Descendants("barcode"))
            {
                string barcodeName = barcordeType.Attribute("name").Value.ToLower();

                switch (barcodeName)
                {
                    /*case "unknown":
                        possibleFormats.Add(DecodeType.);
                        break;*/
                    case "qrcode":
                        possibleFormats.Add(DecodeType.QR);
                        break;
                    case "datamatrix":
                        possibleFormats.Add(DecodeType.DataMatrix);
                        break;
                    case "pdf417":
                        possibleFormats.Add(DecodeType.Pdf417);
                        break;
                    case "upca":
                        possibleFormats.Add(DecodeType.UPCA);
                        break;
                    case "upce":
                        possibleFormats.Add(DecodeType.UPCE);
                        break;
                    /*case "upcsupplemental2digit":
                        possibleFormats.Add(DecodeType.UPCSupplemental2Digit);
                        break;*/
                    /*case "upcsupplemental5digit":
                        possibleFormats.Add(DecodeType.UPCSupplemental5Digit);
                        break;*/
                    case "ean13":
                        possibleFormats.Add(DecodeType.EAN13);
                        break;
                    case "ean8":
                        possibleFormats.Add(DecodeType.EAN8);
                        break;
                    case "interleaved2of5":
                        possibleFormats.Add(DecodeType.Interleaved2of5);
                        break;
                    case "standard2of5":
                        possibleFormats.Add(DecodeType.Standard2of5);
                        break;
                    /*case "industrial2of5":
                        possibleFormats.Add(DecodeType.Industrial2of5);
                        break;*/
                    case "code39":
                        possibleFormats.Add(DecodeType.Code39Standard); //Aspose tem mais de um tipo code39
                        break;
                    case "code39extended":
                        possibleFormats.Add(DecodeType.Code39Extended);
                        break;
                    case "codabar":
                        possibleFormats.Add(DecodeType.Codabar);
                        break;
                    case "postnet":
                        possibleFormats.Add(DecodeType.Postnet);
                        break;
                    /*case "bookland":
                        possibleFormats.Add(DecodeType.Bookland);
                        break;*/
                    case "isbn":
                        possibleFormats.Add(DecodeType.ISBN);
                        break;
                    /*case "jan13":
                        possibleFormats.Add(DecodeType.JAN13);
                        break;*/
                    case "msi2mod10":
                    case "msimod10":
                    case "msimod11":
                    case "msimod11mod10":
                        possibleFormats.Add(DecodeType.MSI);
                        break;
                    /*case "msi2mod10":
                        possibleFormats.Add(DecodeType.MSI2Mod10);
                        break;
                    case "msimod11":
                        possibleFormats.Add(DecodeType.MSIMod11);
                        break;
                    case "msimod11mod10":
                        possibleFormats.Add(DecodeType.MSIMod11Mod10);
                        break;
                    case "modifiedplessey":
                        possibleFormats.Add(DecodeType.ModifiedPlessey);
                        break;*/
                    case "code11":
                        possibleFormats.Add(DecodeType.Code11);
                        break;
                    /*case "usd8":
                        possibleFormats.Add(DecodeType.USD8);
                        break;*/
                    /*case "ucc12":
                        possibleFormats.Add(DecodeType.UCC12);
                        break;
                    case "ucc13":
                        possibleFormats.Add(DecodeType.UCC13);
                        break;*/
                    /*case "logmars":
                        possibleFormats.Add(DecodeType.LOGMARS);
                        break;*/
                    case "code128":
                    case "code128a":
                    case "code128b":
                    case "code128c":
                        possibleFormats.Add(DecodeType.Code128);
                        break;
                    /*case "code128a":
                        possibleFormats.Add(DecodeType.Code128B);
                        break;
                    case "code128b":
                        possibleFormats.Add(DecodeType.DataMatrix);
                        break;
                    case "code128c":
                        possibleFormats.Add(DecodeType.Code128C);
                        break;*/
                    //case "itf14":
                    //    possibleFormats.Add(BarcodeFormat.ITF14);
                    //    break;
                    case "code93":
                        possibleFormats.Add(DecodeType.Code39Standard);
                        break;
                    /*case "telepen":
                        possibleFormats.Add(DecodeType.Telepen);
                        break;
                    case "fim":
                        possibleFormats.Add(DecodeType.FIM);
                        break;
                    case "upceanextension":
                        possibleFormats.Add(DecodeType.UPCEANExtension);
                        break;*/
                    case "aztec":
                        possibleFormats.Add(DecodeType.Aztec);
                        break;
                    /*case "rss14":
                        possibleFormats.Add(DecodeType.RSS14);
                        break;
                    case "rssexpanded":
                        possibleFormats.Add(DecodeType.RSSExpanded);
                        break;*/
                    case "maxicode":
                        possibleFormats.Add(DecodeType.MaxiCode);
                        break;
                    default:
                        //throw new Exception("Was informed a barcode type not recognized");
                        break;
                }
            }

            //Frames to be used
            XElement xFrames = BarcodeSettings.Descendants("frames").FirstOrDefault();
            Frames = xFrames.Attribute("choosenFrames").Value;

            //Rules to be applied
            XElement rules = BarcodeSettings.Descendants("rules").FirstOrDefault();
            foreach (XElement rule in rules.Descendants("rule"))
            {
                FieldCollection fields = null;

                XElement xFields = rule.Descendants("fields").FirstOrDefault();

                if (xFields != null)
                {
                    fields = new FieldCollection();

                    if (Rules == null)
                        Rules = new RulesCollection();

                    foreach (XElement xField in xFields.Descendants("field"))
                    {
                        fields.Add(xField.Attribute("name").Value, xField.Attribute("value").Value, xField.Attribute("description").Value);
                    }

                    Rules.Add(int.Parse(rule.Attribute("order").Value), rule.Attribute("searchType").Value, rule.Attribute("argument").Value, fields);
                }
            }
        }

        private string FindBarcode(string filename, out List<string> resultBarcodes)
        {

            List<string> resultTypes = new List<string>();
            string result = string.Empty;
            string barcodes = string.Empty;
            Bitmap toDecoder = null;
            try
            {
                toDecoder = new Bitmap(filename);
                Size aQuarter = new Size(toDecoder.Width / 2, toDecoder.Height / 2);
                Size halfH = new Size(toDecoder.Width, toDecoder.Height / 2);
                Size halfW = new Size(toDecoder.Width / 2, toDecoder.Height);

                if (Frames.Equals("Frame1"))
                {
                    Rectangle frame = new Rectangle(new Point(toDecoder.Width / 2, 0), aQuarter);
                    toDecoder = toDecoder.Clone(frame, toDecoder.PixelFormat);
                }
                else if (Frames.Equals("Frame2"))
                {
                    Rectangle frame = new Rectangle(new Point(0, 0), aQuarter);
                    toDecoder = toDecoder.Clone(frame, toDecoder.PixelFormat);
                }
                else if (Frames.Equals("Frame3"))
                {
                    Rectangle frame = new Rectangle(new Point(0, toDecoder.Height / 2), aQuarter);
                    toDecoder = toDecoder.Clone(frame, toDecoder.PixelFormat);
                }
                else if (Frames.Equals("Frame4"))
                {
                    Rectangle frame = new Rectangle(new Point(toDecoder.Width / 2, toDecoder.Height / 2), aQuarter);
                    toDecoder = toDecoder.Clone(frame, toDecoder.PixelFormat);
                }
                else if (Frames.Equals("Frame1,Frame2"))
                {
                    Rectangle frame = new Rectangle(new Point(0, 0), halfH);
                    toDecoder = toDecoder.Clone(frame, toDecoder.PixelFormat);
                }
                else if (Frames.Equals("Frame3,Frame4"))
                {
                    Rectangle frame = new Rectangle(new Point(0, toDecoder.Height / 2), halfH);
                    toDecoder = toDecoder.Clone(frame, toDecoder.PixelFormat);
                }
                else if (Frames.Equals("Frame2,Frame3"))
                {
                    Rectangle frame = new Rectangle(new Point(0, 0), halfW);
                    toDecoder = toDecoder.Clone(frame, toDecoder.PixelFormat);
                }
                else if (Frames.Equals("Frame1,Frame4"))
                {
                    Rectangle frame = new Rectangle(new Point(toDecoder.Width / 2, 0), halfW);
                    toDecoder = toDecoder.Clone(frame, toDecoder.PixelFormat);
                }
                BarCodeReader barCodeDecoder = new BarCodeReader(toDecoder, possibleFormats.ToArray());
                BarCodeResult[] decodedBarcodes = barCodeDecoder.ReadBarCodes();

                if (decodedBarcodes != null)
                {
                    //barcodes = new XElement("barcodes");                    

                    List<BarCodeResult> rulesApplieds = new List<BarCodeResult>();
                    if (Rules != null)
                    {
                        foreach (RulesEntity rule in Rules)
                        {
                            if (rule.SearchType.Equals("StartWith", StringComparison.CurrentCultureIgnoreCase))
                            {
                                rulesApplieds.AddRange(decodedBarcodes.Where(x => x.CodeText.StartsWith(rule.Argument)).Select(x => x).ToArray());
                                decodedBarcodes = decodedBarcodes.Where(x => !x.CodeText.StartsWith(rule.Argument)).Select(x => x).ToArray();
                                resultTypes.AddRange(decodedBarcodes.Where(x => !x.CodeText.StartsWith(rule.Argument)).Select(x => x.CodeTypeName));
                            }
                            else if (rule.SearchType.Equals("Contains", StringComparison.CurrentCultureIgnoreCase))
                            {
                                rulesApplieds.AddRange(decodedBarcodes.Where(x => x.CodeText.Contains(rule.Argument)).Select(x => x).ToArray());
                                decodedBarcodes = decodedBarcodes.Where(x => !x.CodeText.Contains(rule.Argument)).Select(x => x).ToArray();
                                resultTypes.AddRange(decodedBarcodes.Where(x => !x.CodeText.Contains(rule.Argument)).Select(x => x.CodeTypeName));
                            }
                            else if (rule.SearchType.Equals("EndWith", StringComparison.CurrentCultureIgnoreCase))
                            {
                                rulesApplieds.AddRange(decodedBarcodes.Where(x => x.CodeText.EndsWith(rule.Argument)).Select(x => x).ToArray());
                                decodedBarcodes = decodedBarcodes.Where(x => !x.CodeText.EndsWith(rule.Argument)).Select(x => x).ToArray();
                                resultTypes.AddRange(decodedBarcodes.Where(x => !x.CodeText.EndsWith(rule.Argument)).Select(x => x.CodeTypeName));
                            }
                            else
                            {
                                resultTypes.AddRange(decodedBarcodes.Select(s => s.CodeTypeName));
                            }
                        }
                    }
                    else
                    {
                        resultTypes.AddRange(decodedBarcodes.Select(s => s.CodeTypeName));
                    }

                    barcodes = string.Join(';', decodedBarcodes.Select(x => x.CodeText));
                }
                else
                {
                    barcodes = "Não foi possível localizar nenhum código de barras";
                }

                resultBarcodes = resultTypes;
                result = barcodes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (toDecoder != null)
                    toDecoder.Dispose();
            }

            return result;
        }

        private Task ClearRowFiles()
        {
            Directory
                .GetFiles(Directory.GetCurrentDirectory(), "*.png", SearchOption.TopDirectoryOnly)
                .ToList().ForEach(f => {
                    File.Delete(f);
                    });

            return null;
        }
#endregion

    }
}
