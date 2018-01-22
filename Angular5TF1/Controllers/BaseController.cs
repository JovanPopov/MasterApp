using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Angular5TF1.Controllers
{
    [Produces("application/json")]
    [Route("api/Base")]
    public abstract class BaseController : Controller
    {
        protected ObjectResult InternalServerError(string message)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, message);
        }
    }
}