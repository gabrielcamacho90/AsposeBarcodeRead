using RestSharp;
using SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.DocumentRequest;
using SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.ExecuteActionRequest;
using SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.FileRequest;
using SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.UploadFileRequest;
using SML.Core.Orquestra.ECM.AntiCorruption.Domain.Helper;
using SML.Core.Orquestra.ECM.AntiCorruption.Domain.Helpers;
using SML.Core.Orquestra.ECM.AntiCorruption.Domain.Helpers.Extensions;
using System;


namespace SML.Core.Orquestra.ECM.AntiCorruption.Application.Service
{
    internal class SilentLogin : Interfaces.ISilentLogin
    {
        #region Properties
        private RestClient _client;
        private RestRequest _request;
        private string _endpointAddress;
        private string _contentType = "application/soap+xml";

        #endregion

        #region Constructors

        public SilentLogin(string ecmAddress)
        {
            _endpointAddress = SilentLoginHelper.GetSilentLoginEndPoint(ecmAddress);
            _client = new RestClient(_endpointAddress);
            _request = new RestRequest(Method.POST);
            _request.AddHeader("Content-Type", _contentType);
        }

        #endregion

        #region Interface Methods
        public string GetProtocolToUploadFile(UploadFileRequestRoot request)
        {
            _request.Parameters.Clear();
            string xml = request.ToXmlString();
            string body = SoapHelper.GetBodyToGetProtocolToUploadFile(xml);
            string envelope = SoapHelper.GetSilentEnvelope(_endpointAddress, body, "GetProtocolToUploadFile");
            _request.AddParameter(_contentType, envelope, ParameterType.RequestBody);
            var response = _client.Execute(_request);
            return SoapHelper.ExtractBody(response.Content);
        }

        public bool RemoveFilesFromFolderProtocol(string protocol)
        {
            _request.Parameters.Clear();
            string body = SoapHelper.GetBodyToFinishUpload(protocol);
            string envelope = SoapHelper.GetSilentEnvelope(_endpointAddress, body, "RemoveFilesFromFolderProtocol");
            _request.AddParameter(_contentType, envelope, ParameterType.RequestBody);
            var response = _client.Execute(_request);
            return bool.Parse(SoapHelper.ExtractBody(response.Content));
        }

        public void FinishUpload(string protocol)
        {
            _request.Parameters.Clear();
            string body = SoapHelper.GetBodyToFinishUpload(protocol);
            string envelope = SoapHelper.GetSilentEnvelope(_endpointAddress, body, "FinishUpload");
            _request.AddParameter(_contentType, envelope, ParameterType.RequestBody);
            _client.Execute(_request);
        }

        public string GetProtocol(string xml)
        {
            _request.Parameters.Clear();
            string body = SoapHelper.GetBodyToGetProtocol(xml);
            string envelope = SoapHelper.GetSilentEnvelope(_endpointAddress, body, "GetProtocol");
            _request.AddParameter(_contentType, envelope, ParameterType.RequestBody);
            IRestResponse response = _client.Execute(_request);
            return SoapHelper.ExtractBody(response.Content);
        }

        public string GetDocument(OrquestraECMRequestRoot request)
        {
            _request.Parameters.Clear();
            string xml = request.ToXmlString();
            string body = SoapHelper.GetBodyToGetDocument(xml);
            string envelope = SoapHelper.GetSilentEnvelope(_endpointAddress, body, "GetDocument");
            _request.AddParameter(_contentType, envelope, ParameterType.RequestBody);
            IRestResponse response = _client.Execute(_request);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception($"Ocorreu um erro ao obter os documentos: {response.Content}");
            return SoapHelper.ExtractBody(response.Content);
        }

        public string ExecuteAction(ExecuteActionRequestRoot request)
        {
            _request.Parameters.Clear();
            string xml = request.ToXmlString();
            string body = SoapHelper.GetBodyToExecuteAction(xml);
            string envelope = SoapHelper.GetSilentEnvelope(_endpointAddress, body, "ExecuteAction");
            _request.AddParameter(_contentType, envelope, ParameterType.RequestBody);
            IRestResponse response = _client.Execute(_request);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception($"Ocorreu um erro ao realizar ExecuteAciont: {response.Content}");
            return SoapHelper.ExtractBody(response.Content);
        }

        public string GetDocumentFile(OrquestraECMFileRequestRoot request)
        {
            _request.Parameters.Clear();
            string xml = request.ToXmlString();
            string body = SoapHelper.GetBodyToGetDocumentFile(xml);
            string envelope = SoapHelper.GetSilentEnvelope(_endpointAddress, body, "GetDocumentFile");
            _request.AddParameter(_contentType, envelope, ParameterType.RequestBody);
            IRestResponse response = _client.Execute(_request);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception($"Ocorreu um erro ao obter os arquivos do documento: {response.Content}");
            return SoapHelper.ExtractBody(response.Content);
        }

        public string GetUserToken(string userName, string password, string domain)
        {
            _request.Parameters.Clear();
            string body = SoapHelper.GetBodyToGetUserToken(userName, password, domain);
            string envelope = SoapHelper.GetSilentEnvelope(_endpointAddress, body, "GetUserToken");
            _request.AddParameter(_contentType, envelope, ParameterType.RequestBody);
            IRestResponse response = _client.Execute(_request);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception($"Ocorreu um erro ao obter o token ECM do usuario: {response.Content}");
            return SoapHelper.ExtractBody(response.Content);
        }

        public string GetXmlModel(string appCode)
        {
            _request.Parameters.Clear();
            string body = SoapHelper.GetBodyToGetXmlModel(appCode);
            string envelope = SoapHelper.GetSilentEnvelope(_endpointAddress, body, "GetXmlModel");
            _request.AddParameter(_contentType, envelope, ParameterType.RequestBody);
            IRestResponse response = _client.Execute(_request);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception($"Ocorreu um erro ao obter o XML model da aplicação: {response.Content}");
            return SoapHelper.ExtractBody(response.Content);

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
