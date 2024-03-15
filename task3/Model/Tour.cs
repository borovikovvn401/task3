using System;
using System.Collections.Generic;

namespace task3.Model
{
    public partial class Tour
    {
        public Tour()
        {
            Types = new HashSet<Type>();
        }

        public int Id { get; set; }
        public int TicketCount { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public byte[]? ImagePreview { get; set; }
        public decimal Price { get; set; }
        public bool IsActual { get; set; }
        public string CountryCode { get; set; } = null!;

        public string tickets => "Билетов: " + TicketCount;
        public string Actual => IsActual ? "Актуален" : "Неактуален";
        public string color => IsActual ? "Green" : "Red";

        public string getPrice => (int) Price + " РУБ.";

        public virtual Country CountryCodeNavigation { get; set; } = null!;

        public virtual ICollection<Type> Types { get; set; }
    }
}
