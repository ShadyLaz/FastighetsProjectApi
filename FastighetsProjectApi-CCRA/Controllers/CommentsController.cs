﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FastighetsProjectApi_CCRA.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using FastighetsProjectApi_CCRA.Areas.Identity.Data;
using FastighetsProjectApi_CCRA.DTOmodel;
using FastighetsProjectApi_CCRA.HelpClasses;
using FastighetsProjectApi_CCRA.Repository;
using FastighetsProjectApi_CCRA.Contracs;

namespace FastighetsProjectApi_CCRA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly DbContext _context;
        private readonly SignInManager<FastighetsProjectApi_CCRAUser> _signInManager;
        private readonly UserManager<FastighetsProjectApi_CCRAUser> _userManager;
        private readonly ICommentRepository _commentRepository;

        public CommentsController(DbContext context, SignInManager<FastighetsProjectApi_CCRAUser> signInManager, UserManager<FastighetsProjectApi_CCRAUser> userManager, ICommentRepository commentRepository)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
            _commentRepository = commentRepository;
        }

        /// <summary>
        /// Requires user to Login and requires an ID, Returns Comments but skips over S comments and gets the T comments.
        /// </summary>
        /// <returns></returns>
        // GET: api/Comments (Skip take)
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Comment>>> GetCommentsTS(int id, [FromQuery] SkipTakeParameters skipTakeParameters)
        {

            var comments =  _commentRepository.GetCommentsTS(id, skipTakeParameters);

            return Ok(comments);
        }

        /// <summary>
        /// Requires user to Login,Returns all the comments posted by a specific user using his Username, returns only 10 comments.
        /// </summary>
        /// <returns></returns>
        //get api/ByUser/USERNAME
        [HttpGet("byuser/{username}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Comment>>> GetCommentsByUserTS(string username, [FromQuery] SkipTakeParameters skipTakeParameters)
        {
            var comments = _commentRepository.GetCommentsByUserTS(username, skipTakeParameters);
            return Ok(comments);
        }
        /// <summary>
        /// Requires user to Login.Creates a new comment.
        /// </summary>
        /// <returns></returns>
        // POST: api/Comments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Comment>> PostComment([FromBody]Comment comment)
        {
            if (comment == null)
            {
                return BadRequest();
            }
            comment.UserName = User.Identity.Name;
            var result = _commentRepository.PostComment(comment);
            
            
            return Created("https//localhost:5001/api/comments", result);// behöver skicka in en annan URI
        }

        // DELETE: api/Comments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
            //////////////////////////////////
             //var username = User.Identity.Name;

            _context.Users.FirstOrDefault(a => a.UserName == comment.UserName).Comments--;
            _context.SaveChanges();
            //////////////////////////////////
            return NoContent();
        }

        private bool CommentExists(int id)
        {
            return _context.Comments.Any(e => e.id == id);
        }
    }
}
