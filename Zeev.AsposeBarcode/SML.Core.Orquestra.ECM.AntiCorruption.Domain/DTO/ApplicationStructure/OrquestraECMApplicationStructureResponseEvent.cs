using System.Xml.Serialization;

namespace SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.ApplicationStructure
{
    public class OrquestraECMApplicationStructureResponseEvent
    {
        public OrquestraECMApplicationStructureResponseEvent()
        {
            Content = new OrquestraECMApplicationStructureResponseContent();
            Option = new OrquestraECMApplicationStructureResponseOption();
        }

        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }

        [XmlElement(ElementName = "option")]
        public OrquestraECMApplicationStructureResponseOption Option { get; set; }

        [XmlElement(ElementName = "content")]
        public OrquestraECMApplicationStructureResponseContent Content { get; set; }
    }
}