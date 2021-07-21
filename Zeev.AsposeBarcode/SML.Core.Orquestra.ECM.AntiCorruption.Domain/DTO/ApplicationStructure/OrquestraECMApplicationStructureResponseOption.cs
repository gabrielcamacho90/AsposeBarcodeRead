using System.Xml.Serialization;

namespace SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.ApplicationStructure
{
    public class OrquestraECMApplicationStructureResponseOption
    {

        [XmlAttribute(AttributeName = "showHeader")]
        public bool ShowHeader { get; set; }

        [XmlAttribute(AttributeName = "showFloatMenu")]
        public bool ShowFloatMenu { get; set; }

        [XmlAttribute(AttributeName = "openHeader")]
        public bool OpenHeader { get; set; }

        [XmlAttribute(AttributeName = "openFloat")]
        public bool OpenFloat { get; set; }

        [XmlAttribute(AttributeName = "fullscreen")]
        public bool Fullscreen { get; set; }

        [XmlAttribute(AttributeName = "viewerTarget")]
        public string ViewerTarget { get; set; }
    }
}