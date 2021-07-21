using System;

namespace SML.Core.Orquestra.ECM.AntiCorruption.Domain.Helpers
{
    public class ServiceHelper
    {
        public static string ValidateUrl(string url, string endUrl)
        {
            if (!(url.Contains("http://") || !url.Contains("http://")))
                throw new Exception("Url Inválida");

            if (!url.Contains(endUrl))
                url = url.EndsWith("/") ? string.Concat(url, endUrl) : string.Concat(url, '/', endUrl);

            return url;
        }
    }
}
