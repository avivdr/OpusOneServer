using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;
using OpusOneServerBL.Models;
using System.IO;
using OpusOneServer.Service;
using OpusOneServerBL.MusicModels;
using OpusOneServer.DTO;

namespace OpusOneServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OpusOneController : ControllerBase
    {
        const string OmniSearchKey_Next = "OmniSearch_Next";
        const string OmniSearchKey_Query = "OmniSearch_Query";
        const string UserKey = "User";

        #region dependency injection
        OpusOneDbContext context;
        readonly OpenOpusService service;
        readonly JsonSerializerOptions options;
        public OpusOneController(OpusOneDbContext context, OpenOpusService _service)
        {
            this.context = context;
            service = _service;
            options = new JsonSerializerOptions()
            {
                ReferenceHandler = ReferenceHandler.Preserve
            };
        }
        #endregion


        [Route(nameof(Hello))]
        [HttpGet]
        public async Task<ActionResult> Hello()
        {
            return Ok("hi");
        }

        [Route(nameof(SearchComposerByName) + "/{query}")]
        [HttpGet]
        public async Task<ActionResult<List<Composer>>> SearchComposerByName([FromRoute] string query)
        {
            if (query.Length < 4)
                return BadRequest();

            List<Composer>? composers = await service.SearchComposerByName(query);
            if (composers != null)
                return Ok(composers);

            return BadRequest();
        }

        #region OmniSearch

        [Route(nameof(OmniSearch) + "/{query}/{next}")]
        [HttpGet]
        public async Task<ActionResult<OmniSearchDTO>> OmniSearch([FromRoute] string query, [FromRoute] int next = 0)
        {
            if (string.IsNullOrEmpty(query) || query.Length < 3) 
                return BadRequest();

            OmniSearchDTO? result = await service.OmniSearch(query, next);
            if (result == null)
                return BadRequest();

            HttpContext.Session.SetString(OmniSearchKey_Query, query);
            HttpContext.Session.SetInt32(OmniSearchKey_Next, result.Next);

            return Ok(result);
        }

        [Route(nameof(NextOmniSearch))]
        [HttpGet]
        public async Task<ActionResult<OmniSearchDTO>> NextOmniSearch()
        {
            string? query;
            int? next;
            try
            {
                query = HttpContext.Session.GetString(OmniSearchKey_Query);
                next = HttpContext.Session.GetInt32(OmniSearchKey_Next);

                if (next == null || next == 0 || query == null)
                    return BadRequest();

                return await OmniSearch(query, (int)next);
            }
            catch(Exception)
            {
                return BadRequest();
            }
        }

        #endregion

        [Route(nameof(GetAllPosts))]
        [HttpGet]
        public async Task<ActionResult<List<Post>>> GetAllPosts()
        {
            return Ok(context.GetPostsWithData().OrderByDescending(x => x.UploadDateTime));
        }

        [Route(nameof(GetPostById) + "/{id}")]
        [HttpGet]
        public async Task<ActionResult<Post>> GetPostById([FromRoute] int id)
        {
            Post? post = context.GetPostsWithData().FirstOrDefault(x => x.Id == id);

            if (post == null)
                return NotFound();

            return Ok(post);
        }

        [Route(nameof(UploadComment))]
        [HttpPost]
        public async Task<ActionResult> UploadComment([FromBody] Comment comment)
        {
            if (comment == null) return BadRequest();

            try
            {
                User? sessionUser = HttpContext.Session.GetObject<User>(UserKey);
                if (comment.Creator == null || sessionUser == null || comment.Creator.Id != sessionUser.Id)
                    return Unauthorized();

                context.Attach(comment);

                context.Comments.Add(comment);

                await context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
         
        [Route(nameof(UploadPost))]
        [HttpPost]
        public async Task<ActionResult> UploadPost([FromForm] string post, IFormFile? file = null)
        {
            Post? p;
            try
            {
                p = JsonSerializer.Deserialize<Post>(post, options);

                if (p == null)
                    return BadRequest();

                User? sessionUser = HttpContext.Session.GetObject<User>(UserKey);
                if (p.Creator == null || sessionUser == null || p.Creator.Id != sessionUser.Id)
                    return Unauthorized();

                context.AttachPostData(p);

                context.Posts.Add(p);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            if (file == null || file.Length == 0)
                return Ok();

            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", p.Id.ToString() + Path.GetExtension(p.FileExtension));

            try
            {
                using (var fileStream = new FileStream(path, FileMode.Create))
                
                file.CopyTo(fileStream);
                return Ok();
            }
            catch (Exception)
            {
                context.Posts.Remove(p);
                await context.SaveChangesAsync();
                return BadRequest();
            }
        }

        #region Login + Register

        [Route(nameof(Login))]
        [HttpPost]  
        public async Task<ActionResult<User>> Login([FromBody] User user)
        {            
            User? u = context.GetUsersWithData().Where(x => x.Username == user.Username && x.Password == user.Password).FirstOrDefault();

            if(u != null)
            {
                HttpContext.Session.SetObject(UserKey, u);;
                return Ok(u);
            }

            return Unauthorized();
        }

        [Route(nameof(Register))]
        [HttpPost]
        public async Task<ActionResult<User>> Register([FromBody] User user)
        {
            if (context.Users.Any(x => x.Username == user.Username))
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

            HttpContext.Session.SetObject(UserKey, user);
            return Ok(user);
        }
        #endregion
    }
}
