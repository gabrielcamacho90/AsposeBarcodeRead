using System.Xml.Serialization;

namespace SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.FileResponse
{
    public class OrquestraECMFileResponseDocument
    {
        [XmlElement(ElementName = "files")]
        public OrquestraECMFileResponseFiles Files { get; set; }

    }
}