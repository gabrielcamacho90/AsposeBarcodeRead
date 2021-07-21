using SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.DocumentRequest;
using SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.ExecuteActionRequest;
using SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.FileRequest;
using SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.UploadFileRequest;
using System;

namespace SML.Core.Orquestra.ECM.AntiCorruption.Application.Interfaces
{
    public interface ISilentLogin : IDisposable
    {
        string GetProtocolToUploadFile(UploadFileRequestRoot request);
        bool RemoveFilesFromFolderProtocol(string protocol);
        void FinishUpload(string protocol);
        string GetProtocol(string xml);
        string GetDocument(OrquestraECMRequestRoot request);
        string ExecuteAction(ExecuteActionRequestRoot request);
        string GetDocumentFile(OrquestraECMFileRequestRoot request);
        string GetUserToken(string userName, string password, string domain);
        string GetXmlModel(string appCode);
    }
}
