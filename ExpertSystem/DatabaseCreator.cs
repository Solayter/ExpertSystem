using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AForge.Fuzzy;

namespace ExpertSystem
{
    public class DatabaseCreator
    {
        
        private Database database;
        private InferenceSystem inferenceSystem;
        public InferenceSystem Create(InformationBase informationBase)
        {
            database = new Database();
            
            AddVariables(informationBase.variables);
            inferenceSystem = new InferenceSystem(database, new CentroidDefuzzifier(1000));
            AddRules(informationBase.rules);
            return inferenceSystem;
        }
        public void AddVariables(Variable[] vars)
        {
            LinguisticVariable lv;
            foreach (Variable var in vars)
            {
                if (var != null)
                {
                    lv = new LinguisticVariable(var.Name, var.Min, var.Max);
                    AddFuzzyLabels(ref lv, var.FuzzyLabels);
                    database.AddVariable(lv);
                }
            }
        }
        public void AddFuzzyLabels(ref LinguisticVariable lv, FuzzyLabel[] fuzzyLabels)
        {
            foreach(FuzzyLabel fl in fuzzyLabels)
            {
                if (fl != null)
                {
                    switch (fl.Type)
                    {
                        case 1:
                            lv.AddLabel(new FuzzySet(fl.Name, new TrapezoidalFunction(fl.Numbers[0], fl.Numbers[1], TrapezoidalFunction.EdgeType.Right)));
                            break;
                        case 2:
                            lv.AddLabel(new FuzzySet(fl.Name, new TrapezoidalFunction(fl.Numbers[0], fl.Numbers[1], TrapezoidalFunction.EdgeType.Left)));
                            break;
                        case 3:
                            lv.AddLabel(new FuzzySet(fl.Name, new TrapezoidalFunction(fl.Numbers[0], fl.Numbers[1], fl.Numbers[2])));
                            break;
                        case 4:
                            lv.AddLabel(new FuzzySet(fl.Name, new TrapezoidalFunction(fl.Numbers[0], fl.Numbers[1], fl.Numbers[2], fl.Numbers[3])));
                            break;
                        case 5:
                            lv.AddLabel(new FuzzySet(fl.Name, new SingletonFunction(fl.Numbers[0])));
                            break;
                        default:
                            break;
                    }
                }
            }
        }
        public void AddRules(Rule[] rules)
        {
            foreach(Rule var in rules)
            {
                if (var != null)
                {
                    inferenceSystem.NewRule(var.Name, var.Value);
                }
            }
        }
    }
}
