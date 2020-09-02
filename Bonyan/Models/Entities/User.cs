
using System.Data.Entity.ModelConfiguration;
using Resources;

namespace Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class User : BaseEntity
    {
        public User()
        { 
            Orders=new List<Order>();
            ActivationCodes = new List<ActivationCode>();
            UserProductsLikes = new List<UserProductsLike>();
            Messages = new List<Message>();
        }


  

        [Display(Name = "CellNum", ResourceType = typeof(Resources.Models.User))]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        [StringLength(20, ErrorMessage = "طول {0} نباید بیشتر از {1} باشد")]
        public string CellNum { get; set; }

        [Display(Name = "Password", ResourceType = typeof(Resources.Models.User))]
        [StringLength(150, ErrorMessage = "طول {0} نباید بیشتر از {1} باشد")]
        public string Password { get; set; }

        [Display(Name = "FullName", ResourceType = typeof(Resources.Models.User))]
        [StringLength(250, ErrorMessage = "طول {0} نباید بیشتر از {1} باشد")]
        public string FullName { get; set; }

        [Display(Name = "ایمیل")]
        [StringLength(300, ErrorMessage = "طول {0} نباید بیشتر از {1} باشد")]
        public string Email { get; set; }

        [Display(Name = "Code", ResourceType = typeof(Resources.Models.User))]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        public int Code { get; set; }


        [Display(Name = "تاریخ تولد")]
        public DateTime? BirthDate { get; set; }

        public Guid RoleId { get; set; }

        public virtual Role Role { get; set; }
        public Guid? GenderId { get; set; }

        public virtual Gender Gender { get; set; }
 
   
        public virtual ICollection<Order> Orders { get; set; }
 
        public virtual ICollection<ActivationCode> ActivationCodes { get; set; }
        public virtual ICollection<UserProductsLike> UserProductsLikes { get; set; }
        public virtual ICollection<Message> Messages { get; set; }

        internal class configuration : EntityTypeConfiguration<User>
        {
            public configuration()
            {
                HasRequired(p => p.Role).WithMany(j => j.Users).HasForeignKey(p => p.RoleId);
                HasOptional(p => p.Gender).WithMany(j => j.Users).HasForeignKey(p => p.GenderId);
            }
        }
    }
}

