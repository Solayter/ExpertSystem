using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AForge.Fuzzy;
using Newtonsoft.Json;

namespace ExpertSystem
{
    public class Logic
    {
        private InformationBase informationBase;
        private DatabaseCreator databaseCreator;
        private InferenceSystem inferenceSystem;
        public Logic()
        {
            informationBase = new InformationBase();
            databaseCreator = new DatabaseCreator();
        }

        public string[] Calculate(string[] varsNames, float[] varsValues, string findName)
        {
            string[] rezult = new string[2];
            inferenceSystem = databaseCreator.Create(informationBase);

            for (int i = 0; i < varsNames.Length; i++)
            {
                inferenceSystem.SetInput(varsNames[i], varsValues[i]);
            }
            try
            {
                rezult[0] = inferenceSystem.Evaluate(findName).ToString();
            }
            catch(Exception ex)
            {
                rezult[1] = ex.Message;
            }
            return rezult;
        }

        public void OpenFile(string file)
        {
            informationBase = JsonConvert.DeserializeObject<InformationBase>(file);
        }

        public string SaveFile()
        {
            return JsonConvert.SerializeObject(informationBase);
        }

        public List<string> GetListForGrid()
        {
            List<string> list = new List<string>();
            foreach (Variable var in informationBase.variables)
                if(var != null)
                    list.Add(var.Name + " | "+ var.Min + " — " + var.Max + " " + var.Units);
            return list;
        }
        public void AddVariable(string name, float min, float max, FuzzyLabel[] fuzzyLabels, string units)
        {
            informationBase.AddVariable(name, min, max, fuzzyLabels, units);
        }

        public void DeleteVariable(string name)
        {
            informationBase.DeleteVariable(name);
        }

        public string[] GetVariablesList()
        {
            return informationBase.GetVariablesList();
        }

        public Variable GetVariable(string name)
        {
            return informationBase.GetVariable(name);
        }

        public Variable[] GetVariables()
        {
            return informationBase.variables;
        }

        public void AddRule(string name, string value)
        {
            informationBase.AddRule(name, value);
        }

        public void DeleteRule(string name)
        {
            informationBase.DeleteRule(name);
        }

        public string[] GetRulesList()
        {
            return informationBase.GetRulesList();
        }

        public Rule GetRule(string name)
        {
            return informationBase.GetRule(name);
        }
    }
}
