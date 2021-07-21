using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace SML.Core.Orquestra.ECM.AntiCorruption.Domain.Helper
{
    public static class SoapHelper
    {
        #region SilentLogin Evelope
        public static string GetSilentEnvelope(string endPoint, string body, string ecmMethod)
        {
            StringBuilder envelope = new StringBuilder();
            envelope.AppendLine("<s:Envelope xmlns:a=\"http://www.w3.org/2005/08/addressing\" xmlns:s=\"http://www.w3.org/2003/05/soap-envelope\">");
            envelope.AppendLine("<s:Header>");
            envelope.AppendLine($"<a:Action s:mustUnderstand=\"1\">http://tempuri.org/ISilentLogin/{ecmMethod}</a:Action>");
            envelope.AppendLine("<a:MessageID>urn:uuid:e75ef20c-045b-4516-82da-a7da40f24ade</a:MessageID>");
            envelope.AppendLine("<a:ReplyTo>");
            envelope.AppendLine("<a:Address>http://www.w3.org/2005/08/addressing/anonymous</a:Address>");
            envelope.AppendLine("</a:ReplyTo>");
            envelope.AppendLine($"<a:To>{endPoint}</a:To>");
            envelope.AppendLine("</s:Header>");
            envelope.AppendLine("<s:Body>");
            envelope.AppendLine($"{body}");
            envelope.AppendLine("</s:Body>");
            envelope.AppendLine("</s:Envelope>");
            return envelope.ToString();
        }

        public static string GetUploadEnvelope(string protocol, string fileName, string fileContent)
        {
            StringBuilder envelope = new StringBuilder();
            envelope.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            envelope.AppendLine("<soap:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\">");
            envelope.AppendLine("<soap:Body>");
            envelope.AppendLine("<UploadFile xmlns=\"http://tempuri.org/\">");
            envelope.AppendLine($"<protocol>{protocol}</protocol>");
            envelope.AppendLine($"<fileName>{fileName}</fileName>");
            envelope.AppendLine($"<fileContent>{fileContent}</fileContent>");
            envelope.AppendLine("</UploadFile>");
            envelope.AppendLine("</soap:Body>");
            envelope.AppendLine("</soap:Envelope>");
            return envelope.ToString();
        }
        #endregion
        #region Upload Envelope

        #endregion
        #region Silent Login Body Methods
        public static string GetBodyToGetXmlModel(string appCode)
        {
            StringBuilder body = new StringBuilder();
            body.AppendLine("<GetXmlModel xmlns=\"http://tempuri.org/\">");
            body.AppendLine($"<applicationCode>{appCode}</applicationCode>");
            body.AppendLine("</GetXmlModel>");
            return body.ToString();
        }
        public static string GetBodyToGetUserToken(string userName, string userPass, string domain)
        {
            StringBuilder body = new StringBuilder();
            body.AppendLine("<GetUserToken xmlns=\"http://tempuri.org/\">");
            body.AppendLine($"<userName>{userName}</userName>");
            body.AppendLine($"<password>{userPass}</password>");
            body.AppendLine($"<domain>{domain}</domain>");
            body.AppendLine("</GetUserToken>");
            return body.ToString();
        }
        public static string GetBodyToGetDocument(string xml)
        {
            StringBuilder body = new StringBuilder();
            body.AppendLine("<GetDocument xmlns=\"http://tempuri.org/\">");
            body.AppendLine("<xml>");
            body.AppendLine($"<![CDATA[{xml}]]>");
            body.AppendLine("</xml>");
            body.AppendLine("</GetDocument>");
            return body.ToString();
        }
        public static string GetBodyToGetDocumentFile(string xml)
        {
            StringBuilder body = new StringBuilder();
            body.AppendLine("<GetDocumentFile xmlns=\"http://tempuri.org/\">");
            body.AppendLine("<xml>");
            body.AppendLine($"<![CDATA[{xml}]]>");
            body.AppendLine("</xml>");
            body.AppendLine("</GetDocumentFile>");
            return body.ToString();
        }
        public static string GetBodyToExecuteAction(string xml)
        {
            StringBuilder body = new StringBuilder();
            body.AppendLine("<ExecuteAction xmlns=\"http://tempuri.org/\">");
            body.AppendLine("<xml>");
            body.AppendLine($"<![CDATA[{xml}]]>");
            body.AppendLine("</xml>");
            body.AppendLine("</ExecuteAction>");
            return body.ToString();
        }
        public static string GetBodyToGetProtocol(string xml)
        {
            StringBuilder body = new StringBuilder();
            body.AppendLine("<GetProtocol xmlns=\"http://tempuri.org/\">");
            body.AppendLine("<xml>");
            body.AppendLine($"<![CDATA[{xml}]]>");
            body.AppendLine("</xml>");
            body.AppendLine("</GetProtocol>");
            return body.ToString();
        }
        public static string GetBodyToGetProtocolToUploadFile(string xml)
        {
            StringBuilder body = new StringBuilder();
            body.AppendLine("<GetProtocolToUploadFile xmlns=\"http://tempuri.org/\">");
            body.AppendLine("<xml>");
            body.AppendLine($"<![CDATA[{xml}]]>");
            body.AppendLine("</xml>");
            body.AppendLine("</GetProtocolToUploadFile>");
            return body.ToString();
        }
        public static string GetBodyToFinishUpload(string protocol)
        {
            StringBuilder body = new StringBuilder();
            body.AppendLine("<FinishUpload xmlns=\"http://tempuri.org/\">");
            body.AppendLine($"<protocol>{protocol}</protocol>");
            body.AppendLine("</FinishUpload>");
            return body.ToString();
        }
        public static string GetBodyToRemoveFilesFromFolderProtocol()
        {
            StringBuilder body = new StringBuilder();
            body.AppendLine("<RemoveFilesFromFolderProtocol xmlns=\"http://tempuri.org/\">");
            body.AppendLine("<protocol>12345</protocol>");
            body.AppendLine("</RemoveFilesFromFolderProtocol>");
            return body.ToString();
        }
        #endregion
        public static string ExtractBody(string envelope)
        {
            XDocument xdoc = XDocument.Parse(envelope);
            var nameSpace = xdoc.Root.GetNamespaceOfPrefix("xmlns");
            var result = xdoc.Descendants().Where(x => x.Name.LocalName.ToLower().Equals("body")).First().Descendants().Descendants().First().Value;
            return result;
        }

    }
}
