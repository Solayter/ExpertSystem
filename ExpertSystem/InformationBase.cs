using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertSystem
{
    public class InformationBase
    {
        public Variable[] variables;
        public Rule[] rules;
        
        public InformationBase()
        {
            variables = new Variable[1];
            rules = new Rule[1];
        }
        public void AddVariable(string name, float min, float max, FuzzyLabel[] fuzzyLabels)
        {
            variables[variables.Length - 1] = new Variable(name, min, max, fuzzyLabels);
            Array.Resize(ref variables, variables.Length + 1);
        }
        public void AddRule(string name, string value)
        {
            rules[rules.Length - 1] = new Rule(name, value);
            Array.Resize(ref rules, rules.Length + 1);
        }
        public string[] GetVariablesList()
        {
            string[] vars = new string[variables.Length];
            for (int i = 0; i < variables.Length - 1; i++)
                vars[i] = variables[i].Name;
            return vars;
        }
        public string[] GetRulesList()
        {
            string[] vars = new string[rules.Length];
            for (int i = 0; i < rules.Length - 1; i++)
                vars[i] = rules[i].Name;
            return vars;
        }
        public Variable GetVariable(string name)
        {
            foreach(Variable var in variables)
            {
                if (var.Name == name)
                    return var;
            }
            return null;
        }
        public Rule GetRule(string name)
        {
            foreach (Rule var in rules)
            {
                if (var.Name == name)
                    return var;
            }
            return null;
        }

        public void DeleteVariable(string name)
        {
            for (int i = 0; i < variables.Length; i++)
            {
                if (variables[i].Name == name)
                {
                    variables[i] = null;
                    break;
                }
            }
            variables = variables.Where(x => x != null).ToArray();
            Array.Resize(ref variables, variables.Length + 1);
        }

        public void DeleteRule(string name)
        {
            for (int i = 0; i < rules.Length; i++)
            {
                if (rules[i].Name == name)
                {
                    rules[i] = null;
                    break;
                }
            }
            rules = rules.Where(x => x != null).ToArray();
            Array.Resize(ref rules, rules.Length + 1);
        }

    }
}
