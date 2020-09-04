using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Models
{
    public class Product : BaseEntity
    {
        public Product()
        {
            OrderDetails = new List<OrderDetail>();
            ProductImages = new List<ProductImage>();
            UserProductsLikes = new List<UserProductsLike>();
        }

        [Display(Name="اولویت نمایش")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        public int Order { get; set; }

        [Display(Name = "Code", ResourceType = typeof(Resources.Models.Product))]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        public int Code { get; set; }

        [Display(Name="هنرمند")]
        public Guid ArtistId { get; set; }
        public virtual Artist Artist { get; set; }

        [Display(Name = "ProductGroupId", ResourceType = typeof(Resources.Models.Product))]
        public Guid? ProductGroupId { get; set; }
        public virtual ProductGroup ProductGroup { get; set; }

        [Display(Name = "Amount", ResourceType = typeof(Resources.Models.Product))]
        [Column(TypeName = "Money")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        public decimal Amount { get; set; }

        [Display(Name = "ImageUrl", ResourceType = typeof(Resources.Models.Product))]
        public string ImageUrl { get; set; }

        [Display(Name="طول")]
        public int? Width { get; set; }

        [Display(Name="عرض")]
        public int? Height { get; set; }
         
        [Display(Name = "موجودی")]
        public int? Quantity { get; set; }

        [Display(Name="سال خلق اثر")]
        public int CreateYear { get; set; }

        [Display(Name = "تعداد لایک")]
        public int? LikeNumber { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<ProductImage> ProductImages { get; set; }
        public virtual ICollection<UserProductsLike> UserProductsLikes { get; set; }

        internal class configuration : EntityTypeConfiguration<Product>
        {
            public configuration()
            {
                HasOptional(p => p.ProductGroup).WithMany(t => t.Products).HasForeignKey(p => p.ProductGroupId);
                HasRequired(p => p.Artist).WithMany(t => t.Products).HasForeignKey(p => p.ArtistId);
           }
        }
         

        [NotMapped]
        [Display(Name = "Amount", ResourceType = typeof(Resources.Models.Order))]
        public string AmountStr
        {
            get { return Amount.ToString("N0"); }
        }
    }
}
