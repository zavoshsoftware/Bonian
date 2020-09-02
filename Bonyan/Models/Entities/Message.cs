using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Models
{
    public class Message:BaseEntity
    {
        public string Subject { get; set; }
        public string Body { get; set; }
        public string AttachmentFileUrl { get; set; }
        public string Response { get; set; }
        public Guid UserId { get; set; }
        public virtual User User { get; set; }

        public Guid MessageStatusId { get; set; }
        public virtual MessageStatus MessageStatus { get; set; }

        internal class Configuration : EntityTypeConfiguration<Message>
        {
            public Configuration()
            {
                HasRequired(p => p.MessageStatus).WithMany(j => j.Messages).HasForeignKey(p => p.MessageStatusId);
                HasRequired(p => p.User).WithMany(j => j.Messages).HasForeignKey(p => p.UserId);
            }
        }
    }
}