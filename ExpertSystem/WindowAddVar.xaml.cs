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
        public FuzzyLabel[] fuzzyLabels;
        public WindowAddVar()
        {
            InitializeComponent();
            ok = false;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            name = textBoxVariableName.Text;
            try
            {
                min = (float)Convert.ToDouble(textBoxVariableMin.Text);
            }
            catch
            {
                MessageBox.Show("В поле Минимум можно вводить только целые и дробные числа!");
            }

            try
            {
                max = (float)Convert.ToDouble(textBoxVariableMax.Text);
            }
            catch
            {
                MessageBox.Show("В поле Максимум можно вводить только целые и дробные числа!");
            }
            ok = true;
            Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
