using SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.Common.Enumerators;
using System.Xml.Serialization;

namespace SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.ExecuteActionRequest
{
    public class ExecuteActionRequestColumn
    {
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }

        [XmlAttribute(AttributeName = "sortable")]
        public TrueFalseEnum Sortable { get; set; }

        [XmlAttribute(AttributeName = "group")]
        public TrueFalseEnum Group { get; set; }

        [XmlAttribute(AttributeName = "visible")]
        public TrueFalseEnum Visible { get; set; } = TrueFalseEnum.resultEmpty;
    }
}
