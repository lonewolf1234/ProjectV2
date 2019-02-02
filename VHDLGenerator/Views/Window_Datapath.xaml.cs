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
        public string GetDataPJSON
        {
            get
            {
                return JsonConvert.SerializeObject(this.Data.GetDataPath, Formatting.Indented);
            }
        }

        private DataPathViewModel Data;
        //Unique ID of each port created
        //private int UID;
        //List of all ports created
        //private List<PortModel> ports = new List<PortModel>();
       
        public Window_Datapath()
        {
            InitializeComponent();
            //ArchNameTB.Text = "Behavioural";
            Data = new DataPathViewModel();
            this.DataContext = Data;
        }

        private void AddPort_Click(object sender, RoutedEventArgs e)
        {
            #region old code
            //UID = UID + 1;

            ////adding data into port model
            //PortModel tempPort = new PortModel
            //{
            //    Name = PortName_TB.Text,
            //    Direction = Direction_CB.Text,
            //    Bus = (bool)Bus_CB.IsChecked,
            //    MSB = MSB_TB.Text,
            //    LSB = LSB_TB.Text
            //};

            ////add port to the data grid
            //PortDataGrid.Items.Add(tempPort);
            ////Add Port to list
            //ports.Add(tempPort);

            ////Clear data present in the port dat fields
            ////Resets it to default
            //PortName_TB.Text = String.Empty;
            //Direction_CB.Text = String.Empty;
            //Bus_CB.IsChecked = false;
            //MSB_TB.Text = String.Empty;
            //LSB_TB.Text = String.Empty;
            #endregion

            Data.AddPortSel = true;
            PortDataGrid.Items.Add(Data.GetPortData);
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Finish_Click(object sender, RoutedEventArgs e)
        {
            #region old code
            //DataPathModel dataPathObj = new DataPathModel()
            //{
            //    ID = 001,
            //    Name = EntityNameTB.Text,
            //    ArchName = ArchNameTB.Text,
            //    Ports = ports
            //};

            //DP_ResultJSON = JsonConvert.SerializeObject(dataPathObj, Formatting.Indented);
            #endregion

            this.DialogResult = true;
            this.Close();
        }

    }
}
