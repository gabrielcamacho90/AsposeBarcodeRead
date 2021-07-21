using System.Xml.Serialization;

namespace SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.ExternalResponse
{
    [XmlRoot(ElementName = "application")]
    public class ExternalApplication
    {
        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }
        [XmlAttribute(AttributeName = "code")]
        public string Code { get; set; }
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
        [XmlAttribute(AttributeName = "alias")]
        public string Alias { get; set; }
        [XmlAttribute(AttributeName = "ownerId")]
        public string OwnerId { get; set; }
        [XmlAttribute(AttributeName = "ownerAlias")]
        public string OwnerAlias { get; set; }
        [XmlAttribute(AttributeName = "compId")]
        public string CompId { get; set; }
        [XmlAttribute(AttributeName = "companyAlias")]
        public string CompanyAlias { get; set; }
        [XmlAttribute(AttributeName = "versionControl")]
        public string VersionControl { get; set; }
    }
}
