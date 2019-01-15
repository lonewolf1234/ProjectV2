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
using System.Text.RegularExpressions;

namespace VHDLGenerator.Views
{
    /// <summary>
    /// Interaction logic for Window_Signal.xaml
    /// </summary>
    public partial class Window_Signal : Window
    {
        public Window_Signal()
        {
            InitializeComponent();
            //MSB_TextBox.IsReadOnly = true;
            //LSB_TextBox.IsReadOnly = true;
        }

        private void Bus_Checked(object sender, RoutedEventArgs e)
        {
            //MSB_TextBox.IsReadOnly = false;
            //LSB_TextBox.IsReadOnly = false;
        }

        private void LSB_TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void MSB_TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Finish_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
