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
            
            listVariables.ItemsSource = logics.GetVariablesList();
            listRules.ItemsSource = logics.GetRulesList();
        }


        private void BtnAddVariable_Click(object sender, RoutedEventArgs e)
        {
            WindowAddVar windowAddVar = new WindowAddVar(logics.GetVariablesList());
            if (windowAddVar.ShowDialog() != true)
            {
                if (windowAddVar.ok)
                {
                    logics.AddVariable(windowAddVar.name, windowAddVar.min, windowAddVar.max, windowAddVar.fuzzyLabels.ToArray());
                    listVariables.ItemsSource = logics.GetVariablesList();
                }
            }
        }

        private void ListVariables_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listVariables.SelectedItem != null)
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
        }

        private void ListFuzzyLabels_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((selectedVar != null) & (listFuzzyLabels.SelectedItem != null))
            {
                FuzzyLabel selectedLabel = selectedVar.GetFuzzyLabel(listFuzzyLabels.SelectedItem.ToString());
                if (selectedLabel != null)
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
            WindowAddRule windowAddRule = new WindowAddRule(logics.GetVariables());
            if (windowAddRule.ShowDialog() != true)
            {
                if (windowAddRule.ok)
                {
                    logics.AddRule(windowAddRule.name, windowAddRule.value);
                    listRules.ItemsSource = logics.GetRulesList();
                }
            }
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

        private void BtnDeleteVariable_Click(object sender, RoutedEventArgs e)
        {
            if (listVariables.SelectedIndex != -1)
                if (MessageBox.Show("Вы уверены что хотите удалить переменную " + listVariables.SelectedItem.ToString() + "?", "Удаление переменной", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    logics.DeleteVariable(listVariables.SelectedItem.ToString());
                    listVariables.ItemsSource = logics.GetVariablesList();
                }
        }

        private void BtnUpdateVariable_Click(object sender, RoutedEventArgs e)
        {
            WindowAddVar windowAddVar = new WindowAddVar(logics.GetVariablesList(), selectedVar.Name, selectedVar.Min, selectedVar.Max, selectedVar.FuzzyLabels);
            if (windowAddVar.ShowDialog() != true)
            {
                if (windowAddVar.ok)
                {
                    if (MessageBox.Show("Вы уверены что хотите изменить переменную " + listVariables.SelectedItem.ToString() + "?", "Изменение переменной", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        logics.DeleteVariable(listVariables.SelectedItem.ToString());
                        logics.AddVariable(windowAddVar.name, windowAddVar.min, windowAddVar.max, windowAddVar.fuzzyLabels.ToArray());
                        listVariables.ItemsSource = logics.GetVariablesList();
                    }
                }
            }
        }
    }
}
