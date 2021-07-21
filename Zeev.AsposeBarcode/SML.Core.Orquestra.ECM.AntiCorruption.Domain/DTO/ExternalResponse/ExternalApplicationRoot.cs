using System.Collections.Generic;
using System.Xml.Serialization;

namespace SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.ExternalResponse
{
    [XmlRoot(ElementName = "eContent")]
    public class ExternalApplicationRoot
    {
        [XmlArray(ElementName = "applications"), XmlArrayItem(ElementName = "application")]
        public List<ExternalApplication> Applications { get; set; }


    }
}
