using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace SML.Core.Orquestra.ECM.AntiCorruption.Domain.Helpers
{
    public class Utf8StringWriter : StringWriter
    {
        public override Encoding Encoding => Encoding.UTF8;
    }

    public static class Serialization
    {
        /// <summary>
        /// Serializa o objeto retornado o Xml
        /// </summary>
        /// <param name="type"></param>
        /// <param name="o"></param>
        /// <returns></returns>
        public static string GetSerializeXml<T>(T input, XmlSerializerNamespaces xmlSerializerNamespaces = null)
        {
            try
            {
                return SerializeXml(input, xmlSerializerNamespaces);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        private static string SerializeXml<T>(T input, XmlSerializerNamespaces xmlSerializerNamespaces)
        {

            string result = "";

            string utf8 = string.Empty;
            using (var writer = new Utf8StringWriter())
            using (var xw = XmlWriter.Create(writer))
            {
                var xSerializer = new XmlSerializer(input.GetType());
                if (xmlSerializerNamespaces == null)
                {
                    xmlSerializerNamespaces = new XmlSerializerNamespaces();
                    xmlSerializerNamespaces.Add(string.Empty, string.Empty);
                    xSerializer.Serialize(xw, input, xmlSerializerNamespaces);
                }
                else
                    xSerializer.Serialize(xw, input, xmlSerializerNamespaces);
                result = writer.ToString();
            }

            return result;
            //using (MemoryStream stream = new MemoryStream())
            //using (StreamWriter writer = new StreamWriter(stream, System.Text.Encoding.UTF8))
            //{
            //    XmlSerializer xml = new XmlSerializer(input.GetType());

            //    if (xmlSerializerNamespaces != null)
            //        xml.Serialize(stream, input, xmlSerializerNamespaces);
            //    else
            //    {
            //        xmlSerializerNamespaces = new XmlSerializerNamespaces();
            //        xmlSerializerNamespaces.Add(string.Empty, string.Empty);
            //        xml.Serialize(stream, input, xmlSerializerNamespaces);
            //    }
            //    result = stream.ToArray();
            //}

            /*
            try
            {
                //gera XML                  
                using (var sw = new StringWriter())
                {
                    using (var xw = XmlWriter.Create(sw))
                    {
                        // Build Xml with xw.
                        XmlSerializer xSerializer = new XmlSerializer(input.GetType());

                        if (xmlSerializerNamespaces != null)
                        {
                            xSerializer.Serialize(xw, input, xmlSerializerNamespaces);
                        }
                        else
                        {
                            xmlSerializerNamespaces = new XmlSerializerNamespaces();
                            xmlSerializerNamespaces.Add(string.Empty, string.Empty);
                            xSerializer.Serialize(xw, input, xmlSerializerNamespaces);
                        }
                    }

                    result = sw.ToString();
                    sw.Close();
                }

                return result;
                 }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
                */

        }

        /// <summary>
        /// Esse método, ignora caracteres especiais na Serialização do Objeto.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <param name="xmlSerializerNamespaces"></param>
        /// <returns></returns>
        public static string GetSerializeXml01<T>(T input, XmlSerializerNamespaces xmlSerializerNamespaces)
        {
            string result = "";

            try
            {
                //gera XML                  
                using (var sw = new StringWriter())
                {
                    var settings = new XmlWriterSettings() { CheckCharacters = false };

                    using (var xw = XmlWriter.Create(sw, settings))
                    {
                        // Build Xml with xw.
                        XmlSerializer xSerializer = new XmlSerializer(input.GetType());

                        if (xmlSerializerNamespaces != null)
                        {
                            xSerializer.Serialize(xw, input, xmlSerializerNamespaces);
                        }
                        else
                        {
                            xmlSerializerNamespaces = new XmlSerializerNamespaces();
                            xmlSerializerNamespaces.Add(string.Empty, string.Empty);

                            xSerializer.Serialize(xw, input, xmlSerializerNamespaces);

                        }
                    }

                    result = sw.ToString();
                    sw.Close();
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }


        /// <summary>
        /// Deserializa o objeto a partir do Xml
        /// </summary>
        /// <param name="type"></param>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static T GetDeserializeXml<T>(T o, string xml)
        {
            object result = null;

            try
            {
                if (string.IsNullOrEmpty(xml))
                    throw new Exception("Xml de Retorno esta vazio ou inválido.");

                using (StringReader reader = new StringReader(xml))
                {
                    XmlSerializer xSerializer = new XmlSerializer(o.GetType(), "");
                    result = xSerializer.Deserialize(reader);
                    reader.Close();
                }

                return (T)result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
