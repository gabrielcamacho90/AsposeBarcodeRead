using System.Collections.Generic;
using System.Xml.Serialization;

namespace SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.ExternalResponse
{
    [XmlRoot(ElementName = "applications")]
    public class ExternalApplications
    {
        [XmlElement(ElementName = "application")]
        public List<ExternalApplication> Application { get; set; }
        [XmlAttribute(AttributeName = "type")]
        public string Type { get; set; }
        [XmlAttribute(AttributeName = "totalRecords")]
        public string TotalRecords { get; set; }
    }
}
