using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.DocumentRequest
{
    public class OrquestraECMRequestContent
    {

        public OrquestraECMRequestContent()
        {
            RequestFields = new List<OrquestraECMRequestRequestField>();
            ResultFields = new List<OrquestraECMRequestRequestField>();
            EContentRequestPaginate = new OrquestraECMRequestPaginate();

        }

        [XmlElement(ElementName = "paginate")]
        public OrquestraECMRequestPaginate EContentRequestPaginate { get; set; }


        //[XmlAttribute(AttributeName = "type")]
        //public ContentType Type { get; set; }

        [XmlArray(ElementName = "fields"), XmlArrayItem(ElementName = "field")]
        public List<OrquestraECMRequestRequestField> RequestFields { get; set; }

        [XmlArray(ElementName = "result"), XmlArrayItem(ElementName = "column")]
        public List<OrquestraECMRequestRequestField> ResultFields { get; set; }


        public void AddRequestField(string name, string value)
        {
            var field = new OrquestraECMRequestRequestField { Name = name, Value = value };
            RequestFields.Add(field);
        }

        public void AddResultField(string name)
        {
            ResultFields.Add(new OrquestraECMRequestRequestField { Name = name });
        }

        public void AddRangeResultField(IEnumerable<string> fieldNameList)
        {
            ResultFields.AddRange(fieldNameList.Select(x => new OrquestraECMRequestRequestField { Name = x }));
        }

    }
}