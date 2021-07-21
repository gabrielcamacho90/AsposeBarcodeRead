using System.Xml.Serialization;

namespace SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.DocumentRequest
{
    public class OrquestraECMRequestEvent
    {

        public OrquestraECMRequestEvent()
        {
            EcontentRequestContent = new OrquestraECMRequestContent();
        }

        [XmlElement(ElementName = "content")]
        public OrquestraECMRequestContent EcontentRequestContent { get; set; }
    }
}