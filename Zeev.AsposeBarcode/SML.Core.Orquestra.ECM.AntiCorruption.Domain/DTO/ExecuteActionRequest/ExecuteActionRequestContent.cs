using SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.Common.Enumerators;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.ExecuteActionRequest
{
    public class ExecuteActionRequestContent
    {
        public ExecuteActionRequestContent()
        {
        }

        [XmlAttribute(AttributeName = "type")]
        public ContentTypeEnum Type { get; set; } = ContentTypeEnum.Basic;

        [XmlElement("files")]
        public ExecuteActionRequestFile Files { get; set; }

        [XmlElement("documentSource")]
        public long DocumentSource { get; set; }

        [XmlElement("documentDestiny")]
        public long DocumentDestiny { get; set; }

        [XmlArray("fields"), XmlArrayItem("field")]
        public List<ExecuteActionRequestField> Fields { get; set; }

        [XmlElement("results")]
        public ExecuteActionRequestResult Result { get; set; } = new ExecuteActionRequestResult();

        public void AddRequestField(string name, string value) => Fields.Add(new ExecuteActionRequestField { Name = name, Value = value });
    }
}