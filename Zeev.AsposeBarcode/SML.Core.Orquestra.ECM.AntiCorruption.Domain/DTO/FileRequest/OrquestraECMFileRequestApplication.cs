using System.Xml.Serialization;

namespace SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.FileRequest
{
    public class OrquestraECMFileRequestApplication
    {
        [XmlAttribute(AttributeName = "code")]
        public string Code { get; set; }
    }
}