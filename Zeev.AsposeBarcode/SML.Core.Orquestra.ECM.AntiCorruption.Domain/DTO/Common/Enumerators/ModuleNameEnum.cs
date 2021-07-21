using System.Xml.Serialization;

namespace SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.Common.Enumerators
{
    public enum ModuleNameEnum
    {
        [XmlEnum("SILENTLOGINEXECUTEACTION")]
        ExecuteAction = 0,
        [XmlEnum("SILENTLOGINSEARCH")]
        Search = 1,
        [XmlEnum("SILENTLOGINUPLOAD")]
        Upload = 2,
        [XmlEnum("SILENTLOGINGETDOCUMENT")]
        GetDocument = 3,
        [XmlEnum("smlPortal")]
        Portal = 4,
        [XmlEnum("smlwebsearch")]
        WebSearch = 5,
        [XmlEnum("smlviewerdocument")]
        Viewer = 6,
        [XmlEnum("smleditdocument")]
        Edit = 7,
        [XmlEnum("smlimported")]
        Import = 8,
        [XmlEnum("smlwebscan")]
        WebScan = 9,
    }
}
