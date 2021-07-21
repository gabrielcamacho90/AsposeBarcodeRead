using System.Xml.Serialization;

namespace SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.ApplicationStructure
{
    public class OrquestraECMApplicationStructureResponseField
    {
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }

        [XmlAttribute(AttributeName = "visible")]
        public bool Visible { get; set; }

        [XmlAttribute(AttributeName = "enabled")]
        public bool Enabled { get; set; }

        [XmlAttribute(AttributeName = "required")]
        public bool Required { get; set; }

        [XmlAttribute(AttributeName = "defaultValue")]
        public string DefaultValue { get; set; }

        [XmlAttribute(AttributeName = "defaultDescription")]
        public string DefaultDescription { get; set; }
    }
}