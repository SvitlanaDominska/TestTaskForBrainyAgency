using Microsoft.VisualBasic;
using System.Collections.ObjectModel;

namespace TestProjectSD.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public virtual Customer? Customer { get; set; }

        public Collection<BusinessLocation>? BusinessLocations { get; set; }
    }
}
