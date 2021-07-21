using SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.Common;
using SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.DocumentRequest;
using SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.ExecuteActionRequest;
using System.Collections.Generic;
using System.Linq;

namespace SML.Core.Orquestra.ECM.AntiCorruption.Domain.Extension
{
    public static class DocumentWorkflowExtension
    {

        public static List<ExecuteActionRequestField> GetNames(this DocumentWorkflow documentWorkflow) =>
             new List<ExecuteActionRequestField>()
            {
                new ExecuteActionRequestField { Name = "DOCTYPE_ID", CommandOrName   = documentWorkflow.DoctypeCommand },
                new ExecuteActionRequestField { Name = "IDXQUEUE_ID",  CommandOrName  = documentWorkflow.QueueName  },
                new ExecuteActionRequestField { Name = "IDXSITDOC_ID", CommandOrName = documentWorkflow.SituationName  },
                new ExecuteActionRequestField { Name = "PNDRSN_ID",  CommandOrName = documentWorkflow.PendencyName },
            };

        public static List<ExecuteActionRequestField> GetIDs(this DocumentWorkflow documentWorkflow) =>
            new List<ExecuteActionRequestField>()
            {
                new ExecuteActionRequestField { Name = "DOCTYPE_ID",    Value  = documentWorkflow.DoctypeId.ToString() },
                new ExecuteActionRequestField { Name = "IDXQUEUE_ID",   Value  = documentWorkflow.QueueId.ToString() },
                new ExecuteActionRequestField { Name = "IDXSITDOC_ID",  Value  = documentWorkflow.SituationId.ToString()  },
                new ExecuteActionRequestField { Name = "PNDRSN_ID",     Value  = documentWorkflow.PendencyId.ToString() },
            };

        public static List<ExecuteActionRequestField> GetValuesNotNullOrZero(this DocumentWorkflow documentWorkflow)
        {
            var result = new List<ExecuteActionRequestField>();

            result.AddRange(GetValue("DOCTYPE_ID", documentWorkflow.DoctypeId, documentWorkflow.DoctypeCommand));
            result.AddRange(GetValue("IDXQUEUE_ID", documentWorkflow.QueueId, documentWorkflow.QueueName));
            result.AddRange(GetValue("IDXSITDOC_ID", documentWorkflow.SituationId, documentWorkflow.SituationName));
            result.AddRange(GetValue("PNDRSN_ID", documentWorkflow.PendencyId, documentWorkflow.PendencyName));

            return result;
        }


        public static List<OrquestraECMRequestRequestField> ToRequestField(this List<ExecuteActionRequestField> documentWorkflow)
        {
            return documentWorkflow.Select(x => new OrquestraECMRequestRequestField { Name = x.Name, Value = x.Value }).ToList();
        }

        private static List<ExecuteActionRequestField> GetValue(string name, int id, string command)
        {
            var result = new List<ExecuteActionRequestField>();
            if (!string.IsNullOrEmpty(command))
                result.Add(new ExecuteActionRequestField { Name = name, CommandOrName = command });
            else if (id > 0)
                result.Add(new ExecuteActionRequestField { Name = name, Value = id.ToString() });

            return result;
        }

    }
}
