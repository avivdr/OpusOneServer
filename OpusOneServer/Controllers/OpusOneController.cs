using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using OpusOneServerBL.Models;
using System.IO;
using OpusOneServerBL.OpenOpusService;

namespace OpusOneServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OpusOneController : ControllerBase
    {
        #region dependency injection
        OpusOneDbContext context;
        readonly OpenOpusService service;
        public OpusOneController(OpusOneDbContext context, OpenOpusService _service)
        {
            this.context = context;
           service = _service;
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

        [Route(nameof(SearchComposerByName))]
        [HttpGet]
        public async Task<ActionResult<List<Composer>>> SearchComposerByName([FromQuery] string query)
        {
            if (query.Length < 4)
                return BadRequest();

            List<Composer>? composers = await service.SearchComposerByName(query);
            if (composers != null)
            {
                return Ok(composers);
            }            

            return BadRequest();
        }

        [Route(nameof(UploadPost))]
        [HttpPost]
        public async Task<ActionResult> UploadPost([FromForm] string post, IFormFile file)
        {
            Post? p = JsonSerializer.Deserialize<Post>(post);
            if (p == null)
                return BadRequest();

            try
            {
                await context.SaveComposer(p.Composer);
                await context.SaveWork(p.Work);

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

            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", newFolderPath, p.Id.ToString(), Path.GetExtension(file.FileName));

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

        #region Login + Register

        [Route(nameof(Login))]
        [HttpPost]  
        public async Task<ActionResult<User>> Login([FromBody] User user)
        {            
            User? u = context.GetUserWithData().Where(x => x.Username == user.Username && x.Password == user.Password).FirstOrDefault();

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
            catch (Exception)
            {
                throw new Exception("Register error");
            }

            HttpContext.Session.SetObject("user", user);
            return Ok();
        }
        #endregion
    }
}
