using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertSystem
{
    public class Variable
    {
        public string Name { get; set; }
        public float Min { get; set; }
        public float Max { get; set; }
        public FuzzyLabel[] FuzzyLabels { get; set; }
        public string Units { get; set; }

        public Variable(string name, float min, float max, FuzzyLabel[] fuzzyLabels, string units)
        {
            Name = name;
            Min = min;
            Max = max;
            FuzzyLabels = fuzzyLabels;
            Units = units;
        }

        public string[] GetLabelsList()
        {
            string[] vars = new string[FuzzyLabels.Length];
            for (int i = 0; i < FuzzyLabels.Length; i++)
                vars[i] = FuzzyLabels[i].Name;
            return vars;
        }
        public FuzzyLabel GetFuzzyLabel(string name)
        {
            foreach (FuzzyLabel var in FuzzyLabels)
                if (var.Name == name)
                    return var;
            return null;
        }
    }
}
