using SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.Common.Enumerators;
using System.Xml.Serialization;

namespace SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.FileRequest
{
    public class OrquestraECMFileRequestHeader
    {
        protected OrquestraECMFileRequestHeader() { }
        public OrquestraECMFileRequestHeader(string identification, ModuleNameEnum moduleName)
        {
            Module = new OrquestraECMFileRequestModule(moduleName);
            Application = new OrquestraECMFileRequestApplication();
            Identification = identification;
        }

        [XmlElement(ElementName = "module")]
        public OrquestraECMFileRequestModule Module { get; set; }

        [XmlElement(ElementName = "application")]
        public OrquestraECMFileRequestApplication Application { get; set; }

        [XmlElement(ElementName = "userToken")]
        public string UserToken { get; set; }

        [XmlElement(ElementName = "identification")]
        public string Identification { get; set; }

        [XmlElement(ElementName = "adHocUser")]
        public string AdHocUser { get; set; }
    }
}