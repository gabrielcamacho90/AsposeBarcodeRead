using System.Xml.Serialization;

namespace SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.DocumentRequest
{
    public class OrquestraECMRequestPaginate
    {
        [XmlAttribute(AttributeName = "returnFile")]
        public bool ReturnFile { get; set; } = true;

        [XmlAttribute(AttributeName = "pageSize")]
        public int PageSize { get; set; } = 1;

        [XmlAttribute(AttributeName = "pageIndex")]
        public int PageIndex { get; set; } = 0;

    }
}