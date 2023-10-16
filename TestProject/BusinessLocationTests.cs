using TestProjectSD.Dtos;
using TestProjectSD.Interface;
using TestProjectSD.Models;
using TestProjectSD.Repositories;

namespace TestProject
{
    internal class BusinessLocationTest:DataTest
    {
        IBusinessLocationRepository _businessLocationRepository;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _businessLocationRepository = new BusinessLocationRepository(_dbContext);
        }


        [Test]
        public void TestForAddBusnessLocation()
        {
            var blName = "Location3";
            var bl = new BusinessLocationToAddDto();
            bl.Name = blName;
            bl.Address = "Address3";
            bl.PhoneNumber = "012-345-6789";

            _businessLocationRepository.AddBusinessLocation(bl, 1);

            var exist = _dbContext.BusinessLocations.Where(e => e.Name == blName && e.Customer.CustomerNumber == 1).FirstOrDefault();
            Assert.IsNotNull(exist);
        }

        [Test]
        public void TestForGetBusnessLocationForCurrentUser()
        {
           var res =  _businessLocationRepository.GetBusinessLocations(1);
           
            Assert.That(res, Is.Not.Null);
        }

        [Test]
        public void TestForGetBusnessLocationForNotExistedCustomer()
        {
            var res = _businessLocationRepository.GetBusinessLocations(3);

            Assert.That(res, Is.Empty);
        }

        [Test]
        public void TestForGetSingleBusnessLocationForCorrectCustomer()
        {
            var res = _businessLocationRepository.GetSingleBusinessLocation(2,1);

            Assert.That(res, Is.Not.Null);
        }

        [Test]
        public void TestForUpdateBusinessLocation()
        {
            var blName = "Location2";
            var adress = "LocationAddress2Updated";
            var businessLocation = _dbContext.BusinessLocations.Where(e => e.Name == blName).FirstOrDefault();

            var businessLocationEdited = new BusinessLocationDataDto();
            businessLocationEdited.Name = businessLocation.Name;
            businessLocationEdited.Address = adress;
            businessLocationEdited.PhoneNumber = businessLocation.PhoneNumber;
            businessLocationEdited.Id = businessLocation.Id;

            _businessLocationRepository.EditBusinessLocation(businessLocationEdited, 1);

            var exist = _dbContext.BusinessLocations.Where(e => e.Name == blName && e.Customer.CustomerNumber == 1).FirstOrDefault();
            Assert.IsNotNull(exist);
            Assert.That(adress, Is.EqualTo(exist.Address));
        }

        [Test]
        public void TestForTryToUpdateBLForAnotherCustomer()
        {
            var blName = "Location2";
            var adress = "LocationAddress2Updated";
            var businessLocation = _dbContext.BusinessLocations.Where(e => e.Name == blName).FirstOrDefault();

            var businessLocationEdited = new BusinessLocationDataDto();
            businessLocationEdited.Name = businessLocation.Name;
            businessLocationEdited.Address = adress;
            businessLocationEdited.PhoneNumber = businessLocation.PhoneNumber;
            businessLocationEdited.Id = businessLocation.Id;

            _businessLocationRepository.EditBusinessLocation(businessLocationEdited, 2);

            var exist = _dbContext.BusinessLocations.Where(e => e.Name == blName && e.Customer.CustomerNumber == 1).FirstOrDefault();
          
            Assert.That(adress, Is.Not.EqualTo(exist.Address));

        }

        [Test]
        public void TestForDeleteBusinessLocation()
        {
            var location = _dbContext.BusinessLocations.Where(e => e.Id == 1).FirstOrDefault();
            _businessLocationRepository.DeleteBusinessLocation(1, 1);

            var exist = _dbContext.BusinessLocations.Where(e => e.Id == 1).FirstOrDefault();
            Assert.That(exist, Is.Null);

        }
        [Test]
        public void TestForTryToDeleteBLForAnotherCustomer()
        {
            var location = _dbContext.BusinessLocations.Where(e => e.Id == 2).FirstOrDefault();
            _businessLocationRepository.DeleteBusinessLocation(1, 2);
            var locationRes = _dbContext.BusinessLocations.Where(e => e.Id == 2).FirstOrDefault();

            Assert.That(locationRes, Is.Not.Null);

        }
    }
}
