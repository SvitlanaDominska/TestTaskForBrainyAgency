using TestProjectSD.Models;

namespace TestProjectSD.Interface
{
    public interface IBusinessLocationRepository
    {
        public List<BusinessLocation> GetBusinessLocations(int customerNumber);
        public BusinessLocation GetSingleBusinessLocation(int locationId, int customerNumber);
        public void EditBusinessLocation(BusinessLocationDataDto location, int customerNumber);
        public bool AddBusinessLocation(BusinessLocationToAddDto location, int customerNumber);
        public bool DeleteBusinessLocation(int locationId, int customerNumber);


    }
}
