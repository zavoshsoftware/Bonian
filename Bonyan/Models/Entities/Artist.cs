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
    }
}