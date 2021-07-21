using SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.Interfaces;
using System;

namespace SML.Core.Orquestra.ECM.AntiCorruption.Domain.Helpers.Extensions
{
    public static class XmlConvertDTOExtencion
    {
        public static string ToXmlString(this IRoot request)
        {
            return Serialization.GetSerializeXml(request).Replace("<?xml version=\"1.0\" encoding=\"utf-8\"?>", "");
        }

        public static string ToXmlString(this object request)
        {
            return Serialization.GetSerializeXml(request).Replace("<?xml version=\"1.0\" encoding=\"utf-8\"?>", ""); ;
        }

        public static T ToObject<T>(this string xml, T o)
        {
            try
            {
                return Serialization.GetDeserializeXml(o, xml);
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível realizar a deserialização dessa string XML!", ex);
            }
        }
    }
}
