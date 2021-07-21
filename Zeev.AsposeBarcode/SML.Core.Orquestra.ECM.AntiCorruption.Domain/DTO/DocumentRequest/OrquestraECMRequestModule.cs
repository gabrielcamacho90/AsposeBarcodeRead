using SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.Common.Enumerators;
using System.Xml.Serialization;

namespace SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.DocumentRequest
{
    public class OrquestraECMRequestModule
    {
        protected OrquestraECMRequestModule() { }
        public OrquestraECMRequestModule(ModuleNameEnum moduleName)
        {
            Name = moduleName;
        }

        [XmlAttribute(AttributeName = "name")]
        public ModuleNameEnum Name { get; set; }

    }
}