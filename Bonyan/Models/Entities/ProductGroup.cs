using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    }
}
