using System.Collections.Generic;

namespace Zeev.AsposeBarcode.Library.Entities
{
    public class FieldEntity
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }

        public FieldEntity(string name, string value, string description)
        {
            this.Name = name; 
            this.Value = value;
            this.Description = description;
        }
    }

    public class FieldCollection : List<FieldEntity>
    {
        public void Add(string name, string value, string description)
        {
            this.Add(new FieldEntity(name, value, description));
        }
    }
}
