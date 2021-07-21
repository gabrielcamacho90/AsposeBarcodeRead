using SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.Common.Enumerators;
using System.Xml.Serialization;

namespace SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.ExecuteActionRequest
{
    public class ExecuteActionRequestModule
    {
        protected ExecuteActionRequestModule()
        {

        }
        public ExecuteActionRequestModule(ModuleNameEnum module)
        {
            Name = module;
        }

        [XmlAttribute(AttributeName = "name")]
        public ModuleNameEnum Name { get; set; }
    }
}