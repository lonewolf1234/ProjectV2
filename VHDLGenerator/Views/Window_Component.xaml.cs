﻿using System;
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
    /// Interaction logic for Window_Component.xaml
    /// </summary>
    public partial class Window_Component : Window
    {
        //JSON that contains the data entered in the datapath window
        //public string GetCompJSON
        //{
        //    get
        //    {
        //        return JsonConvert.SerializeObject(this.Data.GetComponent, Formatting.Indented);
        //    }
        //}

        public ComponentModel GetComponentModel { get { return this.Data.GetComponent; } }

        private ComponentViewModel Data;

        public Window_Component()
        {
            InitializeComponent();
            //ArchNameTB.Text = "Behavioural";
            Data = new ComponentViewModel();
            this.DataContext = Data;
        }

        private void AddPort_Click(object sender, RoutedEventArgs e)
        {
            #region Old code that has been commented
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

            PortDataGrid.Items.Add(Data.GetPortData);
            Data.AddPortSel = true;
            
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Finish_Click(object sender, RoutedEventArgs e)
        {
            #region Old Code
            //Guid guid = Guid.NewGuid();

            //ComponentModel componentObj = new ComponentModel()
            //{
            //    //ID = guid.ToString(),
            //    Name = EntityNameTB.Text,
            //    ArchName = ArchNameTB.Text,
            //    Ports = ports
            //};

            //Comp_ResultJSON = JsonConvert.SerializeObject(this.Data.GetComponent, Formatting.Indented);
            //this.DataContext = Data.GetComponent;
            #endregion

            this.DialogResult = true;
            this.Close();
        }
    }
}
