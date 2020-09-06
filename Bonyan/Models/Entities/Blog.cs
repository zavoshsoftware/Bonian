using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Web.Mvc;

namespace Models
{
    public class Blog : BaseEntity
    {
        [Display(Name = "عنوان")]
        public string Title { get; set; }
        [Display(Name = "عنوان انگلیسی")]
        public string TitleEn { get; set; }

        [Display(Name = "کد")]
        public int Code { get; set; }

        [Display(Name = "اولویت")]
        public int Order { get; set; }

        [DataType(DataType.Html)]
        [AllowHtml]
        [Column(TypeName = "ntext")]
        [UIHint("RichText")]
        [Display(Name = "متن")]
        public string Body { get; set; }

        [DataType(DataType.Html)]
        [AllowHtml]
        [Column(TypeName = "ntext")]
        [UIHint("RichText")]
        [Display(Name = "متن انگلیسی")]
        public string BodyEn { get; set; }

        [Display(Name = "تصویر")]
        public string ImageUrl { get; set; }


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
        [NotMapped]
        public string BodySrt
        {
            get
            {
                string currentCulture = oGetCulture.CurrentLang();
                switch (currentCulture.ToLower())
                {
                    case "en-us":
                        return this.BodyEn;
                    case "fa-ir":
                        return this.Body;
                    default:
                        return String.Empty;
                }
            }
        }
    }
}
