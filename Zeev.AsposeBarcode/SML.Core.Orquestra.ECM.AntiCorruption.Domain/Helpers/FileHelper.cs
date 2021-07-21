using System;
using System.IO;

namespace SML.Core.Orquestra.ECM.AntiCorruption.Domain.Helpers
{
    public static class FileHelper
    {

        public static string GetBase64(byte[] fileBytes)
        => Convert.ToBase64String(fileBytes);

        public static string GetBase64(string filePath)
        {
            byte[] fileBytes = File.ReadAllBytes(filePath);
            return Convert.ToBase64String(fileBytes);
        }

        public static string GetFileName(string filePath)
        => Path.GetFileName(filePath);


        public static string GetFileExtension(string filePath)
        => Path.GetExtension(filePath);


    }
}
