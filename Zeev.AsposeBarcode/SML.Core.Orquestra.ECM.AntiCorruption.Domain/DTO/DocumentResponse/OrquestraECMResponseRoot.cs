using SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.Interfaces;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.DocumentResponse
{
    [XmlRoot(ElementName = "eContent")]
    public class OrquestraECMResponseRoot : IRoot
    {
        [XmlArray(ElementName = "documents"), XmlArrayItem("document")]
        public List<OrquestraECMResponseDocument> Documents { get; set; }
    }
}
