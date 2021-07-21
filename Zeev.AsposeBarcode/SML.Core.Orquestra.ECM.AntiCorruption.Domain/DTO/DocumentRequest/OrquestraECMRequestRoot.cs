using SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.Common.Enumerators;
using SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.Interfaces;
using System.Xml.Serialization;

namespace SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.DocumentRequest
{
    [XmlRoot(ElementName = "eContent")]
    public class OrquestraECMRequestRoot : IRoot
    {
        protected OrquestraECMRequestRoot() { }
        public OrquestraECMRequestRoot(string adHocUser, string identification, ModuleNameEnum moduleName, string appCode, string userToken)
        {
            Header = new OrquestraECMRequestHeader(adHocUser, identification, moduleName, appCode, userToken);
            Structure = new OrquestraECMRequestStructure();
        }

        [XmlElement("header")]
        public OrquestraECMRequestHeader Header { get; set; }

        [XmlElement("structure")]
        public OrquestraECMRequestStructure Structure { get; set; }
    }
}
