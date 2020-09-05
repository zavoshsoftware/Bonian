using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Models
{
    public class Artist:BaseEntity
    {
        public Artist()
        {
            Products=new List<Product>();
        }

        [Display(Name="نام هنرمند")]
        public string FullName { get; set; }

        [Display(Name="رزومه خلاصه")]
        [DataType(DataType.MultilineText)]
        public string Summery { get; set; }

        [Display(Name="رزومه کامل هنرمند")]
        [DataType(DataType.Html)]
        [AllowHtml]
        [Column(TypeName = "ntext")]
        [UIHint("RichText")]
        public string About { get; set; }

        [Display(Name="نام هنرمند انگلیسی")]
        public string FullNameEn { get; set; }

        [Display(Name= "رزومه خلاصه انگلیسی")]
        [DataType(DataType.MultilineText)]
        public string SummeryEn { get; set; }

        [Display(Name= "رزومه کامل هنرمند انگلیسی")]
        [DataType(DataType.Html)]
        [AllowHtml]
        [Column(TypeName = "ntext")]
        [UIHint("RichText")]
        public string AboutEn { get; set; }

        public virtual ICollection<Product> Products { get; set; }


        Helpers.GetCulture oGetCulture = new Helpers.GetCulture();

        [NotMapped]
        public string FullNameSrt
        {
            get
            {
                string currentCulture = oGetCulture.CurrentLang();
                switch (currentCulture.ToLower())
                {
                    case "en-us":
                        return this.FullNameEn;
                    case "fa-ir":
                        return this.FullName;
                    default:
                        return String.Empty;
                }
            }
        }

        [NotMapped]
        public string SummerySrt
        {
            get
            {
                string currentCulture = oGetCulture.CurrentLang();
                switch (currentCulture.ToLower())
                {
                    case "en-us":
                        return this.SummeryEn;
                    case "fa-ir":
                        return this.Summery;
                    default:
                        return String.Empty;
                }
            }
        }

        [NotMapped]
        public string AboutSrt
        {
            get
            {
                string currentCulture = oGetCulture.CurrentLang();
                switch (currentCulture.ToLower())
                {
                    case "en-us":
                        return this.AboutEn;
                    case "fa-ir":
                        return this.About;
                    default:
                        return String.Empty;
                }
            }
        }
    }
}