using SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.Common.Enumerators;
using System.Xml.Serialization;

namespace SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.FileRequest
{
    public class OrquestraECMFileRequestContent
    {
        public OrquestraECMFileRequestContent()
        {
            Paginate = new OrquestraECMFileRequestPaginate();
            DocumentFile = new OrquestraECMFileRequestDocumentFile();
            Type = ContentType.Basic;
        }

        [XmlAttribute("type")]
        public ContentType Type { get; set; }

        [XmlElement("paginate")]
        public OrquestraECMFileRequestPaginate Paginate { get; set; }

        [XmlElement("documentFile")]
        public OrquestraECMFileRequestDocumentFile DocumentFile { get; set; }
    }
}