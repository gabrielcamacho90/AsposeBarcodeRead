using System;
using System.Collections.Generic;


namespace SML.Core.Orquestra.ECM.AntiCorruption.Application.Interfaces
{
    public interface IUpload : IDisposable
    {
        bool UploadFile(string protocol, string fileName, byte[] fileContent);
        bool UploadBase64File(string protocol, string fileName, string base64FileContent);
        void UploadFile(string protocol, List<KeyValuePair<string, string>> files);
    }
}
