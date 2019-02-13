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
using VHDLGenerator.Models;
using VHDLGenerator.ViewModels;
using Newtonsoft.Json;

namespace VHDLGenerator.Views
{
    /// <summary>
    /// Interaction logic for Window_Datapath.xaml
    /// </summary>
    public partial class Window_Datapath : Window
    {
        //JSON that contains the data entered in the datapath window
        //public string GetDataPJSON
        //{
        //    get
        //    {
        //        return JsonConvert.SerializeObject(this.Data.GetDataPath, Formatting.Indented);
        //    }
        //}

        public DataPathModel GetDataPathModel
        {
            get
            {
                return this.Data.GetDataPath;
            }
        }
        private DataPathViewModel Data;
       
        public Window_Datapath()
        {
            InitializeComponent();
            Data = new DataPathViewModel();
            this.DataContext = Data;
        }

        private void AddPort_Click(object sender, RoutedEventArgs e)
        {
            #region old code
            ////Clear data present in the port dat fields
            ////Resets it to default
            //PortName_TB.Text = String.Empty;
            //Direction_CB.Text = String.Empty;
            //Bus_CB.IsChecked = false;
            //MSB_TB.Text = String.Empty;
            //LSB_TB.Text = String.Empty;
            #endregion
            PortDataGrid.Items.Add(Data.GetPortData);
            Data.AddPortSel = true;
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
    }
}