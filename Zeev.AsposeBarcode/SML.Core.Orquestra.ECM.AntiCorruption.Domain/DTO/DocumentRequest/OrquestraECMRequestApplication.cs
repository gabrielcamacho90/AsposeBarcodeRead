using System.Xml.Serialization;

namespace SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.DocumentRequest
{
    public class OrquestraECMRequestApplication
    {
        protected OrquestraECMRequestApplication() { }

        public OrquestraECMRequestApplication(string appCode)
        {
            Code = appCode;
        }

        [XmlAttribute(AttributeName = "code")]
        public string Code { get; set; }

        public override string ToString()
        {
            return Code;
        }
    }
}