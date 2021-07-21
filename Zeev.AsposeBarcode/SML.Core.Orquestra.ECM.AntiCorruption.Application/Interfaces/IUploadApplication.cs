using SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.UploadFileRequest.Enumerators;
using System;
using System.Collections.Generic;

namespace SML.Core.Orquestra.ECM.AntiCorruption.Application.Interfaces
{
    public interface IUploadApplication : IDisposable
    {
        void UploadFiles(long indexId, FileOperationEnum operation, List<KeyValuePair<string, string>> files);
        void UploadFiles(long indexId, FileOperationEnum operation, List<KeyValuePair<string, byte[]>> files);
    }
}
