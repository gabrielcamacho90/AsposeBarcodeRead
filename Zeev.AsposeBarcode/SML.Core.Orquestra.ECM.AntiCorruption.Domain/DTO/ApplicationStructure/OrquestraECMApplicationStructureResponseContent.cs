using SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.Common.Enumerators;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.ApplicationStructure
{
    public class OrquestraECMApplicationStructureResponseContent
    {
        public OrquestraECMApplicationStructureResponseContent()
        {
            Type = ContentType.Basic;
        }

        [XmlAttribute("type")]
        public ContentType Type { get; set; }

        [XmlArray(ElementName = "fields"), XmlArrayItem(ElementName = "field")]
        public List<OrquestraECMApplicationStructureResponseField> Fields { get; set; }
    }
}