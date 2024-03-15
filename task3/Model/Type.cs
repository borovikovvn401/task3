using System;
using System.Collections.Generic;

namespace task3.Model
{
    public partial class Type
    {
        public Type()
        {
            Tours = new HashSet<Tour>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Tour> Tours { get; set; }
    }
}
