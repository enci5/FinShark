namespace api.Dtos.Comment
{
    public class CreateCommentRequestDto
    {
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public int? StockId { get; set; }
    }
}