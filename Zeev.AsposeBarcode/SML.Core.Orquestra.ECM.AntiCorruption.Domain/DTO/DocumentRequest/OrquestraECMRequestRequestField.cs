using System.Xml.Serialization;

namespace SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.DocumentRequest
{
    public class OrquestraECMRequestRequestField
    {
        public OrquestraECMRequestRequestField()
        {
            Value = "";
        }

        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }

        [XmlAttribute(AttributeName = "value")]
        public string Value { get; set; }

        [XmlAttribute(AttributeName = "visible")]
        public bool Visible { get; set; }

        [XmlAttribute(AttributeName = "enabled")]
        public bool Enabled { get; set; }

        [XmlAttribute(AttributeName = "required")]
        public bool Required { get; set; }

        [XmlAttribute(AttributeName = "defaultValue")]
        public string DefaultValue { get; set; }

        /// <summary>
        /// Observações: campo deve estar configurado para aceitar busca por intervalo.
        /// </summary>
        [XmlAttribute(AttributeName = "defaultValueEnd")]
        public bool DefaultValueEnd { get; set; }

        [XmlAttribute(AttributeName = "commandOrName")]
        public string CommandOrName { get; set; }

        /// <summary>
        /// (opcional) – utilizando somente para atribuição de descrição padrão em campo do tipo Fonte de dados.
        /// </summary>
        [XmlAttribute(AttributeName = "defaultDescription")]
        public bool DefaultDescription { get; set; }

        public override string ToString()
        {
            return string.Concat(Name, " : ", Value);
        }
    }
}