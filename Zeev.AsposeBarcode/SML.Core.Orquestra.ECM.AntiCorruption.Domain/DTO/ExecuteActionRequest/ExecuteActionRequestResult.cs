using SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.Common.Enumerators;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.ExecuteActionRequest
{
    public class ExecuteActionRequestResult
    {
        [XmlAttribute(AttributeName = "itemsPerPage")]
        public string ItemsPerPage { get; set; } = "10";

        [XmlAttribute(AttributeName = "showDeleted")]
        public TrueFalseEnum ShowDeleted { get; set; } = TrueFalseEnum.resultEmpty;

        [XmlElement("column")]
        public List<ExecuteActionRequestColumn> Columns { get; set; }
    }
}
