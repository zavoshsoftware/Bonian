namespace Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public  class Gender : BaseEntity
    {
        public Gender()
        {
            Users = new List<User>();
        }


        [Required]
        [StringLength(10)]
        [Display(Name = "جنسیت")]
        public string Title { get; set; }
        public string TitleEn { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
