using Domain.Domain_models;
using Domain.DTO;
using Microsoft.EntityFrameworkCore;
using Repository;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.Implementation
{
    public class CommentService : ICommentService
    {
        private readonly AppDbContext _context;

        public CommentService(AppDbContext context)
        {
            _context = context;
        }

        public CommentDTO AddComment( CommentDTO comment, int rating)
        {
            var commenter = _context.ShopApplicationUsers.FirstOrDefault(u => u.Email == comment.CommenterUsername);
            var receiver = _context.ShopApplicationUsers.FirstOrDefault(u => u.Email == comment.ReceiverUsername);
            
            if(rating !=null)
            {
                receiver.UserRatingCount += 1;
                receiver.UserRatingTotal += rating;
                receiver.UserRating = receiver.UserRatingTotal / receiver.UserRatingCount;
            }

            if (commenter == null || receiver == null)
            {
                throw new ArgumentException("Invalid commenter or receiver.");
            }

            var newComment = new Comment
            {
                Content = comment.Content,
                Commenter = commenter,
                Receiver = receiver,
                CommentDate = DateTime.UtcNow
            };

            _context.Comments.Add(newComment);
            _context.SaveChanges();

            return (CommentDTO)newComment;
        }


        public bool DeleteComment(int commentId)
        {
            var comment = _context.Comments.FirstOrDefault(c => c.Id == commentId);

            if (comment == null)
            {
                return false;
            }

            _context.Comments.Remove(comment);
            _context.SaveChanges();

            return true;
        }
        public List<CommentDTO> GetCommentsByReceiverUsername(string receiverUsername)
        {
            var receiver = _context.ShopApplicationUsers.FirstOrDefault(u => u.Username == receiverUsername);

            if (receiver == null)
            {
                throw new ArgumentException("Receiver not found.");
            }

            return _context.Comments
                .Include(c => c.Commenter) 
                .Where(c => c.Receiver.Id == receiver.Id)
                .Select(c => (CommentDTO)c)
                .ToList();
        }
    }
}
