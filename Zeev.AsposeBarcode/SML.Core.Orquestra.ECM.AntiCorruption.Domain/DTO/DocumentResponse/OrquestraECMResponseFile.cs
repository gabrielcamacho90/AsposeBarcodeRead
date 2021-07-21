using System.Xml.Serialization;

namespace SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.DocumentResponse
{
    public class OrquestraECMResponseFile
    {
        [XmlAttribute(AttributeName = "id")]
        public int Id { get; set; }

        [XmlAttribute(AttributeName = "address")]
        public string Address { get; set; }

        [XmlAttribute(AttributeName = "size")]
        public int Size { get; set; }

        [XmlAttribute(AttributeName = "order")]
        public int Order { get; set; }

        [XmlAttribute(AttributeName = "sourceId")]
        public int SourceId { get; set; }

        [XmlAttribute(AttributeName = "sourceAlias")]
        public string SourceAlias { get; set; }

        [XmlText]
        public string FileName { get; set; }
    }
}