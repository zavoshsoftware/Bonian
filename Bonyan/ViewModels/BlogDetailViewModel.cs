﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;

namespace ViewModels
{
    public class AboutViewModel
    {
        public List<Text> AboutTexts { get; set; }
        public List<Blog> Blogs { get; set; }
    }
}