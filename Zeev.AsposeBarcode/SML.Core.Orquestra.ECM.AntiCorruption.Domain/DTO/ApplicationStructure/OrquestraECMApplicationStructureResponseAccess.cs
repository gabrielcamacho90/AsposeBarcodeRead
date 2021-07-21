using System.Xml.Serialization;

namespace SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.ApplicationStructure
{
    public class OrquestraECMApplicationStructureResponseAccess
    {
        [XmlAttribute(AttributeName = "limit")]
        public string Limit { get; set; }

        [XmlAttribute(AttributeName = "expireDate")]
        public string ExpireDate { get; set; }
    }
}