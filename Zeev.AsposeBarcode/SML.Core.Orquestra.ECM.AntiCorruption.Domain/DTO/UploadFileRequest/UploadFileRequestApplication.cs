using System.Xml.Serialization;

namespace SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.UploadFileRequest
{
    public class UploadFileRequestApplication
    {
        [XmlAttribute(AttributeName = "code")]
        public string Code { get; set; }
    }
}