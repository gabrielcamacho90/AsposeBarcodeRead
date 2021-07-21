using SML.Core.Orquestra.ECM.AntiCorruption.Domain.EntityDb;
using System.Collections.Generic;

namespace SML.Core.Orquestra.ECM.AntiCorruption.Application.ViewModels
{
    public class InstanceViewModel
    {
        public InstanceViewModel() { Instances = new List<InstanceDb>(); }
        public List<InstanceDb> Instances { get; set; }
    }
}
