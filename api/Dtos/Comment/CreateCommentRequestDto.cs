using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Comment
{
    public class CreateCommentRequestDto
    {
        [Required]
        [MinLength(1, ErrorMessage = "Title must be greater than 1")]
        [MaxLength(50, ErrorMessage = "Title cannot be greater than 50")]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MinLength(1, ErrorMessage = "Content cannot be empty")]
        [MaxLength(200, ErrorMessage = "Content cannot be greater than 200")]
        public string Content { get; set; } = string.Empty;
    }
}