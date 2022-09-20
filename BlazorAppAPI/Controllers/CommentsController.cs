using BlazorAppAPI.Data;
using ClassLibraryAPI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlazorAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public CommentsController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/<CommentsController>
        [HttpGet]
        public IEnumerable<CommentDTO> Get()
        {
            List<Comment> comments = _dbContext.Comments.AsNoTracking().ToList();
            List<CommentDTO> commentsDTO = new List<CommentDTO>();
            foreach(Comment comment in comments)
            {
                CommentDTO commentDTO = new CommentDTO()
                {
                    Content = comment.Content,
                    Date = comment.Date
                };
                commentsDTO.Add(commentDTO);
            }
            return commentsDTO;
        }

        // GET api/<CommentsController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/<CommentsController>
        [HttpPost]
        public void Post([FromBody] CommentDTO value)
        {
            Comment comment = new Comment()
            {
                Content = value.Content,
                Date = value.Date
            };
            _dbContext.Comments.Add(comment);
            _dbContext.SaveChanges();
        }

        // PUT api/<CommentsController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        // DELETE api/<CommentsController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
