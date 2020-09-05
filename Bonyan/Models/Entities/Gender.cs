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

        Helpers.GetCulture oGetCulture = new Helpers.GetCulture();

        [NotMapped]
        public string TitleSrt
        {
            get
            {
                string currentCulture = oGetCulture.CurrentLang();
                switch (currentCulture.ToLower())
                {
                    case "en-us":
                        return this.TitleEn;
                    case "fa-ir":
                        return this.Title;
                    default:
                        return String.Empty;
                }
            }
        }

    }
}
