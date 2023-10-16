using System.Security.Claims;
using System.Security.Principal;

namespace TestProjectSD_withDatabase.Extensions
{
    public static class IdentityExtensions
    {
        public static int GetCustomerNumber(this IIdentity identity)
        {
            ClaimsIdentity? claimsIdentity = identity as ClaimsIdentity;
            Claim? claim = claimsIdentity?.FindFirst(CustomClaimTypes.CustomerNumber);

            if (claim == null)
                return 0;

            return int.Parse(claim.Value);
        }
    }
}
