﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cookbook.Models
{
    public class Recipe
    {
        public int RecipeId { get; set; }
        public string Name { get; set; }

        public string Ingredients { get; set; }
        public string Steps { get; set; }
    }
}