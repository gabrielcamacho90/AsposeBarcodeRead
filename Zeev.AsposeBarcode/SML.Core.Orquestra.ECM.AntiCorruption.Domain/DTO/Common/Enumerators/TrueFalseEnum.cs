using System.Xml.Serialization;

namespace SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.Common.Enumerators
{
    public enum TrueFalseEnum
    {
        [XmlEnum("")]
        resultEmpty = 0,
        [XmlEnum("true")]
        resultTrue = 1,
        [XmlEnum("false")]
        resultFalse = 2
    }
}
