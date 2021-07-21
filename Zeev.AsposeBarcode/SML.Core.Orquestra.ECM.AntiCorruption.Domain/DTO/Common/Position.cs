namespace SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.Common
{
    public class DocumentWorkflow
    {
        public int DoctypeId { get; set; }
        public int QueueId { get; set; }
        public int SituationId { get; set; }
        public int PendencyId { get; set; }

        public string DoctypeCommand { get; set; }
        public string QueueName { get; set; }
        public string SituationName { get; set; }
        public string PendencyName { get; set; }
    }
}
