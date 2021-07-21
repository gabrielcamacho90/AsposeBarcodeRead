using SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.ApplicationStructure;
using SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.Common;
using SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.DocumentRequest;
using SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.DocumentResponse;
using SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.UploadFileRequest.Enumerators;
using System;
using System.Collections.Generic;

namespace SML.Core.Orquestra.ECM.AntiCorruption.Application.Interfaces
{
    public interface ISilentLoginApplication : IDisposable
    {
        string GetProtocolToUploadFile(long indexId, int fileCount, FileOperationEnum operation);
        void FinishUpload(string protocol);
        void RemoveFilesFromFolderProtocol(string protocol);

        //[Obsolete("Save is deprecated, please use Save(long indexId, DocumentWorkflow documentWorkflow, Dictionary<string, string> fields) instead.")]
        void Save(long indexId, Dictionary<string, string> fields);
        /// <summary>
        ///  Essa função funciona apenas para a versão 2.9 em diante do e-content
        /// </summary>
        /// <param name="indexId"></param>
        /// <param name="documentWorkflow"></param>
        /// <param name="fields"></param>
        void Save(long indexId, DocumentWorkflow documentWorkflow, Dictionary<string, string> fields);

        [Obsolete("PreIndex is deprecated, please use PreIndex(DocumentWorkflow documentWorkflow, Dictionary<string, string> fields) instead.")]
        long PreIndex(int doctypeId, int queueId, int situationId, int pendencyId, Dictionary<string, string> fields);
        /// <summary>
        /// Essa função funciona apenas para a versão 2.9 em diante do e-content
        /// </summary>
        /// <param name="documentWorkflow">Escolhe a melhor opição para Fila, Situação, Tipo do Documento e Pendência</param>
        /// <param name="fields"> Campos da Aplicação</param>
        /// <returns>IND_ID(codigo do documento)</returns>
        long PreIndex(DocumentWorkflow documentWorkflow, Dictionary<string, string> fields);

        void AdHoc(long indexId, string queueName = null, string situationName = null, string pendencyName = null);
        [Obsolete("AdHoc is deprecated, please use AdHoc(long indexId, string queueName = null, string situationName = null, string pendencyName = null) instead.")]
        void AdHoc(long indexId, int queueId, int situationId, int pendencyId);

        void AdHocQueue(long indexId, int queueId);
        void AdHocSituation(long indexId, int situationId);
        void AdHocPendency(long indexId, int pendencyId);

        void Delete(long indexId);
        void Restore(long indexId);

        OrquestraECMApplicationStructureResponseRoot GetXmlModel();

        OrquestraECMResponseRoot GetDocument(OrquestraECMRequestPaginate paginate, List<OrquestraECMRequestRequestField> requestFields, List<OrquestraECMRequestRequestField> resultFields);
        OrquestraECMResponseRoot GetDocument(Dictionary<string, string> requestFields, List<string> resultFields, int pageIndex = 0, int pageSize = 1, bool returnFile = true, bool ignoreDeleted = true, DocumentWorkflow docFlow = null);

        KeyValuePair<long, string> GetDocumentFile(long indexId, long fileId, string tempPath, int fileIndexView = 0);
        List<byte> GetDocumentFile(long indexId, long fileId, out string fileAlias);

        string GetUserToken(string userName, string password, string domain = "econtent");
        void SetAppCode(string appCode);
    }
}
