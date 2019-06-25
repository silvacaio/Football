using Microsoft.AspNetCore.Mvc;


namespace Football.API.Controllers
{
    public abstract class BaseController : Controller
    {
        protected IActionResult ResponseSuccess(object result = null)
        {
            return Ok(new
            {
                success = true,
                data = result
            });
        }

        protected IActionResult ResponseError(string[] errors)
        {
            return BadRequest(new
            {
                success = false,
                errors
            });
        }
    }
}
