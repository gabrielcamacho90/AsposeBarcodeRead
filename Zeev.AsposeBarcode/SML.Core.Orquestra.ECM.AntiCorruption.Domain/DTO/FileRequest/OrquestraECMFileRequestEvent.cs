using SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.Common.Enumerators;
using System.Xml.Serialization;

namespace SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.FileRequest
{
    public class OrquestraECMFileRequestEvent
    {
        public OrquestraECMFileRequestEvent()
        {
            Content = new OrquestraECMFileRequestContent();
            Name = EventNameEnum.GetDocument;
        }

        [XmlAttribute(AttributeName = "name")]
        public EventNameEnum Name { get; set; }

        [XmlElement(ElementName = "content")]
        public OrquestraECMFileRequestContent Content { get; set; }
    }
}