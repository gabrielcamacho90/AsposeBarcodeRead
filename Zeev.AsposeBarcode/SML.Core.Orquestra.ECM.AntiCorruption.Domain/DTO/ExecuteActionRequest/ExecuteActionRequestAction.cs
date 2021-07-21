using System.Xml.Serialization;

namespace SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.ExecuteActionRequest
{
    public class ExecuteActionRequestAction
    {
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
    }
}
