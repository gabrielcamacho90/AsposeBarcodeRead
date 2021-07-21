using System.Xml.Serialization;

namespace SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.FileRequest
{
    public class OrquestraECMFileRequestPaginate
    {
        public OrquestraECMFileRequestPaginate()
        {
        }

        [XmlAttribute(AttributeName = "packageSize")]
        public int PackageSize { get; set; }

        [XmlAttribute(AttributeName = "packageIndex")]
        public int PackageIndex { get; set; }
    }
}