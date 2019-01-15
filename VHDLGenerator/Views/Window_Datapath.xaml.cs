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
        int UID = 0;

        public string OutputJSON { get { return _OutputJSON; } }

        public string _OutputJSON { get; set; }

        List<Port> ports = new List<Port>();

        DataPath dataPath1 = new DataPath();

        public Window_Datapath()
        {
            InitializeComponent();
        }

        private void AddPort_Click(object sender, RoutedEventArgs e)
        {
            Port tempPort = new Port
            {
                ID = UID + 1,
                Name = PortName_TB.Text,
                Direction = Direction_CB.Text,
                Bus = (bool)Bus_CB.IsChecked,
                MSB = MSB_TB.Text,
                LSB = LSB_TB.Text
            };
            UID = UID + 1;

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

            dataPath1 = dataPathObj;

            _OutputJSON = JsonConvert.SerializeObject(dataPath1, Formatting.Indented);

            this.DialogResult = true;

            this.Close();
        }

    }
}
