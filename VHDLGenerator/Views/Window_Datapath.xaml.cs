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
using Newtonsoft.Json;

namespace VHDLGenerator.Views
{
    /// <summary>
    /// Interaction logic for Window_Datapath.xaml
    /// </summary>
    public partial class Window_Datapath : Window
    {
        //JSON that contains the data entered in the datapath window
        public string DP_ResultJSON { get; private set; }
        //Unique ID of each port created
        private int UID;
        //List of all ports created
        private List<Port> ports = new List<Port>();
       
        public Window_Datapath()
        {
            InitializeComponent();
            UID = 0;
        }

        private void AddPort_Click(object sender, RoutedEventArgs e)
        {
            UID = UID + 1;

            //adding data into port model
            Port tempPort = new Port
            {
                ID = UID,
                Name = PortName_TB.Text,
                Direction = Direction_CB.Text,
                Bus = (bool)Bus_CB.IsChecked,
                MSB = MSB_TB.Text,
                LSB = LSB_TB.Text
            };

            //add port to the data grid
            PortDataGrid.Items.Add(tempPort);
            //Add Port to list
            ports.Add(tempPort);

            //Clear data present in the port dat fields
            //Resets it to default
            PortName_TB.Text = String.Empty;
            Direction_CB.Text = String.Empty;
            Bus_CB.IsChecked = false;
            MSB_TB.Text = String.Empty;
            LSB_TB.Text = String.Empty;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Finish_Click(object sender, RoutedEventArgs e)
        {
            DataPath dataPathObj = new DataPath()
            {
                ID = 001,
                Name = EntityNameTB.Text,
                ArchName = ArchNameTB.Text,
                Ports = ports
            };

            DP_ResultJSON = JsonConvert.SerializeObject(dataPathObj, Formatting.Indented);

            this.DialogResult = true;

            this.Close();
        }

    }
}
