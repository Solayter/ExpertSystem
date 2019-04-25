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
    /// Логика взаимодействия для WindowNewLabel.xaml
    /// </summary>
    public partial class WindowNewLabel : Window
    {
        public string name;
        public int type;
        public float[] array;
        public bool ok;
        List<string> strings;
        float min;
        float max;
        public WindowNewLabel(List<string> strings, float min, float max)
        {
            InitializeComponent();
            this.strings = strings;
            textBoxVar1.IsEnabled = false;
            textBoxVar2.IsEnabled = false;
            textBoxVar3.IsEnabled = false;
            textBoxVar4.IsEnabled = false;
            ok = false;
            this.min = min;
            this.max = max;
        }

        private void ComboBoxType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            switch (comboBoxType.SelectedIndex)
            {
                case 0:
                    {
                        figureStart.Visibility = Visibility.Visible;
                        figureEnd.Visibility = Visibility.Hidden;
                        figureThree.Visibility = Visibility.Hidden;
                        figureFour.Visibility = Visibility.Hidden;
                        figureSingle.Visibility = Visibility.Hidden;

                        textBoxVar1.IsEnabled = true;
                        textBoxVar2.IsEnabled = true;
                        textBoxVar3.IsEnabled = false;
                        textBoxVar4.IsEnabled = false;
                        type = 1;
                    }
                    break;
                case 1:
                    {
                        figureStart.Visibility = Visibility.Hidden;
                        figureEnd.Visibility = Visibility.Visible;
                        figureThree.Visibility = Visibility.Hidden;
                        figureFour.Visibility = Visibility.Hidden;
                        figureSingle.Visibility = Visibility.Hidden;

                        textBoxVar1.IsEnabled = true;
                        textBoxVar2.IsEnabled = true;
                        textBoxVar3.IsEnabled = false;
                        textBoxVar4.IsEnabled = false;
                        type = 2;
                    }
                    break;
                case 2:
                    {
                        figureStart.Visibility = Visibility.Hidden;
                        figureEnd.Visibility = Visibility.Hidden;
                        figureThree.Visibility = Visibility.Visible;
                        figureFour.Visibility = Visibility.Hidden;
                        figureSingle.Visibility = Visibility.Hidden;

                        textBoxVar1.IsEnabled = true;
                        textBoxVar2.IsEnabled = true;
                        textBoxVar3.IsEnabled = true;
                        textBoxVar4.IsEnabled = false;
                        type = 3;
                    }
                    break;
                case 3:
                    {
                        figureStart.Visibility = Visibility.Hidden;
                        figureEnd.Visibility = Visibility.Hidden;
                        figureThree.Visibility = Visibility.Hidden;
                        figureFour.Visibility = Visibility.Visible;
                        figureSingle.Visibility = Visibility.Hidden;

                        textBoxVar1.IsEnabled = true;
                        textBoxVar2.IsEnabled = true;
                        textBoxVar3.IsEnabled = true;
                        textBoxVar4.IsEnabled = true;
                        type = 4;
                    }
                    break;
                case 4:
                    {
                        figureStart.Visibility = Visibility.Hidden;
                        figureEnd.Visibility = Visibility.Hidden;
                        figureThree.Visibility = Visibility.Hidden;
                        figureFour.Visibility = Visibility.Hidden;
                        figureSingle.Visibility = Visibility.Visible;

                        textBoxVar1.IsEnabled = true;
                        textBoxVar2.IsEnabled = false;
                        textBoxVar3.IsEnabled = false;
                        textBoxVar4.IsEnabled = false;
                        type = 5;
                    }
                    break;
                default:
                    break;
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            name = textBoxVariableName.Text.Replace(' ','_');
            if (textBoxVariableName.Text == "")
            {
                MessageBox.Show("Необходимо ввести название!");
                return;
            }
            foreach (string var in strings)
                if(var == name)
                {
                    MessageBox.Show("Терм с таким именем уже существует!");
                    return;
                }
            if (comboBoxType.SelectedIndex == -1)
            {
                MessageBox.Show("Необходимо выбрать тип терма!");
                return;
            }
            
            
            switch (type)
            {
                case 1:
                    {
                        array = new float[2];
                        try
                        {
                            array[0] = (float)Convert.ToDouble(textBoxVar1.Text);
                            array[1] = (float)Convert.ToDouble(textBoxVar2.Text);
                            foreach(float var in array)
                                if((var > max)|(var < min))
                                {
                                    MessageBox.Show("Значения терма не должны выходить за рамки значения переменной!");
                                    return;
                                }
                            ok = true;
                            Close();
                        }
                        catch
                        {
                            MessageBox.Show("В поля можно вводить только целые и дробные числа!");
                            return;
                        }
                    }
                    break;
                case 2:
                    {
                        array = new float[2];
                        try
                        {
                            array[0] = (float)Convert.ToDouble(textBoxVar1.Text);
                            array[1] = (float)Convert.ToDouble(textBoxVar2.Text);
                            foreach (float var in array)
                                if ((var > max) | (var < min))
                                {
                                    MessageBox.Show("Значения терма не должны выходить за рамки значения переменной!");
                                    return;
                                }
                            ok = true;
                            Close();
                        }
                        catch
                        {
                            MessageBox.Show("В поля можно вводить только целые и дробные числа!");
                            return;
                        }
                    }
                    break;
                case 3:
                    {
                        array = new float[3];
                        try
                        {
                            array[0] = (float)Convert.ToDouble(textBoxVar1.Text);
                            array[1] = (float)Convert.ToDouble(textBoxVar2.Text);
                            array[2] = (float)Convert.ToDouble(textBoxVar3.Text);
                            foreach (float var in array)
                                if ((var > max) | (var < min))
                                {
                                    MessageBox.Show("Значения терма не должны выходить за рамки значения переменной!");
                                    return;
                                }
                            ok = true;
                            Close();
                        }
                        catch
                        {
                            MessageBox.Show("В поля можно вводить только целые и дробные числа!");
                            return;
                        }
                    }
                    break;
                case 4:
                    {
                        
                        array = new float[4];
                        try
                        {
                            array[0] = (float)Convert.ToDouble(textBoxVar1.Text);
                            array[1] = (float)Convert.ToDouble(textBoxVar2.Text);
                            array[2] = (float)Convert.ToDouble(textBoxVar3.Text);
                            array[3] = (float)Convert.ToDouble(textBoxVar4.Text);
                            foreach (float var in array)
                                if ((var > max) | (var < min))
                                {
                                    MessageBox.Show("Значения терма не должны выходить за рамки значения переменной!");
                                    return;
                                }
                            ok = true;
                            Close();
                        }
                        catch
                        {
                            MessageBox.Show("В поля можно вводить только целые и дробные числа!");
                            return;
                        }
                    }
                    break;
                case 5:
                    {
                        array = new float[1];
                        try
                        {
                            array[0] = (float)Convert.ToDouble(textBoxVar1.Text);
                            foreach (float var in array)
                                if ((var > max) | (var < min))
                                {
                                    MessageBox.Show("Значения терма не должны выходить за рамки значения переменной!");
                                    return;
                                }
                            ok = true;
                            Close();
                        }
                        catch
                        {
                            MessageBox.Show("В поля можно вводить только целые и дробные числа!");
                            return;
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            ok = false;
            Close();
        }
    }
}
