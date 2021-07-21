using System;
using System.Xml.Serialization;

namespace SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.ExecuteActionRequest
{
    public class ExecuteActionRequestAccess
    {
        DateTime _ExpireDate;

        [XmlAttribute(AttributeName = "limit")]
        public int Limit { get; set; }

        [XmlAttribute(AttributeName = "expireDate")]
        public string ExpireDate
        {
            get
            {
                return (_ExpireDate == DateTime.MinValue)
                    ? ""
                    : _ExpireDate.ToString("yyyy/mm/dd HH:mm");
            }
            set
            {
                _ExpireDate = (!string.IsNullOrEmpty(value))
                    ? DateTime.Parse(value)
                    : DateTime.MinValue;
            }
        }
    }
}
