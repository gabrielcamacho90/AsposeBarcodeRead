using System.Collections.Generic;
using System.Xml.Serialization;

namespace SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.FileRequest
{
    public class OrquestraECMFileRequestStructure
    {
        public OrquestraECMFileRequestStructure()
        {
            Events = new List<OrquestraECMFileRequestEvent>();
        }

        [XmlArray(ElementName = "events"), XmlArrayItem("event")]
        public List<OrquestraECMFileRequestEvent> Events { get; set; }
    }
}