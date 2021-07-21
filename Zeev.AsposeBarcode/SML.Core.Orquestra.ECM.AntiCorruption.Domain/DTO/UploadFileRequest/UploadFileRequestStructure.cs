using System.Xml.Serialization;

namespace SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.UploadFileRequest
{
    public class UploadFileRequestStructure
    {
        public UploadFileRequestStructure()
        {
            Document = new UploadFileRequestDocumentFile();
        }

        [XmlElement("document")]
        public UploadFileRequestDocumentFile Document { get; set; }

    }
}