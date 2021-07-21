using SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.Interfaces;
using System.Xml.Serialization;

namespace SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.FileResponse
{
    [XmlRoot(ElementName = "eContent")]
    public class OrquestraECMFileResponseRoot : IRoot
    {
        [XmlElement(ElementName = "document")]
        public OrquestraECMFileResponseDocument Document { get; set; }
    }
}
