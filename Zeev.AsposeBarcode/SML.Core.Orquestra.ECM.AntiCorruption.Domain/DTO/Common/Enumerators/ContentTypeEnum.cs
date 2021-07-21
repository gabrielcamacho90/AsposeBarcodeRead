using System.Xml.Serialization;

namespace SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.Common.Enumerators
{
    public enum ContentTypeEnum
    {
        [XmlEnum("basic")]
        Basic = 0,
        [XmlEnum("advanced")]
        Advanced = 1
    }
}
