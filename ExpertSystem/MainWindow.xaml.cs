using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AForge.Fuzzy;

namespace ExpertSystem
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Logic logics;
        Variable selectedVar;
        Rule selectedRule;
        public MainWindow()
        {
            InitializeComponent();
            logics = new Logic();
        }

        private void Btn1_Click(object sender, RoutedEventArgs e)
        {
            // linguistic labels (fuzzy sets) that compose the distances
            FuzzySet fsMalo = new FuzzySet("Мало",
                new TrapezoidalFunction(0, 50, TrapezoidalFunction.EdgeType.Right));
             FuzzySet fsMnogo = new FuzzySet("Много",
                new TrapezoidalFunction(50, 101, TrapezoidalFunction.EdgeType.Left));
            // front distance (input)
            LinguisticVariable lvUdobren = new LinguisticVariable("Удобрения", 0, 101);
            lvUdobren.AddLabel(fsMalo);
            lvUdobren.AddLabel(fsMnogo);
            
            // linguistic labels (fuzzy sets) that compose the distances
            FuzzySet fsHolodno = new FuzzySet("Холодно",
                new TrapezoidalFunction(4, 10, 15, 20));
            FuzzySet fsTeplo = new FuzzySet("Тепло",
                new TrapezoidalFunction(15, 31, TrapezoidalFunction.EdgeType.Left));

            // front distance (input)
            LinguisticVariable lvTemp = new LinguisticVariable("Температура", 4, 31);
            lvTemp.AddLabel(fsHolodno);
            lvTemp.AddLabel(fsTeplo);

            // linguistic labels (fuzzy sets) that compose the distances
            FuzzySet fsMalo1 = new FuzzySet("Малая",
                new TrapezoidalFunction(0, 50, TrapezoidalFunction.EdgeType.Right));
            FuzzySet fsSredne1 = new FuzzySet("Средняя",
                new TrapezoidalFunction(40, 55, 75));
            FuzzySet fsMnogo1 = new FuzzySet("Большая",
                new TrapezoidalFunction(70, 100, TrapezoidalFunction.EdgeType.Left));

            // front distance (input)
            LinguisticVariable lvUrozh = new LinguisticVariable("Урожайность", 0, 100);
            lvUrozh.AddLabel(fsMalo1);
            lvUrozh.AddLabel(fsSredne1);
            lvUrozh.AddLabel(fsMnogo1);

            FuzzySet fsPlus = new FuzzySet("Нет", new SingletonFunction(0));
            FuzzySet fsMinus = new FuzzySet("Есть", new SingletonFunction(1));

            LinguisticVariable check = new LinguisticVariable("Чек", 0, 1);

            check.AddLabel(fsPlus);
            check.AddLabel(fsMinus);

            
            
            // the database
            Database fuzzyDB = new Database();
            fuzzyDB.AddVariable(lvUdobren);
            fuzzyDB.AddVariable(lvTemp);
            fuzzyDB.AddVariable(lvUrozh);
            fuzzyDB.AddVariable(check);
            
            // creating the inference system
            InferenceSystem IS = new InferenceSystem(fuzzyDB, new CentroidDefuzzifier(1000));

            // going straight
            IS.NewRule("Rule 1", "IF Удобрения IS Много AND Температура IS Тепло THEN Урожайность IS Большая");
            // turning left
            IS.NewRule("Rule 2", "IF Удобрения IS Мало AND Температура IS Холодно THEN Урожайность IS Малая");

            IS.NewRule("Rule 3", "IF Удобрения IS Мало AND Температура IS Тепло THEN Урожайность IS Средняя");
            
            IS.NewRule("Rule 4", "IF Удобрения IS Много AND Температура IS Холодно THEN Урожайность IS Средняя");

            IS.NewRule("Rule 5", "IF Чек IS Есть THEN Урожайность IS Большая");
            // inference section
            
            // setting inputs
            IS.SetInput("Удобрения", Convert.ToInt32(tb1.Text));
            IS.SetInput("Температура", Convert.ToInt32(tb2.Text));
            IS.SetInput("Чек", checkBox.IsChecked.Value ? 1 : 0);
            // getting outputs
            try
            {
                tb3.Text = IS.Evaluate("Урожайность").ToString();
            }
            catch (Exception)
            {
               
            }

        }

        private void Btn2_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void BtnAddVariable_Click(object sender, RoutedEventArgs e)
        {
            listVariables.ItemsSource = logics.GetVariablesList();
        }

        private void ListVariables_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedVar = logics.GetVariable(listVariables.SelectedItem.ToString());
            
            if (selectedVar != null)
            {
                textBlockVariableName.Text = selectedVar.Name;
                textBlockVariableMin.Text = selectedVar.Min.ToString();
                textBlockVariableMax.Text = selectedVar.Max.ToString();
                listFuzzyLabels.ItemsSource = selectedVar.GetLabelsList();
            }
        }

        private void ListFuzzyLabels_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((selectedVar != null)&(listFuzzyLabels.SelectedItem != null))
            {
                FuzzyLabel selectedLabel = selectedVar.GetFuzzyLabel(listFuzzyLabels.SelectedItem.ToString());
                if(selectedLabel != null)
                {
                    textBlockLabelName.Text = selectedLabel.Name;
                    switch (selectedLabel.Type)
                    {
                        case 1:
                            textBlockLabelType.Text = "Начальная";
                            break;
                        case 2:
                            textBlockLabelType.Text = "Конечная";
                            break;
                        case 3:
                            textBlockLabelType.Text = "Треугольник";
                            break;
                        case 4:
                            textBlockLabelType.Text = "Трапеция";
                            break;
                        case 5:
                            textBlockLabelType.Text = "Одиночная";
                            break;
                        default:
                            break;
                    }
                    listLabelsItems.ItemsSource = selectedLabel.Numbers;
                }
            }
        }

        private void BtnAddRule_Click(object sender, RoutedEventArgs e)
        {
            listRules.ItemsSource = logics.GetRulesList();
        }

        private void ListRules_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedRule = logics.GetRule(listRules.SelectedItem.ToString());
            textBlockRuleValue.TextWrapping = TextWrapping.Wrap;
            textBlockRuleValue.Text = selectedRule.Value;
            textBlockRuleName.Text = selectedRule.Name;


        }

        private void BtnCalc_Click(object sender, RoutedEventArgs e)
        {
            textBlockOutput.Text = logics.Calc(Convert.ToInt32(textBoxInput1.Text), Convert.ToInt32(textBoxInput2.Text), Convert.ToInt32(textBoxInput3.Text));
        }

        private void BtnCalc1_Click(object sender, RoutedEventArgs e)
        {
            textBlockOutput1.Text = logics.Calc2(Convert.ToInt32(textBoxInput11.Text), Convert.ToInt32(textBoxInput22.Text), Convert.ToInt32(textBoxInput33.Text));
        }
    }
}
