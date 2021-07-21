using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Xml.Linq;

namespace SML.Core.Orquestra.ECM.AntiCorruption.Domain.Helpers
{
    public static class SilentLoginHelper
    {

        public static string GetSilentLoginEndPoint(string urlBase)
        {
            string endPoint = string.Empty;
            XDocument wsdl = null;
            string urlWebService = $"{urlBase}/services/silentlogin.svc?wsdl";

            using (WebClient clientConnect = new WebClient())
            {
                Stream stream = clientConnect.OpenRead(urlWebService);
                wsdl = XDocument.Load(stream);
                IEnumerable<XElement> ports = wsdl.Descendants().Elements()
                    .Where(s => s.Name.ToString().Equals("{http://schemas.xmlsoap.org/wsdl/}port", StringComparison.InvariantCultureIgnoreCase));

                if (ports.Count() > 1)
                {
                    if (urlWebService.StartsWith("https", StringComparison.InvariantCultureIgnoreCase))
                    {
                        endPoint = ports.Where(p => p.Attribute("name").Value.ToString().Equals("MetadataExchangeHttpsBinding_ISilentLogin"))
                            .Elements().Where(e => e.Name.ToString().ToLower().Contains("address")).First()
                            .Attribute("location").Value;
                    }
                    else
                    {
                        endPoint = ports.Where(p => p.Attribute("name").Value.ToString().Equals("bindingSilentLogin_ISilentLogin"))
                           .Elements().Where(e => e.Name.ToString().ToLower().Contains("address")).First()
                           .Attribute("location").Value;
                    }
                }
                else
                {
                    endPoint = ports.Where(p => p.Attribute("name").Value.ToString().Equals("MetadataExchangeHttpsBinding_ISilentLogin"))
                        .Elements().Where(e => e.Name.ToString().ToLower().Contains("address")).First()
                        .Attribute("location").Value;
                }

            }

            return endPoint;
        }
    }
}