using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpusOneServerBL.Models;
using System.Text.Json;

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

        [Route(nameof(GetPosts))]
        [HttpGet]
        public async Task<ActionResult<List<Post>>> GetPosts()
        {
            return Ok(context.Posts.ToList());
        }

        [Route(nameof(UploadPost))]
        [HttpPost]
        public async Task<ActionResult> UploadPost([FromForm] string post, IFormFile file)
        {
            Post p = JsonSerializer.Deserialize<Post>(post);
            if (p == null)
                return BadRequest();

            try
            {
                p = context.Posts.Add(p).Entity;
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            if (file == null || file.Length == 0)
                return Ok();

            string newFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", p.CreatorId.ToString());
            if (!Directory.Exists(newFolderPath))            
                Directory.CreateDirectory(newFolderPath);

            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", newFolderPath, p.Id.ToString());

            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                try
                {
                    file.CopyTo(fileStream);
                    return Ok();
                }
                catch (Exception)
                {
                    return BadRequest();
                }
            }
        }

        [Route(nameof(Login))]
        [HttpPost]  
        public async Task<ActionResult<User>> Login([FromBody] User user)
        {
            User u = context.GetUserWithData().Where(x => x.Username == user.Username && x.Pwsd == user.Pwsd).FirstOrDefault();

            if(u != null)
            {
                HttpContext.Session.SetObject("user", u); 
                return Ok(u);
            }

            return Unauthorized();
        }

        [Route(nameof(Register))]
        [HttpPost]
        public async Task<ActionResult> Register([FromBody] User user)
        {
            if (context.Users.FirstOrDefault(u => u.Username == user.Username) != null)
                return Conflict();

            try
            {
                context.Users.Add(user);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Register error");
            }

            return Ok();
        }
    }
}
