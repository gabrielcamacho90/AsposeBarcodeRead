using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sml.Econtent.Ecm.Library.BarcodeManager.Entities
{
    public class RulesEntity
    {
        public int Order { get; set; }
        public string SearchType { get; set; }
        public string Argument { get; set; }
        public FieldCollection Fields { get; set; }

        public RulesEntity(int order, string searchType, string argument, FieldCollection fields)
        {
            this.Order = order;
            this.SearchType = searchType;
            this.Argument = argument;
            this.Fields = fields;
        }
    }

    public class RulesCollection : List<RulesEntity>
    {
        public void Add(int order, string searchType, string argument, FieldCollection fields)
        {
            this.Add(new RulesEntity(order, searchType, argument, fields));
        }
    }
}
