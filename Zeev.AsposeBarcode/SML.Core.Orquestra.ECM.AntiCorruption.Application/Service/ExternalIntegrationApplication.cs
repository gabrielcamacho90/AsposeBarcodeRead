using SML.Core.Orquestra.ECM.AntiCorruption.Application.Interfaces;
using SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.ExternalResponse;
using SML.Core.Orquestra.ECM.AntiCorruption.Domain.Helpers;
using SML.Core.Orquestra.ECM.AntiCorruption.Domain.Helpers.Extensions;
using System;

namespace SML.Core.Orquestra.ECM.AntiCorruption.Application
{
    public class ExternalIntegrationApplication : IExternalIntegrationApplication
    {
        #region Properties

        private IExternalIntegration _external { get; set; }
        private static string _urlExternalIntegrationEnd { get { return "/ExternalIntegration.asmx"; } }

        #endregion

        #region Constructors

        public ExternalIntegrationApplication(string url, string token)
        {
            url = ServiceHelper.ValidateUrl(url, _urlExternalIntegrationEnd);
            _external = new Services.ExternalIntegration.ExternalIntegration(token, url);
        }

        #endregion

        #region Methods

        public ExternalApplicationRoot GetApp(string token, string appAlias, bool showInactive)
            => _external.GetApplication(appAlias, showInactive).ToObject(new ExternalApplicationRoot());

        public string CopyApp(string appCode, int accessGroupID, string newAppCode, int destinyCompanyID, int ownerID, bool executeScript = false)
        {
            return _external.CopyApplication(appCode, accessGroupID, newAppCode, destinyCompanyID, ownerID, executeScript) ?? string.Empty;
        }

        #endregion

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _external.Dispose();
                }
                disposedValue = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
