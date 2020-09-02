using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Models
{
    public class UserProductsLike:BaseEntity
    {
        public Guid UserId { get; set; }
        public virtual User User { get; set; }

        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }


        internal class Configuration : EntityTypeConfiguration<UserProductsLike>
        {
            public Configuration()
            {
                HasRequired(p => p.Product).WithMany(j => j.UserProductsLikes).HasForeignKey(p => p.ProductId);
                HasRequired(p => p.User).WithMany(j => j.UserProductsLikes).HasForeignKey(p => p.UserId);
            }
        }
    }
}