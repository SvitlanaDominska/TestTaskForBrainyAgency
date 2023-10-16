using System.Collections.ObjectModel;

namespace TestProjectSD.Models
{
    public class Customer
    {
        public int CustomerNumber { get; set; }
        public string Name { get; set; } = string.Empty;
        public virtual Collection<BusinessLocation>? BusinessLocations { get; set; }
    }
}
