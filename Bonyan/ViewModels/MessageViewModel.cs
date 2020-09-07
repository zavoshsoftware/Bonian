using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace  ViewModels
{
    public class MessageViewModel
    {
        [Display(Name = "subject")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Subject { get; set; }

        [Display(Name = "Body")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DataType(DataType.MultilineText)]

        public string Body { get; set; }
         

    }
}