using SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.Interfaces;
using System.Xml.Serialization;

namespace SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.ApplicationStructure
{
    [XmlRoot(ElementName = "eContent")]
    public class OrquestraECMApplicationStructureResponseRoot : IRoot
    {
        public OrquestraECMApplicationStructureResponseRoot()
        {
        }

        public OrquestraECMApplicationStructureResponseRoot(string identification, string moduleName)
        {
            Header = new OrquestraECMApplicationStructureResponseHeader(identification, moduleName);
            Structure = new OrquestraECMApplicationStructureResponseStructure();
        }

        [XmlElement(ElementName = "header")]
        public OrquestraECMApplicationStructureResponseHeader Header { get; set; }

        [XmlElement(ElementName = "structure")]
        public OrquestraECMApplicationStructureResponseStructure Structure { get; set; }
    }
}
