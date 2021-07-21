using System.Collections.Generic;
using System.Xml.Serialization;

namespace SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.ExecuteActionRequest
{
    public class ExecuteActionRequestStructure
    {
        public ExecuteActionRequestStructure()
        {
            Events = new List<ExecuteActionRequestEvent>();
        }

        [XmlArray("events"), XmlArrayItem("event")]
        public List<ExecuteActionRequestEvent> Events { get; set; }


    }
}