using SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.Common.Enumerators;
using System.Xml.Serialization;

namespace SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.DocumentRequest
{
    public class OrquestraECMRequestHeader
    {
        protected OrquestraECMRequestHeader() { }

        public OrquestraECMRequestHeader(string adHocUser, string identification, ModuleNameEnum moduleName, string appCode, string userToken)
        {
            EContentRequestModule = new OrquestraECMRequestModule(moduleName);
            EcontentRequestApplication = new OrquestraECMRequestApplication(appCode);
            Identification = identification;
            UserToken = userToken;
            AdHocUser = adHocUser;
        }

        [XmlElement(ElementName = "module")]
        public OrquestraECMRequestModule EContentRequestModule { get; set; }

        [XmlElement(ElementName = "application")]
        public OrquestraECMRequestApplication EcontentRequestApplication { get; set; }

        [XmlElement(ElementName = "userToken")]
        public string UserToken { get; set; }

        [XmlElement(ElementName = "identification")]
        public string Identification { get; set; }

        [XmlElement(ElementName = "adHocUser")]
        public string AdHocUser { get; set; }
    }

}
