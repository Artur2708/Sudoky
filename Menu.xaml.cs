using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace Exam
{
    /// <summary>
    /// Логика взаимодействия для Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        public static bool[] mass = new bool[3];

        public Menu()
        {
            InitializeComponent();
            for (int i = 1; i <= 3; i++)
            {
                comboBox.Items.Add(i);
            }
            if (comboBox.SelectedItem == null)btnGame.IsEnabled = false;
            for (int i = 0; i < mass.Length; i++)
            {
                mass[i] = false;
            }
        }

        public static bool Mass_true()
        {
            if (mass[0] == true && mass[1] == true && mass[2] == true) return true;
            else return false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow(comboBox.SelectedItem);
            main.ShowDialog();
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnGame.IsEnabled = true;
        }

        private void btn_close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
