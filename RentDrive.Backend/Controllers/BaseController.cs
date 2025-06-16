using Microsoft.AspNetCore.Mvc;

namespace RentDrive.Backend.Controllers
{
    public class BaseController : ControllerBase
    {
        protected bool IsGuidValid(string id, ref Guid guidId)
        {
            if (String.IsNullOrWhiteSpace(id))
            {
                return false;
            }
            bool isGuidValid = Guid.TryParse(id, out guidId);
            if (!isGuidValid)
            {
                return false;
            }
            return true;
        }
    }
}
