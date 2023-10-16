using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestProjectSD.Models
{
    public class BusinessLocation
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        [ForeignKey("CustomerNumber")]
        public int CustomerNumber { get; set; }
        public virtual Customer Customer { get; set; }
        public Collection<Employee>? Employees { get; set; }

    }
}
