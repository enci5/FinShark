using System;
using Microsoft.AspNetCore.Mvc;
using api.Models;
using api.Data;
using api.Mappers;
using api.Dtos.Comment;
using Microsoft.EntityFrameworkCore;
using api.Interfaces;
using api.Repository;

namespace api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepo;

        public CommentController(ICommentRepository commentRepo)
        {
            _commentRepo = commentRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var comment = await _commentRepo.GetAllAsync();
            var commentDto = comment.Select(c => c.ToCommentDto());
            return Ok(commentDto);
        }
    }
}
