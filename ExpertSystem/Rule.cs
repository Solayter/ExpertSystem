using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertSystem
{
    public class Rule
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public Rule(string name, string value)
        {
            Name = name;
            Value = value;
        }
    }
}
