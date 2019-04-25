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
            AddTestData();
            
        }

        public string Calc(float f1, float f2, float f3)
        {
            inferenceSystem = databaseCreator.Create(informationBase);
            inferenceSystem.SetInput("Сорт", f1);
            inferenceSystem.SetInput("Удобрений", f2);
            inferenceSystem.SetInput("Осадков", f3);
            return inferenceSystem.Evaluate("Урожайность").ToString();
        }
        public string Calc2(float f1, float f2, float f3)
        {
            inferenceSystem = databaseCreator.Create(informationBase);
            inferenceSystem.SetInput("Возраст", f1);
            inferenceSystem.SetInput("Стенокардия", f2);
            inferenceSystem.SetInput("Пол", f3);
            return inferenceSystem.Evaluate("Вероятность_ИБС").ToString();
        }
        public void AddVariable(string name, float min, float max, FuzzyLabel[] fuzzyLabels)
        {
            informationBase.AddVariable(name, min, max, fuzzyLabels);
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



        public void AddTestData()
        {
            informationBase.AddVariable("Сорт", 1, 3, new FuzzyLabel[] { new FuzzyLabel("Первый", 5, new float[] { 1 }), new FuzzyLabel("Второй", 5, new float[] { 2 }), new FuzzyLabel("Третий", 5, new float[] { 3 }) });
            informationBase.AddVariable("Удобрений", 0, 100, new FuzzyLabel[] { new FuzzyLabel("Мало", 1, new float[] { 0, 100 }), new FuzzyLabel("Много", 2, new float[] { 0, 100 }) });
            informationBase.AddVariable("Осадков", 0, 50, new FuzzyLabel[] { new FuzzyLabel("Мало", 1, new float[] { 0, 50 }), new FuzzyLabel("Много", 2, new float[] { 0, 50 }) });
            informationBase.AddVariable("Урожайность", 100, 300, new FuzzyLabel[] { new FuzzyLabel("Низкая", 1, new float[] { 100, 300 }), new FuzzyLabel("Средняя", 3, new float[] { 100, 200, 300 }), new FuzzyLabel("Высокая", 2, new float[] { 100, 300 }) });


            informationBase.AddRule("Правило 1", "IF Сорт IS Первый AND Удобрений IS Много AND Осадков IS Много THEN Урожайность IS Высокая");
            informationBase.AddRule("Правило 2", "IF Сорт IS Первый AND Удобрений IS Много AND Осадков IS Мало THEN Урожайность IS Средняя");
            informationBase.AddRule("Правило 3", "IF Сорт IS Второй AND Удобрений IS Много AND Осадков IS Много THEN Урожайность IS Низкая");
            informationBase.AddRule("Правило 4", "IF Сорт IS Второй AND Удобрений IS Мало AND Осадков IS Мало THEN Урожайность IS Средняя");
            informationBase.AddRule("Правило 5", "IF Сорт IS Третий AND Удобрений IS Мало AND Осадков IS Мало THEN Урожайность IS Высокая");
            informationBase.AddRule("Правило 0", "IF Сорт IS Первый AND Удобрений IS Мало AND Осадков IS Мало THEN Урожайность IS Низкая");

            informationBase.AddVariable("Возраст", 29, 70, new FuzzyLabel[] {
                new FuzzyLabel("30-39", 3, new float[] { 29, 35, 40}),
                new FuzzyLabel("40-49", 3, new float[] { 39, 45, 50}),
                new FuzzyLabel("50-59", 3, new float[] { 49, 55, 60}),
                new FuzzyLabel("60-69", 3, new float[] { 59, 65, 70}) });


            informationBase.AddVariable("Стенокардия", 1, 4, new FuzzyLabel[] {
                new FuzzyLabel("Типичная_стенокардия", 5, new float[] { 1 }),
                new FuzzyLabel("Атипичная_стенокардия", 5, new float[] { 2 }),
                new FuzzyLabel("Нестенокардическая_боль", 5, new float[] { 3 }),
                new FuzzyLabel("Нет_боли", 5, new float[] { 4 }) });

            informationBase.AddVariable("Пол", 1, 2, new FuzzyLabel[] {
                new FuzzyLabel("Мужской", 5, new float[] { 1 }),
                new FuzzyLabel("Женский", 5, new float[] { 2 }) });
            
            informationBase.AddVariable("Вероятность_ИБС", 0, 100, new FuzzyLabel[] {
                new FuzzyLabel("Очень_низкая", 1, new float[] { 0, 5}),
                new FuzzyLabel("Низкая", 3, new float[] { 5, 7.5f, 10}),
                new FuzzyLabel("Средняя", 3, new float[] { 10, 50, 90}),
                new FuzzyLabel("Высокая", 2, new float[] { 90, 100}) });

            informationBase.AddRule("Правило 6", "IF Возраст IS 30-39 AND Стенокардия IS Типичная_стенокардия AND Пол IS Мужской THEN Вероятность_ИБС IS Средняя");
            informationBase.AddRule("Правило 35", "IF Возраст IS 30-39 AND Стенокардия IS Типичная_стенокардия AND Пол IS Женский THEN Вероятность_ИБС IS Средняя");
            informationBase.AddRule("Правило 7", "IF Возраст IS 30-39 AND Стенокардия IS Атипичная_стенокардия AND Пол IS Мужской THEN Вероятность_ИБС IS Средняя");
            informationBase.AddRule("Правило 8", "IF Возраст IS 30-39 AND Стенокардия IS Атипичная_стенокардия AND Пол IS Женский THEN Вероятность_ИБС IS Очень_низкая");
            informationBase.AddRule("Правило 9", "IF Возраст IS 30-39 AND Стенокардия IS Нестенокардическая_боль AND Пол IS Мужской THEN Вероятность_ИБС IS Низкая");
            informationBase.AddRule("Правило 10", "IF Возраст IS 30-39 AND Стенокардия IS Нестенокардическая_боль AND Пол IS Женский THEN Вероятность_ИБС IS Очень_низкая");
            informationBase.AddRule("Правило 11", "IF Возраст IS 30-39 AND Стенокардия IS Нет_боли AND Пол IS Мужской THEN Вероятность_ИБС IS Очень_низкая");
            informationBase.AddRule("Правило 40", "IF Возраст IS 30-39 AND Стенокардия IS Нет_боли AND Пол IS Женский THEN Вероятность_ИБС IS Очень_низкая");
            informationBase.AddRule("Правило 12", "IF Возраст IS 40-49 AND Стенокардия IS Типичная_стенокардия AND Пол IS Мужской THEN Вероятность_ИБС IS Высокая");
            informationBase.AddRule("Правило 30", "IF Возраст IS 40-49 AND Стенокардия IS Типичная_стенокардия AND Пол IS Женский THEN Вероятность_ИБС IS Средняя");
            informationBase.AddRule("Правило 13", "IF Возраст IS 40-49 AND Стенокардия IS Атипичная_стенокардия AND Пол IS Мужской THEN Вероятность_ИБС IS Средняя");
            informationBase.AddRule("Правило 14", "IF Возраст IS 40-49 AND Стенокардия IS Атипичная_стенокардия AND Пол IS Женский THEN Вероятность_ИБС IS Низкая");
            informationBase.AddRule("Правило 15", "IF Возраст IS 40-49 AND Стенокардия IS Нестенокардическая_боль AND Пол IS Мужской THEN Вероятность_ИБС IS Средняя");
            informationBase.AddRule("Правило 16", "IF Возраст IS 40-49 AND Стенокардия IS Нестенокардическая_боль AND Пол IS Женский THEN Вероятность_ИБС IS Очень_низкая");
            informationBase.AddRule("Правило 17", "IF Возраст IS 40-49 AND Стенокардия IS Нет_боли AND Пол IS Мужской THEN Вероятность_ИБС IS Низкая");
            informationBase.AddRule("Правило 31", "IF Возраст IS 40-49 AND Стенокардия IS Нет_боли AND Пол IS Женский THEN Вероятность_ИБС IS Очень_низкая");
            informationBase.AddRule("Правило 18", "IF Возраст IS 50-59 AND Стенокардия IS Типичная_стенокардия AND Пол IS Мужской THEN Вероятность_ИБС IS Высокая");
            informationBase.AddRule("Правило 32", "IF Возраст IS 50-59 AND Стенокардия IS Типичная_стенокардия AND Пол IS Женский THEN Вероятность_ИБС IS Средняя");
            informationBase.AddRule("Правило 19", "IF Возраст IS 50-59 AND Стенокардия IS Атипичная_стенокардия AND Пол IS Мужской THEN Вероятность_ИБС IS Средняя");
            informationBase.AddRule("Правило 20", "IF Возраст IS 50-59 AND Стенокардия IS Атипичная_стенокардия AND Пол IS Женский THEN Вероятность_ИБС IS Средняя");
            informationBase.AddRule("Правило 21", "IF Возраст IS 50-59 AND Стенокардия IS Нестенокардическая_боль AND Пол IS Мужской THEN Вероятность_ИБС IS Средняя");
            informationBase.AddRule("Правило 22", "IF Возраст IS 50-59 AND Стенокардия IS Нестенокардическая_боль AND Пол IS Женский THEN Вероятность_ИБС IS Низкая");
            informationBase.AddRule("Правило 23", "IF Возраст IS 50-59 AND Стенокардия IS Нет_боли AND Пол IS Мужской THEN Вероятность_ИБС IS Низкая");
            informationBase.AddRule("Правило 33", "IF Возраст IS 50-59 AND Стенокардия IS Нет_боли AND Пол IS Женский THEN Вероятность_ИБС IS Очень_низкая");
            informationBase.AddRule("Правило 24", "IF Возраст IS 60-69 AND Стенокардия IS Типичная_стенокардия AND Пол IS Мужской THEN Вероятность_ИБС IS Высокая");
            informationBase.AddRule("Правило 36", "IF Возраст IS 60-69 AND Стенокардия IS Типичная_стенокардия AND Пол IS Женский THEN Вероятность_ИБС IS Высокая");
            informationBase.AddRule("Правило 25", "IF Возраст IS 60-69 AND Стенокардия IS Атипичная_стенокардия AND Пол IS Мужской THEN Вероятность_ИБС IS Средняя");
            informationBase.AddRule("Правило 37", "IF Возраст IS 60-69 AND Стенокардия IS Атипичная_стенокардия AND  Пол IS Женский THEN Вероятность_ИБС IS Средняя");
            informationBase.AddRule("Правило 27", "IF Возраст IS 60-69 AND Стенокардия IS Нестенокардическая_боль AND Пол IS Мужской THEN Вероятность_ИБС IS Средняя");
            informationBase.AddRule("Правило 38", "IF Возраст IS 60-69 AND Стенокардия IS Нестенокардическая_боль AND Пол IS Женский THEN Вероятность_ИБС IS Средняя");
            informationBase.AddRule("Правило 29", "IF Возраст IS 60-69 AND Стенокардия IS Нет_боли AND Пол IS Мужской THEN Вероятность_ИБС IS Низкая");
            informationBase.AddRule("Правило 39", "IF Возраст IS 60-69 AND Стенокардия IS Нет_боли AND Пол IS Женский THEN Вероятность_ИБС IS Низкая");




            string sas = JsonConvert.SerializeObject(informationBase);
            InformationBase if2 = JsonConvert.DeserializeObject<InformationBase>(sas);
        }
    }
}
