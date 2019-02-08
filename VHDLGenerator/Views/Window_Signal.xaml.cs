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
using VHDLGenerator.ViewModels;
using VHDLGenerator.Models;
using Newtonsoft.Json;

namespace VHDLGenerator.Views
{
    /// <summary>
    /// Interaction logic for Window_Signal.xaml
    /// </summary>
    public partial class Window_Signal : Window
    {
        //public string GetSignalJSON
        //{
        //    get
        //    {
        //        return JsonConvert.SerializeObject(this.Data.GetSignal, Formatting.Indented);
        //    }
        //}

        public SignalModel GetSignalModel { get { return this.Data.GetSignal; } }
        private SignalViewModel Data;

        public Window_Signal(string DataPathJSON)
        {
            InitializeComponent();
            Data = new SignalViewModel(DataPathJSON);
            this.DataContext = Data;
            

            ////Default setting of the view items
            //Bus_CB.IsChecked = false;
            //MSB_TB.IsReadOnly = true;
            //LSB_TB.IsReadOnly = true;
        }

        private void Bus_CB_Checked(object sender, RoutedEventArgs e)
        {
            ////if CheckBox is checked the MSB and LLSB feilds are editable
            //MSB_TB.IsReadOnly = false;
            //LSB_TB.IsReadOnly = false;
        }

        //Create a is not checked CB function to erase data from the MSB and LSB and disable editing
        private void Bus_CB_Unchecked(object sender, RoutedEventArgs e)
        {
            //MSB_TB.Clear();
            //LSB_TB.Clear();
            //MSB_TB.IsReadOnly = true;
            //LSB_TB.IsReadOnly = true;

        }

        private void MSB_TB_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            //Interger Validation
            //Regex regex = new Regex("[^0-9]+");
            //e.Handled = regex.IsMatch(e.Text);
            e.Handled = Validate_Int(e);
        }

        private void LSB_TB_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            //Interger Valdation
            //Regex regex = new Regex("[^0-9]+");
            //e.Handled = regex.IsMatch(e.Text);
            e.Handled = Validate_Int(e);
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Finish_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private bool Validate_Int(TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            //e.Handled = regex.IsMatch(e.Text);
            return (regex.IsMatch(e.Text));
        }
     
    }
}
