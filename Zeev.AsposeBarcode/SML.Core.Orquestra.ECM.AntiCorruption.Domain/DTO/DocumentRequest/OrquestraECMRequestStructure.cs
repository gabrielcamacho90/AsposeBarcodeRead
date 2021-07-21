using System.Collections.Generic;
using System.Xml.Serialization;

namespace SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.DocumentRequest
{
    public class OrquestraECMRequestStructure
    {
        public OrquestraECMRequestStructure()
        {
            Events = new List<OrquestraECMRequestEvent>();
        }

        [XmlArray(ElementName = "events"), XmlArrayItem(ElementName = "event")]
        public List<OrquestraECMRequestEvent> Events { get; set; }


    }

}
