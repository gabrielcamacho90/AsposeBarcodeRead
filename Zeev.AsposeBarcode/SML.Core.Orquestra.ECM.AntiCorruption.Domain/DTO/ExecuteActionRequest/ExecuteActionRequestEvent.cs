using SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.Common.Enumerators;
using System.Xml.Serialization;

namespace SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.ExecuteActionRequest
{
    public class ExecuteActionRequestEvent
    {
        public ExecuteActionRequestEvent()
        {
        }

        public ExecuteActionRequestEvent(EventNameEnum eventName)
        {
            Content = new ExecuteActionRequestContent();
            EventName = eventName;
        }

        [XmlAttribute("name")]
        public EventNameEnum EventName { get; set; }

        [XmlElement(ElementName = "option")]
        public ExecuteActionRequestOption Option { get; set; } = new ExecuteActionRequestOption();

        [XmlElement("content")]
        public ExecuteActionRequestContent Content { get; set; } = new ExecuteActionRequestContent();

        [XmlElement("action")]
        public ExecuteActionRequestAction Action { get; set; } = new ExecuteActionRequestAction();
    }
}