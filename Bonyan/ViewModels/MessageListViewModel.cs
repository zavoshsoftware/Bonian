using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Models;

namespace  ViewModels
{
    public class MessageListViewModel
    {
        public List<Message> Messages { get; set; }
    }
}