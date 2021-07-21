using SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.ExecuteActionRequest;
using System;
using System.Collections.Generic;

namespace SML.Core.Orquestra.ECM.AntiCorruption.Application.Interfaces
{
    public interface ISilentLoginUIApplication : IDisposable
    {
        string Search(ExecuteActionRequestOption option, List<ExecuteActionRequestField> fields, List<ExecuteActionRequestColumn> coluns);
        string Search(ExecuteActionRequestOption option, List<ExecuteActionRequestField> fields, ExecuteActionRequestResult result);

        string Viewer(ExecuteActionRequestOption option, List<ExecuteActionRequestField> fields);

        string Import(ExecuteActionRequestOption option, List<ExecuteActionRequestField> fields);
    }
}