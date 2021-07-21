using SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.Common.Enumerators;
using SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.Interfaces;
using System.Xml.Serialization;

namespace SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.FileRequest
{
    [XmlRoot(ElementName = "eContent")]
    public class OrquestraECMFileRequestRoot : IRoot
    {
        protected OrquestraECMFileRequestRoot()
        {

        }
        public OrquestraECMFileRequestRoot(string identification, ModuleNameEnum moduleName)
        {
            Header = new OrquestraECMFileRequestHeader(identification, moduleName);
            Structure = new OrquestraECMFileRequestStructure();
        }

        [XmlElement(ElementName = "header")]
        public OrquestraECMFileRequestHeader Header { get; set; }

        [XmlElement(ElementName = "structure")]
        public OrquestraECMFileRequestStructure Structure { get; set; }
    }
}
