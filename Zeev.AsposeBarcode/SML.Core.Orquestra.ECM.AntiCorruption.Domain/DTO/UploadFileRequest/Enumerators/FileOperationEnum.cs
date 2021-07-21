using System.Xml.Serialization;

namespace SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.UploadFileRequest.Enumerators
{
    public enum FileOperationEnum
    {
        [XmlEnum("ADDBEFORE")]
        AddBefore = 0,
        [XmlEnum("ADDAFTER")]
        AddAfter = 1,
        [XmlEnum("REPLACE")]
        Replace = 2,
        [XmlEnum("NEWVERSION")]
        NewVersion = 3
    }
}
