using api.Models;
using api.Dtos.Comment;
using api.Extensions;
using Microsoft.AspNetCore.Identity;

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
                CreatedBy = commentModel.AppUser.UserName
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

        public static Comment CommnetFromUpdateDto(this UpdateCommentRequestDto commentDto)
        {
            return new Comment
            {
                Title = commentDto.Title,
                Content = commentDto.Content,
            };
        }
    }
}