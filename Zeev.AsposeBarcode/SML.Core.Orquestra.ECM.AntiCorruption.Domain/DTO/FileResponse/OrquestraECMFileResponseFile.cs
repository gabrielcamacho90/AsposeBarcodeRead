using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.FileResponse
{
    public class OrquestraECMFileResponseFile
    {
        [XmlAttribute(AttributeName = "id")]
        public int Id { get; set; }

        [XmlAttribute(AttributeName = "address")]
        public string Address { get; set; }

        [XmlAttribute(AttributeName = "size")]
        public int Size { get; set; }

        [XmlAttribute(AttributeName = "order")]
        public int Order { get; set; }

        [XmlAttribute(AttributeName = "sourceId")]
        public int SourceId { get; set; }

        [XmlAttribute(AttributeName = "sourceAlias")]
        public string SourceAlias { get; set; }

        [XmlAttribute(AttributeName = "fileAlias")]
        public string FileAlias { get; set; }

        [XmlText]
        public string FileBase64Stream { get; set; }

        [XmlIgnore]
        public List<byte> FileByteStream { get; set; }

        public void FillByteStream()
        {
            var charArray = FileBase64Stream.ToCharArray();
            FileByteStream = Convert.FromBase64CharArray(charArray, 0, charArray.Length).ToList();
        }

        public List<byte> GetByteStream()
        {
            var charArray = FileBase64Stream.ToCharArray();
            return Convert.FromBase64CharArray(charArray, 0, charArray.Length).ToList();
        }

        public void ConcatenateByteStream(string base64String)
        {
            var charArray = FileBase64Stream.ToCharArray();
            FileByteStream.AddRange(Convert.FromBase64CharArray(charArray, 0, charArray.Length));
        }
    }
}