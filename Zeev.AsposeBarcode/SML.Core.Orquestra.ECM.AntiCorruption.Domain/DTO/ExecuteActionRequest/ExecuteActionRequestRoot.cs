using SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.Common.Enumerators;
using SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.Interfaces;
using System.Xml.Serialization;

namespace SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.ExecuteActionRequest
{
    [XmlRoot(ElementName = "eContent")]
    public class ExecuteActionRequestRoot : IRoot
    {
        protected ExecuteActionRequestRoot()
        {

        }
        public ExecuteActionRequestRoot(string identification, ModuleNameEnum module)
        {
            Header = new ExecuteActionRequestHeader(identification, module);
            Structure = new ExecuteActionRequestStructure();
        }

        [XmlElement(ElementName = "header")]
        public ExecuteActionRequestHeader Header { get; set; }

        [XmlElement(ElementName = "structure")]
        public ExecuteActionRequestStructure Structure { get; set; }
    }
}
