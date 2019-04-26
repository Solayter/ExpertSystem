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
using System.IO;
using AForge.Fuzzy;
using Microsoft.Win32;

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
        Variable selectedVarFind;
        List<string> selectedVars;
        List<string> allVars;
        string fileName = "";
        public MainWindow()
        {
            InitializeComponent();
            logics = new Logic();
            selectedVars = new List<string>();
            allVars = new List<string>();

            listVariables.ItemsSource = logics.GetVariablesList();
            listRules.ItemsSource = logics.GetRulesList();
            UpdateVariables();
        }


        private void UpdateVariables()
        {
            allVars = logics.GetListForGrid();
            listVarAll.ItemsSource = null;
            listVarAll.ItemsSource = allVars;
            listVarSelected.ItemsSource = null;
            selectedVars.Clear();
            comboBoxFind.ItemsSource = logics.GetVariablesList();
        }
        private void BtnAddVariable_Click(object sender, RoutedEventArgs e)
        {
            WindowAddVar windowAddVar = new WindowAddVar(logics.GetVariablesList());
            if (windowAddVar.ShowDialog() != true)
            {
                if (windowAddVar.ok)
                {
                    logics.AddVariable(windowAddVar.name, windowAddVar.min, windowAddVar.max, windowAddVar.fuzzyLabels.ToArray(),windowAddVar.units);
                    listVariables.ItemsSource = logics.GetVariablesList();
                    UpdateVariables();
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
                    textBoxVarUnit.Text = selectedVar.Units;

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
        
        private void BtnDeleteVariable_Click(object sender, RoutedEventArgs e)
        {
            if (listVariables.SelectedIndex != -1)
                if (MessageBox.Show("Вы уверены что хотите удалить переменную " + listVariables.SelectedItem.ToString() + "?", "Удаление переменной", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    logics.DeleteVariable(listVariables.SelectedItem.ToString());
                    listVariables.ItemsSource = logics.GetVariablesList();
                    UpdateVariables();
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
                        logics.AddVariable(windowAddVar.name, windowAddVar.min, windowAddVar.max, windowAddVar.fuzzyLabels.ToArray(),windowAddVar.units);
                        listVariables.ItemsSource = logics.GetVariablesList();
                        UpdateVariables();
                    }
                }
            }
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void BtnChoose_Click(object sender, RoutedEventArgs e)
        {
            if (listVarAll.SelectedIndex != -1)
            {
                float input;
                float min = (float)Convert.ToDouble(listVarAll.SelectedItem.ToString().Split(' ')[2]);
                float max = (float)Convert.ToDouble(listVarAll.SelectedItem.ToString().Split(' ')[4]);
                try
                {
                    input = (float)Convert.ToDouble(textBoxVarInput.Text);
                }
                catch
                {
                    MessageBox.Show("Значение переменной должны быть целым или дробным числом!");
                    return;
                }
                if((input > max)|(input < min))
                {
                    MessageBox.Show("Значение переменной не должно выходить за интервалы!");
                    return;
                }
                string sel = listVarAll.SelectedItem.ToString().Split(' ')[0] + " " + input.ToString();
                if (!selectedVars.Contains(sel))
                    selectedVars.Add(sel);
                listVarSelected.ItemsSource = null;
                listVarSelected.ItemsSource = selectedVars;

                allVars.Remove(listVarAll.SelectedItem.ToString());
                listVarAll.ItemsSource = null;
                listVarAll.ItemsSource = allVars;
                textBoxVarInput.Text = "";
            }
        }

        private void BtnReturn_Click(object sender, RoutedEventArgs e)
        {
            listVarAll.ItemsSource = null;
            listVarAll.ItemsSource = allVars = logics.GetListForGrid();
            listVarSelected.ItemsSource = null;
            selectedVars.Clear();
        }

        private void ComboBoxFind_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBoxFind.SelectedIndex != -1)
            {
                selectedVarFind = logics.GetVariable(comboBoxFind.SelectedItem.ToString());
                textBoxInterval.Text = selectedVarFind.Min + " — " + selectedVarFind.Max + " " + selectedVarFind.Units;
                textBoxRezultUnit.Text = selectedVarFind.Units;
            }
        }

        private void BtnRezult_Click(object sender, RoutedEventArgs e)
        {
            List<string> names = new List<string>();
            List<float> values = new List<float>();
            string[] rezult = new string[2];
            if (comboBoxFind.SelectedIndex != -1)
            {
                foreach (string str in selectedVars)
                {
                    names.Add(str.Split(' ')[0]);
                    values.Add((float)Convert.ToDouble(str.Split(' ')[1]));
                }
                rezult = logics.Calculate(names.ToArray(), values.ToArray(), comboBoxFind.SelectedItem.ToString());
                if (rezult[0] != null)
                    textBoxRezult.Text = rezult[0];
                else
                    MessageBox.Show("Недостаточно информации для проведения этого рассчета. Необходимо добавить больше правил!");
            }
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Открыть базу знаний...",
                Filter = "Файлы экспертной системы|*.es|Все файлы|*.*"
            };

            if (openFileDialog.ShowDialog() != null)
            {
                FileStream fs = new FileStream(openFileDialog.FileName, FileMode.Open);
                StreamReader sr = new StreamReader(fs);
                logics.OpenFile(sr.ReadToEnd());
                fileName = openFileDialog.FileName;
                sr.Close();
                fs.Close();
                listVariables.ItemsSource = logics.GetVariablesList();
                listRules.ItemsSource = logics.GetRulesList();
                UpdateVariables();
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (fileName != "")
            {
                FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine(logics.SaveFile());
                sw.Close();
                fs.Close();
                listVariables.ItemsSource = logics.GetVariablesList();
                listRules.ItemsSource = logics.GetRulesList();
                UpdateVariables();
            }
        }

        private void SaveAs_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Title = "Сохранить базу знаний...",
                Filter = "Файлы экспертной системы|*.es|Все файлы|*.*"
            };

            if (saveFileDialog.ShowDialog() != null)
            {
                FileStream fs = new FileStream(saveFileDialog.FileName, FileMode.OpenOrCreate);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine(logics.SaveFile());
                fileName = saveFileDialog.FileName;
                sw.Close();
                fs.Close();
                listVariables.ItemsSource = logics.GetVariablesList();
                listRules.ItemsSource = logics.GetRulesList();
                UpdateVariables();
            }

        }
    }
}
