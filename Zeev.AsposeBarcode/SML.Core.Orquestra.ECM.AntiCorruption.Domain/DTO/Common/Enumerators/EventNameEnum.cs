using System.Xml.Serialization;

namespace SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.Common.Enumerators
{
    public enum EventNameEnum
    {
        [XmlEnum("JOIN")]
        Join = 0,
        [XmlEnum("SAVE")]
        Save = 1,
        [XmlEnum("ADHOC")]
        AdHoc = 2,
        [XmlEnum("MULTINDEX")]
        MultIndex = 3,
        [XmlEnum("RESTORE")]
        Restore = 4,
        [XmlEnum("PREINDEX")]
        PreIndex = 5,
        [XmlEnum("DELETE")]
        Delete = 6,
        [XmlEnum("SMLWEBCAPTURE")]
        SmlWebCapture = 7,
        [XmlEnum("SMLWEBSEARCH")]
        SmlWebSearch = 8,
        [XmlEnum("SILENTLOGINGETDOCUMENT")]
        GetDocument = 9,
        [XmlEnum("smlviewerdocument")]
        SmlViewrDocument = 10,
        [XmlEnum("smlimported")]
        SmlImported = 11
    }
}
