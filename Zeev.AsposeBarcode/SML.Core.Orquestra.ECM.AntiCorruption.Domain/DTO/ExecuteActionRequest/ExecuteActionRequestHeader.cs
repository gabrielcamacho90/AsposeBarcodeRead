using SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.Common.Enumerators;
using System.Xml.Serialization;

namespace SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.ExecuteActionRequest
{
    public class ExecuteActionRequestHeader
    {
        protected ExecuteActionRequestHeader()
        {

        }
        public ExecuteActionRequestHeader(string identification, ModuleNameEnum module)
        {
            Module = new ExecuteActionRequestModule(module);
            Application = new ExecuteActionRequestApplication();
            Identification = identification;
        }

        [XmlElement(ElementName = "module")]
        public ExecuteActionRequestModule Module { get; set; }

        [XmlElement(ElementName = "application")]
        public ExecuteActionRequestApplication Application { get; set; }

        [XmlElement(ElementName = "userToken")]
        public string UserToken { get; set; }

        [XmlElement(ElementName = "identification")]
        public string Identification { get; set; }

        [XmlElement(ElementName = "adHocUser")]
        public string AdHocUser { get; set; }

        [XmlElement(ElementName = "access")]
        public ExecuteActionRequestAccess Access { get; set; } = new ExecuteActionRequestAccess();

    }
}