using System;
using api.Models;
using api.Data;
using api.Dtos.Stock;
using api.Dtos.Comment;

namespace api.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllAsync();
    }
}
