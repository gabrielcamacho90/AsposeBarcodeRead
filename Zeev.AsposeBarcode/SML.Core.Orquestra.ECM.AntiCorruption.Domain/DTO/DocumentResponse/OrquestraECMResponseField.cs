using System.Xml.Serialization;

namespace SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.DocumentResponse
{
    public class OrquestraECMResponseField
    {
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }

        [XmlAttribute(AttributeName = "value")]
        public string Value { get; set; }

        public override string ToString()
        {
            return string.Concat(Name, " : ", Value);
        }
    }
}