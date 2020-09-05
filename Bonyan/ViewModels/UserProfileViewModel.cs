using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ViewModels
{
    public class UserProfileViewModel
    {
        public User User { get; set; }
        public List<UserProductsLike> UserProductsLikes { get; set; }
    }
}