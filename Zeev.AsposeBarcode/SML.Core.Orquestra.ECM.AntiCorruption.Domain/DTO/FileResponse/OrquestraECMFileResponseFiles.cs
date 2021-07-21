using System.Collections.Generic;
using System.Xml.Serialization;

namespace SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.FileResponse
{
    public class OrquestraECMFileResponseFiles
    {
        [XmlAttribute("totalPackage")]
        public int TotalPackage { get; set; }

        [XmlElement("file")]
        public List<OrquestraECMFileResponseFile> Files { get; set; }
    }
}