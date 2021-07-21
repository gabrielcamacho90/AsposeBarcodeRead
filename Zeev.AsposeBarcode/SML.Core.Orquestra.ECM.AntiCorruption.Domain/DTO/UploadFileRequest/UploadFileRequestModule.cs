using SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.Common.Enumerators;
using System.Xml.Serialization;

namespace SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.UploadFileRequest
{
    public class UploadFileRequestModule
    {
        protected UploadFileRequestModule()
        {

        }
        public UploadFileRequestModule(ModuleNameEnum module)
        {
            Name = module;
        }

        [XmlAttribute(AttributeName = "name")]
        public ModuleNameEnum Name { get; set; }
    }
}