using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Models
{
    public class Notification:BaseEntity
    {
        [Display(Name="عنوان")]
        public string Title { get; set; }

        [Display(Name="متن پیام")]
        [DataType(DataType.MultilineText)]
        public string Message { get; set; }
        [Display(Name="انتخاب کاربر")]
        public Guid? UserId { get; set; }
        public virtual User User { get; set; }

        [Display(Name="مشاهده شده؟")]
        public bool Seen { get; set; }


        internal class configuration : EntityTypeConfiguration<Notification>
        {
            public configuration()
            {
                HasRequired(p => p.User).WithMany(j => j.Notifications).HasForeignKey(p => p.UserId);
            }
        }
    }
}