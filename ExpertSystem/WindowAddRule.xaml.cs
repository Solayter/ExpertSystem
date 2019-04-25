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
    /// Логика взаимодействия для WindowAddRule.xaml
    /// </summary>
    public partial class WindowAddRule : Window
    {
        
        public bool ok;
        private bool finalize;
        Variable[] variables;
        public string name;
        public string value;
        public WindowAddRule(Variable[] variables)
        {
            InitializeComponent();
            this.variables = variables;
            listVars.ItemsSource = FindAllStrings();
        }
        private string[] FindAllStrings()
        {
            string[] strings = new string[variables.Length];
            for (int i = 0; i < variables.Length - 1; i++)
                strings[i] = variables[i].Name;
            return strings;
        }
        private Variable FindVariable(string name)
        {
            foreach (Variable var in variables)
                if (var.Name == name)
                    return var;
            return null;
        }
        private List<string> FindLabels(Variable var)
        {
            List<string> strings = new List<string>();
            foreach (FuzzyLabel fl in var.FuzzyLabels)
                strings.Add(fl.Name);
            return strings;
        }
        private void ListVars_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listVars.SelectedIndex != -1)
            {
                listLabels.ItemsSource = FindLabels(FindVariable(listVars.SelectedValue.ToString()));
            }
        }

        private void BtnAddVar_Click(object sender, RoutedEventArgs e)
        {
            if (listVars.SelectedIndex != -1)
            {
                textBoxRule.Text += " " + listVars.SelectedItem;

                btnAddLabel.IsEnabled = true;
                btnAddNoLabel.IsEnabled = true;

                btnAddVar.IsEnabled = false;
                btnAddNoVar.IsEnabled = false;

                btnAnd.IsEnabled = false;
                btnOr.IsEnabled = false;
                btnLeftBrack.IsEnabled = false;
                btnRightBrack.IsEnabled = false;
                btnThen.IsEnabled = false;

                if (textBoxRule.Text.Contains("THEN"))
                {
                    btnAnd.IsEnabled = false;
                    btnOr.IsEnabled = false;
                    btnLeftBrack.IsEnabled = false;
                    btnRightBrack.IsEnabled = false;
                    btnThen.IsEnabled = false;
                }
            }
        }

        private void BtnAddNoVar_Click(object sender, RoutedEventArgs e)
        {
            if (listVars.SelectedIndex != -1)
            {
                textBoxRule.Text += " NO " + listVars.SelectedItem;

                btnAddLabel.IsEnabled = true;
                btnAddNoLabel.IsEnabled = true;

                btnAddVar.IsEnabled = false;
                btnAddNoVar.IsEnabled = false;

                btnAnd.IsEnabled = false;
                btnOr.IsEnabled = false;
                btnLeftBrack.IsEnabled = false;
                btnRightBrack.IsEnabled = false;
                btnThen.IsEnabled = false;

                if (textBoxRule.Text.Contains("THEN"))
                {
                    btnAnd.IsEnabled = false;
                    btnOr.IsEnabled = false;
                    btnLeftBrack.IsEnabled = false;
                    btnRightBrack.IsEnabled = false;
                    btnThen.IsEnabled = false;
                }
            }
        }

        private void BtnAddLabel_Click(object sender, RoutedEventArgs e)
        {
            if (listLabels.SelectedIndex != -1)
            {
                textBoxRule.Text += " IS " + listLabels.SelectedItem;

                btnAddLabel.IsEnabled = false;
                btnAddNoLabel.IsEnabled = false;
                btnAddVar.IsEnabled = false;
                btnAddNoVar.IsEnabled = false;

                btnAnd.IsEnabled = true;
                btnOr.IsEnabled = true;
                btnLeftBrack.IsEnabled = true;
                btnRightBrack.IsEnabled = true;
                btnThen.IsEnabled = true;

                if(textBoxRule.Text.Contains("THEN"))
                {
                    btnAnd.IsEnabled = false;
                    btnOr.IsEnabled = false;
                    btnLeftBrack.IsEnabled = false;
                    btnRightBrack.IsEnabled = false;
                    btnThen.IsEnabled = false;
                    finalize = true;
                }
            }
        }

        private void BtnAddNoLabel_Click(object sender, RoutedEventArgs e)
        {
            if (listLabels.SelectedIndex != -1)
            {
                textBoxRule.Text += " IS NO " + listLabels.SelectedItem;

                btnAddLabel.IsEnabled = false;
                btnAddNoLabel.IsEnabled = false;
                btnAddVar.IsEnabled = false;
                btnAddNoVar.IsEnabled = false;

                btnAnd.IsEnabled = true;
                btnOr.IsEnabled = true;
                btnLeftBrack.IsEnabled = true;
                btnRightBrack.IsEnabled = true;
                btnThen.IsEnabled = true;

                if (textBoxRule.Text.Contains("THEN"))
                {
                    btnAnd.IsEnabled = false;
                    btnOr.IsEnabled = false;
                    btnLeftBrack.IsEnabled = false;
                    btnRightBrack.IsEnabled = false;
                    btnThen.IsEnabled = false;
                    finalize = true;
                }
            }
        }

        private void BtnAnd_Click(object sender, RoutedEventArgs e)
        {
            textBoxRule.Text += " AND";
            
            btnAddLabel.IsEnabled = false;
            btnAddNoLabel.IsEnabled = false;

            btnAddVar.IsEnabled = true;
            btnAddNoVar.IsEnabled = true;

            btnAnd.IsEnabled = false;
            btnOr.IsEnabled = false;
            btnLeftBrack.IsEnabled = true;
            btnRightBrack.IsEnabled = true;
            btnThen.IsEnabled = false;
        }

        private void BtnOr_Click(object sender, RoutedEventArgs e)
        {
            textBoxRule.Text += " OR";

            btnAddLabel.IsEnabled = false;
            btnAddNoLabel.IsEnabled = false;

            btnAddVar.IsEnabled = true;
            btnAddNoVar.IsEnabled = true;

            btnAnd.IsEnabled = false;
            btnOr.IsEnabled = false;
            btnLeftBrack.IsEnabled = true;
            btnRightBrack.IsEnabled = true;
            btnThen.IsEnabled = false;
        }

        private void BtnThen_Click(object sender, RoutedEventArgs e)
        {
            textBoxRule.Text += " THEN";

            btnAddLabel.IsEnabled = false;
            btnAddNoLabel.IsEnabled = false;

            btnAddVar.IsEnabled = true;
            btnAddNoVar.IsEnabled = true;

            btnAnd.IsEnabled = false;
            btnOr.IsEnabled = false;
            btnLeftBrack.IsEnabled = false;
            btnRightBrack.IsEnabled = false;
            btnThen.IsEnabled = false;
        }

        private void BtnLeftBrack_Click(object sender, RoutedEventArgs e)
        {
            textBoxRule.Text += " ( ";

            btnAddLabel.IsEnabled = false;
            btnAddNoLabel.IsEnabled = false;

            btnAddVar.IsEnabled = true;
            btnAddNoVar.IsEnabled = true;

            btnAnd.IsEnabled = false;
            btnOr.IsEnabled = false;
            btnLeftBrack.IsEnabled = false;
            btnRightBrack.IsEnabled = false;
            btnThen.IsEnabled = false;
        }

        private void BtnRightBrack_Click(object sender, RoutedEventArgs e)
        {
            textBoxRule.Text += " ) ";

            btnAddLabel.IsEnabled = false;
            btnAddNoLabel.IsEnabled = false;

            btnAddVar.IsEnabled = true;
            btnAddNoVar.IsEnabled = true;

            btnAnd.IsEnabled = false;
            btnOr.IsEnabled = false;
            btnLeftBrack.IsEnabled = false;
            btnRightBrack.IsEnabled = false;
            btnThen.IsEnabled = false;
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            textBoxRule.Text = "IF";
            btnAddLabel.IsEnabled = false;
            btnAddNoLabel.IsEnabled = false;

            btnAddVar.IsEnabled = true;
            btnAddNoVar.IsEnabled = true;

            btnAnd.IsEnabled = false;
            btnOr.IsEnabled = false;
            btnLeftBrack.IsEnabled = false;
            btnRightBrack.IsEnabled = false;
            btnThen.IsEnabled = false;

            finalize = false;
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            ok = false;
            Close();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            name = textBoxName.Text.Replace(' ', '_');
            value = textBoxRule.Text;
            if(textBoxName.Text == "")
            {
                MessageBox.Show("Необходимо указать название правила!");
                return;
            }
            string[] strings = FindAllStrings();
            foreach(string str in strings)
                if(name == str)
                {
                    MessageBox.Show("Правило с таким названием уже существует!");
                    return;
                }
            if(!finalize)
            {
                MessageBox.Show("Правило не завершено!");
                return;
            }
            ok = true;
            Close();
        }
    }
}
