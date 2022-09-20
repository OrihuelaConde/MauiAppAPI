using System.ComponentModel.DataAnnotations;

namespace ClassLibraryAPI
{
    public class CommentDTO
    {
        [Required]
        public string Content { get; set; }
        public DateTime Date { get; set; }
    }
}