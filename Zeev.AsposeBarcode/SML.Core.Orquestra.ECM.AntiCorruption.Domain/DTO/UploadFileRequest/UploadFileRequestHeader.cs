using SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.Common.Enumerators;
using System.Xml.Serialization;

namespace SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.UploadFileRequest
{
    public class UploadFileRequestHeader
    {
        protected UploadFileRequestHeader()
        {

        }
        public UploadFileRequestHeader(string identification, ModuleNameEnum module)
        {
            Module = new UploadFileRequestModule(module);
            Application = new UploadFileRequestApplication();
            Identification = identification;
        }

        [XmlElement(ElementName = "module")]
        public UploadFileRequestModule Module { get; set; }

        [XmlElement(ElementName = "application")]
        public UploadFileRequestApplication Application { get; set; }

        [XmlElement(ElementName = "userToken")]
        public string UserToken { get; set; }

        [XmlElement(ElementName = "identification")]
        public string Identification { get; set; }

        [XmlElement(ElementName = "adHocUser")]
        public string AdHocUser { get; set; }
    }
}