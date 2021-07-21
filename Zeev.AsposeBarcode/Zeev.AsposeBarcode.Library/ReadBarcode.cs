﻿using Aspose.BarCode.BarCodeRecognition;
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

            foreach (var file in Directory.GetFiles(pathFiles, "*.tif", SearchOption.AllDirectories))
            {
                string barCode = string.Empty;
                string resultOcr = string.Empty;
                string ocrFilePath = string.Empty;

                try
                {
                    string fileName = Path.GetFileName(file);
                    string pathTempImageOcr = Path.Combine(_configuration["Files:PathTemp"], $"{Path.GetRandomFileName()}.bmp");
                    string pathTempResultOcr = Path.Combine(_configuration["Files:PathTemp"], $"{Path.GetRandomFileName()}.txt");
                    List<string> typesBarCode = new List<string>();
                    List<string> resultBarCode = new List<string>();

                    // Valida se diretório existe
                    if (!Directory.Exists(Path.GetDirectoryName(pathTempImageOcr)))
                        Directory.CreateDirectory(Path.GetDirectoryName(pathTempImageOcr));

                    using (var bmpFile = new System.Drawing.Bitmap(file))
                    {
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

                        using (StreamWriter sw = new StreamWriter(pathTempResultOcr))
                        {
                            sw.WriteLine(resultOcr);
                            sw.Close();
                        }
                    }

                    //TODO: Indexar ECM
                    // Campos
                    var fieldsToIndex = GetIndexFields(String.Join(",", typesBarCode), String.Join(",", resultBarCode));

                    var files = new Dictionary<string, string>();                                        
                    files.Add(file, Path.GetFileName(file));

                    if (!string.IsNullOrEmpty(resultOcr))
                    {
                        ocrFilePath = GetOCRFileText(resultOcr);
                        files.Add(file, Path.GetFileName(ocrFilePath));
                    }

                    ZeevDocsIntegration.SendDocument(files, getPreFlowDoc(), getPosFlowDoc(), fieldsToIndex);
                    //ARQUIVOS PARA INDEXAR
                    //file
                    //pathTempResultOcr

                    // Deleta arquivos criado em TEMP
                    File.Delete(pathTempImageOcr);
                    File.Delete(pathTempResultOcr);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
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
                QueueName = "PREINDEXADO",
                DoctypeId = 3,
                SituationName = "EMPROCESSO",
                PendencyName = "NENHUMA"
            };
        }

        private DocumentWorkflow getPosFlowDoc()
        {
            return new DocumentWorkflow
            {
                QueueName = "FINALIZADO",                
                SituationName = "PROCESSADO",
                PendencyName = "NENHUMA"
            };
        }

        private string GetOCRFileText(string fileContent)
        {
            var fileName = Path.GetTempFileName();
            fileName = fileName.Substring(fileName.IndexOf(".") - 1) + ".txt";
            File.WriteAllText(fileName, fileContent);
            return fileName;
        }
    }
}
