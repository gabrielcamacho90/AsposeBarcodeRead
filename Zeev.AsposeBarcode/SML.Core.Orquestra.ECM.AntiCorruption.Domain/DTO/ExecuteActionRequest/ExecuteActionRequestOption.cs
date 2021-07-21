using SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.Common.Enumerators;
using System.Xml.Serialization;

namespace SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.ExecuteActionRequest
{
    public class ExecuteActionRequestOption
    {

        public ExecuteActionRequestOption()
        {
            ShowFloatMenu = TrueFalseEnum.resultFalse;
            ShowHeader = TrueFalseEnum.resultFalse;
            OpenHeader = TrueFalseEnum.resultFalse;
            OpenFloat = TrueFalseEnum.resultFalse;
            Fullscreen = TrueFalseEnum.resultFalse;
        }

        [XmlAttribute("showHeader")]
        public TrueFalseEnum ShowHeader { get; set; }

        [XmlAttribute("showFloatMenu")]
        public TrueFalseEnum ShowFloatMenu { get; set; }

        [XmlAttribute("openHeader")]
        public TrueFalseEnum OpenHeader { get; set; }

        [XmlAttribute("openFloat")]
        public TrueFalseEnum OpenFloat { get; set; }

        [XmlAttribute("fullscreen")]
        public TrueFalseEnum Fullscreen { get; set; }

        [XmlAttribute("viewerTarget")]
        public string ViewerTarget { get; set; }
    }
}
