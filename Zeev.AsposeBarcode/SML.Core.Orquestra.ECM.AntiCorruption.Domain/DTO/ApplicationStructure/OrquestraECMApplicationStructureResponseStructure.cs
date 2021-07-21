using System.Collections.Generic;
using System.Xml.Serialization;

namespace SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.ApplicationStructure
{
    public class OrquestraECMApplicationStructureResponseStructure
    {
        public OrquestraECMApplicationStructureResponseStructure()
        {
            Events = new List<OrquestraECMApplicationStructureResponseEvent>();
        }

        [XmlArray(ElementName = "events"), XmlArrayItem("event")]
        public List<OrquestraECMApplicationStructureResponseEvent> Events { get; set; }
    }
}