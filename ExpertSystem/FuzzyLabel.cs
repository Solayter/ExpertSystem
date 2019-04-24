using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertSystem
{
    public class FuzzyLabel
    {
        public string Name { get; set; }
        public int Type { get; set; } //1 - две цифры и линия справа ; 2 - две цифры и линия слева; 3 - треугольник; 4 - трапеция, 5 - одиночная;
        public float[] Numbers { get; set; }

        public FuzzyLabel(string name, int type, float[] numbers)
        {
            Name = name;
            Type = type;
            Numbers = numbers;
        }
    }
    
}
