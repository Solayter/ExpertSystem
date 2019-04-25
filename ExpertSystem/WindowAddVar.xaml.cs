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
using System.Windows.Shapes;

namespace ExpertSystem
{
    /// <summary>
    /// Логика взаимодействия для Window.xaml
    /// </summary>
    public partial class WindowAddVar : Window
    {
        public string name;
        public float min;
        public float max;
        public bool ok;
        public List<FuzzyLabel> fuzzyLabels;
        string[] variablesStrings;
        string oldName;
        public WindowAddVar(string[] variablesStrings)
        {
            InitializeComponent();
            this.variablesStrings = variablesStrings;
            fuzzyLabels = new List<FuzzyLabel>();
            listFuzzyLabels.ItemsSource = fuzzyLabels;
            ok = false;
        }
        public WindowAddVar(string[] variablesStrings, string name, float min, float max, FuzzyLabel[] fuzzyLabels)
        {
            InitializeComponent();
            this.variablesStrings = variablesStrings;
            this.fuzzyLabels = fuzzyLabels.ToList();
            listFuzzyLabels.ItemsSource = FindFuzzyList();
            listFuzzyLabels.SelectedIndex = 0;
            textBoxVariableName.Text = oldName = name;
            textBoxVariableMax.Text = max.ToString();
            textBoxVariableMin.Text = min.ToString();
            ok = false;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            name = textBoxVariableName.Text.Replace(' ', '_');
            if (textBoxVariableName.Text == "")
            {
                MessageBox.Show("Необходимо ввести название!");
                return;
            }


            if (name != oldName)
                foreach (string str in variablesStrings)
                    if (str == name)
                    {
                        MessageBox.Show("Переменная с таким именем уже существует!");
                        return;
                    }
            
            if (listFuzzyLabels.SelectedIndex == -1)
            {
                MessageBox.Show("Необходимо добавить хотя бы один терм!");
                return;
            }
            try
            {
                min = (float)Convert.ToDouble(textBoxVariableMin.Text);
                max = (float)Convert.ToDouble(textBoxVariableMax.Text);
                ok = true;
                Close();
            }
            catch
            {
                MessageBox.Show("В поля Минимум и Максимум можно вводить только целые и дробные числа!");
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            ok = false;
            Close();
        }

        private void BtnNewLabel_Click(object sender, RoutedEventArgs e)
        {
            float min;
            float max;
            try
            {
                min = (float)Convert.ToDouble(textBoxVariableMin.Text);
                max = (float)Convert.ToDouble(textBoxVariableMax.Text);
            }
            catch
            {
                MessageBox.Show("В поля Минимум и Максимум можно вводить только целые и дробные числа!");
                return;
            }
            WindowNewLabel windowNewLabel = new WindowNewLabel(FindFuzzyList(),min,max);
            if (windowNewLabel.ShowDialog() != true)
            {
                if (windowNewLabel.ok)
                {
                    fuzzyLabels.Add(new FuzzyLabel(windowNewLabel.name, windowNewLabel.type, windowNewLabel.array));
                    listFuzzyLabels.ItemsSource = FindFuzzyList();
                    listFuzzyLabels.SelectedIndex = 0;
                }
                else
                    listFuzzyLabels.SelectedIndex = -1;
            }
        }

        private void BtnDeleteLabel_Click(object sender, RoutedEventArgs e)
        {
            if (listFuzzyLabels.SelectedIndex != -1)
            {
                FuzzyLabel toDel = new FuzzyLabel(null, 0, null);
                foreach (FuzzyLabel var in fuzzyLabels)
                    if (var.Name == listFuzzyLabels.SelectedItem.ToString())
                        toDel = var;
                fuzzyLabels.Remove(toDel);


                List<string> strings = new List<string>();
                foreach (FuzzyLabel var in fuzzyLabels)
                    strings.Add(var.Name);
                listFuzzyLabels.ItemsSource = FindFuzzyList();
            }
        }

        private List<string> FindFuzzyList()
        {
            List<string> strings = new List<string>();
            foreach (FuzzyLabel var in fuzzyLabels)
                strings.Add(var.Name);
            return strings;
        }
        private void ListFuzzyLabels_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (FuzzyLabel var in fuzzyLabels)
                if (listFuzzyLabels.SelectedItem != null)
                {
                    if (var.Name == listFuzzyLabels.SelectedItem.ToString())
                    {
                        textBlockLabelName.Text = var.Name;
                        switch (var.Type)
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
                        listLabelsItems.ItemsSource = var.Numbers;
                    }
                }
            

        }
    }
}
