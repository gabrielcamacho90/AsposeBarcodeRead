using SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.UploadFileRequest.Enumerators;
using System.Xml.Serialization;

namespace SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.ExecuteActionRequest
{
    public class ExecuteActionRequestDocumentFile
    {
        [XmlAttribute(AttributeName = "id")]
        public long IndexId { get; set; }
        [XmlAttribute(AttributeName = "fileAmount")]
        public int FileCount { get; set; }
        [XmlAttribute(AttributeName = "operation")]
        public FileOperationEnum Operation { get; set; }
    }
}