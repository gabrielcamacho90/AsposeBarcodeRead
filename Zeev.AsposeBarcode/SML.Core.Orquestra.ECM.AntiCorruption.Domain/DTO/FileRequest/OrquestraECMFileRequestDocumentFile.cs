using System.Xml.Serialization;

namespace SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.FileRequest
{
    public class OrquestraECMFileRequestDocumentFile
    {
        [XmlAttribute(AttributeName = "id")]
        public long IndId { get; set; }

        [XmlAttribute(AttributeName = "fileId")]
        public long FileId { get; set; }
    }
}