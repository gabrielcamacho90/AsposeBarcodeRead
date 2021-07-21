using System.Xml.Serialization;

namespace SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.ApplicationStructure
{
    public class OrquestraECMApplicationStructureResponseModule
    {
        protected OrquestraECMApplicationStructureResponseModule() { }
        public OrquestraECMApplicationStructureResponseModule(string moduleName)
        {
            Name = moduleName;
        }

        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
    }
}