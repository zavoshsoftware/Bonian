using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

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
        public string Summery { get; set; }

        [Display(Name="رزومه کامل هنرمند")]
        public string About { get; set; }

        [Display(Name="نام هنرمند انگلیسی")]
        public string FullNameEn { get; set; }

        [Display(Name= "رزومه خلاصه انگلیسی")]
        public string SummeryEn { get; set; }

        [Display(Name= "رزومه کامل هنرمند انگلیسی")]
        public string AboutEn { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}