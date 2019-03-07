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
    /// Interaction logic for Window_Datapath.xaml
    /// Sets the DataContext of the Window to that of the Datapath ViewModel
    /// To allow for Bindings
    /// </summary>
    public partial class Window_Datapath : Window
    {
        #region Private Variables
        private DataPathViewModel _Data;
        #endregion

        #region Properties
        //Allows for the MainWindow to retrive the DataPath Model created by the Datapath Menu
        public DataPathModel GetDataPathModel
        {
            get{return this._Data.GetDataPath;}
        }
        #endregion

        #region Methods
        //constructor
        public Window_Datapath()
        {
            InitializeComponent();
            _Data = new DataPathViewModel();            //Creates an instance for the DataPathViewModel
            this.DataContext = _Data;                   //Sets the DataContext of the this Window to that of the DataPathViewModel 
                                                        //to allow for Binding of the VM properties to the XAML
        }

        private void AddPort_Click(object sender, RoutedEventArgs e)
        {
            PortDataGrid.Items.Add(_Data.GetPortData);  //Adds the Port created to the Datagrid 
            _Data.AddPortSel = true;                    //Set the AddPortSel prop to true when the AddPort button is clicked
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();                               //Closes instance of window when Cancel is selected 
        }

        private void Finish_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;                   //Set dialogResult to True to signify that data entry is finished
            this.Close();                               //Closes instance of window when Finish is selected
        }
        #endregion
    }
}