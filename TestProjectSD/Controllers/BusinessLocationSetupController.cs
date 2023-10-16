using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestProjectSD.Interface;
using TestProjectSD.Models;
using TestProjectSD_withDatabase.Exceptions;
using TestProjectSD_withDatabase.Extensions;

namespace TestProjectSD.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class BusinessLocationSetupController : ControllerBase
    {
       readonly IBusinessLocationRepository _businessLocationRepository;
        public BusinessLocationSetupController(IBusinessLocationRepository businessLocationRepository)
        {
            _businessLocationRepository = businessLocationRepository;
        }

        [HttpGet]
        public ActionResult<List<BusinessLocation>> GetBusinessLocations()
        {
             return Ok(_businessLocationRepository.GetBusinessLocations(User.Identity.GetCustomerNumber()));
           
        }

        [HttpGet]
        public ActionResult<BusinessLocation> GetSingleBusinessLocation(int id)
        {
            return _businessLocationRepository.GetSingleBusinessLocation(id, User.Identity.GetCustomerNumber()); 
        }

        [HttpPut]
        public ActionResult EditBusinessLocation(BusinessLocationDataDto location)
        {
            try
            {
                _businessLocationRepository.EditBusinessLocation(location, User.Identity.GetCustomerNumber());
            }
            catch (Exception)
            {
                return BadRequest($"Failed to Update Business Location {location.Name}");
            }

            return Ok();
        }

        [HttpPost]
        public ActionResult AddBusinessLocation(BusinessLocationToAddDto location)
        {
            try
            {
                _businessLocationRepository.AddBusinessLocation(location, User.Identity.GetCustomerNumber());
            }
            catch (IncorrectPhoneNumberException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return BadRequest($"Failed to Add Business Location {location.Name}");
            }

            return Ok();
        }

        [HttpDelete]
        public ActionResult DeleteBusinessLocation(int id)
        {
            try
            {
                _businessLocationRepository.DeleteBusinessLocation(id, User.Identity.GetCustomerNumber());
            }
            catch(Exception )
            {
                return BadRequest($"Failed to Delete Business Location ");
            }

            return Ok();
        }       
    }
}
