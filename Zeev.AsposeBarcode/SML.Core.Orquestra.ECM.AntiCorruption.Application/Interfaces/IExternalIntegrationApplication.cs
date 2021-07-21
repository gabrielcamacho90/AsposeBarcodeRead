using SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.ExternalResponse;
using System;

namespace SML.Core.Orquestra.ECM.AntiCorruption.Application
{
    public interface IExternalIntegrationApplication : IDisposable
    {
        ExternalApplicationRoot GetApp(string token, string appAlias, bool showInactive);
        string CopyApp(string appCode, int accessGroupID, string newAppCode, int destinyCompanyID, int ownerID, bool executeScript);
    }
}
