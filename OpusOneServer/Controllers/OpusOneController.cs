using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpusOneServerBL.Models;

namespace OpusOneServer.Controllers
{
    [Route("[controller]")]
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


        [Route(nameof(Hello))]
        [HttpGet]
        public async Task<ActionResult> Hello()
        {
            return Ok("hi");
        }

        [Route(nameof(Login))]
        [HttpPost]  
        public async Task<ActionResult<User>> Login([FromBody] User user)
        {
            User u = context.Users.Where(x => x.Username == user.Username && x.Pwsd == user.Pwsd).FirstOrDefault();

            if(u != null)
            {
                HttpContext.Session.SetObject("user", u); 
                return Ok(u);
            }

            return Unauthorized();
        }
    }
}
