using SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.Common.Enumerators;
using SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.Interfaces;
using System.Xml.Serialization;

namespace SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.UploadFileRequest
{
    [XmlRoot(ElementName = "eContent")]
    public class UploadFileRequestRoot : IRoot
    {
        protected UploadFileRequestRoot()
        {

        }
        public UploadFileRequestRoot(string identification, ModuleNameEnum module)
        {
            Header = new UploadFileRequestHeader(identification, module);
            Structure = new UploadFileRequestStructure();
        }

        [XmlElement(ElementName = "header")]
        public UploadFileRequestHeader Header { get; set; }

        [XmlElement(ElementName = "structure")]
        public UploadFileRequestStructure Structure { get; set; }
    }
}
