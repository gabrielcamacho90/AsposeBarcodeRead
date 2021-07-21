using System.Xml.Serialization;

namespace SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.ExecuteActionRequest
{
    public class ExecuteActionRequestApplication
    {
        [XmlAttribute(AttributeName = "code")]
        public string Code { get; set; }
    }
}