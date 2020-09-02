using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class MessageStatus : BaseEntity
    {
        public MessageStatus()
        {
            Messages=new List<Message>();
        }
        public int Order { get; set; }
        public string Title { get; set; }
        public string TitleEn { get; set; }

        public virtual ICollection<Message> Messages { get; set; }
    }
}