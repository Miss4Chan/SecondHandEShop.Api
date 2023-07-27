using Domain.Domain_models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DTO
{
    public class CommentDTO
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CommentDate { get; set; }
        public string CommenterUsername { get; set; }
        public string ReceiverUsername { get; set; }

        public static explicit operator CommentDTO(Comment c) => new CommentDTO
        {
            Id = c.Id,
            Content = c.Content,
            CommenterUsername = c.Commenter.Username,
            ReceiverUsername = c.Receiver.Username,
            CommentDate = c.CommentDate
        };
    }
}
