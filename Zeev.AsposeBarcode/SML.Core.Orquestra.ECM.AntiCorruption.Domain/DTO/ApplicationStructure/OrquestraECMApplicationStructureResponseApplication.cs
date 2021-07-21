using System.Xml.Serialization;

namespace SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.ApplicationStructure
{
    public class OrquestraECMApplicationStructureResponseApplication
    {
        [XmlAttribute(AttributeName = "code")]
        public string Code { get; set; }
    }
}