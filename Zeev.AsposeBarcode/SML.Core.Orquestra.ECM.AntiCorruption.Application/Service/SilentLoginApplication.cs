using SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.ApplicationStructure;
using SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.Common;
using SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.Common.Enumerators;
using SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.DocumentRequest;
using SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.DocumentResponse;
using SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.ExecuteActionRequest;
using SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.FileRequest;
using SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.FileResponse;
using SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.UploadFileRequest;
using SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.UploadFileRequest.Enumerators;
using SML.Core.Orquestra.ECM.AntiCorruption.Domain.Extension;
using SML.Core.Orquestra.ECM.AntiCorruption.Domain.Helpers.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Xml.Linq;


namespace SML.Core.Orquestra.ECM.AntiCorruption.Application.Service
{
    public class SilentLoginApplication : Interfaces.ISilentLoginApplication
    {
        #region Properties

        internal string _userToken { get; set; }
        internal string _adHocUser { get; set; }
        internal string _identification { get; set; }
        internal string _appCode { get; set; }
        internal EndpointAddress _endPoint { get; set; }
        internal Interfaces.ISilentLogin _silentLogin { get; set; }


        #endregion

        #region Constructors

        public SilentLoginApplication(string ecmAddress, string appCode, string userToken, string identification, string adHocUser = null)
        {
            SetProperties(appCode, userToken, adHocUser, identification);
            _silentLogin = new SilentLogin(ecmAddress);
        }

        void SetProperties(string appCode, string userToken, string adHocUser, string identification)
        {
            _appCode = appCode;
            _userToken = userToken;
            _adHocUser = adHocUser;
            _identification = identification;
        }

        #endregion

        #region Interface Methods

        #region Upload File
        public string GetProtocolToUploadFile(long indexId, int fileCount, FileOperationEnum operation)
        {
            var request = new UploadFileRequestRoot(_identification, ModuleNameEnum.Upload);
            request.Header.AdHocUser = _adHocUser;
            request.Header.Application.Code = _appCode;
            request.Header.UserToken = _userToken;
            request.Structure.Document.IndexId = indexId;
            request.Structure.Document.FileCount = fileCount;
            request.Structure.Document.Operation = operation;

            return _silentLogin.GetProtocolToUploadFile(request);
        }
        public void FinishUpload(string protocol)
        {
            _silentLogin.FinishUpload(protocol);
        }
        public void RemoveFilesFromFolderProtocol(string protocol)
        {
            _silentLogin.RemoveFilesFromFolderProtocol(protocol);
        }
        #endregion

        #region Execute Action Methods

        public void Save(long indexId, DocumentWorkflow documentWorkflow, Dictionary<string, string> fields)
        {
            var request = GetExecuteActionRootObject();
            var eventObj = new ExecuteActionRequestEvent(EventNameEnum.Save);

            eventObj.Content.DocumentDestiny = indexId;
            eventObj.Content.Fields = BindFields(fields);
            eventObj.Content.Fields.AddRange(documentWorkflow.GetValuesNotNullOrZero());
            request.Structure.Events.Add(eventObj);

            _silentLogin.ExecuteAction(request);

        }
        public void Save(long indexId, Dictionary<string, string> fields)
        {
            var request = GetExecuteActionRootObject();

            var eventObj = new ExecuteActionRequestEvent(EventNameEnum.Save);

            eventObj.Content.DocumentDestiny = indexId;
            eventObj.Content.Fields = BindFields(fields);
            request.Structure.Events.Add(eventObj);

            _silentLogin.ExecuteAction(request);
        }

        public long PreIndex(int doctypeId, int queueId, int situationId, int pendencyId, Dictionary<string, string> fields)
        {
            long indexId = 0;
            var request = GetExecuteActionRootObject();
            var eventObj = new ExecuteActionRequestEvent(EventNameEnum.PreIndex);
            eventObj.Content.Fields = new List<ExecuteActionRequestField>();
            eventObj.Content.Fields = BindFields(fields);
            eventObj.Content.Fields.AddRange(new List<ExecuteActionRequestField>()
            {
                new ExecuteActionRequestField { Name = "DOCTYPE_ID", Value = doctypeId.ToString() },
                new ExecuteActionRequestField { Name = "IDXQUEUE_ID", Value = queueId.ToString()  },
                new ExecuteActionRequestField { Name = "IDXSITDOC_ID", Value = situationId.ToString() },
                new ExecuteActionRequestField { Name = "PNDRSN_ID", Value = pendencyId.ToString() },
            });
            request.Structure.Events.Add(eventObj);
            var response = _silentLogin.ExecuteAction(request);
            var result = XElement.Parse(response);
            long.TryParse(result.Value, out indexId);
            return indexId;
        }
        public long PreIndex(DocumentWorkflow documentWorkflow, Dictionary<string, string> fields)
        {
            long indexId = 0;
            var request = GetExecuteActionRootObject();

            var eventObj = new ExecuteActionRequestEvent(EventNameEnum.PreIndex);
            eventObj.Content.Fields = new List<ExecuteActionRequestField>();
            eventObj.Content.Fields = BindFields(fields);
            eventObj.Content.Fields.AddRange(documentWorkflow.GetValuesNotNullOrZero());
            request.Structure.Events.Add(eventObj);

            var response = _silentLogin.ExecuteAction(request);
            var result = XElement.Parse(response);
            long.TryParse(result.Value, out indexId);

            return indexId;
        }

        public void AdHoc(long indexId, int queueId, int situationId, int pendencyId)
            => _AdHoc(indexId, queueId, situationId, pendencyId);

        public void AdHoc(long indexId, string queueName = null, string situationName = null, string pendencyName = null)
        {
            var request = GetExecuteActionRootObject();
            var eventObj = new ExecuteActionRequestEvent(EventNameEnum.AdHoc);
            eventObj.Content.DocumentDestiny = indexId;

            var fields = new List<ExecuteActionRequestField>();

            if (queueName != null)
                fields.Add(new ExecuteActionRequestField { Name = "IDXQUEUE_ID", CommandOrName = queueName });
            if (situationName != null)
                fields.Add(new ExecuteActionRequestField { Name = "IDXSITDOC_ID", CommandOrName = situationName });
            if (pendencyName != null)
                fields.Add(new ExecuteActionRequestField { Name = "PNDRSN_ID", CommandOrName = pendencyName });

            eventObj.Content.Fields = fields;

            request.Structure.Events.Add(eventObj);
            _silentLogin.ExecuteAction(request);
        }

        private void _AdHoc(long indexId, int? queueId = null, int? situationId = null, int? pendencyId = null)
        {
            var request = GetExecuteActionRootObject();
            var eventObj = new ExecuteActionRequestEvent(EventNameEnum.AdHoc);
            eventObj.Content.DocumentDestiny = indexId;

            var fields = new List<ExecuteActionRequestField>();

            if (queueId != null)
                fields.Add(new ExecuteActionRequestField { Name = "IDXQUEUE_ID", Value = queueId.ToString() });
            if (situationId != null)
                fields.Add(new ExecuteActionRequestField { Name = "IDXSITDOC_ID", Value = situationId.ToString() });
            if (pendencyId != null)
                fields.Add(new ExecuteActionRequestField { Name = "PNDRSN_ID", Value = pendencyId.ToString() });

            eventObj.Content.Fields = fields;

            request.Structure.Events.Add(eventObj);
            _silentLogin.ExecuteAction(request);
        }
        public void AdHocQueue(long indexId, int queueId)
            => _AdHoc(indexId: indexId, queueId: queueId, situationId: null, pendencyId: null);
        public void AdHocSituation(long indexId, int situationId)
            => _AdHoc(indexId: indexId, queueId: null, situationId: situationId, pendencyId: null);
        public void AdHocPendency(long indexId, int pendencyId)
            => _AdHoc(indexId: indexId, queueId: null, situationId: null, pendencyId: pendencyId);

        public void Delete(long indexId)
        {
            var request = GetExecuteActionRootObject();
            var eventObj = new ExecuteActionRequestEvent(EventNameEnum.Delete);
            eventObj.Content.DocumentDestiny = indexId;
            request.Structure.Events.Add(eventObj);
            _silentLogin.ExecuteAction(request);
        }
        public void Restore(long indexId)
        {
            var request = GetExecuteActionRootObject();
            var eventObj = new ExecuteActionRequestEvent(EventNameEnum.Restore);
            eventObj.Content.DocumentDestiny = indexId;
            request.Structure.Events.Add(eventObj);
            _silentLogin.ExecuteAction(request);
        }

        private ExecuteActionRequestRoot GetExecuteActionRootObject()
        {
            var request = new ExecuteActionRequestRoot(_identification, ModuleNameEnum.ExecuteAction);
            request.Header.AdHocUser = _adHocUser;
            request.Header.Application.Code = _appCode;
            request.Header.Identification = _identification;
            request.Header.UserToken = _userToken;
            return request;
        }
        private List<ExecuteActionRequestField> BindFields(Dictionary<string, string> fields)
            => fields.Where(x => !x.Key.StartsWith("PICK_LIST")).Select(x => new ExecuteActionRequestField { Name = x.Key, Value = x.Value }).ToList();

        #endregion
        public OrquestraECMApplicationStructureResponseRoot GetXmlModel()
        {
            var result = _silentLogin.GetXmlModel(_appCode);
            return result.ToObject(new OrquestraECMApplicationStructureResponseRoot());
        }
        public OrquestraECMResponseRoot GetDocument(OrquestraECMRequestPaginate paginate, List<OrquestraECMRequestRequestField> requestFields, List<OrquestraECMRequestRequestField> resultFields)
        {
            var request = new OrquestraECMRequestRoot(_adHocUser, _identification, ModuleNameEnum.Search, _appCode, _userToken);
            var @event = new OrquestraECMRequestEvent();
            @event.EcontentRequestContent.EContentRequestPaginate = paginate;
            @event.EcontentRequestContent.RequestFields.AddRange(requestFields);
            @event.EcontentRequestContent.ResultFields.AddRange(resultFields);
            request.Structure.Events.Add(@event);

            var result = _silentLogin.GetDocument(request);

            return result.ToObject(new OrquestraECMResponseRoot());
        }

        public OrquestraECMResponseRoot GetDocument(Dictionary<string, string> requestFields, List<string> resultFields, int pageIndex = 0, int pageSize = 1, bool returnFile = true, bool ignoreDeleted = true, DocumentWorkflow docFlow = null)
        {

            var paginate = new OrquestraECMRequestPaginate
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                ReturnFile = returnFile
            };

            var filterFields = requestFields.Select(x => new OrquestraECMRequestRequestField { Name = x.Key, Value = x.Value }).ToList();

            if (ignoreDeleted)
                filterFields.Add(new OrquestraECMRequestRequestField { Name = "IND_DELETED", Value = "0" });


            if (docFlow != null)
            {
                List<OrquestraECMRequestRequestField> flowFields = new List<OrquestraECMRequestRequestField>();

                docFlow.GetValuesNotNullOrZero().ForEach(f =>
                {
                    flowFields.Add(new OrquestraECMRequestRequestField
                    {
                        CommandOrName = f.CommandOrName,
                        Name = f.Name
                    });
                });

                filterFields.AddRange(flowFields);
            }

            var responseFields = resultFields.Select(x => new OrquestraECMRequestRequestField { Name = x }).ToList();

            return GetDocument(paginate, filterFields, responseFields);
        }

        public List<byte> GetDocumentFile(long indexId, long fileId, out string fileAlias)
        {
            var request = new OrquestraECMFileRequestRoot(_identification, ModuleNameEnum.GetDocument);
            request.Header.Application.Code = _appCode;
            request.Header.AdHocUser = _adHocUser;
            request.Header.UserToken = _userToken;

            var @event = new OrquestraECMFileRequestEvent();
            @event.Name = EventNameEnum.GetDocument;
            @event.Content.DocumentFile.IndId = indexId;
            @event.Content.DocumentFile.FileId = fileId;
            @event.Content.Paginate.PackageSize = 1048576 / 10;

            var byteList = new List<byte>();
            int currentPackage = 0;
            int totalPackage = 1;
            fileAlias = string.Empty;
            while (currentPackage < totalPackage)
            {
                request.Structure.Events.Clear();
                @event.Content.Paginate.PackageIndex = currentPackage;
                request.Structure.Events.Add(@event);

                var result = _silentLogin.GetDocumentFile(request);
                var response = result.ToObject(new OrquestraECMFileResponseRoot());

                totalPackage = response.Document.Files.TotalPackage;

                var base64 = response.Document.Files.Files.FirstOrDefault()?.FileBase64Stream;

                if (string.IsNullOrEmpty(fileAlias))
                    fileAlias = response.Document.Files.Files.FirstOrDefault()?.FileAlias;

                response.Document.Files.Files.FirstOrDefault()?.FillByteStream();
                byteList.AddRange(response.Document.Files.Files.FirstOrDefault()?.GetByteStream());
                currentPackage++;
            }
            return byteList;
        }
        public KeyValuePair<long, string> GetDocumentFile(long indexId, long fileId, string tempPath, int fileIndexView = 0)
        {
            string fileAlias;
            var byteList = GetDocumentFile(indexId, fileId, out fileAlias);

            return SaveFileOnDisk(indexId, fileId, tempPath, fileIndexView, byteList, fileAlias);

        }

        public string GetUserToken(string userName, string password, string domain = "econtent")
            => _silentLogin.GetUserToken(userName, password, domain);


        public void SetAppCode(string appCode)
        {
            _appCode = appCode;
        }
        #endregion

        #region Private Methods
        private KeyValuePair<long, string> SaveFileOnDisk(long indexId, long fileId, string tempPath, int fileIndexView, List<byte> byteList, string fileAlias)
        {
            tempPath = Path.Combine(tempPath, _appCode, indexId.ToString());
            if (!Directory.Exists(tempPath)) Directory.CreateDirectory(tempPath);
            var stringIndex = fileIndexView > 0 ? fileIndexView.ToString("D3") + "-" : string.Empty;
            var filePath = Path.Combine(tempPath, stringIndex + fileAlias);

            using (var s = new FileStream(filePath, FileMode.Create))
            {
                s.Write(byteList.ToArray(), 0, byteList.Count);
                s.Close();
                s.Dispose();

                return new KeyValuePair<long, string>(fileId, filePath);
            }
        }
        #endregion

        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _userToken = null;
                    _adHocUser = null;
                    _identification = null;
                    _silentLogin.Dispose();
                    _silentLogin = null;
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
