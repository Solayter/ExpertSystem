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
        Logic sas;
        public MainWindow()
        {
            InitializeComponent();
            sas = new Logic();
        }

        private void Btn1_Click(object sender, RoutedEventArgs e)
        {
            // linguistic labels (fuzzy sets) that compose the distances
            FuzzySet fsMalo = new FuzzySet("Мало",
                new TrapezoidalFunction(0, 50, TrapezoidalFunction.EdgeType.Right));
             FuzzySet fsMnogo = new FuzzySet("Много",
                new TrapezoidalFunction(50, 100, TrapezoidalFunction.EdgeType.Left));
            // front distance (input)
            LinguisticVariable lvUdobren = new LinguisticVariable("Удобрения", 0, 100);
            lvUdobren.AddLabel(fsMalo);
            lvUdobren.AddLabel(fsMnogo);

            // linguistic labels (fuzzy sets) that compose the distances
            FuzzySet fsHolodno = new FuzzySet("Холодно",
                new TrapezoidalFunction(5, 10, 15, 20));
            FuzzySet fsTeplo = new FuzzySet("Тепло",
                new TrapezoidalFunction(15, 30, TrapezoidalFunction.EdgeType.Left));

            // front distance (input)
            LinguisticVariable lvTemp = new LinguisticVariable("Температура", 5, 30);
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
        
    }
}
