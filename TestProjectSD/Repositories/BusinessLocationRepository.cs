using TestProjectSD.Interface;
using TestProjectSD.Models;
using TestProjectSD_withDatabase.Helpers;

namespace TestProjectSD.Repositories
{
    public class BusinessLocationRepository : DataRepository, IBusinessLocationRepository
    {
       
        public BusinessLocationRepository(DataBaseContext context):base(context)
        {
        }
        public List<BusinessLocation> GetBusinessLocations(int customerNumber)
        {
            return _dataContext.BusinessLocations.Where(x=>x.CustomerNumber == customerNumber).ToList();
        }

        public BusinessLocation GetSingleBusinessLocation(int locationId, int customerNumber)
        {
            var businessLocation = _dataContext.BusinessLocations.Where(u => u.Id == locationId && u.CustomerNumber == customerNumber).FirstOrDefault();

            if (businessLocation != null)
            {
                return businessLocation;
            }
            throw new Exception("Failed to Get Business Location");
        }

        public void EditBusinessLocation(BusinessLocationDataDto location, int customerNumber)
        {
            var locationDb = _dataContext.BusinessLocations.Where(u => u.Id == location.Id && u.CustomerNumber == customerNumber).FirstOrDefault();

            if (locationDb != null)
            {
                locationDb.Name= location.Name;
                locationDb.Address = location.Address;
                locationDb.PhoneNumber = PhoneNumber.VerifyPhoneNumber(location.PhoneNumber);
            }
            
            SaveChanges();    
        }

        public bool AddBusinessLocation(BusinessLocationToAddDto location, int customerNumber)
        {
           var newLocation = new BusinessLocation();
           
            newLocation.Address = location.Address;
            newLocation.Name = location.Name;
            newLocation.PhoneNumber = PhoneNumber.VerifyPhoneNumber(location.PhoneNumber);
            newLocation.CustomerNumber = customerNumber;

            AddEntity(newLocation);

            return SaveChanges();           
        }

        public bool DeleteBusinessLocation(int locationId, int customerNumber)
        {
           var locationDb = _dataContext.BusinessLocations.Where(u => u.Id == locationId && u.CustomerNumber == customerNumber).FirstOrDefault();

            if (locationDb != null)
            {
                RemoveEntity(locationDb);
                return SaveChanges();
            }
            return false;
        }
    }
}
