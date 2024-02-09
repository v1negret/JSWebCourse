﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSWebCourse.Models
{
    public class Unit
    {
        public int UnitId {  get; set; }
        public string Title {  get; set; }
        public string Description { get; set; }
        public string HtmlCode {  get; set; }
        public int ChapterId { get; set; }
        public Chapter? Chapter { get; set; }
    }
}
