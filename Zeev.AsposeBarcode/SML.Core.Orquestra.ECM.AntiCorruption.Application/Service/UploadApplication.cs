using SML.Core.Orquestra.ECM.AntiCorruption.Application.Interfaces;
using SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.UploadFileRequest.Enumerators;
using SML.Core.Orquestra.ECM.AntiCorruption.Services.Upload;
using System;
using System.Collections.Generic;

namespace SML.Core.Orquestra.ECM.AntiCorruption.Application.Service
{
    public class UploadApplication : IUploadApplication
    {
        #region Fields


        #endregion

        #region Properties

        private string _token { get; set; }
        private string _adHocUser { get; set; }
        private string _identification { get; set; }
        private string _appCode { get; set; }
        private string _ecmAddress { get; set; }
        private ISilentLoginApplication _silentLoginApplication { get; set; }
        private Services.Upload.Upload _upload { get; set; }

        #endregion

        #region Constructors

        public UploadApplication(string appCode, string token, string identification, string ecmAddress, string adHocUser = null)
        {
            _appCode = appCode;
            _token = token;
            _adHocUser = adHocUser;
            _identification = identification;
            _ecmAddress = ecmAddress;
            _silentLoginApplication = new SilentLoginApplication(_ecmAddress, appCode, token, identification, adHocUser);
        }

        #endregion

        #region Interface Methods

        public void UploadFiles(long indexId, FileOperationEnum operation, List<KeyValuePair<string, string>> files)
        {
            var protocol = _silentLoginApplication.GetProtocolToUploadFile(indexId, files.Count, operation);
            using (_upload = new Upload(protocol, _ecmAddress))
                files.ForEach(x => UploadFile(protocol, x.Key, x.Value));
            _silentLoginApplication.FinishUpload(protocol);
        }

        public void UploadFiles(long indexId, FileOperationEnum operation, List<KeyValuePair<string, byte[]>> files)
        {
            var protocol = _silentLoginApplication.GetProtocolToUploadFile(indexId, files.Count, operation);
            using (_upload = new Upload(protocol, _ecmAddress))
                files.ForEach(x => UploadFile(protocol, x.Key, x.Value));
            _silentLoginApplication.FinishUpload(protocol);
        }
        #endregion

        #region Private Methods
        private void UploadFile(string protocol, string fileName, byte[] fileContent)
        {
            bool response = _upload.UploadFile(protocol, fileName, fileContent);
            if (!response)
            {
                _silentLoginApplication.RemoveFilesFromFolderProtocol(protocol);
                throw new Exception("Não foi possível realizar o Upload dos arquivos, favor verificar nos arquivos de Log.");
            }
        }

        private void UploadFile(string protocol, string fileName, string base64File)
        {
            bool response = _upload.UploadBase64File(protocol, fileName, base64File);
            if (!response)
            {
                _silentLoginApplication.RemoveFilesFromFolderProtocol(protocol);
                throw new Exception("Não foi possível realizar o Upload dos arquivos, favor verificar nos arquivos de Log.");
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
                    _token = null;
                    _adHocUser = null;
                    _identification = null;
                    _silentLoginApplication.Dispose();
                    _silentLoginApplication = null;
                    _upload = null;
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
