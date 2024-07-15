using System;
using System.Collections.Generic;

namespace JPOS.Model.Entities
{
    public partial class User
    {
        public User()
        {
            Blogs = new HashSet<Blog>();
            Feedbacks = new HashSet<Feedback>();
            Policies = new HashSet<Policy>();
            Requests = new HashSet<Request>();
            Transactions = new HashSet<Transaction>();
        }

        public string UserId { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? FullName { get; set; }
        public string? PhoneNum { get; set; }
        public string? Address { get; set; }
        public int RoleId { get; set; }
        public DateTime? CreateDate { get; set; }
        public bool? Status { get; set; }
        public string Email { get; set; } = null!;

        public virtual Role Role { get; set; } = null!;
        public virtual ICollection<Blog> Blogs { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; }
        public virtual ICollection<Policy> Policies { get; set; }
        public virtual ICollection<Request> Requests { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
        public string UserStatus => Status.HasValue && Status.Value ? "Active" : "Inactive";
    }
}
