using SML.Core.Orquestra.ECM.AntiCorruption.Application.Interfaces;
using System;

namespace SML.Core.Orquestra.ECM.AntiCorruption.Services
{
    internal class ExternalIntegration : IExternalIntegration
    {
        #region Properties

        private ExternalIntegration _external { get; set; }

        public string Token { get; private set; }


        #endregion

        #region Constructor

        public ExternalIntegration(string token, string url)
        {
            Token = token;
            _external = new ExternalIntegration(url,url);
        }

        #endregion

        #region Interface Methods

        public string CopyApplication(string appCode, int accessGroupID, string newAppCode, int destinyCompanyID, int ownerID, bool executeScript)
        {
            var result = _external.CopyApplication(Token, appCode, accessGroupID, newAppCode, destinyCompanyID, ownerID, executeScript);
            return result;
        }

        public string GetApplication(string appAlias, bool showInactive)
        {
            var app = _external.GetApplication(Token, appAlias, showInactive);
            return app;
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
