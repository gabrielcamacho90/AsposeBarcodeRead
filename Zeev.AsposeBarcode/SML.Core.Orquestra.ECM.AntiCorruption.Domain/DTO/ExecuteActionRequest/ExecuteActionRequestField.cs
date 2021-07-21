using SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.Common.Enumerators;
using System.Xml.Serialization;

namespace SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.ExecuteActionRequest
{
    public class ExecuteActionRequestField
    {
        public ExecuteActionRequestField()
        {
            Value = string.Empty;
        }

        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }

        [XmlAttribute(AttributeName = "visible")]
        public TrueFalseEnum Visible { get; set; }

        [XmlAttribute(AttributeName = "enabled")]
        public TrueFalseEnum Enabled { get; set; }

        [XmlAttribute(AttributeName = "required")]
        public TrueFalseEnum Required { get; set; }

        [XmlAttribute(AttributeName = "defaultValue")]
        public string DefaultValue { get; set; } = string.Empty;

        [XmlAttribute(AttributeName = "defaultValueEnd")]
        public string DefaultValueEnd { get; set; } = string.Empty;

        [XmlAttribute(AttributeName = "defaultDescription")]
        public string DefaultDescription { get; set; } = string.Empty;

        [XmlAttribute(AttributeName = "value")]
        public string Value { get; set; }

        [XmlAttribute(AttributeName = "commandOrName")]
        public string CommandOrName { get; set; }

        public override string ToString()
        {
            return string.Concat(Name, " : ", Value);
        }
    }
}