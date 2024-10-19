using System.ComponentModel.DataAnnotations;

namespace api.DTOs.Comment
{
    public class UpdateCommentRequestDto
    {
        [Required]
        [MinLength(5, ErrorMessage = "Title must be 5 or more characters")]
        [MaxLength(280, ErrorMessage = "Title cannot be over 280 characters")]
        public string Title { get; set; } = string.Empty;
        [Required]
        [MinLength(5, ErrorMessage = "Content must be 5 or more characters")]
        [MaxLength(280, ErrorMessage = "Content cannot be over 280 characters")]
        public string Content { get; set; } = string.Empty;
    }
}