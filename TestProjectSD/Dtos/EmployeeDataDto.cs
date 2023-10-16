using System.Collections.ObjectModel;
using TestProjectSD.Models;

namespace TestProjectSD_withDatabase.Dtos
{
    public class EmployeeDataDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public Collection<BusinessLocationDataDto>? BusinessLocations { get; set; }
    }
}
