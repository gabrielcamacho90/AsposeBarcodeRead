using System.Xml.Serialization;

namespace SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.ExecuteActionRequest
{
    public class ExecuteActionRequestFile
    {
        public ExecuteActionRequestFile()
        {

        }

        public ExecuteActionRequestFile(bool doubleFiles)
        {
            DoubleFiles = doubleFiles;
        }

        [XmlAttribute("doubleFiles")]
        public bool DoubleFiles { get; set; }

    }
}