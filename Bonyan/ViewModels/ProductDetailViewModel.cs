using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ViewModels
{
    public class ProductDetailViewModel
    {
        public Product Product { get; set; }
        public bool IsLike { get; set; }

        public string SizeInInch {
            get
            {
                if (Product.Width != null && Product.Height != null)
                {
                    return (Product.Width.Value * 0.39) + "x" + (Product.Height.Value * 0.39)+" inch";
                }
                else
                {
                    return string.Empty;
                }
            }
        }
    }
}