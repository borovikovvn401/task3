using System;
using System.Collections.Generic;

namespace task3.Model
{
    public partial class Country
    {
        public Country()
        {
            Tours = new HashSet<Tour>();
        }

        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;

        public virtual ICollection<Tour> Tours { get; set; }
    }
}
