using System.Collections.Generic;
using System.Xml.Serialization;

namespace SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.DocumentResponse
{
    public class OrquestraECMResponseDocument
    {
        [XmlElement(ElementName = "field")]
        public List<OrquestraECMResponseField> Fields { get; set; }

        [XmlArray(ElementName = "files"), XmlArrayItem("file")]
        public List<OrquestraECMResponseFile> Files { get; set; }
    }
}