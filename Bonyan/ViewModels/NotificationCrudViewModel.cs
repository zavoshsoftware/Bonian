using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ViewModels
{
    public class NotificationCrudViewModel
    {
           [Display(Name="عنوان")]
        public string Title { get; set; }

        [Display(Name="متن پیام")]
        [DataType(DataType.MultilineText)]
        public string Message { get; set; }

        [Display(Name="یادداشت")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [Display(Name="انتخاب کاربر")]
        public Guid? UserId { get; set; }

        [Display(Name="مشاهده شده؟")]
        public bool Seen { get; set; }
        [Display(Name="ارسال برای همه کاربران")]
        public bool SendToAll { get; set; }
    }
}