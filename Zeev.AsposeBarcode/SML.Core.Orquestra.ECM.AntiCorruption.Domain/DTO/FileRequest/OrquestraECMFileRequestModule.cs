using SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.Common.Enumerators;
using System.Xml.Serialization;

namespace SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.FileRequest
{
    public class OrquestraECMFileRequestModule
    {
        protected OrquestraECMFileRequestModule() { }
        public OrquestraECMFileRequestModule(ModuleNameEnum moduleName)
        {
            Name = moduleName;
        }

        [XmlAttribute(AttributeName = "name")]
        public ModuleNameEnum Name { get; set; }
    }
}