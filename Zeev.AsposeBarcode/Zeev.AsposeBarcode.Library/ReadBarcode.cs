using Aspose.BarCode.BarCodeRecognition;
using Aspose.OCR;
using Microsoft.Extensions.Configuration;
using SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.Common;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;

namespace Zeev.AsposeBarcode.Library
{
    public class ReadBarcode
    {
        private readonly IConfiguration _configuration;
        public ReadBarcode(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ReadFilesByAspose()
        {
            string pathLicenseAspose = _configuration["Files:PathLicenseAspose"];
            string pathFiles = _configuration["Files:PathDocs"];

            Aspose.BarCode.License license = new Aspose.BarCode.License();
            license.SetLicense(pathLicenseAspose);

            Aspose.OCR.License licenseOcr = new Aspose.OCR.License();
            licenseOcr.SetLicense(pathLicenseAspose);

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
    }
}
