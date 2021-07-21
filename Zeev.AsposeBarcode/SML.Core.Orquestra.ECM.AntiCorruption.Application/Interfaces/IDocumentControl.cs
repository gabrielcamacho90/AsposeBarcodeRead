using SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.Common;
using System.Collections.Generic;
using System.IO;

namespace SML.Core.Orquestra.ECM.AntiCorruption.Application.Interfaces
{
    public interface IDocumentControl
    {
        long CreateDocumentWithUpload(List<FileInfo> files, DocumentWorkflow preDocumentWorkflow, DocumentWorkflow posDocumentWorkflow, Dictionary<string, string> fields);
        long CreateDocumentWithUpload(Dictionary<string, string> files, DocumentWorkflow preDocumentWorkflow, DocumentWorkflow posDocumentWorkflow, Dictionary<string, string> fields);
    }
}
