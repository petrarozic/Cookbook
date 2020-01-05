using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cookbook.Models
{
    public class Step
    {
        public int StepId { get; set; }
        public int Order { get; set; }
        public string Description { get; set; }

        public Recipe Recipe { get; set; }
    }
}
