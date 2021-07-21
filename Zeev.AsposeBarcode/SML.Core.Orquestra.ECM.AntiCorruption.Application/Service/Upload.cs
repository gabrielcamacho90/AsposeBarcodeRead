using RestSharp;
using SML.Core.Orquestra.ECM.AntiCorruption.Application.Interfaces;
using SML.Core.Orquestra.ECM.AntiCorruption.Domain.Helper;
using SML.Core.Orquestra.ECM.AntiCorruption.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.IO;

namespace SML.Core.Orquestra.ECM.AntiCorruption.Services.Upload
{
    internal class Upload : IUpload
    {
        #region Properties

        private RestClient _client;
        private RestRequest _request;
        private string _endpointAddress;
        private string _contentType = "text/xml;charset=UTF-8";
        #endregion
        #region Constructors
        public Upload(string protocol, string ecmAddress)
        {
            _endpointAddress = $"{ecmAddress}/webscan/services/upload.asmx";
            _client = new RestClient(_endpointAddress);
            _request = new RestRequest(Method.POST);
            _request.AddHeader("Content-Type", _contentType);
        }
        #endregion
        #region Methods
        public bool UploadFile(string protocol, string fileName, byte[] fileContent)
        {
            RestClient client = new RestClient(_endpointAddress);
            RestRequest request = new RestRequest(Method.POST);

            string envelope = SoapHelper.GetUploadEnvelope(protocol, fileName, FileHelper.GetBase64(fileContent));
            request.AddParameter(_contentType, envelope, ParameterType.RequestBody);
            var response = client.Execute(request);
            return bool.Parse(SoapHelper.ExtractBody(response.Content));
        }

        public bool UploadBase64File(string protocol, string fileName, string base64FileContent)
        {
            RestClient client = new RestClient(_endpointAddress);
            RestRequest request = new RestRequest(Method.POST);

            string envelope = SoapHelper.GetUploadEnvelope(protocol, fileName, base64FileContent);
            request.AddParameter(_contentType, envelope, ParameterType.RequestBody);
            var response = client.Execute(request);
            return bool.Parse(SoapHelper.ExtractBody(response.Content));
        }

        public void UploadFile(string protocol, List<KeyValuePair<string, string>> files)
        {
            files.ForEach(x => UploadFile(protocol, x.Key, File.ReadAllBytes(x.Value)));
        }
        #endregion

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _request = null;
                    _client = null;
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
