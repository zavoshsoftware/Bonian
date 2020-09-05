using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ProductGroup:BaseEntity
    {
        public ProductGroup()
        {
            Products = new List<Product>();
        }
        [Display(Name = "گروه محصول")]
        public string Title { get; set; }
        [Display(Name = "گروه محصول انگلیسی")]
        public string TitleEn { get; set; }
         
        public virtual ICollection<Product> Products { get; set; }

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
