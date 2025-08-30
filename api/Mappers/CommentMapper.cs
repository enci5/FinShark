using api.Models;
using api.Dtos.Comment;

namespace api.Mappers
{

    public static class CommentMapper
    {
        public static CommentDto ToCommentDto(this Comment commentModel)
        {
            return new CommentDto
            {
                Id = commentModel.Id,
                Title = commentModel.Title,
                Content = commentModel.Content,
                CreatedDate = commentModel.CreatedDate,
                StockId = commentModel.StockId,  
            };
        }

        public static Comment CommnetFromCreateDto(this CreateCommentRequestDto commentDto, int stockId)
        {
            return new Comment
            {
                Title = commentDto.Title,
                Content = commentDto.Content,
                StockId = stockId,
            };
        }
    }
}