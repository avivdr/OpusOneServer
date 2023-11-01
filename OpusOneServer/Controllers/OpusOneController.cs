using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpusOneServerBL.Models;

namespace OpusOneServer.Controllers
{
    [Route("OpusOneAPI")]
    [ApiController]
    public class OpusOneController : ControllerBase
    {
        #region Add connection to the db context using dependency injection
        OpusOneDbContext context;
        public OpusOneController(OpusOneDbContext context)
        {
            this.context = context;
        }
        #endregion


    }
}
