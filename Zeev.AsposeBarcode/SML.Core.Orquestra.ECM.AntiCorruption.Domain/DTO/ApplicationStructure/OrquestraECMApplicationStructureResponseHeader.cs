using SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.Interfaces;
using System.Xml.Serialization;

namespace SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.ApplicationStructure
{
    public class OrquestraECMApplicationStructureResponseHeader : IRoot
    {
        protected OrquestraECMApplicationStructureResponseHeader()
        {

        }
        public OrquestraECMApplicationStructureResponseHeader(string identification, string moduleName)
        {
            Module = new OrquestraECMApplicationStructureResponseModule(moduleName);
            Application = new OrquestraECMApplicationStructureResponseApplication();
            Access = new OrquestraECMApplicationStructureResponseAccess();
            Identification = identification;
        }

        [XmlElement(ElementName = "module")]
        public OrquestraECMApplicationStructureResponseModule Module { get; set; }

        [XmlElement(ElementName = "application")]
        public OrquestraECMApplicationStructureResponseApplication Application { get; set; }

        [XmlElement(ElementName = "userToken")]
        public string UserToken { get; set; }

        [XmlElement(ElementName = "access")]
        public OrquestraECMApplicationStructureResponseAccess Access { get; set; }

        [XmlElement(ElementName = "identification")]
        public string Identification { get; set; }
    }
}
